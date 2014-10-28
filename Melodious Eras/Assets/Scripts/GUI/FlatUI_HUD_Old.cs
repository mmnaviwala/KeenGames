using UnityEngine;
using System.Collections;

[RequireComponent (typeof (MeshRenderer))]
[RequireComponent (typeof (MeshFilter))]

public class FlatUI_HUD_Old : MonoBehaviour {
	
	public Texture bottomTexture, topTexture, barTexture;
	private Rect rectSize;
	private float currentHealth = 0.0f;
	private MeshRenderer mR;
	private float xx, yy;
	private GUIStyle smallFont;
	public GUIStyle bigFont;
	public string textToDisplay;
	public float startingNumber = 100;
	public float currentNumber;
	
	void Start ()
	{
		xx = Screen.width / 10;
		yy = Screen.height / 10;
		currentHealth = startingNumber;
		
		rectSize = new Rect(xx * .3f, yy * 8.1f, xx+10, xx+10);

		bigFont.fontSize = System.Convert.ToInt32(Screen.height * 0.06f);
		smallFont = new GUIStyle();
		smallFont.fontSize = System.Convert.ToInt32(Screen.height * 0.03f);
		smallFont.alignment = TextAnchor.MiddleCenter;
		smallFont.normal.textColor = bigFont.normal.textColor;
		smallFont.font = bigFont.font;
	}
	
	void Update ()
	{
		if(currentHealth < Screen.height*1.8)
			 currentHealth += 1f;
	}
	
	void OnGUI ()
	{
		GUI.DrawTexture(rectSize, bottomTexture);

		if(currentHealth < Screen.height*1.8)
			gameObject.GetComponent<Renderer>().material.SetFloat("_Cutoff", Mathf.InverseLerp(0, Screen.width, currentHealth));
		Graphics.DrawTexture(rectSize, barTexture, gameObject.GetComponent<Renderer>().material);

		GUI.DrawTexture(rectSize, topTexture);
		
		GUI.Label(new Rect(xx * .3f, yy * 7.8f, xx+10, xx+10), System.Convert.ToString(currentHealth), bigFont);
		GUI.Label(new Rect(xx * .3f, yy * 8.4f, xx+10, xx+10), textToDisplay, smallFont);
	}
}