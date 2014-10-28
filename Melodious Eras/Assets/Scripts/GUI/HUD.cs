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


    public GUIStyle objectiveTextStyle;
    TrackObjectives objectives;
    public string currentObjective;
    private Rect currentObjectiveRect;


    private PlayerStats player;

    void Awake()
    {
        Cursor.visible = false;
        Screen.lockCursor = true;

        float xx = Screen.width / 10;
        float yy = Screen.height / 10;
        currentObjectiveRect = new Rect(xx * 7f, yy * 0.3f, xx * 3, xx);
        reticle = new Rect(Screen.width / 2 - Screen.width / 60, Screen.height / 2 - Screen.width / 60, Screen.width / 30, Screen.width / 30);
    }
	// Use this for initialization
	void Start () {

        player = GameObject.FindGameObjectWithTag(Tags.PLAYER).GetComponent<PlayerStats>();
        objectives = GameObject.FindGameObjectWithTag(Tags.GAME_CONTROLLER).GetComponent<TrackObjectives>();

        health.Initialize(player);
        armor.Initialize(player);
        ammo.Initialize(player);
        battery.Initialize(player);
	}
	
	// Update is called once per frame
    void Update()
    {
        currentObjective = objectives.currentObjectiveTitle;
	
	}

    void OnGUI()
    {
        health.Display();
        armor.Display();
        ammo.Display();
        battery.Display();

        if (player.playerMovement.IsAiming)
            GUI.Box(reticle, "", reticleStyle);


        if (objectives.allObjectivesComplete)
            GUI.Label(currentObjectiveRect, ("All Objectives Complete!"), objectiveTextStyle);
        else
            GUI.Label(currentObjectiveRect, ("Current Objective:\n" + currentObjective), objectiveTextStyle);
    }
}
