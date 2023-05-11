using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TestScript : MonoBehaviour
{
	[SerializeField] bool trigger;
	[SerializeField] UnityEvent action;

	private void Update()
	{
		if (trigger)
		{
			trigger = false;
			action.Invoke();
		}
	}
}
