using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BoolRef
{
	public bool value;

	public static implicit operator bool(BoolRef r) { return r.value; }
}
