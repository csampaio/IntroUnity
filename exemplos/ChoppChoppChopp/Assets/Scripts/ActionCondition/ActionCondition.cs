using UnityEngine;
using System;
using System.Collections;

public enum KeyMode
{
	Up,
	Down,
	Pressed
}

[Serializable]
public class ActionConditionData
{
	public int selectionValue;
	public int layer;
	public float timeInSeconds;
	public KeyCode keyCode;
	public KeyMode keyMode;
}

public abstract class ActionCondition
{
	protected ActionConditionData data;

	public ActionCondition(ActionConditionData actionData)
	{
		data = actionData;
	}

	public abstract bool VerifyCondition(GameObject gameObject);

	public static ActionCondition GenerateBy(ActionConditionData data)
	{
		int selectionValue = data.selectionValue;

		if (selectionValue == 1)
		{
			return new FireCondition(data);
		}
		else if (selectionValue == 2)
		{
			return new TouchCondition(data);
		}
		else if (selectionValue == 3)
		{
			return new KeyCodePressedCondition(data);
		}
		else if (selectionValue == 4)
		{
			return new TimeCondition(data);
		}

		return new ManualCondition(data);
	}
}


