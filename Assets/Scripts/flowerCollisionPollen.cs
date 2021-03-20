using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flowerCollisionPollen : MonoBehaviour
{
    private LevelManager levelmanager;
    private AudioSource sneezingSound;

    void Start()
    {
        levelmanager = LevelManager.getInstance();
        sneezingSound = GetComponent<AudioSource>();
    }
    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Demon")
        {
            //TODO: animation and sneezing sound
            levelmanager.reduceDemonCount();
            sneezingSound.Play();
            Destroy(other.gameObject, 0.6f);
        }
    }
}
