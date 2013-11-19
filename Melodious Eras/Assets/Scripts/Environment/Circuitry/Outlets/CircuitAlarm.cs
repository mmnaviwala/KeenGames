using UnityEngine;
using System.Collections;

public class CircuitAlarm : CircuitNode
{
    public int alarmCounter = -1;
    private int numTries = 0;
	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    public override bool PerformAction(bool correctCode)
    {
        if (hasPower && !correctCode)
        {
            numTries++;
            Debug.Log(this.transform.GetChild(0).name);
            this.transform.GetChild(0).GetComponent<TextMesh>().text = (alarmCounter - numTries).ToString();
            if (numTries == alarmCounter)
            {
                this.light.enabled = true;
                //((Behaviour)(this.GetComponent("Halo"))).enabled = true;
            }
        }
        return false;
    } 
}
