using UnityEngine;
using System.Collections;

public class LightningArc : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (!other.isTrigger && (other.tag == Tags.ENEMY || other.tag == Tags.PLAYER) && other is CapsuleCollider)
            other.GetComponent<CharacterStats>().TakeDamageThroughArmor(1000);
    }
}
