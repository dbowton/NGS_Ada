using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ParticleTrigger : MonoBehaviour
{
    [SerializeField] ParticleSystem _particleSystem;


    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Test_AI")
        {
            _particleSystem.Play();
            Debug.Log("Explosion");
        }
    }
}
