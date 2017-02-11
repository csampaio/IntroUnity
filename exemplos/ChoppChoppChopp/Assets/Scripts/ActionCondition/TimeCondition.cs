using UnityEngine;
using System.Collections;

public class TimeCondition : ActionCondition 
{

	private float lastMove;

	// Use this for initialization
	public TimeCondition (ActionConditionData data) : base(data) 
	{
		data.selectionValue = 4;
		lastMove = Time.time;
	}

	public override bool VerifyCondition(GameObject gameObject)
	{
		if (Time.time - lastMove > data.timeInSeconds)
		{
			lastMove = Time.time;
			return true;
		}

		return false;
	}
}
