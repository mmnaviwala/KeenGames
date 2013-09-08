using UnityEngine;
using System.Collections;

public class titleMenu : MonoBehaviour 
{
    Rect startButton, titleBar;
	// Use this for initialization
	void Start () 
    {
        float xx = Screen.width / 10;
        float yy = Screen.height / 10;
        startButton = new Rect(xx * 4f, yy * 4.5f, xx * 2f, yy);
        titleBar = new Rect(xx * 3f, yy * 2, xx * 4f, yy * 2f);
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    void OnGUI()
    {
        GUI.Box(titleBar, "Very Elaborate Title Screen");
        if (GUI.Button(startButton, "Start Game")) 
        {
            Application.LoadLevel("scene2");
        }
    }
}
