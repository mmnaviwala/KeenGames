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
 
    }

    void OnTriggerExit(Collider other)
    {
 
    }
}
