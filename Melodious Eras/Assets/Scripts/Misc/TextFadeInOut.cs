using System.Collections;
using UnityEngine;

[AddComponentMenu("Scripts/Misc/Text Fade In Out")]
public class TextFadeInOut : MonoBehaviour
{
	public float fadeDuration = 3.0f; 
	public bool shouldFadeOut;
	public bool destroyAfterFade;

	private void Start () 
	{	
		StartCoroutine(StartFading());
	}
	
	
	
	private IEnumerator StartFading()
	{
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
			
			GetComponent<GUIText>().font.material.color = new Color(GetComponent<GUIText>().font.material.color.r, 
			                                        
			                                        GetComponent<GUIText>().font.material.color.g, 
			                                        
			                                        GetComponent<GUIText>().font.material.color.b, a);
			
			yield return 0;
			
		}
		
	}
	
}