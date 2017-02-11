using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

public abstract class ConditionBasedBehavior : MonoBehaviour {

	[HideInInspector]
	[SerializeField]
	public List<ActionConditionData> conditions;
	
	protected List<ActionCondition> actionConditions = null;

	// Use this for initialization
	public void Update()
	{
		if (actionConditions == null && conditions != null)
		{
			actionConditions = new List<ActionCondition>();

			for (int i = 0; i < conditions.Count; i++)
			{
				actionConditions.Add(ActionCondition.GenerateBy(conditions[i]));
			}
		}

		if (actionConditions != null)
		{
			for (int i = 0; i < actionConditions.Count; i++)
			{
				//Debug.Log("ConditionBasedBehavior::Update - VerifyCondition " + actionConditions[i].VerifyCondition(gameObject));
				if (actionConditions[i].VerifyCondition(gameObject))
				{
					OnConditionSatisfied();
				}
			}
		}
	}

	public abstract void OnConditionSatisfied();

	public void OnTriggerEnter(Collider collider)
	{
		if (actionConditions != null)
		{
			TouchCondition touchCondition;

			for (int i = 0; i < actionConditions.Count; i++)
			{
				touchCondition = actionConditions[i] as TouchCondition;

				if (touchCondition != null)
				{
					touchCondition.VerifyTouch(collider);
				}
			}
		}
	}

	public void ActionConditionDestroyed(int i)
	{
		if (actionConditions != null && actionConditions.Count >= i)
			actionConditions.RemoveAt(i);
	}

	public void RecreateCondition(int i)
	{
		if (actionConditions != null &&  actionConditions.Count >= i) {
			actionConditions[i] = ActionCondition.GenerateBy(conditions[i]);
				
		}
	}


}

#if UNITY_EDITOR
[CustomEditor(typeof(ConditionBasedBehavior))]
public class ConditionBasedEditor : Editor {

	protected bool isUsingDefaultInspector = true;

	public virtual void OnEnable()
	{

	}

	public override void OnInspectorGUI()
	{
		ConditionBasedBehavior generate = target as ConditionBasedBehavior;

		if (generate == null)
		{
			return;
		}

		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField("Conditions");

		if (GUILayout.Button("Add Condition"))
		{
			if (generate.conditions == null)
			{
				generate.conditions = new List<ActionConditionData>();
			}
			generate.conditions.Add(new ActionConditionData());
			EditorUtility.SetDirty(generate);
		}
		EditorGUILayout.EndHorizontal();

		EditorGUI.indentLevel = 1;
		if (generate.conditions != null)
		{
			bool shouldRecreateList = false;

			int lastConditionType = 0;

			for (int i = 0; i < generate.conditions.Count; i++)
			{
				lastConditionType = generate.conditions[i].selectionValue;
				generate.conditions[i] = EditCondition(generate.conditions[i]);

				if (generate.conditions[i] == null)
				{
					shouldRecreateList = true;
					generate.ActionConditionDestroyed(i);
				}
				else if (generate.conditions[i].selectionValue != lastConditionType)
				{
					generate.RecreateCondition(i);
				}
			}		
			if (shouldRecreateList)
			{
				List<ActionConditionData> data = new List<ActionConditionData>();
				for (int i = 0; i < generate.conditions.Count; i++)
				{
					if (generate.conditions[i] != null)
					{
						data.Add(generate.conditions[i]);
					}
				}
				generate.conditions = data;
			}

			EditorUtility.SetDirty(generate);
		}

		EditorGUI.indentLevel = 0;

		if (isUsingDefaultInspector)
			base.OnInspectorGUI();
	}

	public ActionConditionData EditCondition(ActionConditionData condition)
	{
		if (condition == null)
		{
			return null;
		}

		EditorGUI.indentLevel = 1;

		int selected = condition.selectionValue;
		string[] options = new string[]
		{
			"Manual", "On Mouse Down", "On Touched by", "Key pressed", "Repeating timer"
		};
		EditorGUILayout.BeginHorizontal();
		selected = EditorGUILayout.Popup("Condition", selected, options);
		if (GUILayout.Button("Remove"))
	 	{
	 		condition = null;	
	 	}
		EditorGUILayout.EndHorizontal();

		if (condition == null)
		{
			return null;
		}

		condition.selectionValue = selected;

		EditorGUI.indentLevel = 2;

		if (selected == 2)
		{
			int selectedLayer = EditorGUILayout.LayerField("Layer", condition.layer);
			condition.layer = selectedLayer;
	 	}
	 	else if (selected == 3)
	 	{
	 		int keyCodeSelected = 0;
	 		String[] keysName = Enum.GetNames(typeof(KeyCode));
	 		for (int i = 0; i < keysName.Length; i++)
	 		{
	 			if (keysName[i] == condition.keyCode.ToString())
	 			{
	 				keyCodeSelected = i;
	 			}
	 		}
	 		keyCodeSelected = EditorGUILayout.Popup("Key", keyCodeSelected, keysName);
	 		condition.keyCode = (KeyCode) Enum.Parse(typeof(KeyCode), keysName[keyCodeSelected], true);

	 		String[] keysModeName = Enum.GetNames(typeof(KeyMode));
	 		int keyModeSelected = EditorGUILayout.Popup("Key", (int) condition.keyMode, keysModeName);
	 		condition.keyMode = (KeyMode) keyModeSelected;
	 	}
	 	else if (selected == 4)
	 	{
	 		condition.timeInSeconds = EditorGUILayout.FloatField("Seconds", condition.timeInSeconds);
	 	}

	 	EditorGUI.indentLevel = 1;

	 	return condition;
	}
}
#endif
