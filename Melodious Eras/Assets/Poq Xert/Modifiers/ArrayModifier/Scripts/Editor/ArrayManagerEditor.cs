//Created by PoqXert (poqxert@gmail.com)

using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ArrayManager))]
public class ArrayManagerEditor : Editor {
	
	[MenuItem("Component/Modifiers/Array")]
	static void Array(){
		if(Selection.activeGameObject != null)
			Selection.activeGameObject.AddComponent(typeof(ArrayManager));
	}
	[MenuItem("Component/Modifiers/Donate")]
	static void Donate()
	{
		Application.OpenURL("https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=GPPJYJ7GTE4LG");
	}
	
	private int typeArray = 0;
	private string[] arrayName = new string[]{"Linear", "Curve", "Object", "Circle"};
	public override void OnInspectorGUI()
	{
		
		//Отрисовка инспектора по умолчанию
		DrawDefaultInspector();
		
		ArrayManager arrManager = target as ArrayManager;
		EditorGUIUtility.LookLikeControls();
		EditorGUILayout.BeginHorizontal();
		typeArray = EditorGUILayout.Popup("Type", typeArray, arrayName, EditorStyles.popup);
		EditorGUILayout.EndHorizontal();
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.Separator();
		if(GUILayout.Button("Add Array"))
			arrManager.NewArray(typeArray);
		EditorGUILayout.Separator();
		EditorGUILayout.EndHorizontal();
	}
}
