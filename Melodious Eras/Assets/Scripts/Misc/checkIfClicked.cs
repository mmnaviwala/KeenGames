using UnityEngine;
using System.Collections;

public class checkIfClicked : MonoBehaviour {

	public string guiTextureNameShouldBe;
	GUITexture normalTexture;
	GUITexture hoverTexture;

	void OnMouseDown()
	{
		if(guiTexture.name == guiTextureNameShouldBe)
		{
			Debug.Log(guiTextureNameShouldBe + " was clicked");
		}

		if(guiTexture.name == "Gui Button Bottom")
		{
			// load test level
			Application.LoadLevel("scene_stealth_test");
		}

		if(guiTexture.name == "Gui Button Top")
		{
			// load level 1
			Application.LoadLevel("Military Complex");
		}

		if(guiTexture.name == "gui texture bottom right")
		{
			// exit to main menu
			Application.LoadLevel("island2");
		}

		if(guiTexture.name == "gui texture bottom left")
		{
			// resume game
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

	void OnMouseOver()
	{
	}
}
