using UnityEngine;
using System.Collections;

public class splashScreenFloorLights : MonoBehaviour {
	
	public float yTransform = 0.55F;
	bool isCalledAlready = false;

	void Start () 
	{
		guiTexture.enabled = true;
	}
	
	void Update()
	{
		Debug.Log(yTransform);

		if(yTransform < 0.79F)
		{
			transform.localPosition = new Vector3(0.635F, yTransform, 1);
			yTransform = yTransform + 0.001F;
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
