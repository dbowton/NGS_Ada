using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Condition
{
	public enum Predicate
	{ 
		EQUAL,
		LESS,
		LESS_EQUAL,
		GREATER_EQUAL,
		GREATER
	}

	public abstract bool IsTrue();
}
