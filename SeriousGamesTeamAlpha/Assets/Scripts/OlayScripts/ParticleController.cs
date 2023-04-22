using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    public ParticleSystem ExcitedParticleSystem;
    private void Start()
    {
        
    }

    public void LoadParticleAnimation(int i)
    {
        switch (i)
        {
            case 1:
                ExcitedParticleSystem.Play();
                break;
        }
    }
}
