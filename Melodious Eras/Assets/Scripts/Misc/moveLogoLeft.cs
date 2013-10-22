using UnityEngine;
using System.Collections;

public class moveLogoLeft : MonoBehaviour {
	
	public float xTransform;
	
	void Start()
	{
		xTransform = transform.localPosition.x;
		guiTexture.enabled = true;
	}

	
	// Update is called once per frame
	void Update () 
	{
		if(xTransform > -0.3F)
		{
			transform.localPosition = new Vector3(xTransform, 0.51F, 1);
			xTransform = xTransform - 0.01F;
		}
	}
}
