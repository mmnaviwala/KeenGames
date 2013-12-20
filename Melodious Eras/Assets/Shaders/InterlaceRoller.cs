using UnityEngine;
using System.Collections;

public class InterlaceRoller : MonoBehaviour 
{	
	public float scrollSpeed = .1f;
	private Color initialColor;
	private float flicker = 0.0f;
	
	void Start()
	{
		initialColor = this.renderer.material.GetColor("_TintColor");
	}
	
	void Update()
	{
		float offset = Time.time * scrollSpeed;
	    
		this.renderer.material.SetTextureOffset("_Interlace", new Vector2(0.0f, offset));

		float scaleY = Mathf.Sin(Time.time) * 0.2f;
		this.renderer.material.SetTextureScale("_Interlace", new Vector2(2.0f, scaleY + 1.6f));

		if(Random.value >= .7f)
		{
			flicker = Random.value + 0.2f;
			light.intensity = flicker;
			this.renderer.material.SetColor("_TintColor", new Color(initialColor.r * flicker, initialColor.g * flicker, initialColor.b * flicker));
		}
		if(audio)
			audio.pitch = scaleY + 1;
	}
}
