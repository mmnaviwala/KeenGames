using UnityEngine;
using System.Collections;

public class ComputerTerminal : CircuitOutlet 
{
    public string userName; //for display purposes only
    public string password;

    bool playerNearby = false;
    bool alreadyActivated = false;
    float pressTime = 0;

    public Material onScreen, offScreen;
    public Transform[] monitors;
    

	// Use this for initialization
	void Start () 
    {

	}

    // Update is called once per frame
    void Update() 
    {
        if (playerNearby && this.hasPower)
        {
            if (!alreadyActivated && Input.GetButton(InputType.USE))
            {
                pressTime += Time.deltaTime;
                if (pressTime > .75f)
                {
                    this.activated = !this.activated;
                    pressTime = 0;
                    alreadyActivated = true;

                    for (int m = 0; m < monitors.Length; m++)
                    {
                        monitors[m].renderer.material = activated ? onScreen : offScreen;
                        monitors[m].GetChild(0).light.enabled = activated;
                    }
                }
            }
            if (Input.GetButtonUp(InputType.USE))
            {
                pressTime = 0;
                alreadyActivated = false;
            }
            
        }
        if (this.hasPower && this.activated && playerNearby && Input.GetButtonDown(InputType.USE))
        {
            Debug.Log("Using computer");
        }	    
	}

    void OnTriggerEnter(Collider other)
    {
        if (other is CapsuleCollider && other.tag == Tags.PLAYER)
        {
            playerNearby = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other is CapsuleCollider && other.tag == Tags.PLAYER)
        {
            playerNearby = false;
        }
    }

    public override void TurnOnOff(bool on)
    {
        activated = on;
    }
}
