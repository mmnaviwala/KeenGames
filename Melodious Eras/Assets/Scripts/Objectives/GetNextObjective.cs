using UnityEngine;
using System.Collections;

public class GetNextObjective : MonoBehaviour {

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == Tags.PLAYER)
		{
			GameObject.Find("Objectives").GetComponent<TrackObjectives>().SetNextObjective();
		}

		Destroy(gameObject);
	}
}
