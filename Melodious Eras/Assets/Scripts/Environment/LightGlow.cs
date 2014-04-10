using UnityEngine;
using System.Collections;

[AddComponentMenu("Scripts/Environment/Light Glow")]
public class LightGlow : MonoBehaviour 
{
    public float intensity = 1;
    public float variation = 1;
    private float minIntensity;
	// Use this for initialization
	void Start () 
    {
        minIntensity = intensity - variation / 2;
	}
	
	// Update is called once per frame
	void Update () 
    {
        this.light.intensity = Mathf.PerlinNoise(Time.time / 2, 0) * variation + minIntensity;
	}
}
