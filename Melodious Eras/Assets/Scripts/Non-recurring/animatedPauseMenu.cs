using UnityEngine;
using System.Collections;

public class animatedPauseMenu : MonoBehaviour {
	
	private bool gamePaused = false;

	void Update()
	{
		Screen.showCursor = true;
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (Time.timeScale == 1)
			{
				Time.timeScale = 0;

				//GameObject.Find("GUI Text for gui texture left").gameObject.GetComponent<GUIText>().text = "Logs\n\n";

				iTween.MoveTo(GameObject.Find("gui texture bottom left").gameObject, iTween.Hash("path",iTweenPath.GetPath("Move Bottom Left Texture In"), "time", 1, "ignoretimescale", true));
				iTween.MoveTo(GameObject.Find("gui texture bottom right").gameObject, iTween.Hash("path",iTweenPath.GetPath("Move Bottom Right Texture In"), "time", 1, "ignoretimescale", true));
				iTween.MoveTo(GameObject.Find("gui texture left").gameObject, iTween.Hash("path",iTweenPath.GetPath("Move Left Texture In"), "time", 1, "ignoretimescale", true));
				iTween.MoveTo(GameObject.Find("gui texture right").gameObject, iTween.Hash("path",iTweenPath.GetPath("Move Right Texture In"), "time", 1, "ignoretimescale", true));
				iTween.MoveTo(GameObject.Find("gui texture top").gameObject, iTween.Hash("path",iTweenPath.GetPath("Move Top Texture In"), "time", 1, "ignoretimescale", true));

				GameObject.Find("Main Camera").gameObject.GetComponent<CameraMovement3D>().enabled = false;
				GameObject.Find("Main Camera").gameObject.GetComponent<AudioSource>().mute = true;
				GameObject.Find("Dr C Sharp").gameObject.GetComponent<HUD_Stealth>().enabled = false;
				GameObject.Find("player_flashlight").gameObject.GetComponent<Flashlight>().enabled = false;
				//GameObject.Find("gui texture background").gameObject.GetComponent<GUITexture>().enabled = true;
			}
			else
			{
				Time.timeScale = 1;
				iTween.MoveTo(GameObject.Find("gui texture bottom left").gameObject, iTween.Hash("path",iTweenPath.GetPath("Move Bottom Left Texture Out"), "time", 1, "ignoretimescale", true));
				iTween.MoveTo(GameObject.Find("gui texture bottom right").gameObject, iTween.Hash("path",iTweenPath.GetPath("Move Bottom Right Texture Out"), "time", 1, "ignoretimescale", true));
				iTween.MoveTo(GameObject.Find("gui texture left").gameObject, iTween.Hash("path",iTweenPath.GetPath("Move Left Texture Out"), "time", 1, "ignoretimescale", true));
				iTween.MoveTo(GameObject.Find("gui texture right").gameObject, iTween.Hash("path",iTweenPath.GetPath("Move Right Texture Out"), "time", 1, "ignoretimescale", true));
				iTween.MoveTo(GameObject.Find("gui texture top").gameObject, iTween.Hash("path",iTweenPath.GetPath("Move Top Texture Out"), "time", 1, "ignoretimescale", true));	

				GameObject.Find("Main Camera").gameObject.GetComponent<CameraMovement3D>().enabled = true;
				GameObject.Find("Main Camera").gameObject.GetComponent<AudioSource>().mute = false;
				GameObject.Find("Dr C Sharp").gameObject.GetComponent<HUD_Stealth>().enabled = true;
				GameObject.Find("player_flashlight").gameObject.GetComponent<Flashlight>().enabled = true;
				//GameObject.Find("gui texture background").gameObject.GetComponent<GUITexture>().enabled = false;
			}
		}


	}


}
