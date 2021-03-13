using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Events;

public class Mana : MonoBehaviour
{
    public ProgressBar manaBar;
    //public Tilemap decorationsForeground;
    public List<Tilemap> levelTilemapsAscending = new List<Tilemap>();
    //public List<GameObject> flowerPrefabs = new List<GameObject>();
    public float currentValue;
    private Vector3 characterPosition;
    //private bool hasFlower;
    //private float waitTime = 1.0f;
    //private float timer = 0.0f;
    //private bool manaIncreasing = false;
    private LevelManager levelManager;
    //private Calculations calculation = new Calculations();


    // Start is called before the first frame update
    void Start()
    {
        levelManager = LevelManager.getInstance();
        levelManager.setCurrentMana(100.0f);
        manaBar.BarValue = levelManager.getCurrentMana();

    }

    public void useMana(float value)
    {
        levelManager.setCurrentMana(levelManager.getCurrentMana() - value);
        currentValue = levelManager.getCurrentMana();
        if (currentValue <= 0.0f)
        {
            levelManager.setCurrentMana(0.0f);
        }
        manaBar.BarValue = levelManager.getCurrentMana();
    }

    public void increaseMana()
    {
        //do every second
        levelManager.setCurrentMana(levelManager.getCurrentMana() + 0.1f);
        manaBar.BarValue = levelManager.getCurrentMana();

        if (!levelManager.getManaIncreasing())
        {
            return;
        }

        else if (levelManager.getCurrentMana() >= 100.0f)
        {
            levelManager.setCurrentMana(100.0f);
            return;
        }
    }
}

    //private bool isFlowerPresent (Vector3Int currentCell)
    //{
    //    bool isFlowerPresent = false;
    //    var currentTile = decorationsForeground.GetTile(currentCell);
    //    int i = 0;
    //    for (; i < flowerPrefabs.Count; ++i)
    //    {
    //        if (currentTile == flowerPrefabs[i])
    //            break;
    //    }
    //    if (i < flowerPrefabs.Count)
    //    {
    //        isFlowerPresent = true;
    //    }
    //    return isFlowerPresent;
    //}
