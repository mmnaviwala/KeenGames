﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum Difficulty { Easy = 0, Medium = 1, Hard = 2 }
public class GameController : MonoBehaviour
{
    public Difficulty difficulty = Difficulty.Medium;
    public Vector3 wind;
    public float turbulence = 0;

    // Use this for initialization
    void Awake()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        //Physics.gravity = new Vector3(0f, -49f, 0f);
        AudioListener.pause = false;
        Time.timeScale = 1;

        InitializeEnvironment();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void InitializeEnvironment()
    {
        Environment.SetWind(this.wind, this.turbulence);
        StartCoroutine(Environment.BlowWind());
    }
}
