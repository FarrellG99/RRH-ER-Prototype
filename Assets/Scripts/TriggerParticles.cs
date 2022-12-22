using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerParticles : MonoBehaviour
{
    [SerializeField] ParticleSystem leavesParticles1;
    [SerializeField] ParticleSystem leavesParticles2;
    [SerializeField] ParticleSystem leavesParticles3;
    // [SerializeField] ParticleSystem leavesParticles3;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            leavesParticles1.Play();
            leavesParticles2.Play();
            leavesParticles3.Play();
        }
    }
}
