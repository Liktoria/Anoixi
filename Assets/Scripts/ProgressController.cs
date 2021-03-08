using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Tracks the progress made with planting in the level
public class ProgressController : MonoBehaviour
{
    private int tilesToTransform = 0;
    private int tilesCurrentlyTransformed = 0;
    private float currentProgress;
    public ProgressBar progressBar;
    // Start is called before the first frame update
    void Start()
    {
        //get total number of transformable tiles in the level by the static LevelController class
        tilesToTransform = 97;
        //get the number of already transformed tiles at the beginning of the level from LevelController class
        tilesCurrentlyTransformed = 10;
        progressBar.BarValue = (float) tilesCurrentlyTransformed / (float) tilesToTransform * 100;
    }

    //one tile changed in favor of the player
    public void updateProgressIncrease()
    {
        tilesCurrentlyTransformed++;
        //Debug.Log("tilesCurrentlyTransformed: " + tilesCurrentlyTransformed + "; tilesToTranform: " + tilesToTransform);
        float newValue = (float) tilesCurrentlyTransformed / (float) tilesToTransform * 100;
        //Debug.Log(newValue);
        updateProgressBar(newValue);
        if (tilesCurrentlyTransformed >= tilesToTransform)
        {
            //level is won -> win screen is displayed
        }
    }

    //one tile changed in favor of the demons
    public void updateProgressDecrease()
    {
        tilesCurrentlyTransformed--;
        float newValue = (float)tilesCurrentlyTransformed / (float)tilesToTransform * 100;
        updateProgressBar(newValue);
        if (tilesCurrentlyTransformed < -4)
        {
            //level is lost -> loose screen is displayed
        }
    }

    private void updateProgressBar (float newValue)
    {
        progressBar.BarValue = newValue;
    }
}
