using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
[AddComponentMenu("Image Effects/Custom/Night Vision Test")]
public class NightVisionTestCS : ImageEffectBase 
{

	//Called by camera to apply image effect
	void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		Graphics.Blit(source, destination, material);
	}
}
