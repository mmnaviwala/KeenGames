using UnityEngine;
using System.Collections;

public class CameraMovement2DInverted : CameraMovement 
{
    public float smoothness = 5f;
    public float offsetX = -7f,
                 offsetY = 3f,
                 offsetZ = -10f;
    public Vector3 offset;

    Transform player;
    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag(Tags.PLAYER).transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = player.position + offset;//new Vector3(offsetX, offsetY, offsetZ);
        this.transform.position = Vector3.Lerp(this.transform.position, newPos, smoothness * Time.deltaTime);
    }
}
