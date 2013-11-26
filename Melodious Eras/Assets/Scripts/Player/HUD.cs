using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour
{
    public Texture greenPressed,    greenUnpressed, bluePressed,   blueUnpressed,
                   redPressed,      redUnpressed,   purplePressed, purpleUnpressed,
                   greenBarTexture, blueBarTexture, redBarTexture, purpleBarTexture;

    private Rect   greenButtonRect, blueButtonRect, redButtonRect, purpleButtonRect,
                   greenBar,        blueBar,        redBar,        purpleBar,
                   trebleClef,      notesCount;

    bool slowedTime = false;
    

    public float[] barCache;
    public GUIStyle myStyle;
    PlayerMovementBasic player;
    CharacterStats playerStats;
    public bool hide = false;
	// Use this for initialization
    void Awake()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
    }
	void Start () 
    {
        player = this.GetComponent<PlayerMovementBasic>();
        playerStats = this.GetComponent<CharacterStats>();
        barCache = new float[4];


        float xx = Screen.width / 10;
        float yy = Screen.height / 10;

        greenButtonRect = new Rect(xx * .25f, yy * 8f, xx, xx);
        blueButtonRect = new Rect(xx * 1.5f, yy * 8f, xx, xx);
        redButtonRect = new Rect(xx * 7.5f, yy * 8f, xx, xx);
        purpleButtonRect = new Rect(xx * 8.75f, yy * 8f, xx, xx);

        greenBar = new Rect(xx * 3f, 0, xx / 4f, Screen.height);
        blueBar = new Rect(xx * 4.25f, 0, xx / 4f, Screen.height);
        redBar = new Rect(xx * 5.5f, 0, xx / 4f, Screen.height);
        purpleBar = new Rect(xx * 6.75f, 0, xx / 4f, Screen.height);


        trebleClef = new Rect(xx * 3f, yy * 8f, xx * 4f, yy * 2f);
        notesCount = new Rect(0, 0, xx, yy * 3);

        barCache = new float[] {xx * 3f + xx / 8f,//green bar center
                                xx * 4.25f + xx / 8f, //blue bar center
                                xx * 5.5f + xx / 8f,  //red bar center
                                xx * 6.75f + xx / 8f  /*purple bar center*/ };
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetButtonDown("Jump"))
        {
            player.Jump();
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            foreach (EnemyStats enemy in playerStats.vulnerableEnemies)
                if (enemy.enemyColor == EnemyColor.Green)
                    playerStats.Attack(enemy);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            foreach (EnemyStats enemy in playerStats.vulnerableEnemies)
                if (enemy.enemyColor == EnemyColor.Blue)
                    playerStats.Attack(enemy);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            foreach (EnemyStats enemy in playerStats.vulnerableEnemies)
                if (enemy.enemyColor == EnemyColor.Red)
                    playerStats.Attack(enemy);
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            foreach (EnemyStats enemy in playerStats.vulnerableEnemies)
                if (enemy.enemyColor == EnemyColor.Purple)
                    playerStats.Attack(enemy);
        }
        //Toggles slow-motion
        if(Input.GetKey(KeyCode.T))
        {
            Time.timeScale = slowedTime ? 1f : .25f;
            slowedTime = !slowedTime;
        }
	}

    void OnGUI()
    {
        GUI.color = new Color(1, 1, 1, 0.5f);
        if (!hide)
        {
            GUI.DrawTexture(greenBar, greenBarTexture);
            GUI.DrawTexture(blueBar, blueBarTexture);
            GUI.DrawTexture(redBar, redBarTexture);
            GUI.DrawTexture(purpleBar, purpleBarTexture);
            

            //GUI.color = new Color(1, 1, 1, 1f);
            //If green button is pressed, searches through all vulnerable enemies and attacks all that are green
            //(i.e. all green enemies in the green bar at the time)
            //Same logic for other colors
            if (GUI.Button(greenButtonRect, greenUnpressed, myStyle))
            {
                foreach (EnemyStats enemy in playerStats.vulnerableEnemies)
                    if (enemy.enemyColor == EnemyColor.Green)
                        playerStats.Attack(enemy);
            }
            if (GUI.Button(blueButtonRect, blueUnpressed, myStyle))
            {
                foreach (EnemyStats enemy in playerStats.vulnerableEnemies)
                    if (enemy.enemyColor == EnemyColor.Blue)
                        playerStats.Attack(enemy);
            }
            if (GUI.Button(redButtonRect, redUnpressed, myStyle))
            {
                foreach (EnemyStats enemy in playerStats.vulnerableEnemies)
                    if (enemy.enemyColor == EnemyColor.Red)
                        playerStats.Attack(enemy);
            }
            if (GUI.Button(purpleButtonRect, purpleUnpressed, myStyle))
            {
                foreach (EnemyStats enemy in playerStats.vulnerableEnemies)
                    if (enemy.enemyColor == EnemyColor.Purple)
                        playerStats.Attack(enemy);
            }
        }
        if (GUI.Button(trebleClef, "Jump"))
        {
            player.Jump();
        } 
        
        GUI.Box(notesCount, "Notes:   " + playerStats.notes +
                                 "\nGreen:  " + playerStats.greenNotes +
                                 "\nBlue:   " + playerStats.blueNotes +
                                 "\nRed:    " + playerStats.redNotes +
                                 "\nPurple: " + playerStats.purpleNotes +
                                 "\n FPS:   " + 1/Time.smoothDeltaTime);
    }
}
