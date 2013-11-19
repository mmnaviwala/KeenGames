using UnityEngine;
using System.Collections;

/// <summary>
/// Used for typical 2D sidescrolling
/// </summary>
public class CameraMovement2D : CameraMovement 
{
    public float smoothness = 5f;
    public Vector3 offset;
    public static Vector3 defaultOffset = new Vector3(9, 1, -10);

    Transform player;
    PlayerMovementBasic playerMovement;
	// Use this for initialization
	void Start () 
    {
        player = GameObject.FindGameObjectWithTag(Tags.PLAYER).transform;
        playerMovement = player.GetComponent<PlayerMovementBasic>();
        Debug.Log(this.GetComponent<SphereCollider>() == null);
	}
	
	// Update is called once per frame
	void Update ()
    {
        //Debug.Log(this.transform.rotation);
        if ((offset.x > 0) != (playerMovement.facingDirection2D == FacingDirection2D.Right))
            offset.x = -offset.x;

        //else if (offset.z < 0 && playerMovement.facingDirection2D == FacingDirection2D.Right)
        //    offset.z = -offset.z;
        Vector3 newPos = player.position + offset;
        this.transform.position = Vector3.Lerp(this.transform.position, newPos, smoothness * Time.deltaTime);
	}

    void OnCollisionEnter(Collision col)
    {
        Debug.Log("Camera Collision: " + col.contacts[0].normal);
    }
    void OnCollisionStay(Collision col)
    {
        Debug.Log("Camera collision stay");
    }

    public void SetOffset(float X, float Y, float Z)
    {
        offset.x = X;
        offset.y = Y;
        offset.z = Z;
        this.transform.rotation = new Quaternion(0, 0, 0, 1);
        Debug.Log(this.transform.rotation);
    }
    public void SetOffset(Vector3 offsetP)
    {
        offset = offsetP;
        this.transform.rotation = new Quaternion(0, 0, 0, 1);
        Debug.Log(this.transform.rotation);
    }
}
