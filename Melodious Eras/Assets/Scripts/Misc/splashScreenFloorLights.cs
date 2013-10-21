using UnityEngine;
using System.Collections;

public class splashScreenFloorLights : MonoBehaviour {
	
	public float yTransform;
	public float xtransform;
	bool isCalledAlready = false;

	void Start () 
	{
		guiTexture.enabled = true;
		yTransform = Screen.height/1087F;
		xtransform = Screen.width/1377F;
		
		Debug.Log(Screen.width);
		Debug.Log(xtransform);
	}
	
	void Update()
	{
		if(yTransform < 0.79F)
		{
			transform.localPosition = new Vector3(xtransform, yTransform, 1);
			yTransform = yTransform + 0.002F;
		}
		else
		{
			if(!isCalledAlready)
			{
				afterAnimation();
				isCalledAlready = true;
			}
		}
	}
	
	void afterAnimation()
	{
		Application.LoadLevel("testTitleMenuQuads");
	}
	
}
