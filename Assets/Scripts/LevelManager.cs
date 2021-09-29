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
    [SerializeField]
    private List<Transform> demonGoals = new List<Transform>();
    [SerializeField]
    private List<DecorationChanger> decorations = new List<DecorationChanger>();
    [SerializeField]
    private Spawning spawner;

    public static LevelManager GetInstance()
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

    public float GetCurrentMana()
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

    public List<DecorationChanger> getDecorations()
    {
        return decorations;
    }

    public List<Transform> getGoals()
    {
        return demonGoals;
    }

    public void reduceDemonCount()
    {
        if(spawner.demonCounter > 0)
        {
            spawner.demonCounter--;
        }
    }
}
