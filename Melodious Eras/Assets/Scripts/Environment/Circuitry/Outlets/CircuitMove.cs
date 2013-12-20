using UnityEngine;
using System.Collections;

public class CircuitMove : CircuitNode
{
    public Vector3 moveDirection;
    public Vector3[] destinations; //should always have at least 2 (original + 1 or more destinations)
    protected Vector3[] directions;

    protected int destIndex = 0;
    protected Vector3 targetPos, resetPos;
    public float movementSpeed = .5f;

    void Awake()
    {
        if (this.electricGrid != null)
            electricGrid.connectedObjects.Add(this);

        if (destinations != null)
        {
            directions = new Vector3[destinations.Length];
            for (int d = 0; d < destinations.Length; d++)
            {
                Vector3 start = destinations[d];
                Vector3 end = destinations[(d + 1) % destinations.Length];
                directions[(d + 1) % directions.Length] = (end - start).normalized;
            }
        }
    }
	// Use this for initialization
	void Start () 
    {
        targetPos = this.transform.position + moveDirection;
        resetPos = this.transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (activated)
        {
            Move();
        }
	}

    public override bool PerformSwitchAction(bool signal)
    {
        if (isBroken || !hasPower)
            return false;

        base.PerformSwitchAction(signal);

        if (activated)
            destIndex = (destIndex > 0) ? destIndex - 1 : destinations.Length - 1;//destIndex = (destIndex - 1) % destinations.Length;    //will reverse movement if already in action once it receives a new signal
        else
            destIndex = (destIndex + 1) % destinations.Length;
        Debug.Log("Dest Index: " + destIndex);

        activated = true;
        Move();
        return false; 
    }

    private void Move()
    {
        if (Vector3.Distance(this.transform.position, destinations[destIndex]) > .15f)
            this.transform.position += directions[destIndex] * movementSpeed * Time.deltaTime;
        else
        {
            this.transform.position = destinations[destIndex];
            activated = false;
        }
    }
}
