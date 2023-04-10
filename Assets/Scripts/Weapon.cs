using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public string enemyTagName;
    public float damage;
    public bool isAttacking;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isAttacking && other.CompareTag(enemyTagName))
        {
            other.GetComponent<Health>().Damage(damage);
        }
    }
}
