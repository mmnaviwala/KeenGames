using UnityEngine;
using System.Collections;

public class ObjectSpinner : MonoBehaviour {

	public float SpinSpeed = 50.0f;
	
	// Update is called once per frame
	void Update()
	{
		transform.Rotate(Vector3.forward * Time.deltaTime * SpinSpeed);
	}

}
