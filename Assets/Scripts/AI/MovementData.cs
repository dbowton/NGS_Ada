using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MovementData", menuName = "AI/MovementData")]
public class MovementData : ScriptableObject
{
	[Range(0, 20)] public float maxSpeed = 12;
	[Range(0, 20)] public float minSpeed = 6;
	[Range(0, 100)] public float maxForce = 8;
	[Range(0, 500)] public float turnRate = 360;

	public bool orientMovement = true;
}
