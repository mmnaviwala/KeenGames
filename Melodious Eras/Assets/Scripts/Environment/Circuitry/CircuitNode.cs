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

    /// <summary>
    /// Performs an action based on the node type.
    /// Alarm: counts down on False signal
    /// Move: moves toward one destination on true, other on false.
    /// Light: light.enabled = signal.
    /// </summary>
    /// <param name="signal"></param>
    /// <returns></returns>
    public virtual bool PerformSwitchAction(bool signal)
    {
        return false;
    }

    public virtual void TurnOnOff(bool on)
    {
        this.hasPower = on;
    }
}
