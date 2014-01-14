using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
[AddComponentMenu("Image Effects/Custom/Heat Distortion")]
public class HeatDistortionCS : ImageEffectBase 
{
	public Texture2D heatTexture;
	void Start ()
	{
		// Disable if we don't support image effects
		if (!SystemInfo.supportsImageEffects) {
			enabled = false;
			return;
		}
		
		// Disable the image effect if the shader can't
		// run on the users graphics card
		if (!shader.isSupported)
			enabled = false;
		this.material.SetTexture("_NoiseTex", heatTexture);
	}
	//Called by camera to apply image effect
	void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		Graphics.Blit (source, destination, material);
	}
}