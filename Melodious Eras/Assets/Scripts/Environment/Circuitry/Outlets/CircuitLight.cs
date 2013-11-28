using UnityEngine;
using System.Collections;

public class CircuitLight : CircuitNode
{
    public bool flickering = false;
    public float frequency = .5f;
	// Use this for initialization
	void Start () 
    {
        light.enabled = this.hasPower && this.activated;
        if (this.hasPower && this.activated && flickering)
            StartCoroutine(Flicker());
	}
	
	// Update is called once per frame
	void Update () 
    {
        //if (!this.hasPower)
        //   this.light.enabled = false;
	}

    IEnumerator Flicker()
    {
        while (this.hasPower && activated)
        {
            this.light.enabled = false;
            yield return new WaitForSeconds(.1f);
            this.light.enabled = true;
            yield return new WaitForSeconds(Random.Range(0f, frequency));
        }
        this.light.enabled = false; //light needs to turn off once power is lost
    }
    public override bool PerformSwitchAction(bool signal)
    {
        activated = signal;
        this.light.enabled = hasPower && signal;
        if (light.enabled && flickering)
        {
            StartCoroutine(Flicker());
        }
        return false;
    }

    public override void TurnOnOff(bool on)
    {
        PerformSwitchAction(on);
    }
}
