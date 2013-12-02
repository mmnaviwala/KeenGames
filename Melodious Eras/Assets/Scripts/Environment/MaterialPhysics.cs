﻿using UnityEngine;
using System.Collections;

public enum MaterialType { Grass, Carpet, Wood, Concrete, Stone, Marble, LightMetal, HeavyMetal, Glass, BrokenGlass, AbsorbAllSound, Custom };
public class MaterialPhysics : MonoBehaviour 
{
    public MaterialType surfaceMaterial, coreMaterial;

    //1 = normal refraction/amplification, 0 means it absorbs all sound
    public float soundRefraction = 1,    //sound going THROUGH object (raycasting) per unit of thickness
                 soundAmplification = 1; //sound amplification
    float thickness; //should be calculated using Bounds;
    PhysicMaterial mat;

	// Use this for initialization
	void Start () 
    {
        CalculateSoundProperties();
	}

    void CalculateSoundProperties()
    {
        switch (surfaceMaterial)
        {
            case MaterialType.Grass:        soundAmplification = .3f;   break;
            case MaterialType.Carpet:       soundAmplification = .3f;   break;
            case MaterialType.Wood:         
                soundAmplification = 1f;
                this.collider.material = new PhysicMaterial("Wood");
                break;
            case MaterialType.Concrete:     soundAmplification = .9f;   break;
            case MaterialType.Stone:        soundAmplification = 1f;    break;
            case MaterialType.Marble:       soundAmplification = 1.05f; break;
            case MaterialType.LightMetal: soundAmplification = 1.5f;
                this.collider.material = new PhysicMaterial("Metal"); 
                break;

            case MaterialType.HeavyMetal: soundAmplification = 1.15f;
                this.collider.material = new PhysicMaterial("Metal");
                break;
            case MaterialType.Glass:        soundAmplification = 1f;    break;
            case MaterialType.BrokenGlass:  soundAmplification = 2f;    break;
            case MaterialType.AbsorbAllSound: soundAmplification = 0; break;
        }
        switch (coreMaterial)
        {
            case MaterialType.Grass:        soundRefraction = .5f;  break;
            case MaterialType.Carpet:       soundRefraction = 1f;   break; //Why would you even do this
            case MaterialType.Wood:         soundRefraction = .5f;  break;
            case MaterialType.Concrete:     soundRefraction = .2f;  break;
            case MaterialType.Stone:        soundRefraction = .2f;  break;
            case MaterialType.Marble:       soundRefraction = .2f;  break;
            case MaterialType.LightMetal:   soundRefraction = .5f;  break;
            case MaterialType.HeavyMetal:   soundRefraction = .2f;  break;
            case MaterialType.Glass:        soundRefraction = .8f;  break;
            case MaterialType.BrokenGlass:  soundRefraction = 1f;   break;
            case MaterialType.AbsorbAllSound: soundRefraction = 0; break;
        }
    }
}
