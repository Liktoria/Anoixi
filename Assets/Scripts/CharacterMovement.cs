using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CharacterMovement : MonoBehaviour
{
    //speed the character is moving with
    public float movementSpeed = 1f;
    public Tilemap groundTilemap;

    [SerializeField]
    private List<Tile> grassTiles = new List<Tile>();
    [SerializeField]
    private List<Tile> stoneTiles = new List<Tile>();
    [SerializeField]
    private List<DecorationChanger> decorations = new List<DecorationChanger>();

    private LevelManager levelmanager;
    private ProgressController progressController;
    private Vector3 characterPosition;
    private Calculations calculation = new Calculations();
    private List<Tilemap> tilemapsAscending = new List<Tilemap>();

    //the renderer that will display the animation
    CharacterRenderer isoRenderer;

    Rigidbody2D rbody;

    //On starting the game (function is called before Start()) get the necessary components from the character game object, the script is attached to
    private void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        isoRenderer = GetComponent<CharacterRenderer>();
        levelmanager = LevelManager.getInstance();
        tilemapsAscending = levelmanager.getTilemapsAscending();
        progressController = ProgressController.getInstance();
        //Debug.Log(calculation);
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
        characterPosition.y = characterPosition.y + 0.5f;
        Vector3Int currentCell = groundTilemap.WorldToCell(characterPosition);
        currentCell.z = calculation.calculateCorrectZ(currentCell);

        if (gameObject.CompareTag("Player"))
        {
            if (isStone(currentCell, tilemapsAscending[currentCell.z]))
            {
                int randomGrassIndex = Random.Range(0, grassTiles.Count - 1);
                tilemapsAscending[currentCell.z].SetTile(currentCell, grassTiles[randomGrassIndex]);

                progressController.updateLoseCondition(currentCell, true);

                for(int i = 0; i < decorations.Count; i++)
                {
                    if (decorations[i].surroundingCells.Contains(currentCell))
                    {
                        decorations[i].surroundingTileChanged(currentCell, true);
                    }
                }
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
