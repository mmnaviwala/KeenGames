using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
[AddComponentMenu("Image Effects/Custom/Night Vision Test")]
public class NightVisionTestCS : ImageEffectBase 
{
	GameController gameController;
	public Color NV_ambientLight = new Color(.235f, .235f, .235f, 1);
	void Start()
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

		gameController = GameObject.FindGameObjectWithTag(Tags.GAME_CONTROLLER).GetComponent<GameController>();
	}
	//Called by camera to apply image effect
	void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		Graphics.Blit(source, destination, material);
	}

	void OnEnable()
	{
		Debug.Log("Night vision enabled");
		StartCoroutine(TurnOnGoggles());
	}
	void OnDisable()
	{
		if( material ) {
			DestroyImmediate( material );
		}
		this.StopAllCoroutines();
		RenderSettings.ambientLight = gameController.ambientLight;
	}

	IEnumerator TurnOnGoggles()
	{
		while(RenderSettings.ambientLight.r  < NV_ambientLight.r - .01f)
		{
			RenderSettings.ambientLight = Color.Lerp(RenderSettings.ambientLight, NV_ambientLight, 2 * Time.deltaTime);
			yield return new WaitForEndOfFrame();
		}
		Debug.Log("Goggles on");
		RenderSettings.ambientLight = NV_ambientLight;
	}
}
