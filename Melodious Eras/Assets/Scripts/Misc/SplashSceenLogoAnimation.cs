using UnityEngine;
using System.Collections;

public class SplashSceenLogoAnimation : MonoBehaviour {
	
	public float yTransform = 0.55F;
	
	void Start () 
	{
		guiTexture.enabled = true;
		
		StartCoroutine(animateAndGoToNextLevel());
	}
	
	IEnumerator animateAndGoToNextLevel()
	{		
		if(yTransform == 0.788F)
		{
			Application.LoadLevel("testTitleMenuQuads");
		}
		
		yield return null;
	}
}
