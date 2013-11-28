using UnityEngine;
using System.Collections;

public class EndScreen : MonoBehaviour {
    AudioSource music;
    HUD_Stealth playerHUD;
    GUIStyle headingStyle = new GUIStyle();
    CharacterStats playerStats;
    public Font headingFont;
    public Texture2D endScreen, btnTexture;
    bool completeLevel = false;
    public float delayTime = 0f;


    // Use this for initialization
    void Start()
    {
        music = Camera.main.audio;
        playerHUD = GameObject.FindGameObjectWithTag(Tags.PLAYER).GetComponent<HUD_Stealth>();
        playerStats = GameObject.FindGameObjectWithTag(Tags.PLAYER).GetComponent<CharacterStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.P) && completeLevel)
        {
            ResumeGame();
        }
    }

    IEnumerator OnTriggerEnter(Collider other)
    {
        if (other.tag == Tags.PLAYER)
        {
            yield return new WaitForSeconds(delayTime);
            GameObject.FindGameObjectWithTag(Tags.PLAYER).GetComponent<HUD_Stealth>();
            completeLevel = true;
            //  music.Stop();
            //Time.timeScale = 0;
            playerHUD.enabled = false;
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

        if (completeLevel)
        {
            float buttonWidth = Screen.width*0.1F;
		    float buttonHeight = Screen.height*0.05F;
            //int totalNotes = playerStats.blueNotes + playerStats.greenNotes + playerStats.redNotes + playerStats.purpleNotes + playerStats.notes;

            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), endScreen);

            /*GUI.Label(new Rect(Screen.width / 2 - Screen.width / 25, Screen.height / 3 - Screen.height * 0.11F, buttonWidth, buttonHeight), playerStats.blueNotes.ToString(), buttonStyle);

            GUI.Label(new Rect(Screen.width / 2 - Screen.width / 25, Screen.height / 2 - Screen.height * 0.165F, buttonWidth, buttonHeight), playerStats.greenNotes.ToString(), buttonStyle);

            GUI.Label(new Rect(Screen.width / 2 - Screen.width / 25, Screen.height / 2 - Screen.height * 0.06F, buttonWidth, buttonHeight), playerStats.purpleNotes.ToString(), buttonStyle);

            GUI.Label(new Rect(Screen.width / 2 - Screen.width / 25, Screen.height - Screen.height * 0.45F, buttonWidth, buttonHeight), playerStats.redNotes.ToString(), buttonStyle);

            GUI.Label(new Rect(Screen.width / 2 - Screen.width / 25, Screen.height - Screen.height * 0.345F, buttonWidth, buttonHeight), playerStats.notes.ToString(), buttonStyle);

            GUI.Label(new Rect(Screen.width / 2 - Screen.width / 25, Screen.height - Screen.height * 0.27F, buttonWidth, buttonHeight), totalNotes.ToString(), buttonStyle);

            if (GUI.Button(new Rect(Screen.width * .8f, Screen.height * .8f, Screen.width * 0.15F, Screen.height * 0.075F), "Next Level", buttonStyle))
            {
                Debug.Log("Clicked on button");
                Application.LoadLevel(Application.loadedLevel + 1);
            }
             */
        }
    }

    private void ResumeGame()
    {
        completeLevel = false;
    }
}
