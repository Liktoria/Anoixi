using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Spawning : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> demons = new List<GameObject>(); //The flying demon has to be the one on index 0
    [SerializeField]
    private List<Vector3Int> spawnPoints = new List<Vector3Int>();
    [SerializeField]
    private float spawnRate = 30.0f;
    [SerializeField]
    private int maxDemonsOnMap = 8;
    [SerializeField]
    private Tilemap groundTilemap;

    private Vector3 spawnPosition;
    public int demonCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        startSpawning();
    }

    public void startSpawning()
    {
        InvokeRepeating("spawnDemon", 0f, spawnRate);
    }

    public void stopSpawning()
    {
        CancelInvoke();
    }

    private void spawnDemon()
    {
        if (demonCounter < maxDemonsOnMap)
        {
            spawnPosition = groundTilemap.CellToWorld(spawnPoints[Random.Range(0, spawnPoints.Count)]);
            Instantiate(demons[Random.Range(0, demons.Count)], spawnPosition, Quaternion.identity);
            demonCounter++;
        }
        else
        {
            return;
        }
    }

}
