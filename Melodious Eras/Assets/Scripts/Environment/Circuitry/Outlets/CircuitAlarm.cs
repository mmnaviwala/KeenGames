using UnityEngine;
using System.Collections;

[AddComponentMenu("Scripts/Environment/Circuitry/Circuit Alarm")]
public class CircuitAlarm : CircuitNode
{
    public int alarmCounter = -1;
    private int numTries = 0;

    void Awake()
    {
        if (this.electricGrid != null)
            electricGrid.connectedObjects.Add(this);
    }
	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    public override bool PerformSwitchAction(bool signal)
    {
        if (hasPower && !signal)
        {
            numTries++;
            Debug.Log(this.transform.GetChild(0).name);
            this.transform.GetChild(0).GetComponent<TextMesh>().text = (alarmCounter - numTries).ToString();
            if (numTries == alarmCounter)
            {
                this.light.enabled = true;
            }
        }
        return false;
    } 
}
