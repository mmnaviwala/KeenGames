using UnityEngine;
using System.Collections;

public class DocumentScript : MonoBehaviour 
{
    public string message, keycode;
    public GUIStyle docStyle;
    public bool reading = false;
    public Keypad keypadReference;

    private HUD playerHUD;
    private Rect documentRect;
	// Use this for initialization
	void Start () 
    {
        playerHUD = GameObject.FindGameObjectWithTag(Tags.PLAYER).GetComponent<HUD>();

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
        if (other.tag == Tags.PLAYER)
        {
            if (Input.GetButtonDown("Use"))
                reading = true;
            if (Input.GetButtonDown("Start"))
            {
                reading = false;
                playerHUD.enabled = true;
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == Tags.PLAYER)
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
