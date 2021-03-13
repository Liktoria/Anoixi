using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flowerCollisionCenter : MonoBehaviour
{
    private GameObject player;
    private Mana myMana;
    private LevelManager levelManager;
    private bool oldManaIncreasing = false;

    //private bool manaIncreasing = false;

    void Awake()
    {
        player = GameObject.Find("Player");
        myMana = player.GetComponent<Mana>();
        levelManager = LevelManager.getInstance();
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Flower noticed collision.");
        if (other.gameObject.tag == "Player")
        {
            oldManaIncreasing = levelManager.getManaIncreasing();
            levelManager.setManaIncreasing(true);
            if (oldManaIncreasing != levelManager.getManaIncreasing() && levelManager.getCurrentMana() < 100)
            {
                //mana starts filling up
                //playerCollisionStarted.Invoke();
                myMana.InvokeRepeating("increaseMana", 0.2F, 0.1F);
            }

        }
        else if (other.gameObject.tag == "Demon")
        {
            //demon eats flower
            Destroy(transform.parent.gameObject);
        }
    }

    protected void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
                //mana stops filling up
                //playerCollisionEnded.Invoke();
                levelManager.setManaIncreasing(false);
        }
    }
}
