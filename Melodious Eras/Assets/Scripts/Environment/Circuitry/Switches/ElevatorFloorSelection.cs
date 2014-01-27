using UnityEngine;
using System.Collections;

[AddComponentMenu("Scripts/Environment/Circuitry/Elevator Floor Selection")]
public class ElevatorFloorSelection : CircuitSwitch 
{
    private bool inUse;
    public string[] floors;

    private Rect selectionRect, exitRect, promptRect;
    private Rect[] buttons;
    public GUIStyle promptStyle;

    private HUD_Stealth playerHUD;
    private CameraMovement3D cam3d;
    private Elevator elevator;

    void Awake()
    {
        promptRect = new Rect(Screen.width / 2 - 100, Screen.height * .75f, 200, 50);
        if (this.electricGrid != null)
            electricGrid.connectedObjects.Add(this);
    }
	// Use this for initialization
	void Start () 
    {
        elevator = this.transform.parent.GetComponent<Elevator>();
        int h = Screen.height / 10;
        int w = Screen.width / 2;
        buttons = new Rect[floors.Length];

        selectionRect = new Rect(w - h * 2.25f, h, h * 4.5f, h * 8);
        exitRect = new Rect(w + h * 1.75f, h * .75f, h / 2, h / 2);

        for (int b = 0; b < buttons.Length; b++)
        {
            buttons[b] = new Rect(w - h * 2, h * (b + 1) + h * .25f, h * 4, h * .75f);
        }
	}

    // Update is called once per frame
    void Update() 
    {
		if (detectionSphere.playerInRange)
        {
            if (Input.GetButtonDown(InputType.USE))
            {
                UseButtons(true);
            }
            //B (xbox), Circle (playstation), or Escape (master race)
            if (Input.GetButtonDown(InputType.CROUCH) || Input.GetButtonDown(InputType.START))
            {
                UseButtons(false);
            }
        }
	}

    /// <summary>
    /// Should display the floors in the building
    /// </summary>
    void OnGUI()
    {
        if (inUse)
        {
            GUI.Box(selectionRect, "");
            if (GUI.Button(exitRect, "X"))
                UseButtons(false);
            

            for (int b = 0; b < buttons.Length; b++)
            {
                if (GUI.Button(buttons[b], floors[b]))
                {
                    elevator.Call(b);
                    UseButtons(false);
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other is CapsuleCollider && other.tag == Tags.PLAYER)
        {
            playerHUD = other.GetComponent<HUD_Stealth>();
            cam3d = Camera.main.GetComponent<CameraMovement3D>();
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other is CapsuleCollider && other.tag == Tags.PLAYER)
        {
            inUse = false;
        }
    }

    private void UseButtons(bool usingButtons)
    {
        inUse = usingButtons;
        playerHUD.enabled = !usingButtons;
        cam3d.enabled = !usingButtons;
        Screen.showCursor = usingButtons;
        Screen.lockCursor = !usingButtons;
    }
}
