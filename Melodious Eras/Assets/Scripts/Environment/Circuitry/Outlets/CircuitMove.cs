using UnityEngine;
using System.Collections;

public class CircuitMove : CircuitNode
{
    public Vector3 moveDirection;
    private Vector3 targetPos, resetPos;
    public float movementSpeed = .5f;
	// Use this for initialization
	void Start () 
    {
        targetPos = this.transform.position + moveDirection;
        resetPos = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (hasPower && activated)
        {
            activated = PerformSwitchAction(true);
        }
	}

    public override bool PerformSwitchAction(bool signal)
    {
        base.PerformSwitchAction(signal);
        activated = signal && !isBroken;
        if (!activated)
            return false;
        if (Vector3.Distance(this.transform.position, targetPos) > .5f)
        {
            //this.transform.position = Vector3.Lerp(this.transform.position, targetPos, movementSpeed * Time.deltaTime);
            this.transform.position += moveDirection * movementSpeed * Time.deltaTime;
            return true;
        }
        else
        {
            this.transform.position = targetPos;
            return false;
        }
    }
}
