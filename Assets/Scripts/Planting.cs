using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

//Handles the click events by the player e.g. for planting flowers. Attach this to the Character game object
public class Planting : MonoBehaviour
{
    //the tilemap where the new tile is supposed to go
    public Tilemap backgroundDecorations;
    public Tilemap foregroundDecorations;

    //the friendly green tile with flowers, Persephone creates by clicking
    public Tile backgroundFlower;
    public Tile foregroundFlower;

    //the position the character is currently placed at in world coordinates
    protected Vector3 characterPosition;
    //the distance between the clicked spot and the character
    private float distanceToCharacter = 0.0f;

    // Update is called once per frame
    void Update()
    {
        //When the Input system detects the left mouse button is clicked, flowers are spawned
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse clicked.");
            spawnFlowers();
        }
    }

    //changes the clicked tile to another tile within a certain radius around the character
    void spawnFlowers()
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
            Vector3Int clickedCell = backgroundDecorations.WorldToCell(clickPosition);
            //change the tile to the green tile
            backgroundDecorations.SetTile(clickedCell, backgroundFlower);
            foregroundDecorations.SetTile(clickedCell, foregroundFlower);

        }
    }

}
