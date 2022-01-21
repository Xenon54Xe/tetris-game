using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesManager : MonoBehaviour
{
    public GameObject[] particles;

    private void Start()
    {
        particles = GameObject.FindGameObjectsWithTag("WaitingParticles");
    }

    public void SetParticlesOn()
    {
        foreach(GameObject particles in particles)
        {
            particles.GetComponent<ParticleSystem>().Play();
        }
    }

    public void SetParticlesOff()
    {
        foreach (GameObject particles in particles)
        {
            particles.GetComponent<ParticleSystem>().Stop();
        }
    }
}
