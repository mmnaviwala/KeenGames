using UnityEngine;
using System.Collections;

public class FlipSwitch : CircuitSwitch 
{
    bool inRange = false;

    void Awake()
    {
        if (this.electricGrid != null)
            electricGrid.connectedObjects.Add(this);
    }
	// Use this for initialization
	void Start () 
    {
        foreach (CircuitNode node in connectedNodes)
        {
            node.PerformSwitchAction(this.onOffStatus);
        }
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
