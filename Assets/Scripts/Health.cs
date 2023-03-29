using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
	[SerializeField] float maxHealth = 10;
	[SerializeField] UnityEvent OnDeath;
	private float currentHealth;

	public float CurrentHealth { get { return currentHealth; } }

<<<<<<< Updated upstream
=======
	private void Start()
	{
		currentHealth = maxHealth;
	}

>>>>>>> Stashed changes
	public void Damage(float value)
	{
		if (currentHealth > 0)
		{
			currentHealth -= value;

			if (currentHealth <= 0)
			{
				OnDeath.Invoke();
			}
		}
	}

	public void Heal(float value)
	{
		currentHealth = Mathf.Min(currentHealth + value, maxHealth);
	}
}
