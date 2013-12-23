using UnityEngine;
using System.Collections;

[AddComponentMenu("Scripts/Environment/Document Script")]
public class DocumentScript : MonoBehaviour 
{
    public string message, keycode;
    public GUIStyle docStyle;
    public bool reading = false;
    bool inTrigger = false;
    public Keypad keypadReference;

    private HUD_Stealth playerHUD;
    private Rect documentRect, promptRect;
    public GUIStyle promptStyle;
	// Use this for initialization
	void Start () 
    {
        playerHUD = GameObject.FindGameObjectWithTag(Tags.PLAYER).GetComponent<HUD_Stealth>();

        if (keypadReference != null)
        {
            keycode = keypadReference.correctCode;
            message += ' ' + keycode;
        }

        int w = Screen.width / 2;
        int h = Screen.height / 10;
        documentRect = new Rect(w - h * 2.25f, h, h * 4.5f, h * 8);
        promptRect = new Rect(w - 100, Screen.height * .75f, 200, 50);

        docStyle.padding = new RectOffset(h / 4, h / 4, h / 2, h / 2);
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (inTrigger)
        {
            if (Input.GetButtonDown(InputType.USE))
            {
                reading = true;
                playerHUD.rigidbody.velocity = new Vector3(0, playerHUD.rigidbody.velocity.y, 0);
            }
            if (Input.GetButtonDown(InputType.START))
            {
                reading = false;
                playerHUD.enabled = true;
            } 
        }        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other is CapsuleCollider && other.tag == Tags.PLAYER)
        {
            inTrigger = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other is CapsuleCollider && other.tag == Tags.PLAYER)
        {
            inTrigger = false;
            reading = false;
            playerHUD.enabled = true;
        }
    }

    void OnGUI()
    {
        if (inTrigger && !reading)
            GUI.Box(promptRect, "Press [USE] to read.", promptStyle);
        if (reading)
        {
            playerHUD.enabled = false;
            GUI.color = new Color(1, 1, 1, .75f);
            GUI.Box(documentRect, message, docStyle);
        }
    }
}
