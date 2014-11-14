﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum PatrolPattern { Loop, PingPong }

public delegate void AI_Action();

[AddComponentMenu("Scripts/Characters/Enemy AI")]
public class EnemyAI : MonoBehaviour
{
    #region variables
    private const float CHASE_DISTANCE = 25;

    public NPCGroup squad;
	[SerializeField]                private Transform eyes;
    public CharacterStats currentEnemy;

    [SerializeField][Range(0, 2)]   private float hearingMultiplier = 1;     //0 = deaf, 1 = normal, >1 = dogs & security
    [SerializeField][Range(0, 2)]   private float baseAwarenessMultiplier = 1f;
    [SerializeField][Range(0, 360)] private float fieldOfView = 160f;
    [SerializeField]                private float shootingRange;
    [SerializeField]                private float meleeRange;
    [SerializeField][Range(0, 5)]   private float attackSpeed = 1f;
    [SerializeField]                private float patrolSpeed = 1.5f;
    [SerializeField]                private float chaseSpeed = 5;
    [SerializeField]                private float chaseWaitTime = 5;
    [SerializeField]                private float patrolWaitTime = 2;

    [SerializeField]private PatrolPattern patrolPattern = PatrolPattern.Loop;
    private int waypointIterator = 1;
    public Waypoint[] patrolWaypoints;

    private float _awarenessMultiplier;   //
    private float fov;
    private float lightDifferenceMultiplier; //optional feature. Enemies in high-light areas will find it harder to detect players in low-light areas.
    private float nextShotTime = 0;

    private bool _seesPlayer = false;
    private bool _alerted = false;
    private float _sightDistance;
	public LayerMask sightLayer;
    private float _desiredSpeed = 0;

    public Vector3 lastPlayerSighting;
    private Vector3 lpsResetPosition = new Vector3(999, 999, 999);
    private Vector3 inspectingArea = new Vector3(999, 999, 999); //last position to hear a hostile character
    private static Vector3 resetPos = new Vector3(999, 999, 999);
    private float lastPlayerSightingTime = 0;

    private float awarenessOfPlayer = 0f; //0 to 1. When reaching 1, enemy notices player and attacks
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

    public float desiredSpeed       { get { return _desiredSpeed; } }
    public float awarenessMultiplier{ get { return _awarenessMultiplier; } }
    public float sightDistance      { get { return _sightDistance; } }
    public bool seesPlayer          { get { return _seesPlayer; } 
                                      set { _seesPlayer = value; } }
    public bool alerted             { get { return _alerted; } 
                                      set { _alerted = value; } }
    public AI_Action activity       { get { return ai_activity; } 
                                      set { ai_activity = value; } }
    public Vector3 destination      { get { return nav.destination; } 
                                      set { nav.destination = value; } }

    void Awake()
    {
        //awarenessOfChar = new Dictionary<Collider, CharacterStats>();
        nav = this.GetComponent<NavMeshAgent>();
		stats = this.GetComponent<EnemyStats>();
		this.anim = this.GetComponent<Animator>();
        _sightDistance = sight.GetComponent<SphereCollider>().radius;
        lastPlayerSighting = lpsResetPosition;

        this._awarenessMultiplier = baseAwarenessMultiplier;

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
        fov = sight.fovAngle * _awarenessMultiplier;
		//keeping rays permanent to avoid garbage collection
		rayUpper = new Ray();
		rayLower = new Ray();
		rayCenter = new Ray();
        this._seesPlayer = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        this.anim.SetBool(HashIDs.aiming_bool, false);
		if(!this.stats.isDead)
		{
			//this.seesPlayer = false;
            if (lastPlayerSighting != lpsResetPosition && !currentEnemy.isDead)
            {
                if (this._seesPlayer && Vector3.Distance(this.transform.position, currentEnemy.transform.position) < 10)
                    Shooting();
                else
                    Chasing();
            }
            else if (patrolWaypoints != null && patrolWaypoints.Length > 0)
                Patrolling();
            else
                Idle();
	        //detecting enemies; currently only attacks player
			//if(sight.charactersInRange.Count > 0)
			//	this.DetectNearbyCharacters();
		}
		else
		{
			this.nav.enabled = false;
			this.nav.speed = 0;
			this.anim.SetFloat(HashIDs.speed_float, 0f);
		}
	}

