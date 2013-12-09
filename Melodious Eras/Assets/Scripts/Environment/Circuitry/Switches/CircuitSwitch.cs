using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CircuitSwitch : CircuitNode 
{
    public bool onOffStatus = false;
    public List<CircuitNode> connectedNodes;

    void Awake()
    {
        if (this.electricGrid != null)
            electricGrid.connectedObjects.Add(this);
    }
	// Use this for initialization
	void Start () 
    {
	    
	}
	
	// Update is called once per frame
	void Update () 
    {
	    
	}
}
