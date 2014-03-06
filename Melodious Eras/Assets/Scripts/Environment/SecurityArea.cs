﻿using UnityEngine;
using System.Collections;

public enum SecurityLevel { None, Low, Medium, High, ShootOnSight }
[RequireComponent(typeof(BoxCollider))]
public class SecurityArea : MonoBehaviour 
{
    public Faction controllingFaction = Faction.Enemy1;
    public SecurityLevel securityLevel;
    private Color gizmoColor; //None = Light Blue, Low = Green, Medium = Yellow, High = Orange, ShootOnSight = Red
    public bool hide = false;

    void Awake()
    {
        this.gameObject.layer = 2; //ignore raycast layer
    }
	// Use this for initialization
	void Start () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (!other.isTrigger && (other.tag == Tags.PLAYER || other.tag == Tags.ENEMY))
        {
            other.GetComponent<CharacterStats>().currentSecArea = this;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.isTrigger && (other.tag == Tags.PLAYER || other.tag == Tags.ENEMY))
        {
            CharacterStats stats = other.GetComponent<CharacterStats>();
            if(stats.currentSecArea == this) //checking to make sure their security level is still the same. 
                stats.currentSecArea = null; //This prevents adjacent security areas from screwing with each other
        }
 
    }

    void OnDrawGizmos()
    {
        if (!hide)
        {
            switch (securityLevel)
            {
                case SecurityLevel.None: Gizmos.color = new Color(0, .5f, .5f, .15f); break;
                case SecurityLevel.Low: Gizmos.color = new Color(0, 1, .25f, .15f); break;
                case SecurityLevel.Medium: Gizmos.color = new Color(1, 1, 0, .15f); break;
                case SecurityLevel.High: Gizmos.color = new Color32(255, 165, 0, 38); break;
                case SecurityLevel.ShootOnSight: Gizmos.color = new Color32(255, 0, 0, 38); break;
            }
            Gizmos.DrawCube(collider.bounds.center, collider.bounds.size);
        }
    }
    void OnDrawGizmosSelected()
    {
        switch (securityLevel)
        {
            case SecurityLevel.None:        Gizmos.color = new Color(0, .5f, .5f, .5f);     break;
            case SecurityLevel.Low:         Gizmos.color = new Color(0, 1, .25f, .5f);      break;
            case SecurityLevel.Medium:      Gizmos.color = new Color(1, 1, 0, .5f);         break;
            case SecurityLevel.High:        Gizmos.color = new Color32(255, 165, 0, 128);   break;
            case SecurityLevel.ShootOnSight:Gizmos.color = new Color32(255, 0, 0, 128);     break;
        }
        Gizmos.DrawCube(collider.bounds.center, collider.bounds.size);
    }
    public bool Equals(SecurityArea sec)
    {
        return this.securityLevel == sec.securityLevel && this.controllingFaction == sec.controllingFaction;
    }
}
