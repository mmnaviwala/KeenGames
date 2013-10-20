using UnityEngine;
using System.Collections;

public class SplashSceenLogoAnimation : MonoBehaviour {
	
	void Start () 
	{
        StartCoroutine(waitAndGoToNextLevel());
	}
	
	IEnumerator waitAndGoToNextLevel()
	{
		yield return new WaitForSeconds(2.0F);
		Application.LoadLevel("testTitleMenuQuads");
	}
}
