using UnityEngine;
using System.Collections;

public class titleMenu : MonoBehaviour 
{
    Rect startButton, exitButton, titleBar;
    bool firstUpdate = true,
         musicSwitch = false;
    AudioSource[] music;
	public float increaseYInset = 0;	// Pixel Inset for the picture
	
	// Use this for initialization
    void Awake()
    {
        guiTexture.pixelInset = new Rect(0f, 0f, Screen.width, Screen.height);
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
		
		// set the Pixel Inset of the picture here
		if(guiTexture.pixelInset.y <= 210)
		{
			guiTexture.pixelInset = new Rect(00,increaseYInset,Screen.height,Screen.width);
			increaseYInset += 0.05F;
		}
	}

    void OnGUI()
    {
		GUIStyle headingStyle = new GUIStyle();
		Font headingFont = (Font)Resources.Load("fonts/KaushanScript-Regular", typeof(Font));
		headingStyle.fontSize = 60;
		headingStyle.fontStyle = FontStyle.Bold;
		headingStyle.normal.textColor = Color.red;
    	headingStyle.font = headingFont;
		headingStyle.alignment = TextAnchor.MiddleCenter;
		
		GUIStyle otherStyle = new GUIStyle();
		Font otherHeading = (Font)Resources.Load("fonts/DroidSans", typeof(Font));
		otherStyle.fontSize = 36;
		otherStyle.normal.textColor = Color.blue;
    	otherStyle.font = otherHeading;
		otherStyle.alignment = TextAnchor.MiddleCenter;
		
        if (firstUpdate)
        {			
            float xx = Screen.width / 10;
            float yy = Screen.height / 10;
            startButton = new Rect(xx * 3f, yy * 4.5f, xx * 4f, yy);
            exitButton = new Rect(xx * 3f, yy * 7f, xx * 4f, yy);
            titleBar = new Rect(xx * 3f, yy * 2, xx * 4f, yy * 2f);
            firstUpdate = false;
        }
        GUI.Box(titleBar, "Melodious Eras", headingStyle);
        if (GUI.Button(startButton, "Start Game", otherStyle)) 
        {
            Application.LoadLevel("scene2");
        }
        if (GUI.Button(exitButton, "Exit Game", otherStyle))
            Application.Quit();
		
    }
}
