using UnityEngine;
using System.Collections;

public class FindBoxCollider : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Transform t = this.GetComponentInChildren<BoxCollider>().transform;
        Debug.Log(t);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
