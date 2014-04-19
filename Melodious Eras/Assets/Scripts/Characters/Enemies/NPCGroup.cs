using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[AddComponentMenu("Scripts/Characters/NPC Group")]
public class NPCGroup : MonoBehaviour 
{
    public List<EnemyAI> NPCs;

    /// <summary>
    /// Puts the group on alert status
    /// </summary>
    public void AlertGroup(float _awarenessMultiplier)
    {
        foreach (EnemyAI member in NPCs)
            member.Alert(_awarenessMultiplier);
    }
	public void AlertGroup(CharacterStats hostile)
	{
        foreach (EnemyAI member in NPCs)
        {
            member.currentEnemy = hostile;
            member.lastPlayerSighting = hostile.transform.position;
        }
	}
}
