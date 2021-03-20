using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flowerCollisionPollen : MonoBehaviour
{
    [SerializeField]
    private LevelManager levelmanager;

    void Start()
    {
        levelmanager = LevelManager.getInstance();
    }
    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Demon")
        {
            //TODO: animation and sneezing sound
            levelmanager.reduceDemonCount();
            Destroy(other.gameObject);
        }
    }
}
