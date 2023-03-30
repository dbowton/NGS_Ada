using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Perception : MonoBehaviour
{
    public string tagname;
	[Range(1, 40)] public float distance = 1;
	[Range(1, 180)] public float angle = 0;

	public abstract GameObject[] GetGameObjects();
}
