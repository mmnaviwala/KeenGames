using UnityEngine;
using System.Collections;

public class PowerSource : CircuitNode 
{
    void Awake()
    {
        //if (activated)
        //    electricGrid.hasPower = true;
    }
	
	// Update is called once per frame
	void Update () 
    {
	    
	}

    void OnTriggerStay(Collider other)
    {
        if (other.tag == Tags.PLAYER)
        {
            if(Input.GetButtonDown(InputType.USE))
            {
                SwitchOnOff();
            }
        }
    }

    void SwitchOnOff()
    {
        this.activated = !activated;
        this.electricGrid.UpdatePowerSource();
    }
}
