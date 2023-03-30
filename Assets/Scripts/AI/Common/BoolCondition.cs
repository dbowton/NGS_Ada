using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoolCondition : Condition
{
    BoolRef parameter;
    bool condition;

    public BoolCondition(BoolRef parameter, bool condition)
	{
		this.parameter = parameter;
		this.condition = condition;
	}

	public override bool IsTrue()
	{
		return (parameter == condition);
	}
}
