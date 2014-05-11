using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// <para>Wrapper class for CharacterStats. Includes a "detection" float that keeps track of the enemy's detection of that character.</para>
/// <para>Once detection reaches 1, enemy should detect the character.</para>
/// </summary>

public struct CharacterTracker
{
    public CharacterTracker(CharacterStats characterP)
    {
        character = characterP;
        detection = 0;
    }
    public CharacterStats character;
    /// <summary>
    /// Once detection reaches 1, enemy should detect the character.
    /// </summary>
    public float detection;
}

[RequireComponent(typeof(SphereCollider))]
public class EnemySight : MonoBehaviour 
{
    public List<CharacterTracker> characterTrackingList;
    public List<CharacterStats> charactersInRange;
    public EnemyAI ai;
    public float fovAngle = 160f;
    // Use this for initialization
    void Awake()
    {
        characterTrackingList = new List<CharacterTracker>();
        charactersInRange = new List<CharacterStats>();
        if (ai == null)
        {
            Transform temp = this.transform.parent;
            while (temp.parent != null && temp.GetComponent<EnemyAI>() == null)
                temp = temp.parent;
            ai = temp.GetComponent<EnemyAI>();
        }
        ai.sight = this;
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.isTrigger && (other.tag == Tags.ENEMY || other.tag == Tags.PLAYER) && other.transform != ai.transform) //last check prevents this character from seeing itself
        {
            this.charactersInRange.Add(other.GetComponent<CharacterStats>());
            characterTrackingList.Add(new CharacterTracker(other.GetComponent<CharacterStats>()));
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (!other.isTrigger && (other.tag == Tags.ENEMY || other.tag == Tags.PLAYER) && other.transform != ai.transform)
        {
            this.charactersInRange.Remove(other.GetComponent<CharacterStats>());
            this.characterTrackingList.RemoveAll(delegate(CharacterTracker ct) {
                return ct.character == other.GetComponent<CharacterStats>();
            });
        }
    }

    public void DetectNearbyEnemies()
    {
 
    }
}
