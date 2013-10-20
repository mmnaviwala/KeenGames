using UnityEngine;
using System.Collections;
 
public class PauseMenu : MonoBehaviour {
 
    public Rect windowRect = new Rect(295,175,0,0);
	public Texture2D[] pictures;
	public Texture2D btnTexture;
	public Font headingFont;
    public GUIStyle headingStyle = new GUIStyle();

    private bool gamePaused = false;
    private bool firstTime = true;
    private AudioSource music;
    private HUD playerHUD;
	void Start()
	{
		// Loading all the resources in the start() method
		music = Camera.main.audio;
		pictures = new Texture2D[2];
        //pictures[0] = Resources.Load("canon_clef_doodle") as Texture2D;
		//btnTexture = Resources.Load("white_dot") as Texture2D;
		playerHUD = GameObject.FindGameObjectWithTag(Tags.PLAYER).GetComponent<HUD>();
		
		headingStyle.fontSize = (int)Screen.width/13;
        headingStyle.fontStyle = FontStyle.Bold;
        headingStyle.normal.textColor = Color.blue;
        headingStyle.font = headingFont;
        headingStyle.alignment = TextAnchor.MiddleCenter;
        gamePaused = false;
	}
 
    void Update()
	{
		headingStyle.fontSize = (int)Screen.width/13;
		
	    if(Input.GetKeyUp(KeyCode.Escape))
		{
			if(firstTime)
			{
				Start();
				firstTime = false;
			}
				
	        if(gamePaused)
			{
	            gamePaused = false;
				playerHUD.enabled = true;
				guiTexture.enabled = false;
				music.Play();
	            Time.timeScale = 1.0f;
	        } 
			else 
			{
	            gamePaused = true;
				playerHUD.enabled = false;
				music.Pause();
	            Time.timeScale = 0.0f;
			}
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
		// -------------------------------------------------------
		
        if (gamePaused)
		{			
            Time.timeScale = 0.0f;
			guiTexture.enabled = true;
			
			float buttonWidth = Screen.width*0.2F;
			float buttonHeight = Screen.height*0.1F;
			
			GUI.Label(new Rect(Screen.width/2, Screen.height/2 - Screen.height/4, 0, 0), "Game Paused", headingStyle);
			
			if (GUI.Button(new Rect(Screen.width/2 - Screen.width/12, Screen.height/2 - Screen.height*0.12F, buttonWidth, buttonHeight), "Resume", buttonStyle))
			{
            	Debug.Log("Clicked the resume button");
				gamePaused = false;
				guiTexture.enabled = false;
				music.Play();
				playerHUD.enabled = true;
	            Time.timeScale = 1.0f;
			}
			
			if (GUI.Button(new Rect(Screen.width/2 - Screen.width/12, Screen.height/2 + Screen.height*0.001F, buttonWidth, buttonHeight), "Restart", buttonStyle))
			{
				Debug.Log("Clicked the restart button");
				Application.LoadLevel(Application.loadedLevelName);
			}
			
			if (GUI.Button(new Rect(Screen.width/2 - Screen.width/12, Screen.height/2 + Screen.height*0.125F, buttonWidth, buttonHeight), "Options", buttonStyle))
			{
            	Debug.Log("Clicked the options button");
			}
			
			if (GUI.Button(new Rect(Screen.width/2 - Screen.width/12, Screen.height/2 + Screen.height*0.25F, buttonWidth, buttonHeight), "Exit Game", buttonStyle))
			{
            	Debug.Log("Clicked the exit button");
				Application.Quit();
			}
			
			/*
			// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
			// Main pause menu background
			Texture2D texture = new Texture2D(1, 1);
			texture.SetPixel(0,0,Color.white);
			texture.Apply();
			GUI.skin.box.normal.background = texture;
			GUI.Box(new Rect(Screen.width*0.20F, Screen.height*0.20F, Screen.width*0.65F, Screen.height*0.65F), GUIContent.none);
			// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
			
			Texture2D[] characters = new Texture2D[2];
			characters[0] = Resources.Load("MaleChar") as Texture2D;
			characters[1] = Resources.Load("FemaleChar") as Texture2D;
			
			GUI.Box(new Rect(164, 358, 150, 150), characters[1]);
			GUI.Box(new Rect(524, 355, 150, 150), characters[0]);
			
			GUI.Label(new Rect(Screen.width/2 - 50, Screen.height/2 - 150, 100, 50), "Game Paused", headingStyle);
			
			// """""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
			// GUI buttons
			GUI.backgroundColor = Color.blue;
			if (GUI.Button(new Rect(Screen.width/2 - 50, Screen.height/2 - 70, 150, 50), "Resume", buttonStyle))
			{
            	Debug.Log("Clicked the resume button");
				gamePaused = false;
				music.Play();
				playerHUD.enabled = true;
	            Time.timeScale = 1.0f;
			}
			
			if (GUI.Button(new Rect(Screen.width/2 - 50, Screen.height/2 - 05, 150, 50), "Restart", buttonStyle))
			{
				Debug.Log("Clicked the restart button");
				Application.LoadLevel(Application.loadedLevelName);
			}
			
			if (GUI.Button(new Rect(Screen.width/2 - 50, Screen.height/2 + 55, 150, 50), "Options", buttonStyle))
			{
            	Debug.Log("Clicked the options button");
			}
			
			if (GUI.Button(new Rect(Screen.width/2 - 50, Screen.height/2 + 120, 150, 50), "Exit Game", buttonStyle))
			{
            	Debug.Log("Clicked the exit button");
				Application.Quit();
			}
			// """""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
			*/
        }
		
    }
}