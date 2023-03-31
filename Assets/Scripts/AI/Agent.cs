using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    public Movement movement;

    public Animator animator;

    public static T[] GetAgents<T>() where T : Agent
	{
        return FindObjectsOfType<T>();
	}
}
