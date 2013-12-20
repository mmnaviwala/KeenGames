using UnityEngine;
using System.Collections;

public class FlipSwitch : CircuitSwitch 
{
    bool inRange = false;
    private Rect promptRect;
    public GUIStyle promptStyle;

    void Awake()
    {
        promptRect = new Rect(Screen.width / 2 - 100, Screen.height * .75f, 200, 50);
        if (this.electricGrid != null)
            electricGrid.connectedObjects.Add(this);
    }
	// Use this for initialization
	void Start ()
    {
        if (initializeSwitch)
        {
            foreach (CircuitNode node in connectedNodes)
            {
                node.PerformSwitchAction(this.onOffStatus);
            }
        }
	}
    void Update()
    {
        if (inRange)
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

    void OnGUI()
    {
        if (inRange && this.hasPower)
            GUI.Box(promptRect, "Press [USE] to switch " + (onOffStatus ? "off" : "on"), promptStyle);
    }
    void OnTriggerEnter(Collider other)
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
