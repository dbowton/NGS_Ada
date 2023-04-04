using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FloatRef
{
    public float value;

    public static implicit operator float(FloatRef r) { return r.value; }
}
