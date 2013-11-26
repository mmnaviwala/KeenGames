using UnityEngine;
using System.Collections;

public class CircuitNode : MonoBehaviour 
{
    public bool hasPower = false;
    public bool activated = false;
    public CircuitSwitch connectedSwitch;
    public ElectricGrid electricGrid;

	// Use this for initialization
	void Start () 
    {

	}
	
	// Update is called once per frame
	void Update () 
    {
	}

    public virtual bool PerformSwitchAction(bool signal)
    {
        return false;
    }

    public virtual void TurnOnOff(bool power)
    {
        this.hasPower = power;
    }
}
