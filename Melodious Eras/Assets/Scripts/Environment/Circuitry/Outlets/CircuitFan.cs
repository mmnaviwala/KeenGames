using UnityEngine;
using System.Collections;

[AddComponentMenu("Scripts/Environment/Circuitry/Circuit Fan")]
public class CircuitFan : CircuitNode 
{
    public Transform blades;        //Will rotate the blades when powered
    float angularSpeed = 0;
    public float maxSpeed;          //max anguler speed
    public float acceleration;      //angular acceleration

    void Awake()
    {
        this.PlugIn(electricGrid);
    }
	
	// Update is called once per frame
	void Update () 
    {
        if (angularSpeed > 0)
            blades.Rotate(blades.up, angularSpeed * Time.deltaTime);
	}

    public override bool PerformSwitchAction(bool signal)
    {
        bool temp = activated;
        activated = signal && !isBroken;
        if (temp != activated)
        {
            this.StopAllCoroutines();
            StartCoroutine(activated ? SpeedUp() : SlowDown());
        }
        return false;
    }

    IEnumerator SpeedUp()
    {
        while (angularSpeed < maxSpeed)
        {
            angularSpeed += acceleration * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        angularSpeed = maxSpeed;
    }

    IEnumerator SlowDown()
    {
        while (angularSpeed > 0)
        {
            angularSpeed -= acceleration * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        angularSpeed = 0;
    }

    public override void TakeDamage(int damage)
    {
        if (this.durability != -1)
        {
            durability -= damage;
            if (durability <= 0)
            {
                this.isBroken = true;
                this.StopAllCoroutines();
                this.StartCoroutine(SlowDown());
            }
        }
    }
}
