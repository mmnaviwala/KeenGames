using UnityEngine;
using System.Collections;

/// <summary>
/// Used for typical 2D sidescrolling
/// </summary>
public class CameraMovement2D : CameraMovement 
{
    public float smoothness = 5f;
    public Vector3 offset;

    Transform player;
	// Use this for initialization
	void Start () 
    {
        player = GameObject.FindGameObjectWithTag(Tags.PLAYER).transform;
	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 newPos = player.position + offset;
        this.transform.position = Vector3.Lerp(this.transform.position, newPos, smoothness * Time.deltaTime);
	}

    public void SetOffset(float X, float Y, float Z)
    {
        offset.x = X;
        offset.y = Y;
        offset.z = Z;
    }
    public void SetOffset(Vector3 offsetP)
    {
        offset = offsetP;
    }
}
