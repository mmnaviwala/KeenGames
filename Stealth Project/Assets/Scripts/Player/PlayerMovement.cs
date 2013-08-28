using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour 
{
    public AudioClip shoutingClip;
    public float turnSmoothing = 15f;
    public float speedDampTime = 0.1f;

    private Animator anim;
    private HashIDs hash;
    private SphereCollider col;

    void Awake()
    {
        anim = this.GetComponent<Animator>();
        hash = GameObject.FindGameObjectWithTag(Tags.GAME_CONTROLLER).GetComponent<HashIDs>();
        col = this.GetComponent<SphereCollider>();
        anim.SetLayerWeight(1, 1f);
    }
    
    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        bool sneak = Input.GetButton("Sneak");

        MovementManagement(h, v, sneak);
    }
    void Update()
    {
        bool shout = Input.GetButtonDown("Attract");
        anim.SetBool(hash.shoutingBool, shout);
        this.AudioManagement(shout);
    }
    /*void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && other is CapsuleCollider)
        {
            other.GetComponent<EnemySight>().playerInSight = true;

            int playerLayerZeroStateHash = this.anim.GetCurrentAnimatorStateInfo(0).nameHash;
            int playerLayerOneStateHash = this.anim.GetCurrentAnimatorStateInfo(1).nameHash;

            if (playerLayerZeroStateHash == hash.locomotionState || playerLayerOneStateHash == hash.shoutState)
            {
                if (CalculatePathLength(this.transform.position) <= this.col.radius)
                    other.GetComponent<EnemySight>().personalLastSighting = this.transform.position;
            }
        }
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
    }*/

    void MovementManagement(float horizontalValue, float verticalValue, bool sneaking)
    {
        anim.SetBool(hash.sneakingBool, sneaking);

        if (horizontalValue != 0f || verticalValue != 0f)
        {
            //Debug.Log("H: " + horizontalValue + ", V: " + verticalValue);
            Rotate(horizontalValue, verticalValue);
            anim.SetFloat(hash.speedFloat, 5.5f, speedDampTime, Time.deltaTime);

            //Fast but smooth increase in sound bubble (for visual effect)
            if(!sneaking)
                col.radius = (col.radius < 9) ? Mathf.Lerp(col.radius, 10f, 15 * Time.deltaTime) : 10f;
            else
                col.radius = (col.radius > 1) ? Mathf.Lerp(col.radius, 0f, 15 * Time.deltaTime) : 0f;

            //this.col.radius = 10f;
        }
        else
        {
            anim.SetFloat(hash.speedFloat, 0f);
            col.radius = (col.radius > 1) ? Mathf.Lerp(col.radius, 0f, 15 * Time.deltaTime) : 0f;
            //this.col.radius = 0f;
        }
    }

    void Rotate(float horizontal, float vertical)
    {
        //if holding down W(up) and D(right), targetDirection would be up-and-right along the floor
        Vector3 targetDirection = new Vector3(horizontal, 0f, vertical);
        //sets up rotation to new direction, rotating around the y-axis (Vector3.up)
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
        //performs a smooth rotation from current direction to target direction
        Quaternion newRotation = Quaternion.Lerp(rigidbody.rotation, targetRotation, turnSmoothing * Time.deltaTime);
        rigidbody.MoveRotation(newRotation);
    }

    void AudioManagement(bool shout)
    {
        if (anim.GetCurrentAnimatorStateInfo(0).nameHash == hash.locomotionState)
        {
            if (!this.audio.isPlaying)
                this.audio.Play();
            
        }
        else
            this.audio.Stop();

        if (shout)
        {
            AudioSource.PlayClipAtPoint(shoutingClip, this.transform.position);
        }
    }
}
