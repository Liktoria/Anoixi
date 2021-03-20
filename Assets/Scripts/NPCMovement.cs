using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Pathfinding;

public class NPCMovement : MonoBehaviour
{
    //speed the character is moving with
    public float movementSpeed = 1f;
    private Tilemap groundTilemap;

    [SerializeField]
    private AIPath aiPath;
    [SerializeField]
    private List<Tile> grassTiles = new List<Tile>();
    [SerializeField]
    private List<Tile> stoneTiles = new List<Tile>();
    private List<DecorationChanger> decorations = new List<DecorationChanger>();
    private List<Transform> goals = new List<Transform>();
    private Transform currentGoal;

    private Vector3 characterPosition;
    private Calculations calculation = new Calculations();
    private LevelManager levelmanager;
    private ProgressController progressController;
    private List<Tilemap> tilemapsAscending = new List<Tilemap>();
    private Seeker seeker;
    private int stuckCounter;
    private bool pathRecalculated = false;

    //the renderer that will display the animation
    NPCRenderer isoRenderer;

    Rigidbody2D rbody;

    //On starting the game (function is called before Start()) get the necessary components from the character game object, the script is attached to
    private void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        isoRenderer = GetComponent<NPCRenderer>();
        levelmanager = LevelManager.getInstance();
        tilemapsAscending = levelmanager.getTilemapsAscending();
        progressController = ProgressController.getInstance();
        decorations = levelmanager.getDecorations();
        goals = levelmanager.getGoals();
        groundTilemap = levelmanager.getTilemapsAscending()[0];
        seeker = GetComponent<Seeker>();
        determineGoal();
        seeker.StartPath(transform.position, currentGoal.position, OnPathComplete);
    }

    // frame-independent function that uses the frequency of the physics system
    void FixedUpdate()
    {
        Vector2 movement = new Vector2(0, 0);

        Vector3 lastPosition = characterPosition;
        setNewTile();
        if(aiPath.desiredVelocity.x > 0.01f && aiPath.desiredVelocity.y > 0.01f)
        {
            movement = Vector2.one; //(1,1)
        }
        else if(aiPath.desiredVelocity.x > 0.01f && aiPath.desiredVelocity.y < -0.01f)
        {
            movement = new Vector2(1, -1);
        }
        else if(aiPath.desiredVelocity.x < -0.01f && aiPath.desiredVelocity.y > 0.01f)
        {
            movement = new Vector2(-1, 1);
        }
        else if(aiPath.desiredVelocity.x < -0.01f && aiPath.desiredVelocity.y < -0.01f)
        {
            movement = new Vector2(-1, -1);
        }

        //set direction for the renderer so it can figure out, what animation to play
        isoRenderer.SetDirection(movement);

        //if the demon gets stuck
        //recalculate the path
        if(lastPosition == transform.position)
        {
            stuckCounter++;
            if(stuckCounter >= 200)
            {
                if (!pathRecalculated)
                {
                    stuckCounter = 0;
                    aiPath.SearchPath();
                    pathRecalculated = true;
                }
                else
                {
                    startNewPath();
                    pathRecalculated = false;
                }
            }
        }
        //if the destination is reached
        //set a new path
        if(aiPath.reachedDestination)
        {
            startNewPath();
        }
        
    }

    private void setNewTile()
    {
        characterPosition = transform.position;
        characterPosition.z = 0;
        Vector3Int currentCell = groundTilemap.WorldToCell(characterPosition);
        currentCell.z = calculation.calculateCorrectZ(currentCell);

        if (gameObject.CompareTag("Demon"))
        {
            if (isGrass(currentCell, tilemapsAscending[currentCell.z]))
            {
                int randomGrassIndex = Random.Range(0, stoneTiles.Count - 1);
                tilemapsAscending[currentCell.z].SetTile(currentCell, stoneTiles[randomGrassIndex]);

                progressController.updateLoseCondition(currentCell, false);

                for (int i = 0; i < decorations.Count; i++)
                {
                    if (decorations[i].surroundingCells.Contains(currentCell))
                    {
                        decorations[i].surroundingTileChanged(currentCell, false);
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
            //Debug.Log("Found a stone tile.");
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
            //Debug.Log("Found a grass tile.");
        }
        return isGrass;
    }

    private void determineGoal()
    {
        int randomNumber = (int) Random.Range(0, goals.Count);
        currentGoal = goals[randomNumber];
    }

    public void OnPathComplete(Path p)
    {
        Debug.Log("Yay, we got a path back. Did it have an error? " + p.error);
    }

    private void startNewPath()
    {
        determineGoal();
        seeker.StartPath(transform.position, currentGoal.position, OnPathComplete);
    }
}
