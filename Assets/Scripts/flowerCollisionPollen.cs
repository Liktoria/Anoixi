using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flowerCollisionPollen : MonoBehaviour
{
    public bool hasPollen;

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (hasPollen && other.gameObject.tag == "Demon")
        {
            //TODO: animation and sneezing sound
            Destroy(other.gameObject);
        }
    }
}
