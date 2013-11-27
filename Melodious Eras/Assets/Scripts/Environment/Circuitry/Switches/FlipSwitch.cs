using UnityEngine;
using System.Collections;

public class FlipSwitch : CircuitSwitch 
{
    
	// Use this for initialization
	void Start () 
    {
	
	}

    void OnTriggerStay(Collider other)
    {
        if (other.tag == Tags.PLAYER)
        {
            if (Input.GetButtonDown(InputType.USE))
            {
                onOffStatus = !onOffStatus;
                foreach (CircuitNode node in connectedNodes)
                {
                    node.PerformSwitchAction(onOffStatus);
                }
            }
        }
    }
}
