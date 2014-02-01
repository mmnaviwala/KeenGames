using System;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class JetParticleEffect : MonoBehaviour {
    
	// this script controls the jet's exhaust particle system, controlling the
	// size and colour based on the jet's current throttle value.


    public Color minColour;                             // The base colour for the effect to start at
    
	private ParticleSystem system;                      // The particle system that is being controlled
	private float originalStartSize;                    // The original starting size of the particle system
	private float originalLifetime;                     // The original lifetime of the particle system
	private Color originalStartColor;                   // The original starting colout of the particle system

    // Use this for initialization
	void Start () {

        // get the aeroplane from the object hierarchy

        // get the particle system ( it will be on the object as we have a require component set up
		system = GetComponent<ParticleSystem>();

        // set the original properties from the particle system
		originalLifetime = system.startLifetime;
		originalStartSize = system.startSize;
		originalStartColor = system.startColor;
	}


}
