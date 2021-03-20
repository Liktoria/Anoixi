using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PollenSounds : MonoBehaviour
{
    private int _currentNumberOfParticles = 0;
    private AudioSource pollenSound;
    private ParticleSystem pollenParticles;

    // Start is called before the first frame update
    void Start()
    {
        pollenSound = GetComponent<AudioSource>();
        pollenParticles = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        var amount = Mathf.Abs(_currentNumberOfParticles - pollenParticles.particleCount);

        if (pollenParticles.particleCount > _currentNumberOfParticles)
        {
            pollenSound.Play();
        }

        _currentNumberOfParticles = pollenParticles.particleCount;
    }
}
