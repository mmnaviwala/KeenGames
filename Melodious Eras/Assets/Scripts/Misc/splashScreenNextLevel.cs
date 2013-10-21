using UnityEngine;
using System.Collections;

public class splashScreenNextLevel : MonoBehaviour {

	void Start () 
	{
		StartCoroutine(WaitAndGoToNextLevel());
	}
	
	IEnumerator WaitAndGoToNextLevel()
	{
		Application.LoadLevel("testTitleMenuQuads");
		yield return null;
	}
}
