using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Item))]
[AddComponentMenu("Scripts/Items/Pickup Object (Battery)")]
public class Battery : PickupObject 
{
	public float capacity = 20;
	private Suit playerSuit;

    void Awake()
    {
        if (!detectionSphere)
        {
            detectionSphere = this.GetComponentInChildren<DetectionSpherePlayer>();
        }
    }
	
	// Update is called once per frame
	void Update () 
	{
		CheckInput();
	}
	
	protected override void CheckInput()
	{
		if(detectionSphere.playerInRange)
		{
			Ray ray = new Ray(this.transform.position, character.transform.position + new Vector3(0, 1.75f, 0) - this.transform.position);
			RaycastHit hit;

			//If there aren't any obstacles in the way
			if((Physics.Raycast (ray, out hit) 
			    || Physics.Raycast (this.transform.position, character.transform.position + new Vector3(0, .25f, 0) - this.transform.position, out hit)) 
			   && hit.transform == character.transform)
			{
				if(playerSuit.batteryLife < 100)
				{
					//Display prompt
				}

				if(Input.GetButtonDown (InputType.USE))
				{
					if(playerSuit.batteryLife < 100)
					{
						PickUp();
					}
					else
					{
						//Display "Battery Fully Charged"
					}
				}
			}
		}
	}

	void OnTriggerEnter(Collider col)
	{
		if(col is CapsuleCollider && col.tag == Tags.PLAYER)
		{
			Debug.Log("In range: " + col.transform.name);
			inRange = true;
			character = col.GetComponent<CharacterStats>();
			playerSuit = col.GetComponent<Suit>();

			character.lookatTarget = this.transform;
		}
	}
	void OnTriggerExit(Collider col)
	{		
		if(col is CapsuleCollider && col.tag == Tags.PLAYER)
		{
			inRange = false;
			character.lookatTarget = null;
		}
	}

	protected override void PickUp()
	{
		Debug.Log("Picked up battery");

		playerSuit.batteryLife += capacity;
		if(playerSuit.batteryLife > 100)
			playerSuit.batteryLife = 100;

		character.lookatTarget = null;
		Destroy(this.gameObject);
	}
}
