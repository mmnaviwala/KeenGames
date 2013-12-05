using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
    public NPCGroup group;
    public CharacterStats player;
    public float hearingMultiplier = 1;     //0 = deaf, 1 = normal, >1 = dogs & security
    public float awarenessMultiplier = 1;   //
    public float shootingRange, meleeRange;
    private float lightDifferenceMultiplier; //optional feature. Enemies in high-light areas will find it harder to detect players in low-light areas.
    public float patrolSpeed = 2, 
                 chaseSpeed = 5, 
                 chaseWaitTime = 5, 
                 patrolWaitTime = 2;
    public Transform[] patrolWaypoints;

    public bool seesPlayer = false;
    public bool awareOfPlayer = false;

    private Vector3 lastPlayerSighting;
    private NavMeshAgent nav;
    private EnemyStats stats;

    private float chaseTimer = 0;
    private float patrolTimer = 0;
    private int waypointIndex;

	// Use this for initialization
	void Start () 
    {
        nav = this.GetComponent<NavMeshAgent>();
        stats = this.GetComponent<EnemyStats>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (patrolWaypoints != null && patrolWaypoints.Length > 0)
            Patrol();
	}

    void OnTriggerEnter(Collider other)
    {
        if (!other.isTrigger && other is CapsuleCollider && other.tag == Tags.PLAYER)
        {
            player = other.GetComponent<CharacterStats>();
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (!other.isTrigger && other is CapsuleCollider && other.tag == Tags.PLAYER)
        {
            RaycastHit[] hits;
            
            if (Vector3.Angle(other.transform.position - this.transform.position, this.transform.forward) < 50)
            {
                hits = Physics.RaycastAll(this.transform.position, other.transform.position - this.transform.position, Vector3.Distance(this.transform.position, other.transform.position));
                RaycastHit closestHit = hits[0];
                float closestHitDistance = Vector3.Distance(this.transform.position, closestHit.point);
                for (int h = 0; h < hits.Length; h++)
                {
                    if (hits[h].collider.isTrigger)
                        continue;
                    float temp = Vector3.Distance(this.transform.position, hits[h].point);
                    if ( temp < closestHitDistance)
                    {
                        closestHitDistance = temp;
                        closestHit = hits[h];
                    }
                }
                if (closestHit.collider is CapsuleCollider && closestHit.collider.tag == Tags.PLAYER)
                {
                    this.seesPlayer = this.awareOfPlayer = true;
                    Debug.Log("Player sighted!");
                    //stats.attack
                }
            }
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

    void Attack(CharacterStats target)
    {
        target.TakeDamage(10);
    }
}
