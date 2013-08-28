using UnityEngine;
using System.Collections;

public class EnemySight : MonoBehaviour 
{
    public float fovAngle = 110f;
    public bool playerInSight;
    public Vector3 personalLastSighting;

    private NavMeshAgent nav;
    private SphereCollider col;
    private Animator anim;
    public LastPlayerSighting lps;
    private GameObject player;
    private Animator playerAnim;
    private PlayerHealth playerHealth;
    private HashIDs hash;
    private Vector3 previousSighting;

    void Awake()
    {
        nav = this.GetComponent<NavMeshAgent>();
        col = this.GetComponent<SphereCollider>();
        anim = this.GetComponent<Animator>();
        lps = GameObject.FindGameObjectWithTag(Tags.GAME_CONTROLLER).GetComponent<LastPlayerSighting>();

        player = GameObject.FindGameObjectWithTag(Tags.PLAYER);
        playerAnim = player.GetComponent<Animator>();
        playerHealth = player.GetComponent<PlayerHealth>();

        hash = GameObject.FindGameObjectWithTag(Tags.GAME_CONTROLLER).GetComponent<HashIDs>();

        personalLastSighting = LastPlayerSighting.resetPosition;
        previousSighting = LastPlayerSighting.resetPosition;
    }

    void Update()
    {
        if(lps.position != previousSighting)
            personalLastSighting = lps.position;
        previousSighting = lps.position;

        if (playerHealth.health > 0f)
            anim.SetBool(hash.playerInSightBool, playerInSight);
        else
            anim.SetBool(hash.playerInSightBool, false);
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject == player && other is CapsuleCollider)
        {
            playerInSight = false;

            Vector3 direction = other.transform.position - this.transform.position;
            float angle = Vector3.Angle(direction, this.transform.forward);

            if (angle < fovAngle * 0.5f)
            {
                RaycastHit hit;
                //transform.up = 1 unit up (Y), direction.normalized returns direction's unit vector, stores in hit; raycast length = the radius of the sphere
                if (Physics.Raycast(this.transform.position + transform.up, direction.normalized, out hit, col.radius))
                {
                    if (hit.collider.gameObject == player)
                    {
                        playerInSight = true;
                        lps.position = player.transform.position;
                    }
                }
            }
            //checking for if the player can be heard (via shouting or running), based on the player's animation
            int playerLayerZeroStateHash = playerAnim.GetCurrentAnimatorStateInfo(0).nameHash;
            int playerLayerOneStateHash = playerAnim.GetCurrentAnimatorStateInfo(1).nameHash;
            //if player is currently shouting or running/walking
            if (playerLayerZeroStateHash == hash.locomotionState || playerLayerOneStateHash == hash.shoutState)
            {
                if (CalculatePathLength(player.transform.position) <= this.col.radius)
                {
                    personalLastSighting = player.transform.position;
                    lps.position = player.transform.position;
                }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player && other is SphereCollider)
            playerInSight = false;
    }

    /// <summary>
    /// Calculating the length of the path between this transform (enemy) and the target (player).
    /// Since the Nav Mesh Agent covers the whole floor/walking area and NOT the walls, the path will naturally avoid walls
    /// </summary>
    /// <param name="targetPosition"></param>
    /// <returns></returns>
    float CalculatePathLength(Vector3 targetPosition)
    {
        NavMeshPath path = new NavMeshPath();

        if (nav.enabled)
            nav.CalculatePath(targetPosition, path);

        for (int i = 0; i < path.corners.Length - 1; i++)
            Debug.DrawLine(path.corners[i], path.corners[i + 1]);

        Vector3[] pathWaypoints = new Vector3[path.corners.Length + 2];
        
        pathWaypoints[0] = transform.position;
        pathWaypoints[pathWaypoints.Length - 1] = targetPosition;

        for (int wp = 0; wp < pathWaypoints.Length - 1; wp++)
        {
            pathWaypoints[wp + 1] = pathWaypoints[wp];
        }
        //summing up the distance between each waypoint
        float pathLength = 0f;
        for (int wp = 0; wp < pathWaypoints.Length - 1; wp++)
            pathLength += Vector3.Distance(pathWaypoints[wp], pathWaypoints[wp + 1]);

        return pathLength;
    }
}
