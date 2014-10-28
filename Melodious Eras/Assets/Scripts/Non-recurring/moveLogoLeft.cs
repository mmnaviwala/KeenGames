using UnityEngine;
using System.Collections;

[AddComponentMenu("Scripts/Non-recurring/Move Logo Left")]
public class moveLogoLeft : MonoBehaviour {
	
	public float xTransform;
	private bool delayComplete = false;
	public float delayTimeBefore = 0.5f;
	
	void Start()
	{
		xTransform = transform.localPosition.x;
		GetComponent<GUITexture>().enabled = true;
	}

	
	// Update is called once per frame
	void Update () 
	{
		delayTimeBefore -= Time.deltaTime;
		if (delayTimeBefore <= 0f)
			delayComplete = true;

		if(delayComplete)
		{
			if(xTransform > -0.3F)
			{
				transform.localPosition = new Vector3(xTransform, 0.51F, 1);
				xTransform = xTransform - 0.01F;
			}
		}
	}
}
