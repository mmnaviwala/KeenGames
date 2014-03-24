using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[AddComponentMenu("Scripts/Environment/Circuitry/Electric Grid")]
public class ElectricGrid : MonoBehaviour 
{
    public bool hasPower = false;
    public List<CircuitNode> connectedObjects;
    private List<PowerSource> powerSources;

    void Awake()
    {
        powerSources = new List<PowerSource>();

        //Registering all power sources first

        //connectedObjects.ForEach(delegate(CircuitNode node)
        foreach(CircuitNode node in connectedObjects)
        {
            if (node is PowerSource)
            {
                powerSources.Add(node as PowerSource);
                if (node.activated)
                    this.hasPower = true;
            }
        };

        foreach (CircuitNode node in connectedObjects)
            node.hasPower = this.hasPower;
        /*connectedObjects.ForEach(delegate(CircuitNode node) {
            node.hasPower = this.hasPower;
        });*/
    }

    /// <summary>
    /// Updates status of power sources. If all power sources are disabled, whole grid loses power.
    /// Only bothers updating every node if the power state actually changes.
    /// </summary>
    public void UpdatePowerSource()
    {
        bool previousPowerState = hasPower;
        hasPower = powerSources.Exists((PowerSource source) => source.activated && !source.isBroken);

        if (hasPower != previousPowerState)
        {
            //connectedObjects.ForEach(delegate(CircuitNode node) 
            foreach(CircuitNode node in connectedObjects)
            { 
                node.hasPower = this.hasPower; 
                node.TurnOnOff(hasPower);
            };
        }
    }
}
