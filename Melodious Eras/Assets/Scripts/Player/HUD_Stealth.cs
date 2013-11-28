using UnityEngine;
using System.Collections;

public class HUD_Stealth : MonoBehaviour 
{
    public GUIStyle healthMaxStyle, healthCurrentStyle, flashlightLifeStyle, reticleStyle;

    private Rect healthBarMax, healthBarCurrent, flashlightLife, reticle;
    private float maxWidth;
    private bool aiming = false;

    private CharacterStats stats;
    private PlayerMovementBasic player;
    private Camera mainCam;

	// Use this for initialization
	void Start () {
        stats = this.GetComponent<CharacterStats>();
        player = this.GetComponent<PlayerMovementBasic>();
        mainCam = Camera.main;

        maxWidth = Screen.width / 3;
        healthBarMax = new Rect(Screen.width / 20, Screen.height / 20, maxWidth, maxWidth / 8);
        healthBarCurrent = new Rect(healthBarMax.xMin, healthBarMax.yMin, healthBarMax.width, healthBarMax.height);
        reticle = new Rect(Screen.width / 2, Screen.height / 2 - Screen.width / 20, Screen.width / 20, Screen.width / 20);
	}
	
	// Update is called once per frame
	void Update () 
    {
        healthBarCurrent.width = maxWidth * stats.health / 100;
        
	}

    void OnGUI()
    {
        if(aiming)
            GUI.Box(reticle, "X");

        GUI.Box(healthBarMax, "Health " + stats.health + "/100");
        GUI.color = new Color(1, 1, 1, .25f);
        GUI.Box(healthBarCurrent, "", healthCurrentStyle);

    }
}
