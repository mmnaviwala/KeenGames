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
			Application.LoadLevel("scene_stealth_test");
		}

		if(guiTexture.name == "Gui Button Top")
		{
			Application.LoadLevel("Military Complex");
		}
	}

	void OnMouseOver()
	{
	}
}
