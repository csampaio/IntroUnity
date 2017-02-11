using UnityEngine;
using System.Collections;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Move : ConditionBasedBehavior {

	public Vector3 direction;
	
	public override void OnConditionSatisfied () {
		transform.position = transform.position + direction;
	}

	public void ChangeDirectionX()
	{
		direction.x = (-1) * direction.x;
	}

	public void ChangeDirectionY()
	{
		direction.y = (-1) * direction.y;
	}

	public void ChangeDirectionZ()
	{
		direction.z = (-1) * direction.z;
	}

	public void ChangeDirectionX(float x)
	{
		direction.x = x;
	}

	public void ChangeDirectionY(float y)
	{
		direction.x = y;
	}

	public void ChangeDirectionZ(float z)
	{
		direction.x = z;
	}
}

#if UNITY_EDITOR
[CustomEditor(typeof(Move))]
public class MoveEditor : ConditionBasedEditor {

}
#endif
