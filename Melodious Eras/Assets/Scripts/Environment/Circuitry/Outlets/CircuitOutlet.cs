using UnityEngine;
using System.Collections;

[AddComponentMenu("Scripts/Environment/Circuitry/Circuit Outlet")]
public class CircuitOutlet : CircuitNode 
{

    void Awake()
    {
        this.PlugIn(electricGrid);
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
