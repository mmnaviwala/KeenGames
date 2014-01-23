using UnityEngine;
using System.Collections;

[RequireComponent (typeof (MeshRenderer))]

public class FlatUI_HUD : MonoBehaviour {
	
	public Texture bottomTexture, topTexture, barTexture;
	private Rect rectSize, numberLabelSize, stringLabelSize;
	private float currentHealth = 0.0f;
	private float xx, yy;
	private GUIStyle smallFont;
	public GUIStyle bigFont;
	private string textToDisplay;
	public float currentNumber = 100;
	public enum Position { Position1, Position2, Position3, Position4 }
	public enum BarType { Health, Armor, Ammo, Battery }
	public Position positions;
	public BarType bartype;


	void Start ()
	{
		xx = Screen.width / 10;
		yy = Screen.height / 10;

		switch(positions)
		{
		case Position.Position1:
			rectSize = new Rect(xx * .3f, yy * 8.1f, xx+10, xx+10);
			numberLabelSize = new Rect(xx * .3f, yy * 7.8f, xx+10, xx+10);
			stringLabelSize = new Rect(xx * .3f, yy * 8.4f, xx+10, xx+10);
			break;
		case Position.Position2:
			rectSize = new Rect(xx * 1.55f, yy * 8.1f, xx+10, xx+10);
			numberLabelSize = new Rect(xx * 1.55f, yy * 7.8f, xx+10, xx+10);
			stringLabelSize = new Rect(xx * 1.55f, yy * 8.4f, xx+10, xx+10);
			break;
		case Position.Position3:
			rectSize = new Rect(xx * 7.55f, yy * 8.1f, xx+10, xx+10);
			numberLabelSize = new Rect(xx * 7.55f, yy * 7.8f, xx+10, xx+10);
			stringLabelSize = new Rect(xx * 7.55f, yy * 8.4f, xx+10, xx+10);
			break;
		case Position.Position4:
			rectSize = new Rect(xx * 8.80f, yy * 8.1f, xx+10, xx+10);
			numberLabelSize = new Rect(xx * 8.80f, yy * 7.8f, xx+10, xx+10);
			stringLabelSize = new Rect(xx * 8.80f, yy * 8.4f, xx+10, xx+10);
			break;
		}

		switch(bartype)
		{
		case BarType.Health:
			textToDisplay = "Health";
			break;
		case BarType.Armor:
			textToDisplay = "Armor";
			break;
		case BarType.Ammo:
			textToDisplay = "Ammo";
			break;
		case BarType.Battery:
			textToDisplay = "Battery";
			break;
		}

		bigFont.fontSize = System.Convert.ToInt32(Screen.height * 0.05f);
		smallFont = new GUIStyle();
		smallFont.fontSize = System.Convert.ToInt32(Screen.height * 0.03f);
		smallFont.alignment = TextAnchor.MiddleCenter;
		smallFont.normal.textColor = bigFont.normal.textColor;
		smallFont.font = bigFont.font;
	}
	
	void Update ()
	{
		switch(bartype)
		{
		case BarType.Health:
//			currentNumber = 100;
			currentNumber = GameObject.Find("player_FreeCharacter").GetComponent<PlayerStats>().health;
			break;
		case BarType.Armor:
//			currentNumber = 50;
			currentNumber = GameObject.Find("player_FreeCharacter").GetComponent<Suit>().armor;
			break;
		case BarType.Ammo:
			currentNumber = 20;
			break;
		case BarType.Battery:
			currentNumber = GameObject.Find("player_FreeCharacter").GetComponent<Suit>().batteryLife;
//			currentNumber = 80;
			break;
		}

		if(currentNumber > -1)
		{
			currentHealth = (Screen.height*1.8f) - ((Screen.height*1.8f)*(currentNumber/100f)) + 1;
			Debug.Log(bartype + " " + currentHealth + " " + currentNumber);
		}
	}
	
	void OnGUI ()
	{
		GUI.DrawTexture(rectSize, bottomTexture);

		gameObject.renderer.material.SetFloat("_Cutoff", Mathf.InverseLerp(0, Screen.width, currentHealth));
		Graphics.DrawTexture(rectSize, barTexture, gameObject.renderer.material);

		GUI.DrawTexture(rectSize, topTexture);
		
		GUI.Label(numberLabelSize, string.Format("{0:f1}", currentNumber), bigFont);
		GUI.Label(stringLabelSize, textToDisplay, smallFont);
	}
}