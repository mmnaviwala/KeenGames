using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum Difficulty { Easy = 0, Medium = 1, Hard = 2 }
public class GameController : MonoBehaviour 
{
	// Use this for initialization
	void Start () 
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        Physics.gravity = new Vector3(0f, -49f, 0f);
        AudioListener.pause = false;
        Time.timeScale = 1;
	}
	
	// Update is called once per frame
	void Update () 
    {
	    
	}
}
