using UnityEngine;
using System.Collections;

public class BlueDJMovementScript : MonoBehaviour {

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
		if(playerPositionX+10 > transform.localPosition.x)
			isClose = true;
		
		if(isClose && !movedAlready)
		{
			iTween.MoveTo(gameObject, iTween.Hash("path", iTweenPath.GetPath("BlueDJPath"), "time", 5));
			movedAlready = true;
		}
	}
}
