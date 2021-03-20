using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSound : MonoBehaviour
{
    [SerializeField]
    private float loopInterval;
    private AudioSource movingAudio;

    // Start is called before the first frame update
    void Start()
    {
        movingAudio = GetComponent<AudioSource>();
        loopPlaying(loopInterval);
    }

    private void loopPlaying(float loopInterval)
    {
        InvokeRepeating("playSound", 0f, loopInterval);
    }

    private void playSound()
    {
        movingAudio.Play();
    }

    void OnDestroy()
    {
        CancelInvoke();
    }
}
