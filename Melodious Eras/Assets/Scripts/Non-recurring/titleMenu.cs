using UnityEngine;
using System.Collections;

public class titleMenu : MonoBehaviour 
{
    Rect startButton, exitButton, titleBar;
    bool firstUpdate = true,
         musicSwitch = false;
    AudioSource[] music;
	public Texture2D btnTexture;
	public Font btnFont;
	
	// Use this for initialization
    void Awake()
    {
        music = Camera.main.GetComponents<AudioSource>();
    }
	void Start ()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        music[1].Stop();
        music[0].Play();
	}
	
	// Update is called once per frame
	void Update () 
    {
        //Switching between audio sources (for a smooth loop)
        switch (musicSwitch)
        {
            case false:
                music[1].volume = (music[1].volume > 0) ? music[1].volume - .2f * Time.deltaTime : 0;
                if (music[0].time >= 72)
                {
                    music[1].time = 0;
                    music[1].volume = 1f;
                    music[1].Play();
                    musicSwitch = true;
                }
                break;
            default:
                music[0].volume = (music[0].volume > 0) ? music[0].volume - .2f * Time.deltaTime : 0;
                if (music[1].time >= 72)
                {
                    music[0].time = 0;
                    music[0].volume = 1f;
                    music[0].Play();
                    musicSwitch = false;
                }
                break;
        }
	}
	

    void OnGUI()
    {		
		// -------------------------------------------------------
		// Setting the button style	
		GUIStyle buttonStyle = new GUIStyle();
		buttonStyle.normal.background = btnTexture;
		buttonStyle.normal.textColor = Color.blue;		
		buttonStyle.fontStyle = FontStyle.Bold;
		buttonStyle.alignment = TextAnchor.MiddleCenter;
		buttonStyle.fontSize = 40;
		buttonStyle.font = btnFont;
		// -------------------------------------------------------
		
		float buttonWidth = Screen.width*0.35F;
		float buttonHeight = Screen.height*0.12F;
		
		if (GUI.Button(new Rect(Screen.width/2 - Screen.width/6, Screen.height/2 + Screen.height*0.15F, buttonWidth, buttonHeight), "Start Game", buttonStyle))
		{
			Application.LoadLevel("scene2");
		}
		
		if (GUI.Button(new Rect(Screen.width/2 - Screen.width/6, Screen.height/2 + Screen.height*0.3F, buttonWidth, buttonHeight), "Exit Game", buttonStyle))
		{
        	Application.Quit();;
		}
		
    }
}
