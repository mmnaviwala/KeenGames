using UnityEngine;
using System.Collections;

public class SplashScreen : MonoBehaviour 
{

    public float delayTime = 3f;
    public bool done = false; //could be used for doing background work/loading various things
	// Use this for initialization
    void Awake()
    {
        //guiTexture.pixelInset = new Rect(0f, 0f, Screen.width, Screen.height);
        guiTexture.pixelInset = new Rect(0, 0, 
            Screen.width, Screen.height);
    }
	void Start () 
    {
        Debug.Log("Starting");
        
        //Application.LoadLevel("titleMenu");
	}
	
	// Update is called once per frame
	void Update () {
        delayTime -= Time.deltaTime;
        if (delayTime <= 0f)
            Application.LoadLevel("titleMenu");
	}
}
