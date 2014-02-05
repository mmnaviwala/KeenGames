using UnityEngine;
using System.Collections;

public class FlatUI_MainMenuScreen : MonoBehaviour {

	public Texture background, cainText, cainExtendedText, startGameText, exitToDesktopText;

	void OnGUI ()
	{
		GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height), background);
		GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height), cainText);
		GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height), cainExtendedText);
		// left, top, width,height
		if (GUI.Button(new Rect(Screen.width/2.5f, Screen.height/1.4f, Screen.width/4, Screen.height/9), startGameText))
			Application.LoadLevel(Application.loadedLevel + 1);
		if(GUI.Button (new Rect(Screen.width / 2.5f, Screen.height/1.2f, Screen.width/4, Screen.height/9), exitToDesktopText))
			Application.Quit();
	}

}
