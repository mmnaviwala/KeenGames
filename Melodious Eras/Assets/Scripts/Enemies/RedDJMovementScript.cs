using UnityEngine;
using System.Collections;

public class RedDJMovementScript : MonoBehaviour {
	
	bool movedAlready = false;
	bool isClose = false;
	float playerPositionX = 0;
	
	void Start()
	{
		// just for demo purposes
		iTween.MoveTo(gameObject, iTween.Hash("path", iTweenPath.GetPath("RedDJPath"), "time", 5));
	}
	
	/*
	void Update()
	{
		// find out how close the player is to the enemey.
		if(playerPositionX+5 < transform.localPosition.x)
			isClose = true;
		
		if(isClose && !movedAlready)
		{
			iTween.MoveTo(gameObject, iTween.Hash("path", iTweenPath.GetPath("RedDJPath"), "time", 5));
			movedAlready = false;
		}
	}*/
	
}
