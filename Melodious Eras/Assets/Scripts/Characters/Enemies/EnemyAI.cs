using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[AddComponentMenu("Scripts/Characters/Enemy AI")]
public class EnemyAI : MonoBehaviour
{
    public NPCGroup group;
	public Transform eyes;
    public Weapon equippedWeapon;
    public CharacterStats currentEnemy;
    public float hearingMultiplier = 1;     //0 = deaf, 1 = normal, >1 = dogs & security
    public float awarenessMultiplier = 1;   //
	public float fieldOfView = 110f;
	private float fov;
    public float shootingRange, meleeRange;
    private float lightDifferenceMultiplier; //optional feature. Enemies in high-light areas will find it harder to detect players in low-light areas.
    public float patrolSpeed = 2, 
                 chaseSpeed = 5, 
                 chaseWaitTime = 5, 
                 patrolWaitTime = 2;
    public Waypoint[] patrolWaypoints;

    public bool seesPlayer = false;
	public bool awareOfPlayer = false;
	public LayerMask sightLayer;
	public float sightDistance = 20;

    public Vector3 lastPlayerSighting;
    private Vector3 lpsResetPosition = new Vector3(999, 999, 999);
    private NavMeshAgent nav;
    private EnemyStats stats;
    private static Vector3 resetPos = new Vector3(1000, 1000, 1000);

    private float chaseTimer = 0;
    private float patrolTimer = 0;
    private int waypointIndex;

	public List<CharacterStats> enemiesInRange;
	private Ray rayUpper, rayLower, rayCenter;
	private RaycastHit hit;


	// Use this for initialization
	void Start () 
    {
		fov = fieldOfView / 2 * awarenessMultiplier;
		//keeping rays permanent to avoid garbage collection
		rayUpper = new Ray();
		rayLower = new Ray();
		rayCenter = new Ray();

		enemiesInRange = new List<CharacterStats>();
        nav = this.GetComponent<NavMeshAgent>();
        stats = this.GetComponent<EnemyStats>();
		lastPlayerSighting = lpsResetPosition;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (lastPlayerSighting != lpsResetPosition && currentEnemy.health > 0f)
			// ... chase.
			Chasing();
        else if (patrolWaypoints != null && patrolWaypoints.Length > 0)
            Patrol();
		if(enemiesInRange.Count > 0)
		{
			foreach(CharacterStats ch in enemiesInRange)
			{
				//RaycastHit[] hits;
				
				if (Vector3.Angle(ch.transform.position - this.eyes.position, this.eyes.forward) < fov)
				{
					//calculating rays for 3 points on the character
					float charHeight = ch.collider.bounds.max.y - ch.collider.bounds.min.y;
					rayUpper.origin = rayCenter.origin = rayLower.origin = this.eyes.position + this.eyes.forward/4;

					rayUpper.direction = (ch.collider.bounds.max - Vector3.up*charHeight/8) - this.eyes.position;
					rayLower.direction = (ch.collider.bounds.min + Vector3.up*charHeight/8) - this.eyes.position;
					rayCenter.direction = (ch.collider.bounds.min + Vector3.up*charHeight/2) - this.eyes.position;

					//if any rays hit
					if((Physics.Raycast(rayUpper, out hit, sightDistance, sightLayer) ||
					    Physics.Raycast(rayCenter, out hit, sightDistance, sightLayer) ||
					    Physics.Raycast(rayLower, out hit, sightDistance, sightLayer)) 
					    && hit.collider.tag == Tags.PLAYER)
					{
						this.seesPlayer = this.awareOfPlayer = true;
						this.lastPlayerSighting = ch.transform.position;
						currentEnemy = ch;
						break;
					}
				}
			}
		}

	}

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name + " entered trigger, layer " + other.gameObject.layer);
        if (!other.isTrigger && other is CapsuleCollider)
        {
            CharacterStats charStats = other.GetComponent<CharacterStats>();
            if (charStats != null && charStats.faction != this.stats.faction)
            {
                enemiesInRange.Add(charStats);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.isTrigger && other is CapsuleCollider)
        {
            CharacterStats charStats = other.GetComponent<CharacterStats>();
            if (charStats != null && charStats.faction != this.stats.faction)
            {
                enemiesInRange.Remove(charStats);
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

    public void Listen(Vector3 source, float volume)
    {
        if (CalculatePathLengthTo(source) < volume)
        {
            lastPlayerSighting = source;
        }
    }
    float CalculatePathLengthTo(Vector3 source)
    {
        NavMeshPath path = new NavMeshPath();
        if (nav.enabled)
            nav.CalculatePath(source, path);

        Vector3[] allWaypoints = new Vector3[path.corners.Length + 2];

        allWaypoints[0] = this.transform.position;
        allWaypoints[allWaypoints.Length - 1] = source;
        for (int i = 0; i < path.corners.Length; i++)
            allWaypoints[i + 1] = path.corners[i];

        float pathLength = 0;
        for (int w = 0; w < allWaypoints.Length - 1; w++)
            pathLength += Vector3.Distance(allWaypoints[w], allWaypoints[w + 1]);

        return pathLength;
 
    }

    void Attack(PlayerStats target)
    {
		equippedWeapon.Fire(target);
        //target.TakeDamage(10);
    }
}