    public void Idle()
    {
        nav.speed = 0;
    }
    /// <summary>
    /// This enemy's standard patrol route
    /// </summary>
    public void Patrolling()
    {
        nav.speed = _desiredSpeed = patrolSpeed;
        if (nav.remainingDistance < nav.stoppingDistance /*|| nav.destination == lastPlayerSighting*/)
        {
            patrolTimer += Time.deltaTime;
            if (patrolTimer >= patrolWaypoints[waypointIndex].waitTime)
            {
                //reverses iterator if it reaches the end of the array
                if (patrolPattern == PatrolPattern.PingPong &&
                   ((waypointIterator == -1 && waypointIndex == 0) ||
                    (waypointIterator == 1 && waypointIndex == patrolWaypoints.Length - 1)))
                {
                    waypointIterator = -waypointIterator;
                }
                waypointIndex = (waypointIndex + waypointIterator) % patrolWaypoints.Length; // % takes care of Loop patrols
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
    public void Inspect()
    {
        nav.stoppingDistance = 3;
        nav.destination = inspectingArea;
    }

    /// <summary>
    /// Chasing the player
    /// </summary>
    public void Chasing()
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
        nav.speed = _desiredSpeed = chaseSpeed;

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
    public void Shooting()
    {
		//stop movement
        nav.speed = 0;
        this.anim.SetFloat(HashIDs.speed_float, 0f);
        this.anim.SetBool(HashIDs.aiming_bool, true);

		this.Attack(currentEnemy);
    }

    #region Enemy Senses
    public void Notice(CharacterStats character, float noticeability = 100f)
    {
        this.awarenessOfPlayer += noticeability;

        if(awarenessOfPlayer > 1f)
        {
            this._seesPlayer = this._alerted = true;
            this.lastPlayerSighting = character.transform.position;
            currentEnemy = character;
            squad.AlertGroup(character);

            ai_activity = Chasing;
        }
    }

    public bool Listen(Vector3 source, float volume)
    {
        if (CalculatePathLengthTo(source) / _awarenessMultiplier < volume)
        {
            //lastPlayerSighting = source;
            ai_activity = Inspect;
            inspectingArea = source;
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
        this._alerted = true;
        this._awarenessMultiplier = Mathf.Min(baseAwarenessMultiplier * _awarenessMultiplier, 1);
        fov = Mathf.Clamp(sight.fovAngle * _awarenessMultiplier,  0,  80);
    }
    public void Alert(float _awarenessMultiplier, Vector3 source)
    {
        this._alerted = true;
        this._awarenessMultiplier = Mathf.Min(baseAwarenessMultiplier * _awarenessMultiplier, 1);
        fov = Mathf.Clamp(sight.fovAngle * _awarenessMultiplier, 0, 80);
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
		//at correct point in animation:
        if (Time.time > nextShotTime)
        {
            if (stats.equippedWeapon != null/* && Vector3.Distance(this.transform.position, target.transform.position) > 5*/)
            {
                if(stats.equippedWeapon is MeleeWeapon || stats.equippedWeapon is SemiAutoWeapon)
                    stats.equippedWeapon.Fire(target);
                else if (stats.equippedWeapon is AutoWeapon)
                {
                    //if Automatic weapon, burst-fire
                }
                nextShotTime = Time.time + (attackSpeed * Difficulty.attackSpeedMultiplier);
            }
            else //once melee is working
            {
                //perform melee attack
            }
        }
    }
}
