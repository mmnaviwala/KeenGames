using UnityEngine;
using System.Collections;

[AddComponentMenu("Scripts/Items/Flashlight")]
public class Flashlight : MonoBehaviour 
{
    public bool infiniteBattery = true;
    public float maxBatteryLife = 100;  //time in seconds
    //public float batteryLife = 100;    
	public int efficiency = 10;
    private Suit playerSuit;
    public Color32 c_battery;
    public GUIStyle lightGuiStyle, backGuiStyle;

    private Rect batteryMaxLifeRect, batteryLifeRect;

	// Use this for initialization
	void Start ()
    {
        playerSuit = this.transform.root.GetComponent<Suit>();
        float startX = Screen.height / 20;
        float barWidth = Screen.width / 3;
        float barHeight = barWidth / 10;
        
        batteryMaxLifeRect = new Rect(startX, Screen.height - 2 * barHeight, barWidth, barHeight);
        batteryLifeRect = batteryMaxLifeRect;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (infiniteBattery || playerSuit.batteryLife > 0)
        {
            if (Input.GetButtonDown(InputType.TOGGLE_FLASHLIGHT))
            {
                light.enabled = !light.enabled;
            }
            if (!infiniteBattery && light.enabled)
            {
				playerSuit.batteryLife -= Time.deltaTime / efficiency;
				batteryLifeRect.width = batteryMaxLifeRect.width * playerSuit.batteryLife / maxBatteryLife;
            }
        }
	}

    void OnGUI()
    {
		GUI.Box(batteryMaxLifeRect, string.Format("Battery Life: {0:f1}", playerSuit.batteryLife), backGuiStyle);
        GUI.color = new Color(1, 1, 1, .25f);
        GUI.Box(batteryLifeRect, "", lightGuiStyle);
    }

    public void Toggle()
    {
        light.enabled = !light.enabled;
    }

    /// <summary>
    /// Adds battery life; doesn't replace old battery.
    /// </summary>
    /// <param name="capacity"></param>
    public void InsertBattery(float capacity)
    {
		playerSuit.batteryLife += capacity;
		if (playerSuit.batteryLife > 100) 
			playerSuit.batteryLife = 100;
    }
}
