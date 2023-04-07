using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public string enemyTagName;
    public float damage;
    public bool attacking;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (attacking && other.CompareTag(enemyTagName))
        {
            other.GetComponent<Health>().Damage(damage);
        }
    }
}
