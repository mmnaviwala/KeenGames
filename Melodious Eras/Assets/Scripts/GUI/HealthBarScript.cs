using UnityEngine;
using System.Collections;

public class HealthBarScript : MonoBehaviour {

	private GUITexture healthBar;
	private Vector3 healthScale;
	private Rect tempPixelInset;
	public int health = 100;		// Tweak this value in the inspector

//	private Color startColor = new Color(255/255.0f, 170/255.0f, 0/255.0f);
//	private Color endColor = new Color(18/255.0f, 64/255.0f, 171/255.0f);

//	private Color startColor = new Color(189/255.0f, 201/255.0f, 93/255.0f);
//	private Color endColor = new Color(88/255.0f, 77/255.0f, 185/255.0f);

//	private Color startColor = new Color(73/255.0f, 176/255.0f, 189/255.0f);
//	private Color endColor = new Color(170/255.0f, 69/255.0f, 57/255.0f);

	private Color startColor = Color.green;
	private Color endColor = Color.red;

//	private Color startColor = new Color();
//	private Color endColor = new Color();

	void Awake () 
	{
		healthBar = gameObject.GetComponent<GUITexture>();
		healthScale = healthBar.transform.localScale;
	}
	
	void Update () 
	{
		healthBar.color = Color.Lerp(startColor, endColor, 1 - health * 0.01f);

//		healthBar.transform.localScale = new Vector3(healthScale.x * health * 0.01f, healthScale.y, healthScale.z);
//
//		Debug.Log(healthBar.transform.localScale);

		tempPixelInset = healthBar.pixelInset;
		tempPixelInset.width = health;
		healthBar.pixelInset = tempPixelInset;	
	}
}
