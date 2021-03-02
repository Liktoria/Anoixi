using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

//Handles the click events by the player e.g. for planting flowers. Attach this to the Character game object
public class ClickHandler : MonoBehaviour
{
    //public GameObject objectToSpawn;
    public Tilemap groundTilemap;
    public Tile greenTile;
    public Tile underworldTile;
    protected Vector3 characterPosition;
    private float distanceToCharacter = 0.0f;
    public float flowerSpawningFrequency = 0.5F;
    private float nextSpawn = 0.5F;
    private float myTime = 0.0F;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse clicked.");
            nextSpawn = myTime + flowerSpawningFrequency;
            spawnFlowers();
            nextSpawn = nextSpawn - myTime;
            myTime = 0.0F;
        }
    }

    //changes the clicked tile to another tile within a certain radius around the character, not pretty yet though
    void spawnFlowers()
    {
        Vector3 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        clickPosition.z = 0;
        characterPosition = transform.position;
        distanceToCharacter = Vector3.Distance(clickPosition, characterPosition);
        Debug.Log("Click distance: " + distanceToCharacter);

        if (distanceToCharacter < 3.8)// && myTime > nextSpawn)
        {
            Debug.Log("Distance okay.");
            Vector3Int clickedCell = groundTilemap.WorldToCell(clickPosition);
            groundTilemap.SetTile(clickedCell, greenTile);
        }
    }

    //The code to destroy the flowers, when a demon walks over them will go here
    void destroyFlowers()
    {

    }
}
