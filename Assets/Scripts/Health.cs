using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
	[SerializeField] float maxHealth = 10;
	[SerializeField][Tooltip("Not Triggered On Lethal Damage")] public UnityEvent OnHurt;
	public UnityEvent OnDeath;
	private float currentHealth;

	public float CurrentHealth { get { return currentHealth; } }
	public float GetMaxHealth { get { return maxHealth; } }

	private void Start()
	{
		currentHealth = maxHealth;
	}

	public void Damage(float value)
	{
		if (currentHealth > 0)
		{
			currentHealth -= value;

			if (currentHealth <= 0)
			{
				//Debug.Log("health");
				OnDeath.Invoke();
			}
			else
			{
				OnHurt.Invoke();
			}
		}
	}

	public void Heal(float value)
	{
		currentHealth = Mathf.Min(currentHealth + value, maxHealth);
	}
}
