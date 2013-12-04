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
			// do something here
			Debug.Log(guiTextureNameShouldBe + " was clicked");
		}
	}

	void OnMouseOver()
	{
	}
}
