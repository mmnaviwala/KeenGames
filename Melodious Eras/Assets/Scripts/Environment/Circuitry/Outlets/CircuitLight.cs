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
            this.light.enabled = !this.light.enabled;
            yield return new WaitForSeconds(Random.Range(0f, frequency));
        }
    }

    public override void TurnOnOff(bool power)
    {
        this.hasPower = power;
        this.light.enabled = power;
        if (flickering)
            StartCoroutine(Flicker());        
    }
}
