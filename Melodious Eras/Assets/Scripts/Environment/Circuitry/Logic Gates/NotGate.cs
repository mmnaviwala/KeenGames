using UnityEngine;
using System.Collections;

public class NotGate : LogicGate {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public override void Forward()
    {
        output.hasPower = !inputs[0].hasPower;
    }
}
