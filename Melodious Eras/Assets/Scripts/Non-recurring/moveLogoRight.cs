using UnityEngine;
using System.Collections;

[AddComponentMenu("Scripts/Non-recurring/Move Logo Right")]
public class moveLogoRight : MonoBehaviour {
	
	public float xTransform;
	AudioSource[] music;
	private bool delayComplete = false;
	private bool musicPlayed = false;	
	public float delayTimeBefore = 0.5f;
	
	void Start()
	{
		xTransform = transform.localPosition.x;
		GetComponent<GUITexture>().enabled = true;
		music = Camera.main.GetComponents<AudioSource>();
	}

	
	// Update is called once per frame
	void Update () 
	{

		delayTimeBefore -= Time.deltaTime;
		if (delayTimeBefore <= 0f)
			delayComplete = true;

		if(delayComplete && !musicPlayed)
		{
			// Hide gui texture
			//GameObject.Find("5thFloorStudiosTexture").GetComponent<GUITexture>().enabled = false;
			music[2].Play();
			musicPlayed = true;
		}


		if(delayComplete)
		{
			if(xTransform < 1.3F)
			{
				transform.localPosition = new Vector3(xTransform, 0.51F, 1);
				xTransform = xTransform + 0.01F;
			}
		}
	}
}
