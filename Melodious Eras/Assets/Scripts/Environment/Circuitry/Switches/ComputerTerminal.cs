using UnityEngine;
using System.Collections;

[AddComponentMenu("Scripts/Environment/Circuitry/Computer Terminal")]
public class ComputerTerminal : CircuitSwitch
{
    #region variables
    public string userName; //for display purposes only
    public string password;
	protected string passwordGuess = "";

    protected bool playerNearby = false;
	protected bool alreadyActivated = false;
	protected bool usingTerminal = false;
	protected bool hasAccess = false;
	protected float pressTime = 0;

    public Material onScreen, offScreen;
    public Transform[] monitors;

    HUD_Stealth playerHUD;
    PlayerMovementBasic playerMovement;
    animatedPauseMenu pauseMenu;
    CameraMovement3D cam3d;

	protected Rect    terminalRect,  usernameRect,  passwordRect,  emailListRect,  emailBodyRect,  promptRect, emailListNodeRect, exitButtonRect;
    public GUIStyle terminalStyle, usernameStyle, passwordStyle, emailListStyle, emailBodyStyle, promptStyle, exitButtonStyle;


    public int selectedEmailIndex = 0;
    public string[] emailSelection = new string[] { "Grid 1", "Grid 2", "Grid 3", "Grid 4" };
    public string[] emailBody;
    #endregion

    #region MonoBehavior functions

    void Awake()
    {
        if(this.electricGrid != null)
            electricGrid.connectedObjects.Add(this);
    }
    // Use this for initialization
	void Start () 
    {
        pauseMenu = GameObject.FindGameObjectWithTag(Tags.PAUSE_MENU).GetComponent<animatedPauseMenu>();
        cam3d = Camera.main.GetComponent<CameraMovement3D>();

        float terminalWidth = Screen.width * .75f;
        float terminalHeight = Screen.height * .75f;

        promptRect = new Rect(Screen.width / 2 - 100, Screen.height * .75f, 200, 50);

        terminalRect = new Rect((Screen.width - terminalWidth) / 2, (Screen.height - terminalHeight) / 2, terminalWidth, terminalHeight);
        exitButtonRect = new Rect(terminalRect.xMin + terminalRect.width - 25, terminalRect.yMin, 25, 25);
        usernameRect = new Rect(Screen.width / 2 - 100, Screen.height / 2, 200, 50);
        passwordRect = new Rect(Screen.width / 2 - 100, Screen.height / 2 + 75, 200, 50);

        emailListNodeRect = new Rect(terminalRect.xMin + 25, terminalRect.yMin + 25, terminalRect.width / 2 - 50, terminalRect.width / 4);
        emailBodyRect = new Rect(Screen.width / 2, terminalRect.yMin + 25, terminalRect.width / 2 - 25, terminalRect.height - 50);
		for(int m = 0; m < monitors.Length; m++)
		{
			monitors[m].GetChild(0).light.enabled = activated;
			monitors[m].renderer.material = activated ? onScreen : offScreen;
		}
	}

    // Update is called once per frame
    void Update() 
    {
        if (playerNearby && this.hasPower)
        {
            //Turning computer on/off
            if (!alreadyActivated && Input.GetButton(InputType.USE))
            {
                pressTime += Time.deltaTime;
                if (pressTime > .75f)
                {
                    this.activated = !this.activated;
                    pressTime = 0;
                    alreadyActivated = true;

                    for (int m = 0; m < monitors.Length; m++)
                    {
                        monitors[m].renderer.material = activated ? onScreen : offScreen;
                        monitors[m].GetChild(0).light.enabled = activated;
                    }
                }
            }

            //Using computer
            if (this.activated && !alreadyActivated && Input.GetButtonUp(InputType.USE))
            {
                UsingComputer(true);
            }

            if (usingTerminal)
            {
                if (Input.GetButtonDown(InputType.START))
                {
                    UsingComputer(false);
                }
                //reading passwprd input stream for this frame (ASCII characters only)
                else if(!alreadyActivated)
                {
                    for (int i = 0; i < Input.inputString.Length; i++)
                        StartCoroutine(InputKey(Input.inputString[i]));
                }
            }
            if (Input.GetButtonUp(InputType.USE))
            {
                pressTime = 0;
                alreadyActivated = false;
            }
        }    
	}

    void OnGUI()
    {
        if (playerNearby && !usingTerminal && this.hasPower)
        {
            if (this.activated)
                GUI.Box(promptRect, "HOLD [USE] to turn off. Press [USE] to interact.", promptStyle);
            else
                GUI.Box(promptRect, "HOLD [USE] to turn on.", promptStyle);
        }
        else if (usingTerminal)
        {
            GUI.Box(terminalRect, "");
            if (GUI.Button(exitButtonRect, "X"))
            {
                UsingComputer(false);
            }
            //Login screen
            if (!hasAccess)
            {
                GUI.Box(usernameRect, userName);
                GUI.Box(passwordRect, passwordGuess);
            }
            //Email/security screen
            else
            {
                selectedEmailIndex = GUI.SelectionGrid(emailListNodeRect, selectedEmailIndex, emailSelection, 1);
                GUI.Box(emailBodyRect, emailBody[selectedEmailIndex]);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other is CapsuleCollider && other.tag == Tags.PLAYER)
        {
            playerNearby = true;
            playerHUD = other.GetComponent<HUD_Stealth>();
            playerMovement = other.GetComponent<PlayerMovementBasic>();
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other is CapsuleCollider && other.tag == Tags.PLAYER)
        {
            playerNearby = false;
            UsingComputer(false);
        }
    }
    #endregion

    #region Custom functions

    /// <summary>
    /// Populate emails from XML file; use XMLUtilities.
    /// </summary>
    public void GetEmails()
    {
 
    }
    public override void TurnOnOff(bool on)
    {
        activated = on;
    }

    void UsingComputer(bool usingTerminalP)
    {
        usingTerminal =      usingTerminalP;

        cam3d.enabled =     !usingTerminalP;
        playerHUD.enabled = !usingTerminalP;
        playerMovement.enabled = !usingTerminal;
        pauseMenu.enabled = !usingTerminalP;

        Screen.showCursor =  usingTerminalP;
        Screen.lockCursor = !usingTerminalP;
        
    }

    IEnumerator InputKey(char keyVal)
    {
        switch (keyVal)
        {
            case '\n':
            case '\r':
                passwordGuess = passwordGuess == password ? "Welcome, " + userName : "INCORRECT";
                yield return new WaitForSeconds(.5f);
                hasAccess = true;
                //open up email/security window
                break;
            case '\b':
                if(passwordGuess.Length > 0)
                    passwordGuess = passwordGuess.Substring(0, passwordGuess.Length - 1);
                break;
            default:
                if (passwordGuess.Length < 20)
                    passwordGuess += keyVal;
                break;
        }
    }
    #endregion
}
