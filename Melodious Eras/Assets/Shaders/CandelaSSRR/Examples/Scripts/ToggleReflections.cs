using UnityEngine;
using System.Collections;

public class ToggleReflections : MonoBehaviour 
{
	CandelaSSRR ssrr;
	// Use this for initialization
	void Start () {
		ssrr = this.GetComponent<CandelaSSRR>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.M))
			ssrr.enabled = !ssrr.enabled;
	}
}
