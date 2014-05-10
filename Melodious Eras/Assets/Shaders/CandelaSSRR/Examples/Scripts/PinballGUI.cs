using UnityEngine;
using System.Collections;

public class PinballGUI : MonoBehaviour {

	public Camera MainCamera;
	private bool SSRRToggle = true;
	private CandelaSSRR candelassrr;


	void Start ()
	{
		candelassrr = MainCamera.GetComponentInChildren<CandelaSSRR>();
	}


	void OnGUI() 
	{
		SSRRToggle = GUI.Toggle(new Rect(10, 10, 100, 30), SSRRToggle, "SSRR On/Off");
		candelassrr.enabled = SSRRToggle;
	}
}
