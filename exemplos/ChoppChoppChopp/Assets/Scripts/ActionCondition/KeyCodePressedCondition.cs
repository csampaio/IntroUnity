using UnityEngine;
using System.Collections;

public class KeyCodePressedCondition : ActionCondition {

	public KeyCodePressedCondition(ActionConditionData data) : base(data)
	{
		data.selectionValue = 3;
	}

	public override bool VerifyCondition(GameObject gameObject)
	{	
		bool result = false;

		if (data.keyMode == KeyMode.Pressed)
		{
			result = Input.GetKey(data.keyCode);
		}
		else if (data.keyMode == KeyMode.Up)
		{
			result = Input.GetKeyUp(data.keyCode);
		}
		else if (data.keyMode == KeyMode.Down)
		{
			result = Input.GetKeyDown(data.keyCode);
		}

		//Debug.Log("KeyCodePressedCondition::Update - VerifyCondition - keyCode: " + data.keyCode + " keyMode: " + data.keyMode + " = " + result);

		return result;
	}
}
