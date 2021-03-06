﻿using UnityEngine;
using System.Collections;

public class ParticleSystemMultiplier : MonoBehaviour {

	// a simple script to scale the size, speed and lifetime of a particle system

	public float multiplier = 1;

	public void Detonate()
	{
		Debug.Log("multiplier");
		var systems = GetComponentsInChildren<ParticleSystem>();
		this.GetComponent<AudioSource>().Play();
		foreach (ParticleSystem system in systems) {
			system.startSize *= multiplier;
			system.startSpeed *= multiplier;
			system.startLifetime *= Mathf.Lerp (multiplier,1,0.5f);
			system.Clear();
			system.Play();
		}
	}
}
