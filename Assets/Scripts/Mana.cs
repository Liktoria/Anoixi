using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Events;

public class Mana : MonoBehaviour
{
    public ProgressBar manaBar;
    public Tilemap decorationsForeground;
    public List<Tilemap> levelTilemapsAscending = new List<Tilemap>();
    public List<Tile> flowerTiles = new List<Tile>();
    public float currentValue;
    private Vector3 characterPosition;
    private bool hasFlower;
    private float waitTime = 1.0f;
    private float timer = 0.0f;


    // Start is called before the first frame update
    void Start()
    {
        currentValue = 100.0f;
        manaBar.BarValue = currentValue;
    }

    // Update is called once per frame
    void Update()
    {

        characterPosition = transform.position;
        characterPosition.z = 0;
        Vector3Int currentCell = decorationsForeground.WorldToCell(characterPosition);
        currentCell.z = calculateCorrectZ(currentCell);
        hasFlower = isFlowerPresent(currentCell);
        timer += Time.deltaTime;

        if (timer > waitTime && hasFlower && currentValue <= 100.0f)
        {
            increaseMana();
        }
    }

    public void useMana(float value)
    {
        currentValue = currentValue - value;
        if (currentValue <= 0.0f)
        {
            currentValue = 0.0f;
        }
        manaBar.BarValue = currentValue;
    }
    
    private void increaseMana()
    {

        currentValue = currentValue + 0.1f;
        if (currentValue >= 100.0f)
        {
            currentValue = 100.0f;
        }
        manaBar.BarValue = currentValue;
    }

    private bool isFlowerPresent (Vector3Int currentCell)
    {
        bool isFlowerPresent = false;
        var currentTile = decorationsForeground.GetTile(currentCell);
        int i = 0;
        for (; i < flowerTiles.Count; ++i)
        {
            if (currentTile == flowerTiles[i])
                break;
        }
        if (i < flowerTiles.Count)
        {
            isFlowerPresent = true;
        }
        return isFlowerPresent;
    }

    private int calculateCorrectZ(Vector3Int cellToCheck)
    {
        int z = 0;
        for (int i = 0; i < levelTilemapsAscending.Count; i++)
        {
            cellToCheck.z = i;
            if (levelTilemapsAscending[i].HasTile(cellToCheck))
            {
                z = i;
            }
        }

        return z;
    }
}
