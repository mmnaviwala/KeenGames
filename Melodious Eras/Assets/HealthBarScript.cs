using UnityEngine;
using System.Collections;

public class HealthBarScript : MonoBehaviour {

	private GUITexture healthBar;
	private Vector3 healthScale;
	private Rect tempPixelInset;
	public int health = 100;		// Tweak this value in the inspector

	void Awake () 
	{
		healthBar = gameObject.guiTexture;
		healthScale = healthBar.transform.localScale;
		Debug.Log("Health Scale:");
		Debug.Log(healthScale);
		Debug.Log("Pixel Inset");
		Debug.Log(healthBar.pixelInset);
	}
	
	void Update () 
	{
		healthBar.color = Color.Lerp(Color.green, Color.red, 1 - health * 0.01f);

		tempPixelInset = healthBar.pixelInset;
		tempPixelInset.width = health;
		healthBar.pixelInset = tempPixelInset;
	
	}
}
