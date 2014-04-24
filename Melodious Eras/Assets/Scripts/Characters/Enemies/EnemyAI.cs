using UnityEngine;
using System.Collections;
using System.Collections.Generic;

delegate void AI_Action();

[AddComponentMenu("Scripts/Characters/Enemy AI")]
public class EnemyAI : MonoBehaviour
{
    #region variables
    public NPCGroup squad;
	public Transform eyes;
    public CharacterStats currentEnemy;

    [SerializeField][Range(0, 2)]   private float hearingMultiplier = 1;     //0 = deaf, 1 = normal, >1 = dogs & security
    [SerializeField][Range(0, 1)]   private float baseAwarenessMultiplier = .5f;
    [SerializeField][Range(0, 2)]   private float fieldOfView = 160f;
    [SerializeField]                private float shootingRange;
    [SerializeField]                private float meleeRange;
    [SerializeField][Range(0, 5)]   private float attackSpeed = 1f;
    [SerializeField]                private float patrolSpeed = 1.5f;
    [SerializeField]                private float chaseSpeed = 5;
    [SerializeField]                private float chaseWaitTime = 5;
    [SerializeField]                private float patrolWaitTime = 2;
    public Waypoint[] patrolWaypoints;
    private float awarenessMultiplier;   //
    private float fov, fovSqrt;
    private float lightDifferenceMultiplier; //optional feature. Enemies in high-light areas will find it harder to detect players in low-light areas.
    private float nextShotTime = 0;

    public bool seesPlayer = false;
	public bool alerted = false;
	public LayerMask sightLayer;
	public float sightDistance = 20;
    public float desiredSpeed = 0;

    public Vector3 lastPlayerSighting;
    private Vector3 lpsResetPosition = new Vector3(999, 999, 999);
    private Vector3 inspectingArea = new Vector3(999, 999, 999); //last position to hear a hostile character
    private static Vector3 resetPos = new Vector3(999, 999, 999);
    private float lastPlayerSightingTime = 0;

    private float awarenessOfPlayer = 0f;
    //private Dictionary<Collider, CharacterStats> awarenessOfChar;     //for multi-faction AI. not implemented yet

    private NavMeshAgent nav;
    private EnemyStats stats;
	private Animator anim;
    public EnemySight sight;

    private float chaseTimer = 0;
    private float patrolTimer = 0;
    private int waypointIndex;
    
	private Ray rayUpper, rayLower, rayCenter;  //will be used often; avoiding garbage collection
	private RaycastHit hit;                     //
	private AI_Action ai_activity;
    #endregion


    public Animator Anim {get {return anim; } set {anim = value;}}

    void Awake()
    {
        //awarenessOfChar = new Dictionary<Collider, CharacterStats>();
        nav = this.GetComponent<NavMeshAgent>();
		stats = this.GetComponent<EnemyStats>();
		this.anim = this.GetComponent<Animator>();
        sightDistance = sight.GetComponent<SphereCollider>().radius;
        lastPlayerSighting = lpsResetPosition;

        this.awarenessMultiplier = baseAwarenessMultiplier;

        //Adding to squad, if part of any
        if (this.squad == null)
        {
            this.squad = this.transform.parent.GetComponent<NPCGroup>();
            if (this.squad != null)
                squad.NPCs.Add(this);
        }
    }
	// Use this for initialization
	void Start () 
    {
		ai_activity = Patrolling;
        fov = sight.fovAngle * awarenessMultiplier / 2;
        fovSqrt = Mathf.Sqrt(fov);
		//keeping rays permanent to avoid garbage collection
		rayUpper = new Ray();
		rayLower = new Ray();
		rayCenter = new Ray();
        this.seesPlayer = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(!this.stats.isDead)
		{
			//this.seesPlayer = false;
            if (lastPlayerSighting != lpsResetPosition && currentEnemy.health > 0f)
            {
                if (this.seesPlayer && Vector3.Distance(this.transform.position, currentEnemy.transform.position) < 10)
                    Shooting();
                else
                    Chasing();
            }
            else if (patrolWaypoints != null && patrolWaypoints.Length > 0)
                Patrolling();
            else
                Idle();
	        //detecting enemies; currently only detects player
			if(sight.charactersInRange.Count > 0)
				this.DetectNearbyCharacters();
		}
		else
		{
			this.nav.enabled = false;
			this.nav.speed = 0;
			this.anim.SetFloat(HashIDs.speed_float, 0f);
		}
	}

