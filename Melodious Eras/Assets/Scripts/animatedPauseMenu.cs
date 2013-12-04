using UnityEngine;
using System.Collections;

public class animatedPauseMenu : MonoBehaviour {
	
	private bool gamePaused = false;

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (Time.timeScale == 1)
			{
				Time.timeScale = 0;
				iTween.MoveTo(GameObject.Find("gui texture bottom left").gameObject, iTween.Hash("path",iTweenPath.GetPath("Move Bottom Left Texture In"), "time", 1, "ignoretimescale", true));
				iTween.MoveTo(GameObject.Find("gui texture bottom right").gameObject, iTween.Hash("path",iTweenPath.GetPath("Move Bottom Right Texture In"), "time", 1, "ignoretimescale", true));
				iTween.MoveTo(GameObject.Find("gui texture left").gameObject, iTween.Hash("path",iTweenPath.GetPath("Move Left Texture In"), "time", 1, "ignoretimescale", true));
				iTween.MoveTo(GameObject.Find("gui texture right").gameObject, iTween.Hash("path",iTweenPath.GetPath("Move Right Texture In"), "time", 1, "ignoretimescale", true));
				iTween.MoveTo(GameObject.Find("gui texture top").gameObject, iTween.Hash("path",iTweenPath.GetPath("Move Top Texture In"), "time", 1, "ignoretimescale", true));
			}
			else
			{
				Time.timeScale = 1;
				iTween.MoveTo(GameObject.Find("gui texture bottom left").gameObject, iTween.Hash("path",iTweenPath.GetPath("Move Bottom Left Texture Out"), "time", 1, "ignoretimescale", true));
				iTween.MoveTo(GameObject.Find("gui texture bottom right").gameObject, iTween.Hash("path",iTweenPath.GetPath("Move Bottom Right Texture Out"), "time", 1, "ignoretimescale", true));
				iTween.MoveTo(GameObject.Find("gui texture left").gameObject, iTween.Hash("path",iTweenPath.GetPath("Move Left Texture Out"), "time", 1, "ignoretimescale", true));
				iTween.MoveTo(GameObject.Find("gui texture right").gameObject, iTween.Hash("path",iTweenPath.GetPath("Move Right Texture Out"), "time", 1, "ignoretimescale", true));
				iTween.MoveTo(GameObject.Find("gui texture top").gameObject, iTween.Hash("path",iTweenPath.GetPath("Move Top Texture Out"), "time", 1, "ignoretimescale", true));			
			}
		}


	}

//	void OnGUI()
//	{
//		if (Time.timeScale == 0)
//		{
//			GUI.Label(new Rect(10,10,100,20), "Game Paused");
//			Debug.Log("Should have created GUI Label");
//		}
//	}


}
