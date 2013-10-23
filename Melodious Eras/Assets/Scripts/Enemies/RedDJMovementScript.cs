﻿using UnityEngine;
using System.Collections;

public class RedDJMovementScript : MonoBehaviour {
	
	bool movedAlready = false;
	bool isClose = false;
	float playerPositionX = 0;
	Transform player;
	
	void Start()
	{
		player = GameObject.FindGameObjectWithTag(Tags.PLAYER).transform;
	}
	
	
	void Update()
	{
		playerPositionX = player.transform.localPosition.x;
		if(playerPositionX+7 > transform.localPosition.x)
			isClose = true;
		
		if(isClose && !movedAlready)
		{
			iTween.MoveTo(gameObject, iTween.Hash("path", iTweenPath.GetPath("RedDJPath"), "time", 5));
			movedAlready = true;
		}
	}
}
