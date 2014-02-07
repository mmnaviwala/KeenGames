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
        this.PlugIn(electricGrid);
    }
	// Use this for initialization
	void Start ()
    {
        if (initializeSwitch)
        {
            connectedNodes.ForEach(delegate(CircuitNode node) {
                node.PerformSwitchAction(onOffStatus);
            });
        }
	}
    void Update()
    {
		if (detectionSphere.playerInRange)
        {
            if (Input.GetButtonDown(InputType.USE) && this.hasPower)
            {
                onOffStatus = !onOffStatus;

                connectedNodes.ForEach(delegate(CircuitNode node) {
                    node.PerformSwitchAction(onOffStatus);
                });
            }
        }
    }

    void OnGUI()
    {
		if (detectionSphere.playerInRange && this.hasPower)
            GUI.Box(promptRect, "Press [USE] to switch " + (onOffStatus ? "off" : "on"), promptStyle);
    }
}
