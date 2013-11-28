using UnityEngine;
using System.Collections;

public class Flashlight : MonoBehaviour 
{
    public bool infiniteBattery = true;
    public float maxBatteryLife = 100;  //time in seconds
    public float batteryLife = 100;     //
    public Color32 c_battery;
    public GUIStyle lightGuiStyle, backGuiStyle;

    private Rect batteryMaxLifeRect, batteryLifeRect;

	// Use this for initialization
	void Start ()
    {
        float startX = Screen.height / 20;
        float barWidth = Screen.width / 3;
        float barHeight = barWidth / 10;
        
        batteryMaxLifeRect = new Rect(startX, Screen.height - 2 * barHeight, barWidth, barHeight);
        batteryLifeRect = batteryMaxLifeRect;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (infiniteBattery || batteryLife > 0)
        {
            if (Input.GetButtonDown(InputType.TOGGLE_FLASHLIGHT))
            {
                light.enabled = !light.enabled;
            }
            if (!infiniteBattery && light.enabled)
            {
                batteryLife -= Time.deltaTime;
                batteryLifeRect.width = batteryMaxLifeRect.width * batteryLife / maxBatteryLife;
            }
        }
	}

    void OnGUI()
    {
        GUI.Box(batteryMaxLifeRect, string.Format("Battery Life: {0:f1}", batteryLife), backGuiStyle);
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
        batteryLife += capacity;
        if (batteryLife > 100) 
            batteryLife = 100;
    }
}
