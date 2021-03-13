using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flowerCollisionPollen : MonoBehaviour
{
    public bool hasPollen;

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (hasPollen && collision.gameObject.tag == "Demon")
        {
            //TODO: animation and sneezing sound
            Destroy(collision.gameObject);
        }
    }
}
