using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Tracks the progress made with planting in the level
public class ProgressController : MonoBehaviour
{
    [SerializeField]
    private int tilesToTransform = 45;
    private int tilesCurrentlyTransformed = 0;
    [SerializeField]
    private int columnsToTransform = 5;
    private int columnsTransformed = 0;
    public ProgressBar progressBar;
    [SerializeField]
    private List<Vector3Int> baseTiles = new List<Vector3Int>();
    private static ProgressController instance;

    public static ProgressController getInstance()
    {
        return instance;
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        progressBar.BarValue = 100.0f;
    }

    //one tile changed in favor of the player
    public void updateWinCondition(bool changedToSpring)
    {
        if(changedToSpring)
        {
            columnsTransformed++;
        }
        else
        {
            columnsTransformed--;
        }

        if(columnsTransformed == columnsToTransform)
        {
            //level is won -> win screen is displayed
        }
    }

    //one tile changed in favor of the demons
    public void updateLoseCondition(Vector3Int changedTile, bool changedToGrass)
    {
        if (baseTiles.Contains(changedTile))
        {
            if (changedToGrass)
            {
                tilesCurrentlyTransformed--;

            }
            else
            {
                tilesCurrentlyTransformed++;
            }
            float newValue = (float)tilesCurrentlyTransformed / (float)tilesToTransform * 100;
            updateProgressBar(newValue);
        }

        if (tilesCurrentlyTransformed == tilesToTransform)
        {
            //level is lost -> loose screen is displayed
        }
    }

    private void updateProgressBar (float newValue)
    {
        progressBar.BarValue = newValue;
    }
}
