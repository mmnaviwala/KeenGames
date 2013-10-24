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
	
	}

    public void SetOffset(Vector3 newOffset)
    {

    }
}
