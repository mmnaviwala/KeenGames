using UnityEngine;
using System.Collections;

[AddComponentMenu("Scripts/Misc/Tutorial Enemy")]
public class TutorialEnemy : MonoBehaviour
{
    public Texture2D[] pictures;
    GUIStyle headingStyle = new GUIStyle();
    AudioSource music;
    HUD_Stealth playerHUD;
    public Font headingFont;
    bool onTutorial = false, finishedTutorial = false;
    string[] text_top, text_bottom;
    float musicTime;
    int numClicks = 0;
  
    // Use this for initialization
    void Start()
    {
        music = Camera.main.GetComponent<AudioSource>();
        playerHUD = GameObject.FindGameObjectWithTag(Tags.PLAYER).GetComponent<HUD_Stealth>();
        pictures = new Texture2D[3];
        pictures[0] = Resources.Load("picture_seen") as Texture2D;
        pictures[1] = Resources.Load("picture_attack") as Texture2D;
        pictures[2] = Resources.Load("picture_miss") as Texture2D;

        text_top = new string[3];
        text_bottom = new string[3];

        text_top[0] = "Enemies will appear from";
        text_bottom[0] = "the right side of the screen!";

        text_top[1] = "Once the enemy cross the same color bar";
        text_bottom[1] = "hit the corresponding button to attack!";

        text_top[2] = "If the enemy slips by";
        text_bottom[2] = "you will lose some notes!";

        Color headingColor = new Color(0.0F, 0.0F, 0.0F);		// black

        headingStyle.fontSize = (int)Screen.width / 30;
        headingStyle.fontStyle = FontStyle.Bold;
        headingStyle.normal.textColor = headingColor;
        headingStyle.font = headingFont;
        headingStyle.alignment = TextAnchor.MiddleCenter;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.P) && onTutorial)
        {
            ResumeGame();
        }
        if (finishedTutorial)
        {
            Destroy(this.gameObject);
        }

    }

    IEnumerator OnTriggerEnter(Collider other)
    {
        if (other.tag == Tags.PLAYER)
        {
            GameObject.FindGameObjectWithTag(Tags.PLAYER).GetComponent<HUD_Stealth>();
            onTutorial = true;
            music.Pause();
            musicTime = music.time;
            Time.timeScale = 0;
            playerHUD.enabled = false;
        }
        return null;
    }

    void OnGUI()
    {
        if (onTutorial)
        {
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), pictures[numClicks]);
            GUI.Label(new Rect(Screen.width / 12, Screen.height / 40, Screen.width * .8f, Screen.height * .1f), text_top[numClicks], headingStyle);
            GUI.Label(new Rect(Screen.width / 10, Screen.height / 2, Screen.width * .8f, Screen.height * .1f), text_bottom[numClicks], headingStyle);

            if (GUI.Button(new Rect(Screen.width / 2 - Screen.width / 8, Screen.height * .8f, Screen.width / 4, Screen.height / 4), "Next"))
            {
                numClicks++;
                if (numClicks == 3)
                {
                    ResumeGame();
                }
            }
        }
    }

    private void ResumeGame()
    {

        onTutorial = false;
        Time.timeScale = 1;
        music.time = musicTime;
        music.Play();
        finishedTutorial = true;
        playerHUD.enabled = true;
    }
}
