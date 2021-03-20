using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flowerCollisionCenter : MonoBehaviour
{
    [SerializeField]
    private float manaIncreaseValue = 0.01f;
    private GameObject player;
    private Mana myMana;
    private LevelManager levelManager;
    private bool oldManaIncreasing = false;
    private AudioSource bitingAudio;

    //private bool manaIncreasing = false;

    void Start()
    {
        player = GameObject.Find("Player");
        myMana = player.GetComponent<Mana>();
        levelManager = LevelManager.getInstance();
        bitingAudio = GetComponent<AudioSource>();
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            oldManaIncreasing = levelManager.getManaIncreasing();
            levelManager.setManaIncreasing(true);
            if (oldManaIncreasing != levelManager.getManaIncreasing() && levelManager.getCurrentMana() < 100)
            {
                //mana starts filling up
                //playerCollisionStarted.Invoke();
                myMana.setManaIncrease(manaIncreaseValue);
                myMana.InvokeRepeating("increaseMana", 0F, 0.01F);
            }

        }
        else if (other.gameObject.tag == "Demon")
        {
            //demon eats flower
            bitingAudio.Play();
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
