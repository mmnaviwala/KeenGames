using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FlatUI_PauseMenu : MonoBehaviour
{

    public Texture background, log, inventory, settings, resume, exit, logo, contentSquarePurple, contentSquareCyan, contentSquareGreen;
    private Rect logRect, inventoryRect, settingRect, resumeRect, exitRect, gamePausedRect, logoRect, contentSquareRect, contentToDisplayRect;
    private float xx, yy;
    public string gamePausedText = "Game Paused";
    private string contentSquareToDraw = "Purple";
    public GUIStyle gamePausedStyle, logStyle;
    private bool isGamePaused = false;
    private List<string> logList;
    private string allLogs = "Logs\n";
    public bool logsDisplayed = false;


    // Use this for initialization
    void Start()
    {
        xx = Screen.width / 10;
        yy = Screen.height / 10;
        logRect = new Rect(xx * 2.9f, yy * 3.7f, xx, xx);
        inventoryRect = new Rect(xx * 2.9f, yy * 5.7f, xx, xx);
        settingRect = new Rect(xx * 2.9f, yy * 7.7f, xx, xx);
        resumeRect = new Rect(xx * 0.7f, yy * 5.1f, xx, xx);
        exitRect = new Rect(xx * 0.7f, yy * 7.1f, xx, xx);
        // left, top, width, height

        gamePausedStyle.fontSize = System.Convert.ToInt32(Screen.height * .18f);
        gamePausedStyle.alignment = TextAnchor.MiddleRight;
        gamePausedRect = new Rect(xx * 8.7f, yy * 0.4f, xx, xx);

        logStyle.fontSize = System.Convert.ToInt32(Screen.height * .05f);
        logStyle.alignment = TextAnchor.UpperLeft;
        logStyle.normal.textColor = Color.white;
        logStyle.wordWrap = true;

        logoRect = new Rect(xx * 0.3f, yy * 0.6f, xx * 2.2f, xx * 1.5f);

        contentSquareRect = new Rect(xx * 3.6f, yy * 2.6f, xx * 7.0f, xx * 4.5f);

        contentToDisplayRect = new Rect(xx * 4.8f, yy * 3.6f, xx * 7.0f, xx * 4.5f);

    }

    void OnGUI()
    {
        Color guiColorOriginal = GUI.backgroundColor;
        if (isGamePaused)
        {
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), background);

            GUI.backgroundColor = Color.clear;
            if (GUI.Button(logRect, log))
            {
                contentSquareToDraw = "Purple";
            }
            if (GUI.Button(inventoryRect, inventory))
            {
                contentSquareToDraw = "Cyan";
            }
            if (GUI.Button(settingRect, settings))
            {
                contentSquareToDraw = "Green";
            }
            if (GUI.Button(resumeRect, resume))
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                isGamePaused = false;
                Time.timeScale = 1;

                Camera.main.GetComponent<CameraMovement3D>().enabled = true;
                Camera.main.GetComponent<AudioSource>().mute = false;
                foreach (Transform child in GameObject.Find("Flat_UI HUD").gameObject.transform)
                {
                    if (child.GetComponent<FlatUI_HUD>() != null)
                        child.GetComponent<FlatUI_HUD>().enabled = true;
                    if (child.GetComponent<DisplayObjectives>() != null)
                        child.GetComponent<DisplayObjectives>().enabled = true;
                }
                //GameObject.Find("player_flashlight").gameObject.GetComponent<Flashlight>().enabled = true;
                //GameObject.Find("gui texture background").gameObject.GetComponent<GUITexture>().enabled = false;
            }
            if (GUI.Button(exitRect, exit))
            {
                Application.LoadLevel(2);
            }
            GUI.backgroundColor = guiColorOriginal;

            GUI.DrawTexture(logoRect, logo);
            GUI.Label(gamePausedRect, gamePausedText, gamePausedStyle);

            if (contentSquareToDraw == "Purple")
                GUI.DrawTexture(contentSquareRect, contentSquarePurple);
            else if (contentSquareToDraw == "Cyan")
                GUI.DrawTexture(contentSquareRect, contentSquareCyan);
            else if (contentSquareToDraw == "Green")
                GUI.DrawTexture(contentSquareRect, contentSquareGreen);

            if (contentSquareToDraw == "Purple")
            {
                GUI.Label(contentToDisplayRect, allLogs, logStyle);
            }
        }
    }

    void Update()
    {
        Cursor.visible = true;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 1)
            {
                Screen.lockCursor = false;
                Cursor.visible = true;
                isGamePaused = true;
                Time.timeScale = 0;

                Camera.main.GetComponent<CameraMovement3D>().enabled = false;
                Camera.main.gameObject.GetComponent<AudioSource>().mute = true;
                //GameObject.FindGameObjectWithTag(Tags.PLAYER).gameObject.GetComponent<HUD_Stealth>().enabled = false;
                foreach (Transform child in GameObject.Find("Flat_UI HUD").gameObject.transform)
                {
                    if (child.GetComponent<FlatUI_HUD>() != null)
                        child.GetComponent<FlatUI_HUD>().enabled = false;
                    if (child.GetComponent<DisplayObjectives>() != null)
                        child.GetComponent<DisplayObjectives>().enabled = false;
                }
                //GameObject.Find("player_flashlight").gameObject.GetComponent<Flashlight>().enabled = false;
                //GameObject.Find("gui texture background").gameObject.GetComponent<GUITexture>().enabled = true;

                logList = GameObject.FindWithTag(Tags.GAME_CONTROLLER).GetComponent<LogSystem>().logList;
				allLogs = "Logs\n";
                foreach (string eachLog in logList)
                {
                    allLogs += eachLog + "\n";
                }
            }
            else
            {
                Screen.lockCursor = true;
                Cursor.visible = false;
                isGamePaused = false;
                Time.timeScale = 1;

                Camera.main.GetComponent<CameraMovement3D>().enabled = true;
                Camera.main.GetComponent<AudioSource>().mute = false;
                //GameObject.FindGameObjectWithTag(Tags.PLAYER).gameObject.GetComponent<HUD_Stealth>().enabled = true;
                foreach (Transform child in GameObject.Find("Flat_UI HUD").gameObject.transform)
                {
                    if (child.GetComponent<FlatUI_HUD>() != null)
                        child.GetComponent<FlatUI_HUD>().enabled = true;
                    if (child.GetComponent<DisplayObjectives>() != null)
                        child.GetComponent<DisplayObjectives>().enabled = true;
                }
                //GameObject.Find("player_flashlight").gameObject.GetComponent<Flashlight>().enabled = true;
                //GameObject.Find("gui texture background").gameObject.GetComponent<GUITexture>().enabled = false;
            }
        }
    }






}
