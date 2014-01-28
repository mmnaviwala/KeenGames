using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Item))]
[AddComponentMenu("Scripts/Items/Pickup Object")]
public class PickupObject : MonoBehaviour 
{
	public Item item;
	protected CharacterStats character;
	protected bool inRange = false;

	void Awake()
	{
		if(item == null)
			item = this.GetComponent<Item>();
	}
	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
		CheckInput();
	}
	protected virtual void CheckInput()
	{
		if(inRange)
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
	}

	void OnTriggerEnter(Collider col)
	{
		if(col is CapsuleCollider && col.tag == Tags.PLAYER)
		{
			inRange = true;
			character = col.transform.GetComponent<CharacterStats>();
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

	protected virtual void PickUp()
	{
		Debug.Log("Picked up " + this.name);
		
		character.lookatTarget = null;
		Destroy(this.gameObject);
	}
}
