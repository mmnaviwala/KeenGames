using UnityEngine;
using System.Collections;

public class CameraMovement3D : MonoBehaviour 
{
    public float offsetX = 0, offsetY = 0, offsetZ = 0;
    public float smoothness = 5;
    Transform player;
	// Use this for initialization
	void Start () 
    {
        player = GameObject.FindGameObjectWithTag(Tags.PLAYER).transform;
	}
	
	// Update is called once per frame
	void Update () 
    {
        SmoothLook();
        this.transform.position = Vector3.Lerp( this.transform.position, 
                                                player.transform.position + player.transform.forward * offsetZ + player.transform.right * offsetX + player.transform.up * offsetY , 
                                                2 * Time.deltaTime);
	}

    public void SetOffset(Vector3 newOffset)
    {
        offsetX = newOffset.x;
        offsetY = newOffset.y;
        offsetZ = newOffset.z;
    }
    void SmoothLook()
    {
        Vector3 relPlayerPosition = player.transform.position + player.transform.forward * 10 + player.transform.up * offsetY - this.transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(relPlayerPosition, Vector3.up);
        transform.rotation = Quaternion.Lerp(this.transform.rotation, lookRotation, 10 * Time.deltaTime);
    }
}
