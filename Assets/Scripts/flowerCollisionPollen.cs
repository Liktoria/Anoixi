using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class flowerCollisionPollen : MonoBehaviour
{
    private LevelManager levelmanager;
    public UnityEvent onPollenCollision;

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
            onPollenCollision.Invoke();
            Destroy(other.gameObject);
        }
    }
}
