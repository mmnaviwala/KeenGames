using UnityEngine;
using System.Collections;
using System.Linq;

public class AndGate : LogicGate {
	
	// Update is called once per frame
	void Update () {
        Forward();
	}

    public override void Forward()
    {
        output.hasPower = inputs.All(input => input.hasPower);
    }
}
