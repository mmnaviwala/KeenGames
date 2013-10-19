using UnityEngine;
using System.Collections;

public class CameraMovement2DInverted : CameraMovement 
{
    public float smoothness = 5f;
    public float offsetX = -7f,
                 offsetY = 3f,
                 offsetZ = -10f;

    Transform player;
    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag(Tags.PLAYER).transform;
    }

    // Update is called once per frame
    void Update()
    {
        /*float playerY = this.camera.WorldToScreenPoint(player.position).y;
        if (playerY < Screen.height * .8f && playerY > Screen.height * .5f)
        {
            Vector3 newPos = new Vector3(player.position.x + cameraOffsetX, this.transform.position.y, player.position.z - 10f);
            this.transform.position = Vector3.Lerp(this.transform.position, newPos, cameraSmoothness * Time.deltaTime);
        }
        else
        {*/
        Vector3 newPos = player.position + new Vector3(offsetX, offsetY, offsetZ);
        this.transform.position = Vector3.Lerp(this.transform.position, newPos, smoothness * Time.deltaTime);
        //}
    }
}
