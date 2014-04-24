using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshRenderer))]

public class FlatUI_HUD : MonoBehaviour
{

    public Texture bottomTexture, topTexture, barTexture;
    private Rect rectSize, numberLabelSize, stringLabelSize;
    private float xx, yy;
    private GUIStyle smallFont;
    public GUIStyle bigFont;
    private string textToDisplay;
    public float currentNumber = 100;
    public enum Position { Position1, Position2, Position3, Position4 }
    public enum BarType { Health, Armor, Ammo, Battery }
    public Position positions;
    public BarType bartype;

    public CharacterStats playerStats;
    public Suit playerSuit;
    public Weapon playerWeapon;

    void Start()
    {
        this.Initialize();
    }

    void Update()
    {
        switch (bartype)
        {
            case BarType.Health:
                currentNumber = playerStats.health;
                break;
            case BarType.Armor:
                currentNumber = playerSuit.armor;
                break;
            case BarType.Ammo:
                currentNumber = playerWeapon.ammoInClip + playerWeapon.extraAmmo;
                break;
            case BarType.Battery:
                currentNumber = playerSuit.batteryLife;
                break;
        }
    }

    void OnGUI()
    {
        GUI.DrawTexture(rectSize, bottomTexture);
        renderer.material.SetFloat("_Cutoff", Mathf.Clamp(Mathf.InverseLerp(0f, 100, 100 - currentNumber), .01f, 1));
        Graphics.DrawTexture(rectSize, barTexture, gameObject.renderer.material);
        GUI.DrawTexture(rectSize, topTexture);

        GUI.Label(numberLabelSize, string.Format("{0:f1}", currentNumber), bigFont);
        GUI.Label(stringLabelSize, textToDisplay, smallFont);
    }

    /// <summary>
    /// Common initializations
    /// </summary>
    protected void Initialize()
    {
        this.playerStats = GameObject.FindGameObjectWithTag(Tags.PLAYER).GetComponent<CharacterStats>();
        this.playerSuit = GameObject.FindGameObjectWithTag(Tags.PLAYER).GetComponent<Suit>();
        this.playerWeapon = GameObject.FindGameObjectWithTag(Tags.PLAYER).GetComponent<PlayerStats>().equippedWeapon;

        xx = Screen.width / 10;
        yy = Screen.height / 10;

        switch (positions)
        {
            case Position.Position1:
                rectSize = new Rect(xx * .3f, yy * 8.1f, xx + 10, xx + 10);
                numberLabelSize = new Rect(xx * .3f, yy * 7.8f, xx + 10, xx + 10);
                stringLabelSize = new Rect(xx * .3f, yy * 8.4f, xx + 10, xx + 10);
                break;
            case Position.Position2:
                rectSize = new Rect(xx * 1.55f, yy * 8.1f, xx + 10, xx + 10);
                numberLabelSize = new Rect(xx * 1.55f, yy * 7.8f, xx + 10, xx + 10);
                stringLabelSize = new Rect(xx * 1.55f, yy * 8.4f, xx + 10, xx + 10);
                break;
            case Position.Position3:
                rectSize = new Rect(xx * 7.55f, yy * 8.1f, xx + 10, xx + 10);
                numberLabelSize = new Rect(xx * 7.55f, yy * 7.8f, xx + 10, xx + 10);
                stringLabelSize = new Rect(xx * 7.55f, yy * 8.4f, xx + 10, xx + 10);
                break;
            case Position.Position4:
                rectSize = new Rect(xx * 8.80f, yy * 8.1f, xx + 10, xx + 10);
                numberLabelSize = new Rect(xx * 8.80f, yy * 7.8f, xx + 10, xx + 10);
                stringLabelSize = new Rect(xx * 8.80f, yy * 8.4f, xx + 10, xx + 10);
                break;
        }

        switch (bartype)
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
}