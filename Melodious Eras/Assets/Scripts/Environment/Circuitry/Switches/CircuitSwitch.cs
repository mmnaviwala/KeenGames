using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[AddComponentMenu("Scripts/Environment/Circuitry/Circuit Switch")]
public class CircuitSwitch : CircuitNode
{
    public bool initializeSwitch = true;
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
