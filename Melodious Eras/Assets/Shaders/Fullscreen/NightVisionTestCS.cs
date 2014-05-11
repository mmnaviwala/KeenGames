using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
[AddComponentMenu("Image Effects/Custom/Night Vision Test")]
public class NightVisionTestCS : ImageEffectBase 
{
	GameController gameController;
	public Color NV_ambientLight = new Color(.235f, .235f, .235f, 1);
	public Vector4 adjustColorRGBA = new Vector4(-.10f, .010f, .010f, 0.0f);

	private YieldInstruction waitforframe = new WaitForEndOfFrame();

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
		material.SetVector("_ColorAdjustment", adjustColorRGBA);
		Graphics.Blit(source, destination, material);
	}

	void OnEnable()
	{
		this.StartCoroutine(this.TurnOnGoggles());
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
        if (!gameController)
            gameController = GameObject.FindGameObjectWithTag(Tags.GAME_CONTROLLER).GetComponent<GameController>();
        Color targetColor = gameController.ambientLight + NV_ambientLight;
        while (RenderSettings.ambientLight.r < targetColor.r - .01f)
        {
            RenderSettings.ambientLight = Color.Lerp(RenderSettings.ambientLight, targetColor, 2 * Time.deltaTime);
            yield return waitforframe;
        }
        Debug.Log("Goggles on");
        RenderSettings.ambientLight = targetColor;
	}
}
