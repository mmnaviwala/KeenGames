using UnityEngine;
using System.Collections;

public class titleMenuLogoScript : MonoBehaviour {
	
	public GUIStyle buttonStyle;
	public float buttonWidth;
	public float buttonHeight;
	bool musicSwitch = false;
    AudioSource[] music;
	
	private bool textBlinking = true;
	private int blinked = 1;
	
	void Awake()
    {
        music = Camera.main.GetComponents<AudioSource>();
    }
	
	void Start()
	{
		// -------------------------------------------------------
		// Setting the button style  
		buttonStyle = new GUIStyle();
		Color buttonStyleColor = new Color(0.09F, 0.87F, 1F);
		buttonStyle.normal.textColor = buttonStyleColor;    
		buttonStyle.fontStyle = FontStyle.Bold;
		buttonStyle.alignment = TextAnchor.MiddleCenter;
		buttonStyle.fontSize = 40;
		// -------------------------------------------------------
		
		buttonWidth = Screen.width*0.35F;
		buttonHeight = Screen.height*0.12F;
		
		music[1].Stop();
        music[0].Play();
		
	}
	
	void Update()
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
		
		if (blinked%30 == 1)
	    {
	    	blinked++;
			textBlinking = !textBlinking;
	    }
		else
			blinked++;
		
	}

	void OnGUI()
	{
		if(textBlinking)
		{
            Debug.Log(Time.timeScale);
			if (GUI.Button(new Rect(Screen.width/2 - Screen.width/6, Screen.height/2 + Screen.height*0.20F, buttonWidth, buttonHeight), "Click here to start game", buttonStyle))
			{
				Application.LoadLevel("scene2");
			}
		}
	}
}
