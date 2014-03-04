﻿using UnityEngine;
using System.Collections;

public class Cain3D_Camera_Movement : MonoBehaviour {

	private float XRotationSpeed = 0.05f;
	private float YRotationSpeed = 0.05f;
	float speed = 20;

	void Start ()
	{
		StartCoroutine(rotateX());
		rotateX();
	}

	IEnumerator rotateX ()
	{
		while(transform.eulerAngles.x < 352f)
		{
			transform.eulerAngles = new Vector3(transform.eulerAngles.x + XRotationSpeed, 47, 0);
			yield return new WaitForSeconds(0.01f);
		}

		if(transform.eulerAngles.x >= 352f)
		{
			yield return new WaitForSeconds(0.009f);
			StartCoroutine(rotateY());
		}
	}

	IEnumerator rotateY()
	{
		while(transform.eulerAngles.y > 23f)
		{
			transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y - YRotationSpeed, 0);
			yield return new WaitForSeconds(0.01f);
		}

		if (transform.eulerAngles.y <= 23f)
		{
			yield return new WaitForSeconds(0.015f);
			StartCoroutine(rotateXnY());
		}
	}

	IEnumerator rotateXnY()
	{
		while(transform.eulerAngles.y >= 10 && transform.eulerAngles.x < 366)
		{
			transform.eulerAngles = new Vector3(transform.eulerAngles.x + XRotationSpeed, transform.eulerAngles.y - YRotationSpeed, 0);
			yield return new WaitForSeconds(0.001f);
		}

		if(transform.eulerAngles.y < 10 && transform.eulerAngles.x >= 5)
		{
			StartCoroutine(moveZRotateX());
		}
	}

	IEnumerator moveZRotateX()
	{
		Vector3 targetPosition = GameObject.Find("Sewer Black").transform.position;
		while (transform.position != targetPosition)
		{
			transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed*Time.deltaTime);
			yield return new WaitForSeconds(0.011f);
		}

		if (transform.position == targetPosition)
		{
			transform.position = new Vector3(transform.position.x, transform.position.y-50, transform.position.z);
		}
	}

}
