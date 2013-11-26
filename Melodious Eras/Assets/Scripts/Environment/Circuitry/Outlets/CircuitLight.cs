using UnityEngine;
using System.Collections;

public class CircuitLight : CircuitNode
{
    public bool flickering = false;
    public float frequency = .5f;
	// Use this for initialization
	void Start () 
    {
        if (flickering)
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
        while (this.hasPower)
        {
            this.light.enabled = false;
            yield return new WaitForSeconds(.1f);
            this.light.enabled = true;
            yield return new WaitForSeconds(Random.Range(0f, frequency));
        }
    }
    public override bool PerformSwitchAction(bool signal)
    {
        this.TurnOnOff(signal);
        return false;
    }

    public override void TurnOnOff(bool power)
    {
        this.hasPower = power;
        this.light.enabled = power;
        if (flickering)
            StartCoroutine(Flicker());        
    }
}
