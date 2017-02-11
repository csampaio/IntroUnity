using UnityEngine;
using System.Collections;

public class FireCondition : ActionCondition
{
	public FireCondition(ActionConditionData data) : base(data)
	{
		data.selectionValue = 1;
	}

	public override bool VerifyCondition(GameObject gameObject)
	{
		//Debug.Log("FireCondition::Update - VerifyCondition " + Input.GetMouseButtonDown(0));

		return Input.GetMouseButtonDown(0);
	}
}