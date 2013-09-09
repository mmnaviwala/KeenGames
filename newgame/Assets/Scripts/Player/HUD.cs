using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour 
{
    Rect greenButton, blueButton, redButton, purpleButton,
         greenBar,    blueBar,    redBar,    purpleBar,
         trebleClef;
	// Use this for initialization
    CharacterStats playerStats;
	void Start () 
    {
        playerStats = this.GetComponent<CharacterStats>();
        float xx = Screen.width / 10;
        float yy = Screen.height / 10;

        greenButton = new Rect(xx, yy * 8f, xx / 2f, yy);
        blueButton = new Rect(xx * 2f, yy * 8f, xx / 2f, yy);
        redButton = new Rect(xx * 7.5f, yy * 8f, xx / 2f, yy);
        purpleButton = new Rect(xx * 8.5f, yy * 8f, xx / 2f, yy);

        greenBar = new Rect(xx * 3f, 0, xx / 4f, Screen.height);
        blueBar = new Rect(xx * 4.25f, 0, xx / 4f, Screen.height);
        redBar = new Rect(xx * 5.5f, 0, xx / 4f, Screen.height);
        purpleBar = new Rect(xx * 6.75f, 0, xx / 4f, Screen.height);

        trebleClef = new Rect(xx * 3f, yy * 8f, xx * 4f, yy * 2f);
	}
	
	// Update is called once per frame
	void Update () 
    {

	}

    void OnGUI()
    {
        GUI.Box(greenBar, "G");
        GUI.Box(blueBar, "B");
        GUI.Box(redBar, "R");
        GUI.Box(purpleBar, "P");
        GUI.Box(trebleClef, "TREBLE Clef");

        if (GUI.Button(greenButton, "Green"))
        {
            foreach(GameObject enemy in playerStats.vulnerableEnemies)
                if (enemy.GetComponent<EnemyMovementBasic>().color == "green")
                    playerStats.GetComponent<CharacterStats>().Attack(enemy);
            Debug.Log("Green pressed");
        }
        if (GUI.Button(blueButton, "Blue"))
        {
            foreach (GameObject enemy in playerStats.vulnerableEnemies)
                if (enemy.GetComponent<EnemyMovementBasic>().color == "blue")
                    playerStats.GetComponent<CharacterStats>().Attack(enemy);
            Debug.Log("Blue pressed");
        }
        if (GUI.Button(redButton, "Red"))
        {
            foreach (GameObject enemy in playerStats.vulnerableEnemies)
                if (enemy.GetComponent<EnemyMovementBasic>().color == "red")
                    playerStats.GetComponent<CharacterStats>().Attack(enemy);
            Debug.Log("Red pressed");
        }
        if (GUI.Button(purpleButton, "Purple"))
        {
            foreach (GameObject enemy in playerStats.vulnerableEnemies)
                if (enemy.GetComponent<EnemyMovementBasic>().color == "purple")
                    playerStats.GetComponent<CharacterStats>().Attack(enemy);
            Debug.Log("Purple pressed");
        }
    }
}
