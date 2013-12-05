using UnityEngine;
using System.Collections;

public class HUD_Stealth : MonoBehaviour
{
    public GUIStyle healthMaxStyle, healthCurrentStyle, armorMaxStyle, armorCurrentStyle, flashlightLifeStyle, reticleStyle;

    private Rect healthBarMax, healthBarCurrent, armorBarMax, armorBarCurrent, flashlightLife, reticle, weaponRect;
    private float width_max_health, width_max_armor;

    private PlayerStats stats;
    private PlayerMovementBasic player;
    private Camera mainCam;
    private Weapon weapon;
    private Suit suit;

    // Use this for initialization
    void Start()
    {
        Screen.showCursor = false;

        stats = this.GetComponent<PlayerStats>();
        player = this.GetComponent<PlayerMovementBasic>();
        weapon = this.GetComponent<PlayerStats>().equippedWeapon;
        suit = this.GetComponent<Suit>();
        mainCam = Camera.main;

        width_max_health = Screen.width / 3;
        width_max_armor = width_max_health * ((float)suit.maxArmor / stats.maxHealth);


        healthBarMax = new Rect(Screen.height / 20, Screen.height / 20, width_max_health, width_max_health / 10);
        healthBarCurrent = new Rect(healthBarMax.xMin, healthBarMax.yMin, width_max_health * ((float)stats.health / stats.maxHealth), healthBarMax.height);

        armorBarMax = new Rect(healthBarMax.xMin, healthBarMax.xMin * 2.5f, width_max_armor, healthBarMax.height);
        armorBarCurrent = new Rect(healthBarMax.xMin, healthBarMax.xMin * 2.5f, width_max_armor * ((float)suit.armor / suit.maxArmor), healthBarMax.height);
        
        weaponRect = new Rect(healthBarMax.xMin, Screen.height - 3 * healthBarMax.height, healthBarMax.width, healthBarMax.height);

        reticle = new Rect(Screen.width / 2 - Screen.width / 60, Screen.height / 2 - Screen.width / 60, Screen.width / 30, Screen.width / 30);
    }

    // Update is called once per frame
    void Update()
    {
        Screen.lockCursor = true;
        healthBarCurrent.width = width_max_health * stats.health / stats.maxHealth;
        armorBarCurrent.width = width_max_armor * suit.armor / suit.maxArmor;
    }

    void OnGUI()
    {
        if (player.isAiming)
            GUI.Box(reticle, "", reticleStyle);

        GUI.Box(healthBarMax, stats.health.ToString(), healthMaxStyle);
        GUI.Box(armorBarMax, suit.armor.ToString(), armorMaxStyle);
        GUI.Label(weaponRect, weapon.HudString(), weapon.hudStyle);

        GUI.color = new Color(1, 1, 1, .25f);
        GUI.Box(healthBarCurrent, "", healthCurrentStyle);
        GUI.Box(armorBarCurrent, "", armorCurrentStyle);
    }

    public void Enable()
    {
        this.enabled = true;
        Screen.lockCursor = true;
        Screen.showCursor = false;
    }

    public void Disable()
    {
        this.enabled = false;
        Screen.lockCursor = false;
        Screen.showCursor = true;
 
    }
}