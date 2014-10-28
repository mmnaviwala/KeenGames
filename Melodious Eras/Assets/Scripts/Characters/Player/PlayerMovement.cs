using UnityEngine;
using System.Collections;

[AddComponentMenu("Scripts/Characters/Player Movement")]
public class PlayerMovement : MonoBehaviour {

    public AudioClip shoutingClip;
    public float turnSmoothing = 15f;
    public float speedDampTime = 0.1f;

    private Animator anim;
    //private HashIDs hash;
    private SphereCollider col;

    void Awake()
    {
        anim = this.GetComponent<Animator>();
        //hash = GameObject.FindGameObjectWithTag(Tags.GAME_CONTROLLER).GetComponent<HashIDs>();
        col = this.GetComponent<SphereCollider>();
        anim.SetLayerWeight(1, 1f);
    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        bool sneak = Input.GetButton(InputType.RUN);

        MovementManagement(h, v, sneak);
    }
    void Update()
    {

    }

    void MovementManagement(float horizontalValue, float verticalValue, bool sneaking)
    {
        if (horizontalValue != 0f || verticalValue != 0f)
        {
            //Debug.Log("H: " + horizontalValue + ", V: " + verticalValue);
            Rotate(horizontalValue, verticalValue);
			anim.SetFloat(HashIDs.speed_float, 5.5f, speedDampTime, Time.deltaTime);

            //Fast but smooth increase in sound bubble (for visual effect)
            if (!sneaking)
                col.radius = (col.radius < 9) ? Mathf.Lerp(col.radius, 10f, 15 * Time.deltaTime) : 10f;
            else
                col.radius = (col.radius > 1) ? Mathf.Lerp(col.radius, 0f, 15 * Time.deltaTime) : 0f;

            //this.col.radius = 10f;
        }
        else
        {
			anim.SetFloat(HashIDs.speed_float, 0f);
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
        Quaternion newRotation = Quaternion.Lerp(GetComponent<Rigidbody>().rotation, targetRotation, turnSmoothing * Time.deltaTime);
        GetComponent<Rigidbody>().MoveRotation(newRotation);
    }

}
