using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour 
{
	Transform hinge, doorBody;
	public Transform doorknob;
	Quaternion closedRotation, openRotation;
	bool inRange = false;
	bool openingOrClosing = false;
	public bool locked = false;
	public bool closed = true;

	// Use this for initialization
	void Awake()
	{
		closedRotation = new Quaternion(0, 0, 0, 1);
		openRotation = new Quaternion(0, .7071068f, 0, .7071068f);
	}
	void Start () 
	{
		hinge = this.transform.GetChild(0);
		doorBody = hinge.GetChild(0);
		doorknob = doorBody.GetChild(0);
		Debug.Log("Hinge rotation: " + hinge.localRotation.y + ", " + hinge.localRotation.w);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(inRange && Input.GetButtonDown(InputType.USE) && !openingOrClosing)
		{
			StartCoroutine(closed ? OpenDoor() : CloseDoor());
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == Tags.PLAYER && other is CapsuleCollider)
		{
			inRange = true;
			Debug.Log("Entering light trigger");
		}
	}
	void OnTriggerExit(Collider other)
	{
		if(other.tag == Tags.PLAYER && other is CapsuleCollider)
		{
			inRange = false;
			Debug.Log("Exiting light trigger");
		}
	}

	IEnumerator CloseDoor()
	{
		openingOrClosing = true;
		BoxCollider doorCollider = doorBody.GetComponent<BoxCollider>();
		doorCollider.enabled = false;
		while(hinge.localEulerAngles.y > 1)
		{
			hinge.localRotation = Quaternion.Slerp(hinge.localRotation, closedRotation, 4 * Time.deltaTime);
			yield return new WaitForEndOfFrame();
		}
		hinge.localRotation = closedRotation;
		doorCollider.enabled = true;
		closed = true;
		openingOrClosing = false;
		Debug.Log("Closed door");
	}

	IEnumerator OpenDoor()
	{
		openingOrClosing = true;
		BoxCollider doorCollider = doorBody.GetComponent<BoxCollider>();
		doorCollider.enabled = false;
		while(hinge.localEulerAngles.y < 89)
		{
			hinge.localRotation = Quaternion.Slerp(hinge.localRotation, openRotation, 4 * Time.deltaTime);
			yield return new WaitForEndOfFrame();
		}
		hinge.localRotation = openRotation;
		doorCollider.enabled = true;
		closed = false;
		openingOrClosing = false;
		Debug.Log("Opened Door");
	}
}
