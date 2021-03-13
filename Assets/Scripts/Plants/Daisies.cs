using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Daisies : Flowers
{
    private ParticleSystem daisyPollen;

    // Start is called before the first frame update
    void Awake()
    {
        daisyPollen = GetComponentInChildren<ParticleSystem>();
        daisyPollen.Play();
    }
}
