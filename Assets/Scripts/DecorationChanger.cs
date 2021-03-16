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
    private List<Tilemap> levelTilemapsAscending = new List<Tilemap>();
    [SerializeField]
    private bool isColumn;
    [Header("What should happen, when a column changed to its spring theme?")]
    public UnityEvent columnWon;
    [Header("What should happen, when a column changed back to its underworld theme?")]
    public UnityEvent columnLost;


    private Vector3 position;
    private Vector3Int cellUnderneath;
    [System.NonSerialized]
    public List <Vector3Int> surroundingCells = new List<Vector3Int>();
    private int progressCounter;
    private Calculations calculation = new Calculations();

    // Start is called before the first frame update
    void Awake()
    {
        position = transform.position;
        position.z = 0;
        cellUnderneath = groundTilemap.WorldToCell(position);
        initializeSurroundingCells(cellUnderneath);
        progressCounter = 0;
    }

    private void initializeSurroundingCells(Vector3Int cellUnderneath)
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
                Debug.Log("current progress on column: " + progressCounter);
                }
                else
                {
                    progressCounter--;
                    Debug.Log("current progress on column: " + progressCounter);
                    changeSprite(underworldSprite);
                    if(isColumn)
                    {
                        columnLost.Invoke();
                    }
                }
            }

        if(progressCounter == 8)
        {
            changeSprite(springSprite);
            if(isColumn)
            {
                columnWon.Invoke();
            }
        }
    }

    private void changeSprite (Sprite newSprite)
    {
        GetComponent<SpriteRenderer>().sprite = newSprite;
    }
}
