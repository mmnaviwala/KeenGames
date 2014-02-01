using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ExplosionPhysicsForce : MonoBehaviour {

	public float explosionForce = 4;

	public void Detonate()
	{
		Debug.Log ("explosion " + explosionForce);
		// wait one frame because some explosions instantiate debris which should then
		// be pushed by physics force
		//yield return null;

		float multiplier = GetComponent<ParticleSystemMultiplier>().multiplier;

		float r = 10 * multiplier;
		Collider[] cols = Physics.OverlapSphere (transform.position, r);
		List<Rigidbody> rigidbodies = new List<Rigidbody>();
		foreach (var col in cols)
		{
			if (!col.isTrigger && col.attachedRigidbody != null && !rigidbodies.Contains( col.attachedRigidbody ))
			{
				rigidbodies.Add(col.attachedRigidbody);
			}
		}
		foreach (var rb in rigidbodies)
		{
			//avoiding forces on enemies, since it causes weird navmesh issues
			if(!(rb.tag == Tags.ENEMY || rb.tag == Tags.PLAYER))
				rb.AddExplosionForce( explosionForce*multiplier, transform.position, r, 1*multiplier, ForceMode.Impulse );


			CharacterStats character = rb.GetComponent<CharacterStats>();
			if(character != null)
			{
				float distance = Vector3.Distance(this.transform.position, rb.position);
				float damage = 200*explosionForce*multiplier / (distance * distance);
				character.TakeDamage((int)damage); 
			}
		}
	}
}