    void Idle()
    {
        nav.speed = 0;
    }
    /// <summary>
    /// This enemy's standard patrol route
    /// </summary>
    void Patrolling()
    {
        nav.speed = desiredSpeed = patrolSpeed;
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

    /// <summary>
    /// Inspecting some anomaly, such as a sound or open door (that shouldn't be open)
    /// </summary>
    void Inspect()
    {
 		
    }

    /// <summary>
    /// Chasing the player
    /// </summary>
    void Chasing()
    {
        
        // Create a vector from the enemy to the last sighting of the player.
        Vector3 sightingDeltaPos = lastPlayerSighting - transform.position;

        // If the the last personal sighting of the player is not close...
        if (/*sightingDeltaPos.sqrMagnitude*/CalculatePathLengthTo(lastPlayerSighting) > 4f)
		{
            // ... set the destination for the NavMeshAgent to the last personal sighting of the player.
            nav.destination = lastPlayerSighting;
		}

        // Set the appropriate speed for the NavMeshAgent.
        nav.speed = desiredSpeed = chaseSpeed;

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
                inspectingArea = resetPos;
                chaseTimer = 0f;
            }
        }
        else
            // If not near the last sighting personal sighting of the player, reset the timer.
            chaseTimer = 0f;
    }

    /// <summary>
    /// Shooting at the player
    /// </summary>
    void Shooting()
    {
		//stop movement
        nav.speed = 0;
        //this.anim.SetFloat(HashIDs.speed_float, 0f);
        this.anim.SetFloat(HashIDs.aimWeight_float, 1f);

		this.Attack(currentEnemy);
    }

    #region Enemy Senses
	/// <summary>
	/// Determines visibility of each character within range
	/// </summary>
	public void DetectNearbyCharacters()
	{
		foreach(CharacterStats ch in sight.charactersInRange)
		{
			float angle = Vector3.Angle(ch.transform.position + Vector3.up - this.eyes.position, this.eyes.forward);
			if ( angle < fov)
			{
				//calculating rays for 3 points on the character
				float charHeight = ch.collider.bounds.max.y - ch.collider.bounds.min.y;
				rayUpper.origin = rayCenter.origin = rayLower.origin = this.eyes.position;
				
				rayUpper.direction =  (ch.collider.bounds.max - Vector3.up*charHeight/8) - this.eyes.position;
				rayLower.direction =  (ch.collider.bounds.min + Vector3.up*charHeight/8) - this.eyes.position;
				rayCenter.direction = (ch.collider.bounds.min + Vector3.up*charHeight/2) - this.eyes.position;
				
				//reducing sight distance at wide angles, to simulate peripheral vision
				//This determines whether or not the enemy can even detect the player
				float adjustedSightDistance = (angle > 30) ?
					sightDistance * (Mathf.Sqrt(fov - angle) / fovSqrt) :
						sightDistance;
				
				//if any rays hit
				if (Physics.Raycast(rayUpper, out hit, adjustedSightDistance, sightLayer) ||
				    Physics.Raycast(rayCenter, out hit, adjustedSightDistance, sightLayer) ||
				    Physics.Raycast(rayLower, out hit, adjustedSightDistance, sightLayer))
				{
					if (hit.collider.tag == Tags.PLAYER)
					{
						this.awarenessOfPlayer += Time.deltaTime;
						
						this.seesPlayer = this.alerted = true;
						this.lastPlayerSighting = ch.transform.position;
						currentEnemy = ch;
						squad.AlertGroup(ch);
                        ai_activity = Chasing;
					}
					else 
					{
                        //this.seesPlayer = false;
                        if (hit.collider.tag == Tags.ENEMY && ch.isDead)
                        {
                            this.Alert(2f);
                            squad.AlertGroup(2f);
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

    public bool Listen(Vector3 source, float volume)
    {
        if (CalculatePathLengthTo(source) * awarenessMultiplier < volume)
        {
            //lastPlayerSighting = source;
            inspectingArea = source;
			return true;
        }
		return false;
    }
    public bool See(Transform tran)
    {
 		if(Physics.Raycast(this.eyes.position, tran.position + Vector3.up, sightLayer))
		{
			this.seesPlayer = this.alerted = true;
			lastPlayerSighting = tran.position;
			return true;
		}
		return false;
    }
    /// <summary>
    /// Puts the NPC on alert and further multiplies their current awareness by _awarenessMultiplier
    /// </summary>
    /// <param name="_awarenessMultiplier"></param>
    public void Alert(float _awarenessMultiplier)
    {
        this.alerted = true;
        this.awarenessMultiplier = Mathf.Min(baseAwarenessMultiplier * _awarenessMultiplier, 1);
        fov = sight.fovAngle * awarenessMultiplier / 2;
        fovSqrt = Mathf.Sqrt(fov);
    }
    public void Alert(float _awarenessMultiplier, Vector3 source)
    {
        this.alerted = true;
        this.awarenessMultiplier = Mathf.Min(baseAwarenessMultiplier * _awarenessMultiplier, 1);
        fov = sight.fovAngle * awarenessMultiplier / 2;
        fovSqrt = Mathf.Sqrt(fov);
        //implement inspection of source
    }
    #endregion
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

    void Attack(CharacterStats target)
    {
		//play animation, aimed at player
		
		//at correct point in animation:

        if (Time.time > nextShotTime)
        {
            if (stats.equippedWeapon != null && Vector3.Distance(this.transform.position, target.transform.position) > 5)
            {
                stats.equippedWeapon.Fire(target);
                //if Automatic weapon, burst-fire
                nextShotTime = Time.time + (attackSpeed * GameController.difficulty_attackSpeedMultiplier);
            }
            else
            {
                //perform melee attack
            }
        }
    }
}
