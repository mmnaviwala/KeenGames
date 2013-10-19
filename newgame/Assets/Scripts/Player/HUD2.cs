using UnityEngine;
using System.Collections;

public class HUD2 : MonoBehaviour {
    public Rect greenButton, blueButton, redButton, purpleButton,
         trebleClef, notesCount;
    public Texture greenPressed, greenUnpressed, bluePressed, blueUnpressed,
                   redPressed, redUnpressed, purplePressed, purpleUnpressed;

    bool slowedTime = false;

    public float[] barCache;
    bool firstUpdate = true;
    PlayerMovementBasic player;
    CharacterStats playerStats;
    // Use this for initialization
    void Start()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        player = this.GetComponent<PlayerMovementBasic>();
        playerStats = this.GetComponent<CharacterStats>();
        barCache = new float[4];
    }

    // Update is called once per frame
    void Update()
    {
        if (firstUpdate)
        {
            float xx = Screen.width / 10;
            float yy = Screen.height / 10;

            greenButton = new Rect(xx * .25f, yy * 8f, xx, xx);
            blueButton = new Rect(xx * 1.5f, yy * 8f, xx, xx);
            redButton = new Rect(xx * 7.5f, yy * 8f, xx, xx);
            purpleButton = new Rect(xx * 8.75f, yy * 8f, xx, xx);

            trebleClef = new Rect(xx * 3f, yy * 8f, xx * 4f, yy * 2f);
            notesCount = new Rect(0, 0, xx, yy * 2);

            barCache = new float[] {xx * 3f + xx / 8f,//green bar center
                                xx * 4.25f + xx / 8f, //blue bar center
                                xx * 5.5f + xx / 8f,  //red bar center
                                xx * 6.75f + xx / 8f  /*purple bar center*/ };
            firstUpdate = false;
        }

        if (Input.GetKey(KeyCode.A))
        {
            foreach (GameObject enemy in playerStats.vulnerableEnemies)
                if (enemy.GetComponent<EnemyMovementBasic2>().color == "green")
                    playerStats.GetComponent<CharacterStats>().Attack(enemy);
            Debug.Log("Green pressed");
        }
        if (Input.GetKey(KeyCode.S))
        {
            foreach (GameObject enemy in playerStats.vulnerableEnemies)
                if (enemy.GetComponent<EnemyMovementBasic2>().color == "blue")
                    playerStats.GetComponent<CharacterStats>().Attack(enemy);
            Debug.Log("Blue pressed");
        }
        if (Input.GetKey(KeyCode.D))
        {
            foreach (GameObject enemy in playerStats.vulnerableEnemies)
                if (enemy.GetComponent<EnemyMovementBasic2>().color == "red")
                    playerStats.GetComponent<CharacterStats>().Attack(enemy);
            Debug.Log("Red pressed");
        }
        if (Input.GetKey(KeyCode.F))
        {
            foreach (GameObject enemy in playerStats.vulnerableEnemies)
                if (enemy.GetComponent<EnemyMovementBasic2>().color == "purple")
                    playerStats.GetComponent<CharacterStats>().Attack(enemy);
            Debug.Log("Purple pressed");
        }

        if (Input.GetKey(KeyCode.T))
        {
            Time.timeScale = slowedTime ? 1f : .25f;
            slowedTime = !slowedTime;
        }
    }

    void OnGUI()
    {
        //GUI.Box(trebleClef, "TREBLE Clef");
        GUI.Box(notesCount, "Notes:   " + playerStats.notes +
                            "\nGreen:  " + playerStats.greenNotes +
                            "\nBlue:   " + playerStats.blueNotes +
                            "\nRed:    " + playerStats.redNotes +
                            "\nPurple: " + playerStats.purpleNotes);

        if (GUI.Button(greenButton, greenUnpressed))
        {
            foreach (GameObject enemy in playerStats.vulnerableEnemies)
                if (enemy.GetComponent<EnemyStats>().enemyColor == EnemyStats.EnemyColor.Green)
                    playerStats.GetComponent<CharacterStats>().Attack(enemy);
        }
        if (GUI.Button(blueButton, blueUnpressed))
        {
            foreach (GameObject enemy in playerStats.vulnerableEnemies)
                if (enemy.GetComponent<EnemyStats>().enemyColor == EnemyStats.EnemyColor.Blue)
                    playerStats.GetComponent<CharacterStats>().Attack(enemy);
        }
        if (GUI.Button(redButton, redUnpressed))
        {
            foreach (GameObject enemy in playerStats.vulnerableEnemies)
                if (enemy.GetComponent<EnemyStats>().enemyColor == EnemyStats.EnemyColor.Red)
                    playerStats.GetComponent<CharacterStats>().Attack(enemy);
        }
        if (GUI.Button(purpleButton, purpleUnpressed))
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
