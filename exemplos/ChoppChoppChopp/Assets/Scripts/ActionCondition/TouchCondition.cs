using UnityEngine;
using System.Collections;

public class TouchCondition : ActionCondition
{
	public bool touched;

	public TouchCondition(ActionConditionData data) : base(data)
	{
		data.selectionValue = 2;
	}

	public override bool VerifyCondition(GameObject gameObject)
	{
		//Debug.Log("TouchCondition::Update - VerifyCondition - " + touched);
		if (touched)
		{
			touched = false; // Consumed

			return true;
		}
		return touched;
	}

	/// <summary>
	/// 
	/// </summary>
	public void VerifyTouch(Collider collider)
	{
		if (collider.gameObject != null && collider.gameObject.layer == data.layer)
		{
			touched = true;
		}
	}
}
