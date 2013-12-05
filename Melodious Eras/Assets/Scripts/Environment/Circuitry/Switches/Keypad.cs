using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Keypad : CircuitSwitch 
{
    private GameObject player;
	private HUD_Stealth playerHUD;
    private CameraMovement3D cam3d;
    public AudioClip accessDeniedClip;

    public bool usingKeypad = false;
    bool playerNearKeypad = false;
    public bool randomCode = false;
    public string correctCode;
    private string enteredCode;

	private Rect keypadRect;
    private Rect codeDisplayRect;
    private Rect exitRect;
    private Rect[] keyRects;
    private string[] keyNums;

    private const int ENTER_NUM = 10,
                      CLEAR_NUM = 11;
    private const string NO_POWER = "NO POWER";


	// Use this for initialization

    void Awake()
    {
        if (randomCode)
            GenerateCode();
    }
	void Start ()
    {
        int h = Screen.height / 10;
        int w = Screen.width / 2;
        enteredCode = "";

        player = GameObject.FindGameObjectWithTag(Tags.PLAYER);
		playerHUD = player.GetComponent<HUD_Stealth>();
        cam3d = Camera.main.GetComponent<CameraMovement3D>();
        
		keypadRect = new Rect(w - h * 2.25f, h, h * 4.5f, h * 8);
        keyRects = new Rect[12];
        keyNums = new string[12];

        foreach (CircuitNode connected in connectedNodes)
            connected.connectedSwitch = this;

        //Arranging key locations below this line
        exitRect = new Rect(w + h * 1.5f, h, h / 2, h / 2);
        codeDisplayRect = new Rect(w - h * 2, h * 1.5f, h * 4, h);

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
        if (!isBroken && playerNearKeypad && Input.GetKeyDown(KeyCode.H))
            StartCoroutine(OnHacking());
        if (!isBroken && playerNearKeypad && Input.GetButtonDown(InputType.USE))
            usingKeypad = true;
		if(hasPower && usingKeypad)
		{
            playerHUD.enabled = false;
            for (int k = 0; k < 10; k++)
                if (Input.GetKeyDown(KeyCode.Keypad0 + k) || Input.GetKeyDown(KeyCode.Alpha0 + k))
                    StartCoroutine(InputKey(keyNums[k]));

            if (Input.GetButtonDown(InputType.START))
            {
                usingKeypad = false;
                playerHUD.enabled = true;
            }
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
            playerNearKeypad = true;
            //cam3d.target = this.transform;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other is CapsuleCollider && other.gameObject.tag == Tags.PLAYER)
        {
            //if (Input.GetButtonDown(InputType.USE))
            //{
            //    Vector3 relPlayerPos = other.transform.position - this.transform.position;
            //    Ray ray = new Ray(this.transform.position, relPlayerPos);
            //    Debug.DrawLine(ray.origin, player.transform.position, Color.red, 2);

            //    RaycastHit hit;
            //    Physics.Raycast(ray, out hit);
            //    if (hit.collider == other)
            //    {
            //        usingKeypad = true;
            //        playerHUD.rigidbody.velocity = new Vector3(0, playerHUD.rigidbody.velocity.y, 0);
            //    } 
            //}
        }
    }
    
    //In case the player gets pushed out of range
    void OnTriggerExit(Collider other)
    {
        if (other is CapsuleCollider && other.gameObject.tag == Tags.PLAYER)
        {
            usingKeypad = false;
            playerNearKeypad = false;
            cam3d.target = null;
        }
    }

	void OnGUI()
	{
		if(hasPower && usingKeypad)
		{
            GUI.Box(keypadRect, "Keypad");
            GUI.Box(codeDisplayRect, enteredCode);

            if (GUI.Button(exitRect, "X"))
            {
                usingKeypad = false;
                playerHUD.enabled = true;
            }

            for (int k = 0; k < 12; k++)
            {
                if (GUI.Button(keyRects[k], keyNums[k]) /*|| (k < 10 && Input.GetKeyDown(KeyCode.Keypad0 + k))*/)
                {
                    Debug.Log("Event: " + Event.current.type);
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
                foreach (CircuitNode connected in connectedNodes)
                {
                    if (connected != null)
                        connected.PerformSwitchAction(enteredCode == correctCode);
                }
                if (enteredCode == correctCode)
                {
                    enteredCode = "CORRECT";
                    //if(connectedObject != null)
                     //   connectedObject.GetComponent<CircuitScript>().activated = true;
                    yield return new WaitForSeconds(.5f);

                    enteredCode = "";
                    usingKeypad = false;
                    playerHUD.enabled = true;
                }
                else
                {
                    enteredCode = "INCORRECT";
                    this.audio.PlayOneShot(accessDeniedClip);
                    yield return new WaitForSeconds(.5f);
                    enteredCode = "";
                }
                break;
            default:
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

        StartCoroutine(Test());
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

    IEnumerator Test()
    {
        Debug.Log("Starting");
        yield return new WaitForSeconds(1.5f);
        Debug.Log("Ending");
    }
}
