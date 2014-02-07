using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Used primarily for keeping track of multiple triggers on an object.
/// For example, the player has a smaller sphere (melee trigger) and larger sphere (sound trigger), so we're using
/// 2 of these to keep melee-range and sound-range enemies seperate.
/// </summary>
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
		}
	}
	void OnTriggerExit(Collider other)
	{
		if (!other.isTrigger && (other.tag == Tags.ENEMY || other.tag == Tags.PLAYER))
		{
			this.charactersInRange.Remove(other.GetComponent<EnemyStats>());
		}
	}
}
