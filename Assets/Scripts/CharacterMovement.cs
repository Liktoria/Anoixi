using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Events;

public class CharacterMovement : MonoBehaviour
{
    //speed the character is moving with
    public float movementSpeed = 1f;
    public List<Tilemap> levelTilemapsAscending = new List<Tilemap>();
    public Tilemap groundTilemap;

    public List<Tile> grassTiles = new List<Tile>();
    public List<Tile> stoneTiles = new List<Tile>();
    public List<Tile> elevatedStoneTiles = new List<Tile>();
    public List<Tile> elevatedGrassTiles = new List<Tile>();

    [Header("What should happen, when a tile changes to grass?")]
    public UnityEvent tileWon;

    private Vector3 characterPosition;
    private Calculations calculation = new Calculations();

    //the renderer that will display the animation
    CharacterRenderer isoRenderer;

    Rigidbody2D rbody;

    //On starting the game (function is called before Start()) get the necessary components from the character game object, the script is attached to
    private void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
        isoRenderer = GetComponent<CharacterRenderer>();
    }


    // frame-independent function that uses the frequency of the physics system
    void FixedUpdate()
    {
        //get the current position of the character and the input from 'WASD'
        Vector2 currentPos = rbody.position;
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        //save the input into a vector and clamp it to prevent diagonal movement becoming faster than movement in the cardinal directions
        Vector2 inputVector = new Vector2(horizontalInput, verticalInput);
        inputVector = Vector2.ClampMagnitude(inputVector, 1);

        //multiply with movement speed and calculate the new position
        Vector2 movement = inputVector * movementSpeed;
        Vector2 newPos = currentPos + movement * Time.fixedDeltaTime; //use Time.fixedDeltaTime to make sure the movement is stable across different frame rates

        setNewTile();

        //set direction for the renderer so it can figure out, what animation to play
        isoRenderer.SetDirection(movement);

        //move the character
        rbody.MovePosition(newPos);
        transform.position = new Vector3(newPos.x, newPos.y, transform.position.z);

    }

    private void setNewTile()
    {
        characterPosition = transform.position;
        characterPosition.z = 0;
        Vector3Int currentCell = groundTilemap.WorldToCell(characterPosition);
        currentCell.z = calculation.calculateCorrectZ(currentCell, levelTilemapsAscending);

        if (gameObject.CompareTag("Player"))
        {
            if (isStone(currentCell, levelTilemapsAscending[currentCell.z]))
            {
                int randomGrassIndex = Random.Range(0, grassTiles.Count - 1);
                levelTilemapsAscending[currentCell.z].SetTile(currentCell, grassTiles[randomGrassIndex]);

                //if ((currentCell.z > 0) && (currentCell.z % 2 == 0))
                //{
                //    for (int i = currentCell.z - 1; i >= 0; i--)
                //    {
                //        currentCell.z = i;
                //        int randomIndex = Random.Range(0, elevatedGrassTiles.Count - 1);
                //        levelTilemapsAscending[i].SetTile(currentCell, elevatedGrassTiles[randomIndex]);
                //    }
                //}
                tileWon.Invoke();
            }
        }
    }

    private bool isStone(Vector3Int cellToCheck, Tilemap tilemapToCheck)
    {
        bool isStone = false;
        var currentTile = tilemapToCheck.GetTile(cellToCheck);
        int i = 0;
        for (; i < stoneTiles.Count; ++i)
        {
            if (currentTile == stoneTiles[i])
                break;
        }
        if (i < stoneTiles.Count)
        {
            isStone = true;
            Debug.Log("Found a stone tile.");
        }
        return isStone;
    }

    //only important in case there will be tiles not classifiable as either stone or grass
    private bool isGrass(Vector3Int cellToCheck, Tilemap tilemapToCheck)
    {
        bool isGrass = false;
        var currentTile = tilemapToCheck.GetTile(cellToCheck);
        int i = 0;
        for (; i < grassTiles.Count; ++i)
        {
            if (currentTile == grassTiles[i])
                break;
        }
        if (i < grassTiles.Count)
        {
            isGrass = true;
            Debug.Log("Found a grass tile.");
        }
        return isGrass;
    }
}
