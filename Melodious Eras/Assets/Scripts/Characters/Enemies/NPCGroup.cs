using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[AddComponentMenu("Scripts/Characters/NPC Group")]
public class NPCGroup : MonoBehaviour 
{
    public List<EnemyAI> NPCs;

	public void AlertGroup(Vector3 sightingCoords)
	{
        foreach (EnemyAI member in NPCs)
            member.lastPlayerSighting = sightingCoords;
		/*NPCs.ForEach(delegate(EnemyAI member) {
			member.lastPlayerSighting = sightingCoords;
		});*/
	}
}
