﻿using UnityEngine;
using System.Collections;

public class NightVisionGoggles : MonoBehaviour {
	public Suit playerSuit;
	public float efficiency = 5;

	private NightVisionTestCS nightvision;
	private Bloom bloom;
	private Fisheye fisheye;
	private DepthOfFieldScatter dof;
	public bool activated = false;

	// Use this for initialization
	void Start () {
		if(playerSuit == null)
		{
			Transform temp_transform = this.transform;
			while(temp_transform.tag != Tags.PLAYER)
			{
				temp_transform = temp_transform.parent;
			}
			playerSuit = temp_transform.GetComponent<Suit>();
		}
		nightvision = Camera.main.GetComponent<NightVisionTestCS>();
		bloom = Camera.main.GetComponent<Bloom>();
		fisheye = Camera.main.GetComponent<Fisheye>();
		dof = Camera.main.GetComponent<DepthOfFieldScatter>();
	}
	
	// Update is called once per frame
	void Update () 
	{		
		if(Input.GetButtonDown(InputType.TOGGLE_NIGHTVISION))
		{
			activated = !activated;
			nightvision.enabled = activated;
			bloom.enabled = activated;
			fisheye.enabled = activated;
			dof.enabled = activated;
		}
		if(activated)
		{
			playerSuit.batteryLife -= Time.deltaTime / efficiency;
		}
	}
}
