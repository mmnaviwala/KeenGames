using UnityEngine;
using System.Collections;

public class PowerSource : CircuitNode
{
    void Awake()
    {
        if (this.electricGrid != null)
            electricGrid.connectedObjects.Add(this);
    }

    void Update()
    {
 
    }

    void OnTriggerStay(Collider other)
    {
        if (other is CapsuleCollider && other.tag == Tags.PLAYER)
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

    public override void TakeDamage(int damage)
    {
        if (durability != -1)
        {
            durability -= (durability > damage) ? damage : durability;
            if (durability == 0)
            {
                this.activated = false;
                this.electricGrid.UpdatePowerSource();
                this.isBroken = true;
            }
        }
    }
}
