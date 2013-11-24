using UnityEngine;
using System.Collections;

public class ExitVolcano : MonoBehaviour {

	// Use this for initialization
	void OnTriggerEnter(Collider other) 
	{
		GameObject.Find("Dr C Sharp").GetComponent<testCircleMovement>().enabled = false; 	// stop rotations
		GameObject.Find("trigger_enter_volcano").GetComponent<SwitchPerspectives>().enabled = false;	// exit 3D perspective mode 
		GameObject.Find("Main Camera").GetComponent<CameraMovement3D>().enabled = false;	// Disbale Camera 3D movement script
		GameObject.Find("Main Camera").GetComponent<CameraMovement2D>().enabled = true;		// Enable Camera 2D movement script


		GameObject.Find("Main Camera").transform.rotation = new Quaternion(0,0,0,0);	// Reset camera rotations to 0,0,0

		GameObject.Find("Dr C Sharp").GetComponent<Rigidbody>().useGravity = true;		// Turn on Player's gravity again
		GameObject.Find("Dr C Sharp").transform.rotation = new Quaternion(0.0f, 90.0f, 0.0f, 90.0f);	// Make sure Player is facing the right directions

		// Move player a little in x and y axis once gravity is enabled
		GameObject.Find("Dr C Sharp").transform.position = new Vector3(GameObject.Find("Dr C Sharp").transform.position.x+10f,GameObject.Find("Dr C Sharp").transform.position.y+10f,GameObject.Find("Dr C Sharp").transform.position.z);

	
	}
}
