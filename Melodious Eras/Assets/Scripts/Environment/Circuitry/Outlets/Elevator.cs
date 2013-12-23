using UnityEngine;
using System.Collections.Generic;

[AddComponentMenu("Scripts/Environment/Circuitry/Elevator")]
public class Elevator : MonoBehaviour 
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

    //public int triggerCount = 0; //2 triggers in elevator. Avoiding OnTriggerExit action if still inside
    void OnTriggerEnter(Collider other)
    {
        if (!other.isTrigger && other is CapsuleCollider)
        {
            Passenger passenger = passengers.Find(p => p.passengerTransform.Equals(other.transform));
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
}
