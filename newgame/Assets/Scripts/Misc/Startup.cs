using UnityEngine;
using System.Collections;

public class Startup : MonoBehaviour {
    
	// Use this for initialization
	void Start () 
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        Application.LoadLevel("titleMenu");
        
	}
}
