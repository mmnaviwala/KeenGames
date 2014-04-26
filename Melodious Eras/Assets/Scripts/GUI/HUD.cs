using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour {

    [SerializeField]
    private HealthBar health;
    [SerializeField]
    private ArmorBar armor;
    [SerializeField]
    private AmmoBar ammo;
    [SerializeField]
    private BatteryBar battery;

    private Rect reticle;
    [SerializeField]
    private GUIStyle reticleStyle;

    public GUIStyle defaultBigFont;
    private GUIStyle defaultSmallFont;


    private PlayerStats playerStats;
    private PlayerMovementBasic player;
    private Suit playerSuit;
    private Weapon playerWeapon;

    void Awake()
    {
        Screen.showCursor = false;
        Screen.lockCursor = true;
    }
	// Use this for initialization
	void Start () {
        
        playerStats = GameObject.FindGameObjectWithTag(Tags.PLAYER).GetComponent<PlayerStats>();
        player = playerStats.playerMovement;
        playerSuit = playerStats.suit;
        playerWeapon = playerStats.equippedWeapon;

        health.Initialize();
        armor.Initialize();
        ammo.Initialize();
        battery.Initialize();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        health.Display();
        armor.Display();
        ammo.Display();
        battery.Display();
    }
}
