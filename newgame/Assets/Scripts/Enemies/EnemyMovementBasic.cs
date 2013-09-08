using UnityEngine;
using System.Collections;

public class EnemyMovementBasic : MonoBehaviour 
{
    private Camera mainCam;
    private EnemyStats stats;

    private enum EnemyColor { Green = 0, Blue = 1, Red = 2, Purple = 3 }
    private float[] barCache = new float[4];
    private bool isVulnerable = false;

    private string color = "";
	// Use this for initialization
	void Start () 
    {
        float xx = Screen.width / 10;

        mainCam = Camera.main;
        stats = this.GetComponent<EnemyStats>();
        barCache[0] = xx * 3f + xx / 8f;    //green bar center
        barCache[1] = xx * 4.25f + xx / 8f; //blue bar center
        barCache[2] = xx * 5.5f + xx / 8f;  //red bar center
        barCache[3] = xx * 6.75f + xx / 8f; //purple bar center

        for (int i = 0; i < 4; i++)
            Debug.Log("Bar " + i + " position " + barCache[i]);
        /*
        greenBar = new Rect(xx * 3f, 0, xx / 4f, Screen.height);
        blueBar = new Rect(xx * 4.25f, 0, xx / 4f, Screen.height);
        redBar = new Rect(xx * 5.5f, 0, xx / 4f, Screen.height);
        purpleBar = new Rect(xx * 6.75f, 0, xx / 4f, Screen.height);
        */

        switch (stats.enemyColor)
        {
            case 0: color = "green"; break;
            case 1: color = "blue"; break;
            case 2: color = "red"; break;
            case 3: color = "purple"; break;
        }
	}

    // Update is called once per frame
    void Update() 
    {
        this.transform.position = new Vector3(transform.position.x - 2f * Time.deltaTime, transform.position.y, transform.position.z);

        float myScreenXPos = mainCam.WorldToScreenPoint(this.transform.position).x;
        //if target is (approx) touching corresponding bar
        if (myScreenXPos < barCache[stats.enemyColor] + Screen.width / 20 && myScreenXPos > barCache[stats.enemyColor] - Screen.width / 20)
        {
            //if they've just entered
            if (isVulnerable == false)
            {
                isVulnerable = true;
                Debug.Log(color + " is vulnerable");
                this.transform.localScale = new Vector3(1, .1f, 1);
            }
        }
        //when they leave
        else if (isVulnerable == true)
        {
            isVulnerable = false;
            Debug.Log(color + " is no longer vulnerable");

            this.transform.localScale = new Vector3(1, 1, 1);
        }
	}
}
