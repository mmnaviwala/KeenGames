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
        angleFromEyes = 0f;
        adjAngleFromEyes = 0f;
    }
    public CharacterStats character;
    /// <summary>
    /// Once detection reaches 1, enemy should detect the character.
    /// </summary>
    public float detection;
    public float angleFromEyes, adjAngleFromEyes;
}

[RequireComponent(typeof(SphereCollider))]
public class EnemySight : MonoBehaviour
{
    public Dictionary<CharacterStats, CharacterTracker> characterTracker = new Dictionary<CharacterStats, CharacterTracker>();
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
        //TODO: all this AI logic needs to go in EnemyAI class once all the sight mechanics are set up
        foreach (CharacterStats ch in this.charactersInRange)
        {
            //if any rays hit
            if (this.SeesCharacter(ch))
            {
                if (hit.collider.tag == Tags.PLAYER)
                {
                    awarenessOfPlayer += Time.deltaTime * ai.awarenessMultiplier * ch.visibility;

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
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (belongsToOtherCharacter(other)) //last check prevents this character from seeing itself
        {
            var character = other.GetComponent<CharacterStats>();

            charactersInRange.Add(character);
            characterTrackingList.Add(new CharacterTracker(character));

            try
            {
                characterTracker.Add(character, new CharacterTracker(character));
            }
            catch(System.Exception e)
            {

            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (belongsToOtherCharacter(other))
        {
            var character = other.GetComponent<CharacterStats>();
            charactersInRange.Remove(character);
            characterTrackingList.RemoveAll(tracker => tracker.character == character);

            try
            {
                characterTracker.Remove(character);
            }
            catch(System.ArgumentException)
            {

            }
        }
    }

    bool belongsToOtherCharacter(Collider col)
    {
        return !col.isTrigger &&
                col is CapsuleCollider &&
                (col.tag == Tags.ENEMY || col.tag == Tags.PLAYER) &&
                col.transform != ai.transform;
    }

    public void GainAwarenessOf(CharacterStats character)
    {

    }

    public float AngleFromEyes(Vector3 loc)
    {
        return Vector3.Angle(loc - this.eyes.position, this.eyes.forward);
    }

    public float AdjustedSightDistance(float angle)
    {
        return (angle > 30) ? Mathf.Pow(angle - fovAngle * ai.awarenessMultiplier, 2) / fovAngle * ai.awarenessMultiplier : ai.sightDistance;
        /* return (angle > 30) ? Mathf.Pow(angle - fov, 2)/fov : sightDistance; */
    }

    public bool SeesCharacter(CharacterStats ch)
    {
        float angle = AngleFromEyes(ch.transform.position + 2 * Vector3.up);

        if (angle > fovAngle * ai.awarenessMultiplier) //if player is outside cone of vision
            return false;

        else if (true /*if object has MaterialPhysics && isn't opaque*/)
        {
            //re-raycast from collision, if TOTAL raycast range doesn't exceed adjustedSightDistance * opacity
        }

        //calculating rays for 3 points on the character
        var charCapsule = ch.GetComponent<CapsuleCollider>();

        float charHeight = charCapsule.bounds.max.y - charCapsule.bounds.min.y;
        rayUpper.origin = rayCenter.origin = rayLower.origin = this.eyes.position;

        rayUpper.direction =  (charCapsule.bounds.max - Vector3.up * charHeight / 8) - this.eyes.position;
        rayLower.direction =  (charCapsule.bounds.min + Vector3.up * charHeight / 8) - this.eyes.position;
        rayCenter.direction = (charCapsule.bounds.min + Vector3.up * charHeight / 2) - this.eyes.position;

        //TODO: ADJUST FIELD OF VIEW
        //reducing sight distance at wide angles, to simulate peripheral vision
        //This determines whether or not the enemy can even detect the player
        float adjustedSightDistance = AdjustedSightDistance(angle);

        return Physics.SphereCast(rayLower,  0.1f, out hit, adjustedSightDistance, sightLayer) ||
               Physics.SphereCast(rayCenter, 0.1f, out hit, adjustedSightDistance, sightLayer) ||
               Physics.SphereCast(rayUpper,  0.1f, out hit, adjustedSightDistance, sightLayer);
    }

    void CheckAwareness()
    {

    }

    public void DetectNearbyEnemies()
    {
 
    }
}
