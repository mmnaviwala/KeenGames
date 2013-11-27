using UnityEngine;
using System.Collections;

public class CameraMovement3D : CameraMovement 
{
    public float offsetX = 0, offsetY = 0, offsetZ = 0;
    public float smoothness = 5;
    public static Vector3 defaultOffset = new Vector3(-0.5f, 1.5f, -1f);
    public float sensitivity = 10;
    public int inversion = 1; //1 = not inverted, -1 = inverted (for mouse look)
    public Vector3 targetPos;

    Transform player;
    public Transform target = null;
	// Use this for initialization
	void Start () 
    {
        player = GameObject.FindGameObjectWithTag(Tags.PLAYER).transform;
        this.transform.position = player.transform.position + defaultOffset;
	}
	
	// Update is called once per frame
	void LateUpdate ()
    {
        if (Input.GetButtonDown("Shift View"))
            offsetX = -offsetX;
        this.transform.position = player.position 
                                + this.transform.right * offsetX
                                + this.transform.up * offsetY
                                + this.transform.forward * offsetZ;

        //---------------------------------------------------
        //Testing area
        float intensityX = Input.GetAxis("Mouse X");
        if (intensityX != 0)
        {
            this.transform.RotateAround(player.position + new Vector3(0, offsetY, 0), this.transform.up, intensityX * sensitivity);
        }

        float intensityY = Input.GetAxis("Mouse Y") * inversion;
        if (intensityY != 0)
        {
            if (this.transform.eulerAngles.x < 30 || this.transform.eulerAngles.x > 330/*(intensityY > 0 && this.transform.eulerAngles.x < 30) || intensityY < 0 && this.transform.eulerAngles.x > 330*/)
            {
                this.transform.RotateAround(player.position + new Vector3(0, offsetY, 0), this.transform.right, -intensityY * sensitivity);
            }
            else if (this.transform.eulerAngles.x > 30 && intensityY > 0)
            {
                this.transform.RotateAround(player.position + new Vector3(0, offsetY, 0), this.transform.right, -intensityY * sensitivity);
            }
            else if (this.transform.eulerAngles.x < 330 && intensityY < 0)
            {
                this.transform.RotateAround(player.position + new Vector3(0, offsetY, 0), this.transform.right, -intensityY * sensitivity);
            }
            /*if (this.transform.eulerAngles.x < 30 || this.transform.eulerAngles.x > 330)
            {
                this.transform.RotateAround(player.position + new Vector3(0, offsetY, 0), this.transform.right, -intensityY * sensitivity);
            }*/
        }
        /*if ((intensityY > 0 && this.transform.eulerAngles.x < 30) || intensityY < 0 && this.transform.eulerAngles.x > 330)
        {
            Debug.Log("IntensityY: " + intensityY + 
                '\n' + this.transform.forward);
            this.transform.RotateAround(player.position + new Vector3(0, offsetY, 0), this.transform.right, -intensityY * sensitivity);
        }*/
        //this.transform.position += this.transform.right * offsetX;
        transform.LookAt(player.position + this.transform.up * offsetY + this.transform.right * offsetX, Vector3.up);
        //---------------------------------------------------

        /*if (target == null)
            SmoothLook();
        else
            transform.LookAt(target.position, Vector3.up);
        this.transform.position = Vector3.Lerp( this.transform.position, //third-person camera over-the-shoulder offset
                                                player.transform.position + player.transform.forward * offsetZ + player.transform.right * offsetX + player.transform.up * offsetY , 
                                                smoothness * Time.deltaTime);*/
        
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
