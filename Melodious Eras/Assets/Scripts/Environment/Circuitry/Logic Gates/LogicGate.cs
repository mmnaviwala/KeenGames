﻿using UnityEngine;
using System.Collections;

public abstract class LogicGate : MonoBehaviour 
{
    public CircuitNode[] inputs;
    public CircuitNode output;
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public abstract void Forward();
}
