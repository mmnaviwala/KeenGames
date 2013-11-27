using UnityEngine;
using System.Collections;

public class HUD_Stealth : MonoBehaviour 
{
    CharacterStats stats;
    private Rect healthBarMax, healthBarCurrent;
    public GUIStyle healthMaxStyle, healthCurrentStyle;
    float maxWidth;
    Color32 plain, health;
	// Use this for initialization
	void Start () {
        stats = this.GetComponent<CharacterStats>();
        maxWidth = Screen.width / 3;
        healthBarMax = new Rect(Screen.width / 20, Screen.height / 20, maxWidth, maxWidth / 8);
        healthBarCurrent = new Rect(healthBarMax.xMin, healthBarMax.yMin, healthBarMax.width, healthBarMax.height);
        plain = new Color32(255, 255, 255, 128);
        health = new Color32(200, 25, 25, 255);
	}
	
	// Update is called once per frame
	void Update () 
    {
        healthBarCurrent.width = maxWidth * stats.health / 100;
	}

    void OnGUI()
    {
        GUI.Box(healthBarMax, "Health " + stats.health + "/100", healthMaxStyle);
        GUI.color = health;
        GUI.Box(healthBarCurrent, "", healthCurrentStyle);
    }
}
