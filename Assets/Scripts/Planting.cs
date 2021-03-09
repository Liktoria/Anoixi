using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

//Handles the click events by the player e.g. for planting flowers. Attach this to the Character game object
public class Planting : MonoBehaviour
{
    //the tilemap where the new tile is supposed to go
    public Tilemap backgroundDecorations;
    public Tilemap foregroundDecorations;

    public List<Tile> plantsForeground = new List<Tile>();
    public List<Tile> plantsBackground = new List<Tile>();
    public List<Button> plantButtons = new List<Button>();

    private Mana myMana;
    private int plantIndex = 100;
    private bool[] plantable;
    private float[] manaValues = { 10.0f, 5.0f, 20.0f, 15.0f };

    //the position the character is currently placed at in world coordinates
    protected Vector3 characterPosition;
    //the distance between the clicked spot and the character
    private float distanceToCharacter = 0.0f;

    void Start()
    {
        plantable = new bool[plantsForeground.Count];
        for (int i = 0; i < plantable.Length; i++)
        {
            plantable[i] = true;
        }
        myMana = GetComponent<Mana>();
    }

    // Update is called once per frame
    void Update()
    {
        //When the Input system detects the left mouse button is clicked, flowers are spawned
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse clicked.");
            spawnPlants();
        }
    }

    //changes the clicked tile to another tile within a certain radius around the character
    void spawnPlants()
    {
        //detect the position of the mouse and convert the coordinates to world coordinates
        Vector3 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        clickPosition.z = 0; //set to 0, because we are working with 2D

        //get the current character position by accessing the transform of the game object this script is attached to
        characterPosition = transform.position;

        //calculate the distance (can maybe be improved to make it more realistic for the player)
        distanceToCharacter = Vector3.Distance(clickPosition, characterPosition);
        Debug.Log("Click distance: " + distanceToCharacter);

        //if the distance is small enough (value can be altered to need)
        if (distanceToCharacter < 3.86)
        {
            Debug.Log("Distance okay.");
            //determine the cell in the tilemap corresponding to the mouse position
            Vector3Int clickedCell = foregroundDecorations.WorldToCell(clickPosition);

            //plant the selected plant
            //TODO: Fix planting on elevated tiles
            if (plantIndex < plantsForeground.Count && plantable[plantIndex])
            {
                if (myMana.currentValue >= manaValues[plantIndex])
                {
                    backgroundDecorations.SetTile(clickedCell, plantsBackground[plantIndex]);
                    foregroundDecorations.SetTile(clickedCell, plantsForeground[plantIndex]);
                    myMana.useMana(manaValues[plantIndex]);
                    StartCoroutine(plantCooldown(plantIndex));
                }
                else
                {
                    //can't plant stuff cause not enough mana
                    // TODO: display text/animation and stuff
                }
            }

        }
    }

    public void setPlantType (int newPlantIndex)
    {
        plantIndex = newPlantIndex;
    }

    IEnumerator plantCooldown(int currentIndex)
    {
        plantButtons[currentIndex].enabled = false;
        plantable[currentIndex] = false;

        //configure cooldown times for each type of plant seperately
        //TODO: add animations to buttons and call them here
        switch (currentIndex)
        {
            case 0:
                yield return new WaitForSeconds(0);
                break;
            case 1:
                yield return new WaitForSeconds(0);
                break;
            case 2:
                yield return new WaitForSeconds(0);
                break;
            case 3:
                yield return new WaitForSeconds(0);
                break;
            case 4:
                yield return new WaitForSeconds(5);
                break;
        }
        plantButtons[currentIndex].enabled = true;
        plantable[currentIndex] = true;
    }
}
