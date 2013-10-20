using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Not yet implemented.
/// </summary>
public class HUDScramble : MonoBehaviour 
{
    public HUD playerHUD;
    public bool scrambling = false;
    bool scrambled = false;
    float timer = 0f;
    float[] initBarCache = null;
    float[] scrambledBarCenters = null;
    float[] scrambledBars = null;
    float[] scrambledButtons = null;

	// Use this for initialization
	void Start () 
    {
        playerHUD = GameObject.FindGameObjectWithTag(Tags.PLAYER).GetComponent<HUD>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (scrambling)
            Scramble();
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == Tags.PLAYER)
        {
            scrambling = true;

            initBarCache = new float[4];
            for (int b = 0; b < 4; b++)
                initBarCache[b] = playerHUD.barCache[b];

            Scramble();
        }
    }
    void OnGUI()
    {
        scrambling = true;

        initBarCache = new float[4];
        for (int b = 0; b < 4; b++)
            initBarCache[b] = playerHUD.barCache[b];
    }

    public void Scramble()
    {
        if (!scrambled)
        {
            
        }
        if (timer < 5f)
        {
            timer += Time.deltaTime;
            
        }
    }
}
