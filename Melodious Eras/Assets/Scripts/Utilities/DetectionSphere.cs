using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DetectionSphere : MonoBehaviour {
	
	public List<CharacterStats> charactersInRange;
	// Use this for initialization
	void Awake () 
	{
		charactersInRange = new List<CharacterStats>();
	}
	
	void OnTriggerEnter(Collider other)
	{
		if (!other.isTrigger && (other.tag == Tags.ENEMY || other.tag == Tags.PLAYER))
		{
			this.charactersInRange.Add(other.GetComponent<EnemyStats>());
			Debug.Log("Enemies within " + this.name + ": " + this.charactersInRange.Count);
		}
	}
	void OnTriggerExit(Collider other)
	{
		if (!other.isTrigger && (other.tag == Tags.ENEMY || other.tag == Tags.PLAYER))
		{
			this.charactersInRange.Remove(other.GetComponent<EnemyStats>());
			Debug.Log("Enemies within " + this.name + ": " + this.charactersInRange.Count);
		}
	}
}
