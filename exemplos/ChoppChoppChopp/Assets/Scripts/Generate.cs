using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


#if UNITY_EDITOR
using UnityEditor;
#endif

public enum GenerationPosition
{
	SamePosition = 0,
	ObjectAsReference = 1
}

public class Generate : ConditionBasedBehavior {
	public GameObject prefab;

	[HideInInspector]
	public GenerationPosition generationPosition;

	[HideInInspector]
	public Transform referencePosition;

	// Update is called once per frame
	public override void OnConditionSatisfied()
	{
		Debug.Log("Generate::OnConditionSatisfied");
		SetupGeneratedObject(GenerateObject(prefab));
	}

	public virtual GameObject GenerateObject(GameObject prefab)
	{
		GameObject generated = Instantiate(prefab) as GameObject;
		return generated;
	}

	public virtual void SetupGeneratedObject(GameObject generated)
	{
		if (generationPosition == GenerationPosition.SamePosition)
			generated.transform.position = transform.position;
		else if (generationPosition == GenerationPosition.ObjectAsReference)
			generated.transform.position = referencePosition.position;
		//generated.transform.parent = transform;
	}
}

#if UNITY_EDITOR
[CustomEditor(typeof(Generate))]
public class GenerateEditor : ConditionBasedEditor {

	public virtual void GenerateOnEditor()
	{
		Generate generate = target as Generate;

		if (generate == null)
		{
			return;
		}

		generate.SetupGeneratedObject(generate.GenerateObject(generate.prefab));
	}

	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();

		Generate generate = target as Generate;

		if (generate == null)
		{
			return;
		}

		int selected =  (int) generate.generationPosition;
		string[] options = new string[]
		{
			"Same position", "Using an object as reference "
		};
		EditorGUILayout.BeginHorizontal();
		selected = EditorGUILayout.Popup("Generation position", selected, options);
		EditorGUILayout.EndHorizontal();
		generate.generationPosition = (GenerationPosition) selected;

		EditorGUI.indentLevel = 1;

		if (generate.generationPosition == GenerationPosition.ObjectAsReference)
		{
			generate.referencePosition = (Transform) EditorGUILayout.ObjectField("Reference object ", generate.referencePosition, typeof(Transform), true);
		}
		
		if (GUILayout.Button("Generate"))
		{
			GenerateOnEditor();
		}
	}


}
#endif


