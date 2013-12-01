using UnityEngine;
using System.Collections;

public class CircuitNode : MonoBehaviour 
{
    public bool hasPower = false;
    public bool activated = false;
    public bool isBroken = false;
    public CircuitSwitch connectedSwitch;
    public ElectricGrid electricGrid;
    public AudioClip activateSound, breakingSound;
    public int durability = -1; //-1 = invulnerable

    void Awake()
    {
 
    }
	// Use this for initialization
	void Start () 
    {

	}
	
	// Update is called once per frame
	void Update () 
    {
	}

    /// <summary>
    /// Performs an action based on the node type.
    /// Alarm: counts down on False signal
    /// Move: moves toward one destination on true, other on false.
    /// Light: light.enabled = signal.
    /// </summary>
    /// <param name="signal"></param>
    /// <returns></returns>
    public virtual bool PerformSwitchAction(bool signal)
    {
        return false;
    }

    public virtual void TurnOnOff(bool on)
    {
        this.hasPower = on;
    }

    public virtual void TakeDamage(int damage)
    {
        if (durability != -1)
        {
            durability -= (durability > damage) ? damage : durability;
            //break animation/sound
        }
    }
}
