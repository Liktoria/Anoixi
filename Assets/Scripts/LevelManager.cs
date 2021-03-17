using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelManager : MonoBehaviour
{
    private float currentMana = 100.0f;
    private bool manaIncreasing = false;
    private static LevelManager instance;
    [SerializeField]
    private List<Tilemap> levelTilemapsAscending = new List<Tilemap>();


    public static LevelManager getInstance()
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

    public void setCurrentMana(float newValue)
    {
        currentMana = newValue;
    }

    public void setManaIncreasing(bool newValue)
    {
        manaIncreasing = newValue;
    }

    public float getCurrentMana()
    {
        return currentMana;
    }

    public bool getManaIncreasing()
    {
        return manaIncreasing;
    }

    public List<Tilemap> getTilemapsAscending()
    {
        return levelTilemapsAscending;
    }
}
