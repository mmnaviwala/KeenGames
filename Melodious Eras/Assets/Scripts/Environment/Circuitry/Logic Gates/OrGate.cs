using UnityEngine;
using System.Collections;
using System.Linq;

public class OrGate : LogicGate {
	// Update is called once per frame
	void Update () {
	
	}

    public override void Forward()
    {
        output.hasPower = inputs.Any(input => input.hasPower);
    }
}
