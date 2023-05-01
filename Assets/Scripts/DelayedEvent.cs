using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DelayedEvent : MonoBehaviour
{
	[SerializeField] UnityEvent action;

	public void Trigger(float delay)
	{
		new Timer(delay, () => action.Invoke());
	}
}
