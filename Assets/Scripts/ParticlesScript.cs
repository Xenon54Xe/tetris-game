using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesScript : MonoBehaviour
{
    private void Start()
    {
        GetComponent<ParticleSystem>().Play();
    }

    public void StartPlaying()
    {
        GetComponent<ParticleSystem>().Play();
    }

    public void StopPlaying()
    {
        GetComponent<ParticleSystem>().Stop();
    }
}
