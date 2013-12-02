using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
    public NPCGroup group;
    public CharacterStats player;
    public float hearingMultiplier = 1;     //0 = deaf, 1 = normal, >1 = dogs & security
    public float awarenessMultiplier = 1;   //
    private float lightDifferenceMultiplier; //optional feature. Enemies in high-light areas will find it harder to detect players in low-light areas.
    public float patrolSpeed = 2, 
                 chaseSpeed = 5, 
                 chaseWaitTime = 5, 
                 patrolWaitTime = 2;
    public Transform[] patrolWaypoints;

    private Vector3 lastPlayerSighting;
    private NavMeshAgent nav;

    private float chaseTimer = 0;
    private float patrolTimer = 0;
    private int waypointIndex;

	// Use this for initialization
	void Start () 
    {
        nav = this.GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (patrolWaypoints != null && patrolWaypoints.Length > 0)
            Patrol();
	}

    void OnTriggerEnter(Collider other)
    {
        if (other is CapsuleCollider && other.tag == Tags.PLAYER)
        {
            player = other.GetComponent<CharacterStats>();
        }
    }

    void Patrol()
    {
        nav.speed = patrolSpeed;
        if (nav.remainingDistance < nav.stoppingDistance /*|| nav.destination == lastPlayerSighting*/)
        {
            patrolTimer += Time.deltaTime;
            if (patrolTimer >= patrolWaitTime)
            {
                waypointIndex = (waypointIndex + 1) % patrolWaypoints.Length;
                patrolTimer = 0;
            }
        }
        else
            patrolTimer = 0;

        nav.destination = patrolWaypoints[waypointIndex].position;
    }

    public void Listen()
    {
 
    }
}
