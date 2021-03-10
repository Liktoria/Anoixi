using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Calculations
{
    public int calculateCorrectZ(Vector3Int cellToCheck, List<Tilemap> levelTilemapsAscending)
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
