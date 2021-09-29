using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class flowerCollisionCenter : MonoBehaviour
{
    [SerializeField]
    private float manaIncreaseValue = 0.01f;
    private GameObject player;
    private Mana myMana;
    private LevelManager levelManager;
    private bool oldManaIncreasing = false;
    private AudioSource bitingAudio;
    //public UnityEvent OnFlowerEaten;

    //private bool manaIncreasing = false;

    void Start()
    {
        player = GameObject.Find("Player");
        myMana = player.GetComponent<Mana>();
        levelManager = LevelManager.GetInstance();
        bitingAudio = GetComponent<AudioSource>();
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            oldManaIncreasing = levelManager.getManaIncreasing();
            levelManager.setManaIncreasing(true);
            if (oldManaIncreasing != levelManager.getManaIncreasing() && levelManager.GetCurrentMana() < 100)
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
            StartCoroutine("playAudioAndDestroy");
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

    IEnumerator playAudioAndDestroy()
    {
        bitingAudio.Play();
        while (bitingAudio.isPlaying)
            yield return null;
        Destroy(transform.parent.gameObject);
    }
}
