using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Events;

public class DecorationChanger : MonoBehaviour
{
    [SerializeField]
    private Sprite springSprite;
    [SerializeField]
    private Sprite underworldSprite;
    [SerializeField]
    private Tilemap groundTilemap;
    [SerializeField]
    private bool isColumn;
    [SerializeField]
    private int tilesCovered = 1;
    [SerializeField]
    private int plantIndexToUnlock;
    [Header("What should happen, when a column changed to its spring theme?")]
    public UnityEvent columnWon;
    [Header("What should happen, when a column changed back to its underworld theme?")]
    public UnityEvent columnLost;

    private Vector3 position;
    private Vector3Int cellUnderneath;
    [System.NonSerialized]
    public List <Vector3Int> surroundingCells = new List<Vector3Int>();
    private int progressCounter;
    private int progressSuccess;
    private Calculations calculation = new Calculations();
    private LevelManager levelmanager;
    private ProgressController progressController;
    private List<Tilemap> tilemapsAscending;

    // Start is called before the first frame update
    void Start()
    {
        position = transform.position;
        position.z = 0;
        cellUnderneath = groundTilemap.WorldToCell(position);
        progressCounter = 0;
        progressSuccess = 8;
        levelmanager = LevelManager.getInstance();
        tilemapsAscending = levelmanager.getTilemapsAscending();
        progressController = ProgressController.getInstance();
        initializeSurroundingCells(cellUnderneath);
        //Debug.Log("number of tiles that have to be transformed: " + progressSuccess);
    }

    private void initializeSurroundingCells(Vector3Int cellUnderneath)
    {
        if (tilesCovered == 1)
        {
            surroundingCells.Add(new Vector3Int(cellUnderneath.x - 1, cellUnderneath.y - 1, 0));
            surroundingCells.Add(new Vector3Int(cellUnderneath.x - 1, cellUnderneath.y, 0));
            surroundingCells.Add(new Vector3Int(cellUnderneath.x - 1, cellUnderneath.y + 1, 0));
            surroundingCells.Add(new Vector3Int(cellUnderneath.x, cellUnderneath.y - 1, 0));
            surroundingCells.Add(new Vector3Int(cellUnderneath.x, cellUnderneath.y + 1, 0));
            surroundingCells.Add(new Vector3Int(cellUnderneath.x + 1, cellUnderneath.y - 1, 0));
            surroundingCells.Add(new Vector3Int(cellUnderneath.x + 1, cellUnderneath.y, 0));
            surroundingCells.Add(new Vector3Int(cellUnderneath.x + 1, cellUnderneath.y + 1, 0));
        }
        else if(tilesCovered == 4)
        {

        }
        
        //for elevated tiles
        for(int i = 0; i < surroundingCells.Count; i++)
        {
            Vector3Int temporaryStorage = new Vector3Int();
            temporaryStorage = surroundingCells[i];
            temporaryStorage.z = calculation.calculateCorrectZ(temporaryStorage);

            if(!(tilemapsAscending[0].HasTile(temporaryStorage)) && temporaryStorage.z == 0)
            {
                progressSuccess--;
            }
            surroundingCells[i] = temporaryStorage;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void surroundingTileChanged (Vector3Int coordinates, bool changedToGrass)
    {
            if(surroundingCells.Contains(coordinates))
            {
                if(changedToGrass)
                {
                    progressCounter++;
                //Debug.Log("current progress on column: " + progressCounter);
                }
                else
                {
                    progressCounter--;
                    //Debug.Log("current progress on column: " + progressCounter);
                    changeSprite(underworldSprite);
                    if(isColumn)
                    {
                        progressController.updateWinCondition(false);
                        columnLost.Invoke();
                    }
                }
            }

        if(progressCounter == progressSuccess)
        {
            changeSprite(springSprite);
            if(isColumn)
            {
                progressController.updateWinCondition(true);
                columnWon.Invoke();
            }
        }
    }

    private void changeSprite (Sprite newSprite)
    {
        GetComponent<SpriteRenderer>().sprite = newSprite;
    }
}
