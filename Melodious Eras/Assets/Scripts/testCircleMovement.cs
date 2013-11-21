using UnityEngine;
using System.Collections;

public class testCircleMovement : MonoBehaviour {

	public float RotationSpeed = 200f;
	public Vector3 axis = new Vector3(0, 1, 0);
	protected GameObject rotateRespectiveTo;
	
	void Start () {
		rotateRespectiveTo = GameObject.FindGameObjectWithTag("Enemy");
	}
	
	void Update () {
		transform.RotateAround (rotateRespectiveTo.transform.position, axis, RotationSpeed* Time.deltaTime);
	}
}
