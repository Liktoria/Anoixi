using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flowerCollisionPollen : MonoBehaviour
{
    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Demon")
        {
            //TODO: animation and sneezing sound
            Destroy(other.gameObject);
        }
    }
}
