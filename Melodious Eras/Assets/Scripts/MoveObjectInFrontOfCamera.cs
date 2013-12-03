using UnityEngine;
using System.Collections;

public class MoveObjectInFrontOfCamera : MonoBehaviour {

	public bool moveInX;
	public bool moveInY;

	public float moveYBy = 0.1f;
	public float moveXBy = 0.1f;

	public float xOffset;
	public float yOffset;

	private float xValueShouldBe;
	private float yValueShouldBe;

	private bool yMoveDown = false;
	private bool xMoveRight = false;


	void Start () 
	{
		if (moveInX)
			xValueShouldBe = Camera.main.transform.position.x + xOffset;
		else
			xValueShouldBe = transform.position.x;

		if (moveInY)
			yValueShouldBe = Camera.main.transform.position.y + yOffset;
		else
			yValueShouldBe = transform.position.y;


		if (transform.position.y > yValueShouldBe)
			yMoveDown = true;

		if (transform.position.x < xValueShouldBe)
			xMoveRight = true;

	}

	void Update ()
	{
		if(transform.position.y > yValueShouldBe && yMoveDown)
			transform.position = new Vector3(transform.position.x, transform.position.y - moveYBy, transform.position.z);
		else if(transform.position.y < yValueShouldBe && !yMoveDown)
			transform.position = new Vector3(transform.position.x, transform.position.y + moveYBy, transform.position.z);

		if(transform.position.x < xValueShouldBe && xMoveRight)
			transform.position = new Vector3(transform.position.x + moveXBy, transform.position.y, transform.position.z);
		else if(transform.position.x > xValueShouldBe && !xMoveRight)
			transform.position = new Vector3(transform.position.x - moveXBy, transform.position.y, transform.position.z);


	}










}
