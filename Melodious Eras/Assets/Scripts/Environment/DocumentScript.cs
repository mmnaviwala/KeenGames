using UnityEngine;
using System.Collections;

public class DocumentScript : MonoBehaviour 
{
    public string message, keycode;
    public GUIStyle docStyle;
    public bool reading = false;
    public Keypad keypadReference;

    private HUD_Stealth playerHUD;
    private Rect documentRect;
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
        docStyle.padding = new RectOffset(h / 4, h / 4, h / 2, h / 2);
	}
	
	// Update is called once per frame
	void Update () 
    {

	}

    void OnTriggerStay(Collider other)
    {
        if (other is CapsuleCollider && other.tag == Tags.PLAYER)
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
    void OnTriggerExit(Collider other)
    {
        if (other is CapsuleCollider && other.tag == Tags.PLAYER)
        {
            reading = false;
            playerHUD.enabled = true;
        }
    }

    void OnGUI()
    {
        if (reading)
        {
            playerHUD.enabled = false;
            GUI.color = new Color(1, 1, 1, .75f);
            GUI.Box(documentRect, message, docStyle);
        }
    }
}
