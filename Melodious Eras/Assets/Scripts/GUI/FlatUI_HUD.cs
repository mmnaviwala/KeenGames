using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshRenderer))]

public class FlatUI_HUD : MonoBehaviour
{

    public Texture bottomTexture, topTexture, barTexture;
    private Rect rectSize, numberLabelSize, stringLabelSize, reticle;
    [SerializeField] private GUIStyle reticleStyle;
    private GUIStyle smallFont;
    public GUIStyle bigFont;
    private string textToDisplay;
    public float maxNumber = 100;
    private float currentNumber = 100;
    public enum Position { Position1, Position2, Position3, Position4 }
    public enum BarType { Health, Armor, Ammo, Battery }
    public Position positions;
    public BarType bartype;

    public PlayerStats playerStats;
    private PlayerMovementBasic player;
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
                maxNumber = playerStats.maxHealth;
                currentNumber = playerStats.health;
                break;
            case BarType.Armor:
                maxNumber = playerSuit.maxArmor;
                currentNumber = playerSuit.armor;
                break;
            case BarType.Ammo:
                maxNumber = playerWeapon.maxAmmo;
                currentNumber = playerWeapon.ammoInClip + playerWeapon.extraAmmo;
                break;
            case BarType.Battery:
                maxNumber = playerSuit.maxBatteryLife;
                currentNumber = playerSuit.batteryLife;
                break;
        }
    }

    void OnGUI()
    {
        GUI.DrawTexture(rectSize, bottomTexture);
        renderer.material.SetFloat("_Cutoff", Mathf.Clamp(Mathf.InverseLerp(0f, maxNumber, maxNumber - currentNumber), .01f, 1));
        //renderer.material.color = Color.Lerp(Color.clear, Color.white, currentNumber / maxNumber);
        Graphics.DrawTexture(rectSize, barTexture, gameObject.renderer.material);
        GUI.DrawTexture(rectSize, topTexture);

        if(bartype == BarType.Battery)
            GUI.Label(numberLabelSize, string.Format("{0:f1}", currentNumber), bigFont);
        else
            GUI.Label(numberLabelSize, string.Format("{0}", currentNumber), bigFont);

        GUI.Label(stringLabelSize, textToDisplay, smallFont);


        if (player.IsAiming && bartype == BarType.Ammo)
            GUI.Box(reticle, "", reticleStyle);
    }

    /// <summary>
    /// Common initializations
    /// </summary>
    protected void Initialize()
    {
        Screen.showCursor = false;
        Screen.lockCursor = true;
        this.player = GameObject.FindGameObjectWithTag(Tags.PLAYER).GetComponent<PlayerMovementBasic>();
        this.playerStats = player.GetComponent<PlayerStats>();
        this.playerSuit = playerStats.suit;
        this.playerWeapon = playerStats.equippedWeapon;

        float xx = Screen.width / 10;
        float yy = Screen.height / 10;

        reticle = new Rect(Screen.width / 2 - Screen.width / 60, Screen.height / 2 - Screen.width / 60, Screen.width / 30, Screen.width / 30);

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
                textToDisplay = playerWeapon.weaponName;
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