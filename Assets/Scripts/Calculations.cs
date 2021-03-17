using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Calculations
{
    private LevelManager levelmanager;
    private List<Tilemap> tilemapsAscending = new List<Tilemap>();

    public int calculateCorrectZ(Vector3Int cellToCheck)
    {
        levelmanager = LevelManager.getInstance();
        tilemapsAscending = levelmanager.getTilemapsAscending();
        int z = 0;
        for (int i = 0; i < tilemapsAscending.Count; i++)
        {
            cellToCheck.z = i;
            if (tilemapsAscending[i].HasTile(cellToCheck))
            {
                z = i;
            }
        }

        return z;
    }
}
