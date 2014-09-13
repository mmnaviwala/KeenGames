using UnityEngine;
using System.Collections;

[AddComponentMenu("Scripts/Non-recurring/Test Title Menu Script")]
public class testTitleMenuScript : MonoBehaviour
{

    Rect startButton, exitButton, titleBar;
    bool musicSwitch = false;
    AudioSource[] music;
    public float yPosBackground = 0,
                 scrollSpeed = .004f;
    public Font headingFont, otherHeadingFont;

    GUIStyle headingStyle = new GUIStyle();
    GUIStyle otherStyle = new GUIStyle();
    

    void Awake()
    {
        music = Camera.main.GetComponents<AudioSource>();
    }
    // Use this for initialization
    void Start()
    {
        transform.localScale = new Vector3(Screen.width / 45, Screen.height / 20, 0);
        yPosBackground = -5;

        headingStyle.fontSize = 60;
        headingStyle.fontStyle = FontStyle.Bold;
        headingStyle.normal.textColor = Color.red;
        headingStyle.font = headingFont;
        headingStyle.alignment = TextAnchor.MiddleCenter;

        otherStyle.fontSize = 36;
        otherStyle.normal.textColor = Color.blue;
        otherStyle.font = otherHeadingFont;
        otherStyle.alignment = TextAnchor.MiddleCenter;

        float xx = Screen.width / 10;
        float yy = Screen.height / 10;
        startButton = new Rect(xx * 3f, yy * 5.5f, xx * 4f, yy);
        exitButton = new Rect(xx * 3f, yy * 8f, xx * 4f, yy);
        titleBar = new Rect(xx * 3f, yy * 2, xx * 4f, yy * 2f);

        music[1].Stop();
        music[0].Play();
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(Screen.width / 45, Screen.height / 20, 0);
        transform.localPosition = new Vector3(0, yPosBackground, 0);
        yPosBackground += scrollSpeed;

        if (yPosBackground > 7.25f)
            scrollSpeed = -.004f;
        else if (yPosBackground < -5.25f)
            scrollSpeed = .004f;

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
        //GUI.Box(titleBar, "Melodious Eras", headingStyle);
        if (GUI.Button(startButton, "Start Game", otherStyle))
        {
            Application.LoadLevel("scene2");
        }
        if (GUI.Button(exitButton, "Exit Game", otherStyle))
            Application.Quit();

    }
}
