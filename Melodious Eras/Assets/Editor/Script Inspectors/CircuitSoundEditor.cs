using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(CircuitSound))]
[CanEditMultipleObjects]
public class CircuitSoundEditor : Editor 
{
	new void OnInspectorGUI()
	{
		var myScript = target as CircuitSound;
		
		myScript.useTrigger = EditorGUILayout.Toggle(myScript.useTrigger, "Use Trigger");

		if(myScript.useTrigger)
			myScript.triggerArea = (DetectionSpherePlayer)EditorGUILayout.ObjectField(myScript.triggerArea, typeof(DetectionSphere), true);
		
	}
}
