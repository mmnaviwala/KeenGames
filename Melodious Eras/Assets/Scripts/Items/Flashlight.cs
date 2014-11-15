using UnityEngine;
using System.Collections;

[AddComponentMenu("Scripts/Items/Flashlight")]
public class Flashlight : MonoBehaviour
{
    delegate void FlashlightState();


    public bool infiniteBattery = true;
    //public float maxBatteryLife = 100;  //time in seconds
    //public float batteryLife = 100;    
	public int efficiency = 10;
    Suit playerSuit;

    private Rect batteryMaxLifeRect, batteryLifeRect;
    private LightShafts lightShafts;

    Light mainLight;
    Light ambientLight;
    FlashlightState[] flashlight_state;
    int state_index = 0;

	void Awake()
	{
		lightShafts = this.GetComponent<LightShafts>();
        mainLight = this.GetComponent<Light>();
        ambientLight = gameObject.GetComponentInChildrenOnly<Light>();

        flashlight_state = new FlashlightState[]{FlashlightOff, FlashlightOn};
	}
	// Use this for initialization
	void Start ()
    {
        playerSuit = GetComponentInParent<Suit>();
        float startX = Screen.height / 20;
        float barWidth = Screen.width / 3;
        float barHeight = barWidth / 10;
        
        batteryMaxLifeRect = new Rect(startX, Screen.height - 2 * barHeight, barWidth, barHeight);
        batteryLifeRect = batteryMaxLifeRect;
        if (lightShafts)
        {
            lightShafts.enabled = mainLight.enabled;
            lightShafts.m_Brightness = Environment.globalDustLevel;
        }
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetButtonDown(InputType.TOGGLE_FLASHLIGHT))
            Toggle();
        flashlight_state[state_index]();
	}

    void FlashlightOff()
    {

    }

    void FlashlightOn()
    {
        if(!infiniteBattery)
        {
            playerSuit.batteryLife -= Time.deltaTime / efficiency;
            batteryLifeRect.width = batteryMaxLifeRect.width * playerSuit.batteryLife / playerSuit.maxBatteryLife;

            if (playerSuit.batteryLife <= 0) //turn flashlight back off
                Toggle();
        }
    }

    public void Toggle()
    {
        mainLight.enabled = !mainLight.enabled;
        ambientLight.enabled = !ambientLight.enabled;
        if (lightShafts)
            lightShafts.enabled = mainLight.enabled;

        state_index = (state_index + 1) % 2;
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
