using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrackObjectives : MonoBehaviour {

	public string currentObjectiveTitle, currentObjectiveDescription;
	public LinkedList<Objective> allObjectivesList;
	public bool allObjectivesComplete = false;
	private static int linkedListCount = 0;

	void Start ()
	{
		allObjectivesList = GameObject.FindGameObjectWithTag(Tags.GAME_CONTROLLER).GetComponent<ObjectiveSystem>().objectives;
		SetNextObjective();
	}

	public void SetNextObjective ()
	{
		if (linkedListCount <= allObjectivesList.Count)
		{
			int i = 0;
			foreach (Objective obj in allObjectivesList)
			{
				if(linkedListCount == i)
				{
					currentObjectiveTitle = obj.Title;
					currentObjectiveDescription = obj.Description;
					linkedListCount++;
					i++;
				}
				i++;
			}
		}
		else
		{
			currentObjectiveTitle = "All Objectives Complete";
			currentObjectiveDescription = "";
		}
	}

}
