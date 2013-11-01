using UnityEngine;
using System.Collections;

public class moveLogoRight : MonoBehaviour {
	
	public float xTransform;
	AudioSource[] music;
	
	void Start()
	{
		xTransform = transform.localPosition.x;
		guiTexture.enabled = true;
		music = Camera.main.GetComponents<AudioSource>();
		music[2].Play();
	}

	
	// Update is called once per frame
	void Update () 
	{
		if(xTransform < 1.3F)
		{
			transform.localPosition = new Vector3(xTransform, 0.51F, 1);
			xTransform = xTransform + 0.01F;
		}
	}
}
