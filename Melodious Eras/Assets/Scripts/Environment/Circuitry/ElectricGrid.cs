using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ElectricGrid : MonoBehaviour 
{
    
    public bool hasPower = false;
    private bool isSwitching = true;
    public List<CircuitNode> connectedObjects;
    private List<PowerSource> powerSources;

    void Awake()
    {
        powerSources = new List<PowerSource>();

        connectedObjects.ForEach(delegate(CircuitNode node)
        {
            if (this.hasPower)
                node.hasPower = true;
            if (node is PowerSource)
            {
                powerSources.Add(node as PowerSource);
                if (node.activated) 
                    this.hasPower = true;
            }
        });
        if (this.hasPower)
        {
            connectedObjects.ForEach(delegate(CircuitNode node)
            {
                //node.hasPower = this.hasPower; 
                node.TurnOnOff(hasPower);
            });
        }
        /*foreach (CircuitNode node in connectedObjects)
        {
            if (this.hasPower)
                node.hasPower = true;
            if (node is PowerSource)
                powerSources.Add(node as PowerSource);
        }*/
        Debug.Log("Power Sources: " + powerSources.Count);

        this.enabled = false;
    }

    public void UpdatePowerSource()
    {
        bool previousPowerState = hasPower;
        hasPower = powerSources.Exists((PowerSource source) => source.activated);
        Debug.Log(previousPowerState + " -> " + hasPower);
        if (hasPower != previousPowerState)
        {
            connectedObjects.ForEach(delegate(CircuitNode node) { 
                //node.hasPower = this.hasPower; 
                node.TurnOnOff(hasPower);
            });
        }
    }
}
