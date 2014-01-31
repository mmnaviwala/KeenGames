//Created by PoqXert (poqxert@gmail.com)

using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CircleArray))]
public class CircleArrayEditor : Editor {
	
	private GameObject array;
	private GameObject go = null;
		
	#region Переменные изменений
	private int _count1 = 0;
	private float _radius1 = 0;
	private Vector3 _directionRadius1 = Vector3.zero;
	private Vector3 _axisRotation1 = Vector3.zero;
	private bool _alloworiginal1 = true;
	private bool _userot1 = true;
	#endregion
	public override void OnInspectorGUI ()
	{
		DrawDefaultInspector();
		
		CircleArray arrCircle = target as CircleArray;
		if(arrCircle.UseRotation)
		{
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.Separator();
			if(GUILayout.Button("Create Array"))
				CircleArray();
			EditorGUILayout.Separator();
			EditorGUILayout.EndHorizontal();
			EditorGUILayout.Separator();
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.Separator();
			if(GUILayout.Button("Apply"))
			{
				DestroyImmediate(go);
				DestroyImmediate(arrCircle.GetComponent<CircleArray>());
			}
			EditorGUILayout.Separator();
			if(GUILayout.Button("Cancel"))
			{
				array = GameObject.Find("Array Object " + arrCircle.gameObject.name);
				if(array != null)
					DestroyImmediate(array.gameObject);
				DestroyImmediate(arrCircle.GetComponent<CircleArray>());
			}
			EditorGUILayout.Separator();
			EditorGUILayout.EndHorizontal();
		}
		if(arrCircle.Count == 0)
			arrCircle.Count = 1;
		if(arrCircle.Radius == 0)
			arrCircle.Radius = 1;
	}
	void OnSceneGUI(){
		CircleArray arrCircle = (CircleArray) target as CircleArray;
		if((_count1 != arrCircle.Count) || (_alloworiginal1 != arrCircle.AllowOriginal)
		   || (_userot1 != arrCircle.UseRotation) || (_radius1 != arrCircle.Radius) || (_directionRadius1 != arrCircle.directionRadius)
			|| (_axisRotation1 != arrCircle.axisRotation))
		{
			CircleArray();
			_count1 = arrCircle.Count; _alloworiginal1 = arrCircle.AllowOriginal;
			_userot1 = arrCircle.UseRotation; _radius1 = arrCircle.Radius; _directionRadius1 = arrCircle.directionRadius;
			_axisRotation1 = arrCircle.axisRotation;
		}
	}
	public void CircleArray(){
		CircleArray arrCircle = target as CircleArray;
		
		if(arrCircle.Count > 0){
			for(int i = 0; i < arrCircle.Count; i++){
				if(i == 0){
					//Если учитывается оригинал
					if(arrCircle.AllowOriginal)
						i++;
					array = GameObject.Find("Array Object " + arrCircle.gameObject.name);
					if(array != null)
						DestroyImmediate(array.gameObject);
					array = new GameObject("Array Object " + arrCircle.gameObject.name);
					//Создание центра окружности
					go = new GameObject("CenterArrayCircle");
					//go.hideFlags = HideFlags.HideInHierarchy;
					go.transform.parent = arrCircle.transform;
					go.transform.position = arrCircle.transform.position;
					go.transform.Translate(arrCircle.transform.TransformDirection(arrCircle.directionRadius.normalized * arrCircle.Radius));
					go.transform.parent = array.transform;
					go.transform.rotation = arrCircle.transform.rotation;
				}
				
				GameObject newGO = Instantiate(arrCircle.gameObject) as GameObject;
				newGO.name = (arrCircle.gameObject.name + " " + i);
				DestroyImmediate(newGO.GetComponent<CircleArray>());
				newGO.transform.parent = go.transform;
				go.transform.Rotate(arrCircle.axisRotation.normalized * 360/arrCircle.Count * i);
				newGO.transform.parent = array.transform;
				go.transform.rotation = arrCircle.transform.rotation;
				
				//Если используется вращение
				if(!arrCircle.UseRotation){
					newGO.transform.rotation = arrCircle.gameObject.transform.rotation;
				}
				
				//Созданный объект в массиве
				go.transform.parent = array.transform;
			}
			
			if(arrCircle.AllowOriginal)
				array.transform.parent = arrCircle.gameObject.transform;
		}
	}
}
