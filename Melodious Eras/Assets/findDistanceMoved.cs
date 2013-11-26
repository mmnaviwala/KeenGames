using UnityEngine;
using System.Collections;

public class findDistanceMoved : MonoBehaviour {

	public float distanceTravelled = 0;
	Vector3 lastPosition;
	
	void Start()
	{
		lastPosition = transform.position;
	}
	
	void Update()
	{
		distanceTravelled += Vector3.Distance(transform.position, lastPosition);
		lastPosition = transform.position;
	}
}
