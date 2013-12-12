using UnityEngine;
using System.Collections;

public class CircuitFan : CircuitNode 
{
    public Transform blades;
    float speed = 0;
    public float maxSpeed;
    public float acceleration;
    void Awake()
    {
        this.PlugIn(electricGrid);
    }
	// Use this for initialization
	void Start () 
    {
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (speed > 0)
        {
            blades.Rotate(blades.up, speed * Time.deltaTime);
        }
	}

    public override bool PerformSwitchAction(bool signal)
    {
        bool temp = activated;
        activated = signal && !isBroken;
        if(temp != activated)
            StartCoroutine(activated ? SpeedUp() : SlowDown());
        return false;
    }

    IEnumerator SpeedUp()
    {
        //speed = 0;
        while (speed < maxSpeed)
        {
            speed += acceleration * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        speed = maxSpeed;
    }

    IEnumerator SlowDown()
    {
        while (speed > 0)
        {
            speed -= acceleration * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        speed = 0;
    }
}
