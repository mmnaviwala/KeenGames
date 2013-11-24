using UnityEngine;
using System.Collections;

public class ChangePlayerPositionInVolcano : MonoBehaviour {

	void OnTriggerEnter(Collider other) 
	{
		Vector3 playerPosition = GameObject.Find("Capsule to rotate around").transform.position;
		playerPosition.y += 4;
		playerPosition.z += 2;
		GameObject.Find("Dr C Sharp").transform.position = playerPosition;

		GameObject.Find("Dr C Sharp").GetComponent<testCircleMovement>().enabled = true;
		GameObject.Find("Dr C Sharp").GetComponent<Rigidbody>().useGravity = false;
		//GameObject.Find("Dr C Sharp").GetComponent<testCircleMovement>().yOffset = 0.09f;
	}
}
