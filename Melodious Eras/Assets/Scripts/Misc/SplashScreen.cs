using UnityEngine;
using System.Collections;

public class SplashScreen : MonoBehaviour 
{

    public float delayTime = 2f;
    public bool done = false; //could be used for doing background work/loading various things
	
	
	void Start () 
    {
        Debug.Log("Starting");
        
        //Application.LoadLevel("titleMenu");
	}
	
	// Update is called once per frame
	void Update () 
	{
        delayTime -= Time.deltaTime;
        if (delayTime <= 0f)
            Application.LoadLevel("testTitleMenuQuads");
	}
}
