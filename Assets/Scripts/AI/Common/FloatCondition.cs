using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatCondition : Condition
{
	FloatRef parameter;
	float condition;
	Predicate predicate;

	public FloatCondition(FloatRef parameter, Predicate predicate, float condition)
	{
		this.parameter = parameter;
		this.condition = condition;
		this.predicate = predicate;
	}

	public override bool IsTrue()
	{
		bool result = false;
		switch (predicate)
		{
			case Predicate.EQUAL:
				result = (parameter == condition);
				break;
			case Predicate.LESS:
				result = (parameter < condition);
				break;
			case Predicate.LESS_EQUAL:
				result = (parameter <= condition);
				break;
			case Predicate.GREATER_EQUAL:
				result = (parameter >= condition);
				break;
			case Predicate.GREATER:
				result = (parameter > condition);
				break;
			default:
				break;
		}

		return result;
	}
}
