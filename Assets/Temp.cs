using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temp : MonoBehaviour
{
    [SerializeField] Health playerHealth;


    void Update()
    {
        if (playerHealth != null)
            UIGameManager.Instance.healthBar.value = playerHealth.CurrentHealth / playerHealth.GetMaxHealth * 100f;
    }
}
