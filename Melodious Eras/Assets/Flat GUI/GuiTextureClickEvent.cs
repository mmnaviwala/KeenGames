using UnityEngine;
using System.Collections;

public class GuiTextureClickEvent : MonoBehaviour {

	void OnMouseDown ()
	{
		if (this.gameObject.name == "Start Game Text")
		{
			Application.LoadLevel(2);
		}
		else if (this.gameObject.name == "Exit To Desktop Text")
		{
			Application.Quit();
		}
	}
}
