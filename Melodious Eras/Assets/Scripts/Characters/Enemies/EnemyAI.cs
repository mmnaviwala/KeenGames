﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[AddComponentMenu("Scripts/Characters/Enemy AI")]
public class EnemyAI : MonoBehaviour
{
    public NPCGroup group;
    public Weapon equippedWeapon;
    public CharacterStats currentEnemy;
    public float hearingMultiplier = 1;     //0 = deaf, 1 = normal, >1 = dogs & security
    public float awarenessMultiplier = 1;   //
    public float shootingRange, meleeRange;
    private float lightDifferenceMultiplier; //optional feature. Enemies in high-light areas will find it harder to detect players in low-light areas.
    public float patrolSpeed = 2, 
                 chaseSpeed = 5, 
                 chaseWaitTime = 5, 
                 patrolWaitTime = 2;
    public Waypoint[] patrolWaypoints;

    public bool seesPlayer = false;
    public bool awareOfPlayer = false;

    private Vector3 lastPlayerSighting;
    private NavMeshAgent nav;
    private EnemyStats stats;
    private static Vector3 resetPos = new Vector3(1000, 1000, 1000);

    private float chaseTimer = 0;
    private float patrolTimer = 0;
    private int waypointIndex;
	private bool enemyInRange = false;

	private List<CharacterStats> enemiesInRange;

	// Use this for initialization
	void Start () 
    {
		enemiesInRange = new List<CharacterStats>();
        nav = this.GetComponent<NavMeshAgent>();
        stats = this.GetComponent<EnemyStats>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (patrolWaypoints != null && patrolWaypoints.Length > 0)
            Patrol();
		if(enemyInRange)
		{
			foreach(CharacterStats ch in enemiesInRange)
			{
				RaycastHit[] hits;
				
				if (Vector3.Angle(ch.transform.position - this.transform.position, this.transform.forward) < 75)
				{
					hits = Physics.RaycastAll(this.transform.position, 
					                          ch.transform.position - this.transform.position, 
					                          Vector3.Distance(this.transform.position, ch.transform.position));
					RaycastHit closestHit = hits[0];
					float closestHitDistance = Vector3.Distance(this.transform.position, closestHit.point);
					for (int h = 0; h < hits.Length; h++)
					{
						if (hits[h].collider.isTrigger)
							continue;
						float temp = Vector3.Distance(this.transform.position, hits[h].point);
						if (temp < closestHitDistance)
						{
							closestHitDistance = temp;
							closestHit = hits[h];
						}
					}

					CharacterStats hitCharStats = closestHit.collider.GetComponent<CharacterStats>();
					if (closestHit.collider is CapsuleCollider && hitCharStats != null && hitCharStats.faction != this.stats.faction)
					{
						this.seesPlayer = this.awareOfPlayer = true;
						currentEnemy = ch;
						Debug.Log("Player sighted!");
						//stats.attack
					}
				}
			}
		}
	}

    void OnTriggerEnter(Collider other)
    {
        if (!other.isTrigger && other is CapsuleCollider)
        {
			CharacterStats charStats = other.GetComponent<CharacterStats>();
			if(charStats != null && charStats.faction != this.stats.faction)
			{
				enemiesInRange.Add(charStats);
				enemyInRange = true;
			}
        }
    }

	void OnTriggerExit(Collider other)
	{
		if (!other.isTrigger && other is CapsuleCollider)
		{
			CharacterStats charStats = other.GetComponent<CharacterStats>();
			if(charStats != null && charStats.faction != this.stats.faction)
			{
				enemiesInRange.Remove(charStats);
				enemyInRange = enemiesInRange.Count == 0;
			}
		}
	}

    void Patrol()
    {
        nav.speed = patrolSpeed;
        if (nav.remainingDistance < nav.stoppingDistance /*|| nav.destination == lastPlayerSighting*/)
        {
            patrolTimer += Time.deltaTime;
            if (patrolTimer >= patrolWaypoints[waypointIndex].waitTime)
            {
                waypointIndex = (waypointIndex + 1) % patrolWaypoints.Length;
                patrolTimer = 0;
            }
        }
        else
            patrolTimer = 0;

        nav.destination = patrolWaypoints[waypointIndex].transform.position;
    }

    void Chasing()
    {
        // Create a vector from the enemy to the last sighting of the player.
        Vector3 sightingDeltaPos = lastPlayerSighting - transform.position;

        // If the the last personal sighting of the player is not close...
        if (sightingDeltaPos.sqrMagnitude > 4f)
            // ... set the destination for the NavMeshAgent to the last personal sighting of the player.
            nav.destination = lastPlayerSighting;

        // Set the appropriate speed for the NavMeshAgent.
        nav.speed = chaseSpeed;

        // If near the last personal sighting...
        if (nav.remainingDistance < nav.stoppingDistance)
        {
            // ... increment the timer.
            chaseTimer += Time.deltaTime;

            // If the timer exceeds the wait time...
            if (chaseTimer >= chaseWaitTime)
            {
                // ... reset last global sighting, the last personal sighting and the timer.
                lastPlayerSighting = resetPos;
                lastPlayerSighting = resetPos;
                chaseTimer = 0f;
            }
        }
        else
            // If not near the last sighting personal sighting of the player, reset the timer.
            chaseTimer = 0f;
    }

    public void Listen()
    {
 
    }

    void Attack(PlayerStats target)
    {
        target.TakeDamage(10);
    }
}
