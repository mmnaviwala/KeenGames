#pragma strict

var dirty : float = 1.5;

function Start () {
    GetComponent.<Renderer>().material.shader = Shader.Find("_DirtyBumpedSpecular");
}

function OnGUI () {
    dirty = GUI.HorizontalSlider (Rect (25, 50, 100, 30), dirty, 0.0, 1.5);
}

function Update () {
    GetComponent.<Renderer>().material.SetFloat( "_MakeDirty", dirty);
}