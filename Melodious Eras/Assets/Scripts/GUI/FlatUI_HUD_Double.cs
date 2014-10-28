using UnityEngine;
using System.Collections;

//[RequireComponent (typeof (MeshRenderer))]

public class FlatUI_HUD_Double : MonoBehaviour {
	
	public Texture bottomTexture, topTexture, outerBarTexture, innerBarTexture;
	private Rect innerRect, outerRect, firstLabel, secondLabel;
	private float currentHealth1 = 0.0f;
	private float currentHealth2 = 0.0f;
	private float xx, yy;
	public GUIStyle smallFont1, smallFont2;
	private string textToDisplay1, textToDisplay2;
	public float currentNumber1 = 100;
	public float currentNumber2 = 100;
    private float maxNumber1 = 100;
    private float maxNumber2 = 100;

	public enum Position { Position1, Position3 }
	public enum BarType { HealthArmor, Ammo }
	public Position positions;
	public BarType bartype;

	private PlayerStats stats;
	private Gun weapon;
    private Suit suit;
    private Material outerMat, innerMat;
	
	void Start ()
	{
		stats = GameObject.FindGameObjectWithTag(Tags.PLAYER).GetComponent<PlayerStats>();
		suit = stats.GetComponent<Suit>();
		weapon = stats.equippedWeapon as Gun;

        outerMat = GetComponent<Renderer>().materials[0];
        innerMat = GetComponent<Renderer>().materials[1];


		xx = Screen.width / 10;
		yy = Screen.height / 10;

		textToDisplay1 = "Health: ";
		textToDisplay2 = "Armor: ";
		
		smallFont1.fontSize = System.Convert.ToInt32(Screen.height * 0.023f);
		smallFont2.fontSize = System.Convert.ToInt32(Screen.height * 0.023f);

		switch(positions)
		{
		case Position.Position1:
			innerRect = new Rect(xx * 0.30f, yy * 8.12f, xx+10, xx+10);
			outerRect = new Rect(xx * 0.22f, yy * 8.0f, xx+30, xx+30);
			
			firstLabel = new Rect(xx * 0.22f, yy * 7.8f, xx+30, xx+30);
			secondLabel = new Rect(xx * 0.22f, yy * 8.2f, xx+30, xx+30);
			break;
		case Position.Position3:
			innerRect = new Rect(xx * 7.55f, yy * 8.13f, xx+10, xx+10);
			outerRect = new Rect(xx * 7.47f, yy * 8.0f, xx+30, xx+30);
			
			firstLabel = new Rect(xx * 7.47f, yy * 7.8f, xx+30, xx+30);
			secondLabel = new Rect(xx * 7.47f, yy * 8.2f, xx+30, xx+30);
			break;
		}

		switch(bartype)
		{
		case BarType.Ammo:
			textToDisplay1 = "Clips: ";
			textToDisplay2 = "Bullets: ";
			break;
		case BarType.HealthArmor:
			textToDisplay1 = "Health: ";
			textToDisplay2 = "Armor: ";
			break;
		}

	}
	
	void Update ()
	{
		switch(bartype)
		{
		case BarType.Ammo:
			currentNumber1 = weapon.ammoInClip;
			currentNumber2 = weapon.extraAmmo;
            maxNumber1 = weapon.clipSize;
            maxNumber2 = weapon.maxAmmo;
			break;
		case BarType.HealthArmor:
			currentNumber1 = stats.health;
			currentNumber2 = suit.armor;
			break;
		}
	}
	
	void OnGUI ()
	{
		GUI.DrawTexture(outerRect, bottomTexture);
		
		outerMat.SetFloat("_Cutoff", Mathf.InverseLerp(0, maxNumber1, currentNumber1));
		Graphics.DrawTexture(outerRect, outerBarTexture, outerMat);
		
		GUI.DrawTexture(outerRect, topTexture);

		// -----------------------------------------------------

		GUI.DrawTexture(innerRect, bottomTexture);
		
		innerMat.SetFloat("_Cutoff", Mathf.InverseLerp(0, maxNumber2, currentNumber2));
		Graphics.DrawTexture(innerRect, innerBarTexture, innerMat);
		
		GUI.DrawTexture(innerRect, topTexture);

		GUI.Label(firstLabel, textToDisplay1 + string.Format("{0:f1}", currentNumber1), smallFont1);
		GUI.Label(secondLabel, textToDisplay2 + string.Format("{0:f1}", currentNumber2), smallFont2);
	}
}

