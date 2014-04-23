using UnityEngine;
using System.Collections;

public class DisplayObjectives : MonoBehaviour {
	
	public GUIStyle textGuiStyle;
	public string currentObjective;

	private float xx, yy;
	private Rect currentObjectiveRect;

	void Start () 
	{
		xx = Screen.width / 10;
		yy = Screen.height / 10;

		currentObjectiveRect = new Rect(xx * 7f, yy * 0.3f, xx*3, xx);

		textGuiStyle.fontSize = System.Convert.ToInt32(Screen.height * .04f);
		textGuiStyle.normal.textColor = Color.gray;
		textGuiStyle.onHover.textColor = Color.white;
		textGuiStyle.alignment = TextAnchor.UpperLeft;
		textGuiStyle.wordWrap = true;
	}

	void Update()
	{
		currentObjective = GameObject.Find("Objectives").GetComponent<TrackObjectives>().currentObjectiveTitle;
	}
	
	void OnGUI ()
	{
		if (GameObject.Find("Objectives").GetComponent<TrackObjectives>().allObjectivesComplete)
			GUI.Label(currentObjectiveRect, ("All Objectives Complete!"), textGuiStyle);
		else
			GUI.Label(currentObjectiveRect, ("Current Objective:\n" + currentObjective), textGuiStyle);
	}
}
