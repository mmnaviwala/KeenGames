using UnityEngine;
using System.Collections.Generic;

[AddComponentMenu("Scripts/Environment/Circuitry/Elevator")]
public class Elevator : CircuitMove 
{
    //wrapper for each passenger in the elevator; just keeps track of their trigger count.
    private class Passenger
    {
        public Transform passengerTransform;
        public int triggerCount;

        public Passenger(Transform passenger)
        {
            this.passengerTransform = passenger;
            triggerCount = 0;
        }
    }

    private List<Passenger> passengers = new List<Passenger>();
    private int moveUpDown = 0; //1 = up, -1 = down
    private bool doorsClosed = true;
    private bool openingClosing = false;
    public Transform leftDoor, rightDoor;

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
		StartCoroutine(CloseDoors());
    }
	void Update()
	{

	}

    //public int triggerCount = 0; //2 triggers in elevator. Avoiding OnTriggerExit action if still inside
    void OnTriggerEnter(Collider other)
    {
        if (!other.isTrigger && other is CapsuleCollider)
        {
            Passenger passenger = passengers.Find((Passenger p) => p.passengerTransform.Equals(other.transform));
            if (passenger == null)
            {
                passenger = new Passenger(other.transform);
                passengers.Add(passenger);
                other.transform.parent = this.transform;
                Debug.Log("New passenger: " + passengers.Count);
            }
            passenger.triggerCount++;

            Debug.Log("Trigger count: " + passengers[passengers.Count - 1].triggerCount);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.isTrigger && other is CapsuleCollider)
        {
            Passenger passenger = passengers.Find(p => p.passengerTransform.Equals(other.transform));
            passenger.triggerCount--;
            Debug.Log("Trigger count: " + passenger.triggerCount);
            if (passenger.triggerCount == 0)
            {
                passengers.Remove(passenger);
                other.transform.parent = null;
            }
        }
    }

    public override bool PerformSwitchAction(bool signal)
    {
        //needs to close/open doors too
        return false;
    }

    protected override void Move()
    {
        if (Vector3.Distance(this.transform.position, destinations[destIndex]) > .05f ||
            (moveUpDown == 1 && this.transform.position.y < destinations[destIndex].y) ||
            (moveUpDown == -1 && this.transform.position.y > destinations[destIndex].y))
        {
            this.transform.position += directions[destIndex] * movementSpeed * Time.deltaTime;
        }
        else
        {
            this.transform.position = destinations[destIndex];
            activated = false;
			StartCoroutine(OpenDoors());
        }
    }
	private IEnumerator<Coroutine> MoveElevator ()
	{
		yield return StartCoroutine(CloseDoors());
		Move ();
	}

	IEnumerator<YieldInstruction> CloseDoors()
	{
		while(leftDoor.localPosition.x >= 1.25f)
		{			
			leftDoor.localPosition = new Vector3(leftDoor.localPosition.x - Time.deltaTime, leftDoor.localPosition.y, leftDoor.localPosition.z);
			rightDoor.localPosition = new Vector3(rightDoor.localPosition.x + Time.deltaTime, rightDoor.localPosition.y, rightDoor.localPosition.z);
			yield return new WaitForEndOfFrame();
		}
		Debug.Log("Done Closing");
		leftDoor.localPosition = new Vector3(1.25f, leftDoor.localPosition.y, leftDoor.localPosition.z);
		rightDoor.localPosition = new Vector3(-1.25f, leftDoor.localPosition.y, leftDoor.localPosition.z);
	}
	IEnumerator<YieldInstruction> OpenDoors()
	{
		while(leftDoor.localPosition.x <= 3.5f)
		{
			leftDoor.localPosition = new Vector3(leftDoor.localPosition.x + Time.deltaTime, leftDoor.localPosition.y, leftDoor.localPosition.z);
			rightDoor.localPosition = new Vector3(rightDoor.localPosition.x - Time.deltaTime, rightDoor.localPosition.y, rightDoor.localPosition.z);
			yield return new WaitForEndOfFrame();
		}
		leftDoor.localPosition = new Vector3(3.5f, leftDoor.localPosition.y, leftDoor.localPosition.z);
		rightDoor.localPosition = new Vector3(-3.5f, rightDoor.localPosition.y, rightDoor.localPosition.z);
	}

    /// <summary>
    /// Calls the elevator to this floor.
    /// </summary>
    /// <param name="floorIndex"></param>
    public void Call(int floorIndex)
    {
        destIndex = floorIndex;
		StartCoroutine(MoveElevator());
        Debug.Log("Elevator called to floor " + floorIndex);
    }
}
