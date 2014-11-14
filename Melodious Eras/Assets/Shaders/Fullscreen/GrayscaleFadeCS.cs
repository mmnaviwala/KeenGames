using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("Image Effects/Custom/Grayscale Fade")]
public class GrayscaleFadeCS : ImageEffectBase {
	[SerializeField] Texture textureRamp;
    [SerializeField] float   rampOffset;

    float effectAmount = 1;
    CharacterStats playerStats;

    void Start()
    {
        var playerTransform = GameObject.FindGameObjectWithTag(Tags.PLAYER).transform;
        playerStats = playerTransform.GetComponent<CharacterStats>();
    }
	
	// Called by camera to apply image effect
	void OnRenderImage (RenderTexture source, RenderTexture destination) 
    {
        //start grayscale effect when under 50 health
        if (playerStats && !playerStats.isDead && playerStats.health < 50f)
            effectAmount = (50f - playerStats.health) / 50f;
        else
            effectAmount = 0f;

        material.SetTexture("_RampTex", textureRamp);
        material.SetFloat("_RampOffset", rampOffset);
        material.SetFloat("_EffectAmount", effectAmount);
        Graphics.Blit(source, destination, material);
	}
}