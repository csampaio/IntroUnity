using UnityEngine;
using System.Collections;

public class ManualCondition : ActionCondition
{
	public ManualCondition(ActionConditionData data) : base(data)
	{
		data.selectionValue = 0;
	}

	public override bool VerifyCondition(GameObject gameObject)
	{
		//Debug.Log("ManualCondition::Update - VerifyCondition - false");
		return false;
	}
}
