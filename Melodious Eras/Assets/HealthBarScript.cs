using UnityEngine;
using System.Collections;

public class HealthBarScript : MonoBehaviour {

	private GUITexture healthBar;
	private Vector3 healthScale;
	private Rect tempPixelInset;
	public int health = 100;

	// Use this for initialization
	void Awake () 
	{
		healthBar = gameObject.guiTexture;
		healthScale = healthBar.transform.localScale;
		Debug.Log("Health Scale:");
		Debug.Log(healthScale);
		Debug.Log("Pixel Inset");
		Debug.Log(healthBar.pixelInset);
	}
	
	// Update is called once per frame
	void Update () 
	{
		// Set the health bar's colour to proportion of the way between green and red based on the player's health.
		healthBar.color = Color.Lerp(Color.green, Color.red, 1 - health * 0.01f);
		
		// Set the scale of the health bar to be proportional to the player's health.
		// healthBar.transform.localScale = new Vector3(healthScale.x * health * 0.01f, 1, 1);
		tempPixelInset = healthBar.pixelInset;
		tempPixelInset.width = health;
		healthBar.pixelInset = tempPixelInset;
	
	}
}
