using UnityEngine;
using System.Collections;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class DestroyMe : ConditionBasedBehavior {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	public override void OnConditionSatisfied()
	{
		Destroy(gameObject);
	} 
}

#if UNITY_EDITOR
[CustomEditor(typeof(DestroyMe))]
public class DestroyMeEditor : ConditionBasedEditor {

}
#endif
