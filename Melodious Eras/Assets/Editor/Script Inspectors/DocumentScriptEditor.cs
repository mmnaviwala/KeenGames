using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(DocumentScript))]
public class DocumentScriptEditor : Editor {

    //public override void OnInspectorGUI()
    //{
    //    var document = target as DocumentScript;
    //    document.message = EditorGUILayout.TextArea(document.message);
    //}
}
