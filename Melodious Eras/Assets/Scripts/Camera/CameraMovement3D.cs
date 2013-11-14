using UnityEngine;
using System.Collections;

public class CameraMovement3D : CameraMovement 
{
    public float offsetX = 0, offsetY = 0, offsetZ = 0;
    public float smoothness = 5;
    public static Vector3 defaultOffset = new Vector3(-0.5f, 1.5f, -1f);

    Transform player;
    private Transform target = null;
	// Use this for initialization
	void Start () 
    {
        player = GameObject.FindGameObjectWithTag(Tags.PLAYER).transform;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (target == null)
            SmoothLook();
        else

            transform.LookAt(target.position, Vector3.up);

        this.transform.position = Vector3.Lerp( this.transform.position, //third-person camera over-the-shoulder offset
                                                player.transform.position + player.transform.forward * offsetZ + player.transform.right * offsetX + player.transform.up * offsetY , 
                                                smoothness * Time.deltaTime);
	}

    public void SetOffset(Vector3 newOffset)
    {
        offsetX = newOffset.x;
        offsetY = newOffset.y;
        offsetZ = newOffset.z;
        target = null;
    }
    public void SetOffset(Vector3 newOffset, Transform targetP)
    {
        offsetX = newOffset.x;
        offsetY = newOffset.y;
        offsetZ = newOffset.z;
        target = targetP;
    }

    void SmoothLook()
    {
        //looking ahead of the player
        Vector3 relPlayerPosition = player.transform.position + player.transform.forward * 100 + player.transform.up * offsetY - this.transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(relPlayerPosition, Vector3.up);
        transform.rotation = Quaternion.Lerp(this.transform.rotation, lookRotation, smoothness * Time.deltaTime);
    }
}
