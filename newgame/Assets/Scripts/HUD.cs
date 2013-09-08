using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour 
{
    Rect greenButton, blueButton, redButton, purpleButton,
         greenBar,    blueBar,    redBar,    purpleBar,
         trebleClef;
	// Use this for initialization
	void Start () 
    {
        float xx = Screen.width / 10;
        float yy = Screen.height / 10;

        greenButton = new Rect(xx, yy * 8, xx / 2, yy);
        blueButton = new Rect(xx* 2, yy * 8, xx / 2, yy);
        redButton = new Rect(xx * 7.5f, yy * 8, xx / 2, yy);
        purpleButton = new Rect(xx * 8.5f, yy * 8, xx / 2, yy);

        greenBar = new Rect(xx * 3f, 0, xx / 4, Screen.height);
        blueBar = new Rect(xx * 4.25f, 0, xx / 4, Screen.height);
        redBar = new Rect(xx * 5.5f, 0, xx / 4, Screen.height);
        purpleBar = new Rect(xx * 6.75f, 0, xx / 4, Screen.height);

        trebleClef = new Rect(xx * 3f, yy * 8, xx * 4, yy * 2);
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
            Debug.Log("Green pressed");
        }
        if (GUI.Button(blueButton, "Blue"))
        {
            Debug.Log("Blue pressed");
        }
        if (GUI.Button(redButton, "Red"))
        {
            Debug.Log("Red pressed");
        }
        if (GUI.Button(purpleButton, "Purple"))
        {
            Debug.Log("Purple pressed");
        }
    }
}
