using UnityEngine;
using System.Collections;

public class titleMenuLogoScript : MonoBehaviour {
	
	public GUIStyle buttonStyle;
	public float buttonWidth;
	public float buttonHeight;
	
	
	void Start()
	{
		// -------------------------------------------------------
		// Setting the button style  
		buttonStyle = new GUIStyle();
		Color buttonStyleColor = new Color(0.09F, 0.87F, 1F);
		buttonStyle.normal.textColor = buttonStyleColor;    
		buttonStyle.fontStyle = FontStyle.Bold;
		buttonStyle.alignment = TextAnchor.MiddleCenter;
		buttonStyle.fontSize = 40;
		// -------------------------------------------------------
		
		buttonWidth = Screen.width*0.35F;
		buttonHeight = Screen.height*0.12F;
		
	}

	void OnGUI()
	{
		if (GUI.Button(new Rect(Screen.width/2 - Screen.width/6, Screen.height/2 + Screen.height*0.20F, buttonWidth, buttonHeight), "Click here to start game", buttonStyle))
		{
			Application.LoadLevel("scene2");
		}
	}
}
