using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Weapon : MonoBehaviour
{
    [SerializeField]ParticleSystem weaponTrail;

    List<Collider> colliders = new List<Collider>();
    public string enemyTagName;
    public float damage;
    public bool isAttacking;
    public float attackDelay = 2f;
    public int maxAttackCount = 2;

    [SerializeField] bool isKillBox = false;

    public UnityEvent OnHitSFX;

    void Start()
    {
        if (weaponTrail) weaponTrail.Stop();
    }

    void Update()
    {
        if ((isKillBox || !isAttacking) && colliders.Count > 0)
        {
            colliders.Clear();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (isAttacking && other.CompareTag(enemyTagName))
        {
            if (!colliders.Contains(other))
            {
                other.GetComponent<Health>().Damage(damage);
                colliders.Add(other);
                OnHitSFX.Invoke();
            }
            
        }
    }

    public void PlayWeaponFX()
    {
        if (weaponTrail) weaponTrail.Play();
    }

    public void StopWeaponFX()
    {
        if (weaponTrail) weaponTrail.Stop();
    }
}
