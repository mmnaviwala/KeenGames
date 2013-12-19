#pragma strict

var dirty : float = 1.5;

function Start () {
    renderer.material.shader = Shader.Find("_DirtyBumpedSpecular");
}

function OnGUI () {
    dirty = GUI.HorizontalSlider (Rect (25, 75, 100, 30), dirty, 0.0, 1.5);
}

function Update () {
    renderer.material.SetFloat( "_MakeDirty", dirty);
}