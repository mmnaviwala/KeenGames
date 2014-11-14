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
    public EnemyStats stats;
    public float fovAngle = 160f;


    private Transform eyes;
    public LayerMask sightLayer;
    private Ray rayUpper, rayLower, rayCenter;  //will be used often; avoiding garbage collection
    private RaycastHit hit;                     //
    private float awarenessOfPlayer = 0f; //0 to 1. When reaching 1, enemy notices player and attacks

    // Use this for initialization
    void Awake()
    {
        characterTrackingList = new List<CharacterTracker>();
        charactersInRange = new List<CharacterStats>();
        eyes = this.transform;
        if (stats == null)
        {
            Transform temp = this.transform.parent;
            while (temp.parent != null && temp.GetComponent<EnemyStats>() == null)
                temp = temp.parent;
            stats = temp.GetComponent<EnemyStats>();
        }
        if (ai == null)
        {
            Transform temp = this.transform.parent;
            while (temp.parent != null && temp.GetComponent<EnemyAI>() == null)
                temp = temp.parent;
            ai = temp.GetComponent<EnemyAI>();
        }
        ai.sight = this;
    }

    void Update()
    {
        foreach (CharacterStats ch in this.charactersInRange)
        {
            float angle = Vector3.Angle(ch.transform.position + 2 * Vector3.up - this.eyes.position, this.eyes.forward);
            if (angle < fovAngle * ai.awarenessMultiplier)
            {
                //calculating rays for 3 points on the character
                float charHeight = ch.GetComponent<Collider>().bounds.max.y - ch.GetComponent<Collider>().bounds.min.y;
                rayUpper.origin = rayCenter.origin = rayLower.origin = this.eyes.position;

                rayUpper.direction = (ch.GetComponent<Collider>().bounds.max - Vector3.up * charHeight / 8) - this.eyes.position;
                rayLower.direction = (ch.GetComponent<Collider>().bounds.min + Vector3.up * charHeight / 8) - this.eyes.position;
                rayCenter.direction = (ch.GetComponent<Collider>().bounds.min + Vector3.up * charHeight / 2) - this.eyes.position;

                //TODO: ADJUST FIELD OF VIEW
                //reducing sight distance at wide angles, to simulate peripheral vision
                //This determines whether or not the enemy can even detect the player
                float adjustedSightDistance = (angle > 30) ?
                        Mathf.Pow(angle - fovAngle * ai.awarenessMultiplier, 2) / fovAngle * ai.awarenessMultiplier :
                        ai.sightDistance;
                /*
                float adjustedSightDistance = (angle > 30) ?
                        Mathf.Pow(angle - fov, 2)/fov :
                        sightDistance;*/
                //if any rays hit
                if (Physics.Raycast(rayUpper, out hit, adjustedSightDistance, sightLayer) ||
                    Physics.Raycast(rayCenter, out hit, adjustedSightDistance, sightLayer) ||
                    Physics.Raycast(rayLower, out hit, adjustedSightDistance, sightLayer))
                {
                    if (hit.collider.tag == Tags.PLAYER)
                    {
                        awarenessOfPlayer += Time.deltaTime * ai.awarenessMultiplier;

                        ai.seesPlayer = ai.alerted = true;
                        ai.lastPlayerSighting = ch.transform.position;
                        ai.currentEnemy = ch;
                        ai.squad.AlertGroup(ch);

                        ai.activity = ai.Chasing;
                    }
                    else
                    {
                        //this.seesPlayer = false;
                        if (hit.collider.tag == Tags.ENEMY && ch.isDead)
                        {
                            ai.Alert(2f);
                            ai.squad.AlertGroup(2f);

                            ai.activity = ai.Inspect;
                            ai.destination = ch.transform.position;
                        }
                    }
                }
                else if (true /*if object has MaterialPhysics && isn't opaque*/)
                {
                    //re-raycast from collision, if TOTAL raycast range doesn't exceed adjustedSightDistance * opacity
                }
            }
        }
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
