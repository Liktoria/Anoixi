using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

//Handles the click events by the player e.g. for planting flowers. Attach this to the Character game object
public class Planting : MonoBehaviour
{
    public List<GameObject> plantPrefabs = new List<GameObject>();
    public List<Button> plantButtons = new List<Button>();
    public List<Tilemap> levelTilemapsAscending = new List<Tilemap>();

    private Mana myMana;
    private int plantIndex = 100;
    private bool[] plantable;
    private float[] manaValues = { 15.0f, 30.0f, 22.0f, 25.0f, 40.0f };
    //private Calculations calculation = new Calculations();

    //the position the character is currently placed at in world coordinates
    protected Vector3 characterPosition;
    //the distance between the clicked spot and the character
    private float distanceToCharacter = 0.0f;
    private LevelManager levelManager;
    [SerializeField]
    private AudioSource plantingSound;

    void Start()
    {
        plantable = new bool[plantPrefabs.Count];
        for (int i = 0; i < plantable.Length; i++)
        {
            plantable[i] = true;
        }
        myMana = GetComponent<Mana>();
        levelManager = LevelManager.getInstance();
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
        if (distanceToCharacter < 1.7)
        {
            //Debug.Log("Distance okay.");
            //determine the cell in the tilemap corresponding to the mouse position
            //Vector3Int clickedCell = foregroundDecorations.WorldToCell(clickPosition);
            //clickedCell.z = calculation.calculateCorrectZ(clickedCell, levelTilemapsAscending);

            //plant the selected plant
            //TODO: Fix planting on elevated tiles
            if (plantIndex < plantPrefabs.Count && plantable[plantIndex])
            {
                if (levelManager.getCurrentMana() >= manaValues[plantIndex])
                {
                    plantingSound.Play();
                    plantable[plantIndex] = false;
                    Instantiate(plantPrefabs[plantIndex], clickPosition, Quaternion.identity);
                    myMana.useMana(manaValues[plantIndex]);
                    plantButtons[plantIndex].GetComponent<ButtonCooldown>().startCooldown();
                    StartCoroutine(plantCooldown(plantIndex));
                }
                else
                {
                    Debug.Log("Not enough mana.");
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

        //configure cooldown times for each type of plant seperately
        //TODO: add animations to buttons and call them here & switch index to enum
        switch (currentIndex)
        {
            case 0:
                yield return new WaitForSeconds(3);
                break;
            case 1:
                yield return new WaitForSeconds(12);
                break;
            case 2:
                yield return new WaitForSeconds(7);
                break;
            case 3:
                yield return new WaitForSeconds(7);
                break;
            case 4:
                yield return new WaitForSeconds(17);
                break;
        }
        //plantButtons[currentIndex].enabled = true;
        plantable[currentIndex] = true;
    }
}
