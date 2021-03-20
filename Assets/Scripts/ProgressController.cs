using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//Tracks the progress made with planting in the level
public class ProgressController : MonoBehaviour
{
    [SerializeField]
    private int tilesToTransform = 45;
    private int tilesCurrentlyTransformed = 0;
    [SerializeField]
    private int columnsToTransform = 5;
    private int columnsTransformed = 0;
    [SerializeField]
    private ProgressBar progressBar;
    [SerializeField]
    private TMP_Text progressText;
    [SerializeField]
    private List<Vector3Int> baseTiles = new List<Vector3Int>();
    private LoadingManager loadingManager;
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
        loadingManager = GetComponent<LoadingManager>();
        updateText();
    }

    //one tile changed in favor of the player
    public void updateWinCondition(bool changedToSpring)
    {
        if(changedToSpring)
        {
            columnsTransformed++;
            updateText();
        }
        else
        {
            columnsTransformed--;
            updateText();
        }

        if(columnsTransformed == columnsToTransform)
        {
            //level is won -> win screen is displayed
            loadingManager.loadScene("YouWon");
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
            float newValue = 100.0f - ((float)tilesCurrentlyTransformed / (float)tilesToTransform * 100);
            updateProgressBar(newValue);
        }

        if (tilesCurrentlyTransformed == tilesToTransform)
        {
            //level is lost -> loose screen is displayed
            loadingManager.loadScene("YouLost");
        }
    }

    private void updateProgressBar (float newValue)
    {
        progressBar.BarValue = newValue;
    }

    private void updateText()
    {
        progressText.text = "" + columnsTransformed + " / " + columnsToTransform + " columns transformed";
    }
}
