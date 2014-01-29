using UnityEngine;
using System.Collections;

public class VisibilityTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnBecameVisible()
	{
		Debug.Log("Visible");
	}
	void OnBecameInvisible()
	{
		Debug.Log ("Invisible");
	}
}
