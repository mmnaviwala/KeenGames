using UnityEngine;
using System.Collections;

public class GuiTextureClickEvent : MonoBehaviour {

	void OnMouseDown ()
	{
		if (this.gameObject.name == "Start Game Text")
		{
			Application.LoadLevel(Application.loadedLevel + 1);
		}
		else if (this.gameObject.name == "Exit To Desktop Text")
		{
			Application.Quit();
		}
	}
}
