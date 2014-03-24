using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[AddComponentMenu("Scripts/Environment/Circuitry/Keypad")]
public class Keypad : CircuitSwitch 
{
    public Door door;                   //optional: door for the keypad to unlock
	private HUD_Stealth playerHUD;      //used to disable default HUD when using keypad
    private CameraMovement3D cam3d;     //used to disable camera movement when using keypad
    public AudioClip accessDeniedClip; 

    public bool usingKeypad = false;
    public bool randomCode = false;     //check this to randomly generate a code
    public string correctCode;          
    private string enteredCode;         //code the player has entered

    //GUI stuff
	private Rect keypadRect, codeDisplayRect, exitRect, promptRect;
    public GUIStyle promptStyle;
    private Rect[] keyRects;

    private string[] keyNums;

    private const int ENTER_NUM = 10, //keynums[10] = Enter
                      CLEAR_NUM = 11; //keynums[11] = Clear


	// Use this for initialization
    void Awake()
    {
        if (this.electricGrid != null) //"Plugs in" this circuit node to the electric grid
            this.PlugIn(electricGrid);
        if (randomCode)
            GenerateCode();
    }
	void Start ()
    {
        enteredCode = "";

        foreach (CircuitNode node in connectedNodes)
            node.connectedSwitch = this;
        /*connectedNodes.ForEach(delegate(CircuitNode node) { //probably not necessary anymore
            node.connectedSwitch = this;
        });*/

        //All GUI stuff below here
        int h = Screen.height / 10;
        int w = Screen.width / 2;
        
		keypadRect = new Rect(w - h * 2.25f, h, h * 4.5f, h * 8);
        keyRects = new Rect[12];
        keyNums = new string[12];

        //Arranging key locations below this line
        exitRect = new Rect(w + h * 1.5f, h, h / 2, h / 2);
        codeDisplayRect = new Rect(w - h * 2, h * 1.5f, h * 4, h);
        promptRect = new Rect(Screen.width / 2 - 100, Screen.height * .75f, 200, 50);

        float keyY = 3f*h;
        float keyX = w - h*2;
        for (int k = 1; k < 10; k++) //#1-9
        {
            keyRects[k] = new Rect(keyX, keyY, h, h);
            keyX += 1.5f * h;
            if (k % 3 == 0)
            {
                keyY += 1.5f * h;
                keyX = w - h * 2f;
            }
            keyNums[k] = k.ToString();
        }
        //CLEAR
        keyRects[CLEAR_NUM] = new Rect(keyX, keyY, h, h);
        keyNums[CLEAR_NUM] = "C";
        //0
        keyRects[0] = new Rect(keyX + h * 1.5f, keyY, h, h);
        keyNums[0] = "0";
        //ENTER
        keyRects[ENTER_NUM] = new Rect(keyX + h * 3f, keyY, h, h);
        keyNums[ENTER_NUM] = "ENTER";
	}

