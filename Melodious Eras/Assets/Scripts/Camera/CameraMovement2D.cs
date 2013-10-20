using UnityEngine;
using System.Collections;

/// <summary>
/// Used for typical 2D sidescrolling
/// </summary>
public class CameraMovement2D : CameraMovement 
{
    public float smoothness = 5f;
    public Vector3 offset;
    private static bool inverted = false;

    Transform player;
	// Use this for initialization
	void Start () 
    {
        player = GameObject.FindGameObjectWithTag(Tags.PLAYER).transform;
	}
	
	// Update is called once per frame
	void Update ()
    {
        /*float playerY = this.camera.WorldToScreenPoint(player.position).y;
        if (playerY < Screen.height * .8f && playerY > Screen.height * .5f)
        {
            Vector3 newPos = new Vector3(player.position.x + cameraOffsetX, this.transform.position.y, player.position.z - 10f);
            this.transform.position = Vector3.Lerp(this.transform.position, newPos, cameraSmoothness * Time.deltaTime);
        }
        else
        {*/
        Vector3 newPos = player.position + offset;// new Vector3(offsetX, offsetY, offsetZ);
            this.transform.position = Vector3.Lerp(this.transform.position, newPos, smoothness * Time.deltaTime);
        //}
	}

    public static void Invert()
    {
        inverted = true;
    }
    public static void SetToNormal()
    {
        inverted = false;
    }
    public void SetOffset(float X, float Y, float Z)
    {
 
    }
    public void SetOffset(Vector3 offsetP)
    {
        offset = offsetP;
    }
}
