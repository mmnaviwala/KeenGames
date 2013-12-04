using UnityEngine;
using System.Collections;

public class checkIfClicked : MonoBehaviour {

	public string guiTextureNameShouldBe;

	void OnMouseDown()
	{
		if(guiTexture.name == guiTextureNameShouldBe)
		{
			// do something here
			Debug.Log(guiTextureNameShouldBe + " was clicked");
		}
	}
}
