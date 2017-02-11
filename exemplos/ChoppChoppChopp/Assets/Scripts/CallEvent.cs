using UnityEngine;
using UnityEngine.Events;
using System.Collections;

#if UNITY_EDITOR
using UnityEditor;
#endif


public class CallEvent : ConditionBasedBehavior {

	public UnityEvent action;

	public override void OnConditionSatisfied () {
		if (action != null)
		{
			action.Invoke();
		}
	}


}

#if UNITY_EDITOR
[CustomEditor(typeof(CallEvent))]
public class CallEventEditor : ConditionBasedEditor {

}
#endif
