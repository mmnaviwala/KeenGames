using UnityEngine;
using System.Collections;

[AddComponentMenu("Scripts/Environment/Circuitry/Flip Switch")]
public class FlipSwitch : CircuitSwitch 
{
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
		if (detectionSphere.playerInRange)
        {
            if (Input.GetButtonDown(InputType.USE) && this.hasPower)
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
		if (detectionSphere.playerInRange && this.hasPower)
            GUI.Box(promptRect, "Press [USE] to switch " + (onOffStatus ? "off" : "on"), promptStyle);
    }
}
