using UnityEngine;
using System.Collections;

[AddComponentMenu("Scripts/Environment/Circuitry/Power Source")]
public class PowerSource : CircuitNode
{
    bool playerNearby = false;
    private Rect promptRect;
    public GUIStyle promptStyle;
    void Awake()
    {
        promptRect = new Rect(Screen.width / 2 - 100, Screen.height * .75f, 200, 50);
        if (this.electricGrid != null)
            electricGrid.connectedObjects.Add(this);
    }

    void Update()
    {
        if (playerNearby && Input.GetButtonDown(InputType.USE))
        {
            SwitchOnOff();
        }
    }

    void OnGUI()
    {
        if (playerNearby && !this.isBroken)
        {
            GUI.Box(promptRect, "Press [USE] to turn power " + (this.activated ? "off." : "on."), promptStyle);
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
