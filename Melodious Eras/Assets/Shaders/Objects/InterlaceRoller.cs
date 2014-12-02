using UnityEngine;
using System.Collections;

public class InterlaceRoller : MonoBehaviour 
{	
	public float scrollSpeed = .1f;
    public Light _Light;
    public int screenMatIndex = 0;
	public bool flickering = false;
    public Material screenMat;

	private Color initialColor;
	private float flicker = 0.0f;
    Renderer _renderer;
    AudioSource _audio;

    private delegate void RenderEffects(float scaleY);
    private RenderEffects effects;
	
	void Start()
	{
        //initialColor = this.renderer.materials[screenMatIndex].GetColor("_TintColor");
        _renderer = GetComponent<Renderer>();
        _audio = GetComponent<AudioSource>();
        screenMat = _renderer.materials[screenMatIndex];
        initialColor = screenMat.GetColor("_TintColor");

        if (_Light == null && this.GetComponent<Light>() != null)
            _Light = this.GetComponent<Light>();
	}
	
	void Update()
	{
		float offset = Time.time * scrollSpeed;
        screenMat.SetTextureOffset("_Interlace", new Vector2(0.0f, offset));

		float scaleY = Mathf.Sin(Time.time) * 0.2f;
        screenMat.SetTextureScale("_Interlace", new Vector2(2.0f, scaleY + 1.6f));

		if(flickering && Random.value >= .7f)
		{
			flicker = Random.value + 0.2f;
            screenMat.SetColor("_TintColor", new Color(initialColor.r * flicker, initialColor.g * flicker, initialColor.b * flicker));
            if(_Light != null)
                _Light.intensity = flicker;
		}
		if(_audio)
			_audio.pitch = scaleY + 1;
	}
}
