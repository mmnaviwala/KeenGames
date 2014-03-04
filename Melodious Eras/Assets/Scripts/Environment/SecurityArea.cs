using UnityEngine;
using System.Collections;

public enum SecurityLevel { None, Low, Medium, High, ShootOnSight }
[RequireComponent(typeof(BoxCollider))]
public class SecurityArea : MonoBehaviour 
{
    public SecurityLevel securityLevel;

    void Awake()
    {
        this.gameObject.layer = 2; //ignore raycast layer
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (!other.isTrigger && (other.tag == Tags.PLAYER || other.tag == Tags.ENEMY))
        {
            other.GetComponent<CharacterStats>().currentSecLevel = this.securityLevel;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.isTrigger && (other.tag == Tags.PLAYER || other.tag == Tags.ENEMY))
        {
            CharacterStats stats = other.GetComponent<CharacterStats>();
            if(stats.currentSecLevel == this.securityLevel) //checking to make sure their security level is still the same. 
                stats.currentSecLevel = this.securityLevel; //This prevents adjacent security areas from screwing with each other
        }
 
    }
}
