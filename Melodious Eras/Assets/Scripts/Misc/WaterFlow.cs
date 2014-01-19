using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class WaterFlow : MonoBehaviour 
{
	public float flowSpeed = .1f;
	public Vector2 flowDirectionXZ;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(renderer.material.shader.isSupported)
			Camera.main.depthTextureMode |= DepthTextureMode.Depth;

		float offset = Time.time * flowSpeed;
		renderer.material.SetTextureOffset ("_Normalmap", flowDirectionXZ * offset);
		
		renderer.material.SetTextureOffset ("_MainTex", flowDirectionXZ * offset);
	}
}
/*
 * // Scroll main texture based on time

//@script ExecuteInEditMode

var scrollSpeed = 0.1;

function Update () 
{
	if(renderer.material.shader.isSupported)
		Camera.main.depthTextureMode |= DepthTextureMode.Depth;
	 
    var offset = Time.time * scrollSpeed;
    renderer.material.SetTextureOffset ("_BumpMap", Vector2(offset/-7.0, offset));
    
    renderer.material.SetTextureOffset ("_MainTex", Vector2(offset/10.0, offset));
}
*/