    // Update is called once per frame
    void Update() 
	{
        //Press H to start hacking. Not implemented yet
		if (detectionSphere.playerInRange && !isBroken && Input.GetKeyDown(KeyCode.H))
            StartCoroutine(OnHacking());

        if (detectionSphere.playerInRange && !this.isBroken && this.hasPower && Input.GetButtonDown(InputType.USE))
        {
            UseKeypad(true);
        }

		if(usingKeypad)
		{
            //Checking to see if any of the keys (or numpad keys) have been pressed this frame
            for (int k = 0; k < 10; k++)
                if (Input.GetKeyDown(KeyCode.Keypad0 + k) || Input.GetKeyDown(KeyCode.Alpha0 + k))
                    StartCoroutine(InputKey(keyNums[k]));


            if (Input.GetButtonDown(InputType.START)) //if player hits Start or Escape
                UseKeypad(false);

            //special conditions for Enter/Clear selections
            if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return))
                StartCoroutine(InputKey(keyNums[ENTER_NUM]));
            if (Input.GetKeyDown(KeyCode.Backspace))
                StartCoroutine(InputKey(keyNums[CLEAR_NUM]));
		}
	}

    void OnTriggerEnter(Collider other)
    {
        //Locking 3rd-person camera onto this keypad
        if (other is CapsuleCollider && other.tag == Tags.PLAYER)
        {
			detectionSphere.playerInRange = true;
            if (playerHUD == null || cam3d == null)
            {
                //caching HUD and camera script once the player enters the trigger the first time
                playerHUD = other.GetComponent<HUD_Stealth>();
                cam3d = Camera.main.GetComponent<CameraMovement3D>();
            }
        }
    }
    
    //In case the player gets pushed out of range
    void OnTriggerExit(Collider other)
    {
        if (other is CapsuleCollider && other.gameObject.tag == Tags.PLAYER)
            UseKeypad(false);
    }

	void OnGUI()
	{
		if (detectionSphere.playerInRange && !usingKeypad && this.hasPower)
        {
            GUI.Box(promptRect, "Press [USE] to interact.", promptStyle);
        }
		if(hasPower && usingKeypad)
		{
            GUI.Box(keypadRect, "Keypad");
            GUI.Box(codeDisplayRect, enteredCode);

            if (GUI.Button(exitRect, "X"))
            {
                UseKeypad(false);
            }

            for (int k = 0; k < 12; k++)
            {
                if (GUI.Button(keyRects[k], keyNums[k]))
                {
                    StartCoroutine(InputKey(keyNums[k]));
                }
            }
		}
	}

    IEnumerator InputKey(string keyVal)
    {
        switch (keyVal)
        {
            case "C":
                if (enteredCode.Length > 1)
                    enteredCode = enteredCode.Substring(0, enteredCode.Length - 1);
                else
                    enteredCode = "";
                break;
            case "ENTER":
                //emits a signal to all connected nodes saying whether the code was correct
                foreach (CircuitNode node in connectedNodes)
                    node.PerformSwitchAction(enteredCode == correctCode);
                /*connectedNodes.ForEach(delegate(CircuitNode node) {
                    node.PerformSwitchAction(enteredCode == correctCode);
                });*/

                if (enteredCode == correctCode) //if correct
                {
                    if (door != null)
                        door.locked = false;
                    enteredCode = "CORRECT";

                    yield return new WaitForSeconds(.5f);

                    enteredCode = "";
                    UseKeypad(false);
                }
                else //if incorrect
                {
                    enteredCode = "INCORRECT";
                    this.audio.PlayOneShot(accessDeniedClip);
                    yield return new WaitForSeconds(.5f);
                    enteredCode = "";
                }
                break;
            default: //[0-9] adds number to enteredCode
                enteredCode += keyVal;
                break;
        }
    }

    private void GenerateCode()
    {
        int codeLength = Random.Range(4, 8);
        correctCode = "";
        for (int n = 0; n < codeLength; n++)
            correctCode += Random.Range(0, 9);
    }

    IEnumerator OnHacking()
    {
        CameraMovement3D cam = Camera.main.GetComponent<CameraMovement3D>();

        //disable default camera movement
        cam.enabled = false;
        yield return new WaitForEndOfFrame();
        cam.transform.position = this.transform.position + this.transform.forward;
        cam.transform.LookAt(this.transform);

        //StartCoroutine(Hack());
        //enable default camera movement
        cam.enabled = true;
    }

    IEnumerator Hack()
    {
        while (true /*while hacking*/)
        {
            yield return new WaitForEndOfFrame();
        }
    }

    /// <summary>
    /// Toggles HUD, cursor, and camera movement when using (or not using) the keypad
    /// </summary>
    /// <param name="usingKeypad"></param>
    void UseKeypad(bool usingKeypad)
    {
        this.usingKeypad = usingKeypad;
        playerHUD.enabled = !usingKeypad;
        Screen.lockCursor = !usingKeypad;
        Screen.showCursor = usingKeypad;
        cam3d.enabled = !usingKeypad;
    }
}
