using UnityEngine;
using System.Collections;

[AddComponentMenu("Scripts/Environment/Circuitry/Elevator Call Button")]
public class ElevatorCallButton : CircuitSwitch 
{
    private Rect promptRect;
    public GUIStyle promptStyle;
    public Elevator connectedElevator;
    public int floor;
    void Awake()
    {
        promptRect = new Rect(Screen.width / 2 - 100, Screen.height * .75f, 200, 50);
        if (this.electricGrid != null)
            electricGrid.connectedObjects.Add(this);
    }
	
	// Update is called once per frame
	void Update () 
    {
        if (detectionSphere.playerInRange && Input.GetButtonDown(InputType.USE))
            connectedElevator.Call(floor);
	}

    void OnGUI()
    {
		if (detectionSphere.playerInRange && this.hasPower)
            GUI.Box(promptRect, "Press [USE] to switch " + (onOffStatus ? "off" : "on"), promptStyle);
    }
}
