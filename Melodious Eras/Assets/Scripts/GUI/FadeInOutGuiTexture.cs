using UnityEngine;
using System.Collections;

public class FadeInOutGuiTexture : MonoBehaviour {

	public float fadeDuration = 3.0f; 
	public float waitTimeBeforeFade = 5.0f; 
	public bool shouldFadeOut;
	public bool destroyAfterFade;
	private bool waitBeforeFadeComplete = false;

	private void Start () 
	{
		guiTexture.color = new Color(guiTexture.color.r, 
		                             guiTexture.color.g, 
		                             guiTexture.color.b, 0);
		StartCoroutine(StartFading());
	}

	
	private IEnumerator StartFading()
	{
		yield return new WaitForSeconds(waitTimeBeforeFade);

		yield return StartCoroutine(Fade(0.0f, 1.0f, fadeDuration));
		
		if(shouldFadeOut)
			yield return StartCoroutine(Fade(1.0f, 0.0f, fadeDuration));
		
		if (destroyAfterFade)
			Destroy(gameObject);

	}

	
	private IEnumerator Fade (float startLevel, float endLevel, float time) 
	{ 
		
		float speed = 1.0f/time; 

		for (float t = 0.0f; t < 1.0; t += Time.deltaTime*speed) 	
		{ 	
			float a = Mathf.Lerp(startLevel, endLevel, t);
			guiTexture.color = new Color(guiTexture.color.r, 
			                             guiTexture.color.g, 
			                             guiTexture.color.b, a);
			yield return 0;
			
		}
		
	}

}
