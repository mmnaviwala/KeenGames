using UnityEngine;
using System.Collections;

public class FlipSwitch : CircuitSwitch 
{
    bool inRange = false;
	// Use this for initialization
	void Start () 
    {
	
	}
    void Update()
    {
        if (inRange)
        {
            if (Input.GetButtonDown(InputType.USE))
            {
                Debug.Log("Flipped Switch");
                onOffStatus = !onOffStatus;
                foreach (CircuitNode node in connectedNodes)
                {
                    node.PerformSwitchAction(onOffStatus);
                }
            }
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other is CapsuleCollider && other.tag == Tags.PLAYER)
            inRange = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other is CapsuleCollider && other.tag == Tags.PLAYER)
            inRange = false;
    }
}
