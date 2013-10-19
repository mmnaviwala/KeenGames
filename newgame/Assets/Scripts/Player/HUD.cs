using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour
{
    public Texture greenPressed, greenUnpressed, bluePressed, blueUnpressed,
                   redPressed, redUnpressed, purplePressed, purpleUnpressed,
                   greenBarTexture, blueBarTexture, redBarTexture, purpleBarTexture;
    private Rect greenButtonRect, blueButtonRect, redButtonRect, purpleButtonRect,
                 greenBar,        blueBar,        redBar,        purpleBar,
                 trebleClef,      notesCount;

    bool slowedTime = false;

    public float[] barCache;
    bool firstUpdate = true;
    PlayerMovementBasic player;
    CharacterStats playerStats;
	// Use this for initialization
	void Start () 
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
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
        notesCount = new Rect(0, 0, xx, yy * 2);

        barCache = new float[] {xx * 3f + xx / 8f,//green bar center
                                xx * 4.25f + xx / 8f, //blue bar center
                                xx * 5.5f + xx / 8f,  //red bar center
                                xx * 6.75f + xx / 8f  /*purple bar center*/ };
        firstUpdate = false;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (firstUpdate)
        {

        } 
        if (Input.GetKeyDown(KeyCode.Space))
        {
            player.Jump();
        }

        if (Input.GetKey(KeyCode.A))
        {
            foreach (GameObject enemy in playerStats.vulnerableEnemies)
                if (enemy.GetComponent<EnemyStats>().enemyColor == EnemyStats.EnemyColor.Green)
                    playerStats.GetComponent<CharacterStats>().Attack(enemy);
        }
        if (Input.GetKey(KeyCode.S))
        {
            foreach (GameObject enemy in playerStats.vulnerableEnemies)
                if (enemy.GetComponent<EnemyStats>().enemyColor == EnemyStats.EnemyColor.Blue)
                    playerStats.GetComponent<CharacterStats>().Attack(enemy);
        }
        if (Input.GetKey(KeyCode.D))
        {
            foreach (GameObject enemy in playerStats.vulnerableEnemies)
                if (enemy.GetComponent<EnemyStats>().enemyColor == EnemyStats.EnemyColor.Red)
                    playerStats.GetComponent<CharacterStats>().Attack(enemy);
        }
        if (Input.GetKey(KeyCode.F))
        {
            foreach (GameObject enemy in playerStats.vulnerableEnemies)
                if (enemy.GetComponent<EnemyStats>().enemyColor == EnemyStats.EnemyColor.Purple)
                    playerStats.GetComponent<CharacterStats>().Attack(enemy);
        }

        if(Input.GetKey(KeyCode.T))
        {
            Time.timeScale = slowedTime ? 1f : .25f;
            slowedTime = !slowedTime;
        }
	}

    void OnGUI()
    {
        GUI.Box(greenBar, greenBarTexture);
        GUI.Box(blueBar, blueBarTexture);
        GUI.Box(redBar, redBarTexture);
        GUI.Box(purpleBar, purpleBarTexture);
        //GUI.Box(trebleClef, "TREBLE Clef");
        GUI.Box(notesCount,  "Notes:   " + playerStats.notes +
                            "\nGreen:  " + playerStats.greenNotes +
                            "\nBlue:   " + playerStats.blueNotes +
                            "\nRed:    " + playerStats.redNotes +
                            "\nPurple: " + playerStats.purpleNotes);

        if (GUI.Button(greenButtonRect, greenUnpressed))
        {
            foreach(GameObject enemy in playerStats.vulnerableEnemies)
                if (enemy.GetComponent<EnemyStats>().enemyColor == EnemyStats.EnemyColor.Green)
                    playerStats.GetComponent<CharacterStats>().Attack(enemy);
        }
        if (GUI.Button(blueButtonRect, blueUnpressed))
        {
            foreach (GameObject enemy in playerStats.vulnerableEnemies)
                if (enemy.GetComponent<EnemyStats>().enemyColor == EnemyStats.EnemyColor.Blue)
                    playerStats.GetComponent<CharacterStats>().Attack(enemy);
        }
        if (GUI.Button(redButtonRect, redUnpressed))
        {
            foreach (GameObject enemy in playerStats.vulnerableEnemies)
                if (enemy.GetComponent<EnemyStats>().enemyColor == EnemyStats.EnemyColor.Red)
                    playerStats.GetComponent<CharacterStats>().Attack(enemy);
        }
        if (GUI.Button(purpleButtonRect, purpleUnpressed))
        {
            foreach (GameObject enemy in playerStats.vulnerableEnemies)
                if (enemy.GetComponent<EnemyStats>().enemyColor == EnemyStats.EnemyColor.Purple)
                    playerStats.GetComponent<CharacterStats>().Attack(enemy);
        }
        if (GUI.Button(trebleClef, "Jump"))
        {
            player.Jump();
        }
    }
}
