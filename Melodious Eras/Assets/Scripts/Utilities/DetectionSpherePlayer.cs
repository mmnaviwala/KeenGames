using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/// <summary>
/// Used for activating lamps, etc.
/// </summary>
public class DetectionSpherePlayer : DetectionSphere 
{
	public bool playerInRange = false;
	
	void OnTriggerEnter(Collider other)
	{
		if (!other.isTrigger && other.tag == Tags.PLAYER)
		{
			playerInRange = true;
		}
	}
	void OnTriggerExit(Collider other)
	{
		if (!other.isTrigger && other.tag == Tags.PLAYER)
		{
			playerInRange = false;
		}
	}
}
