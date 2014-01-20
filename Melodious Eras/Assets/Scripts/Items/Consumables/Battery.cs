using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Item))]
[AddComponentMenu("Scripts/Items/Pickup Object (Battery)")]
public class Battery : PickupObject 
{
	public float capacity = 20;
	private Suit playerSuit;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		CheckInput();
	}
	
	protected override void CheckInput()
	{
		if(inRange)
		{
			if(playerSuit.batteryLife < 100)
			{
				Ray ray = new Ray(this.transform.position, character.transform.position + new Vector3(0, 1.75f, 0) - this.transform.position);
				RaycastHit hit;
				if(Physics.Raycast (ray, out hit) && hit.transform == character.transform)
				{
					//Display prompt
					if(Input.GetButtonDown(InputType.USE))
					{
						PickUp();
					}
				}
			}
			else
			{
				//Display "Battery Fully Charged"
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
