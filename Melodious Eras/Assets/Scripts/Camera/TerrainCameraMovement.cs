using UnityEngine;
using System.Collections;

public class TerrainCameraMovement : MonoBehaviour {

	public float positionX;
	public float positionZ;
	public float rotationY;
	public float rotationx;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.position = new Vector3(transform.position.x + positionX, transform.position.y, transform.position.z + positionZ);
		if (transform.position.x > 500f)
			transform.rotation = new Quaternion(transform.rotation.x + rotationx, transform.rotation.y + rotationY, transform.rotation.z, transform.rotation.w);
		else
			transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y + rotationY, transform.rotation.z, transform.rotation.w);
	}
}
