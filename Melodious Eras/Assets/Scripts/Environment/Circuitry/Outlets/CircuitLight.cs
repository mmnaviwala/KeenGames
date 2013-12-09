using UnityEngine;
using System.Collections;

public class CircuitLight : CircuitNode
{
    public Light lightbulb;
    public bool flickering = false;
    public float frequency = .5f;
	// Use this for initialization

    void Awake()
    {
        if (this.electricGrid != null)
            electricGrid.connectedObjects.Add(this);
        if (lightbulb == null)
            lightbulb = this.light; 
    }
	void Start () 
    {
        lightbulb.enabled = this.hasPower && this.activated && !this.isBroken;
        if (this.hasPower && !isBroken && this.activated && flickering)
            StartCoroutine(Flicker());
	}

    IEnumerator Flicker()
    {
        while (this.hasPower && activated && !isBroken)
        {
            this.lightbulb.enabled = false;
            yield return new WaitForSeconds(.1f);
            this.lightbulb.enabled = true;
            yield return new WaitForSeconds(Random.Range(0f, frequency));
        }
        this.lightbulb.enabled = false; //light needs to turn off once power is lost
    }
    public override bool PerformSwitchAction(bool signal)
    {
        activated = signal && !isBroken;
        this.lightbulb.enabled = hasPower && signal && !isBroken;
        if (lightbulb.enabled && flickering)
        {
            StartCoroutine(Flicker());
        }
        return false;
    }

    public override void TurnOnOff(bool on)
    {
        PerformSwitchAction(on);
    }

    public override void TakeDamage(int damage)
    {
        if (durability != -1)
        {
            durability -= damage;
            if (durability <= 0)
            {
                this.lightbulb.enabled = false;
                this.isBroken = true;
            }
        }
    }
}
