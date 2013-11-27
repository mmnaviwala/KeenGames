using UnityEngine;
using System.Collections;

public enum CameraOffset { Default, Walk, Crouch, PDA };
public class CameraMovement3D : CameraMovement 
{
    public float offsetX = 0, offsetY = 0, offsetZ = 0;
    public float smoothness = 5;

    public Vector3 defaultOffset = new Vector3(-0.5f, 1.5f, -1f);
    public Vector3 walkOffset;
    public Vector3 crouchOffset;
    public Vector3 PDA_Offset;
    private Vector3 activeOffset;

    public float sensitivity = 10;
    public int inversion = 1; //1 = not inverted, -1 = inverted (for mouse look)
    public Vector3 targetPos;

    Transform player;
    Transform flashlight;
    public Transform target = null;

    public CameraOffset testOffset = CameraOffset.Default;

	// Use this for initialization
	void Start () 
    {
        player = GameObject.FindGameObjectWithTag(Tags.PLAYER).transform;
        flashlight = player.GetComponent<PlayerMovementBasic>().flashlight.transform;
        this.transform.position = player.transform.position + defaultOffset;
	}
	
	// Update is called once per frame
	void LateUpdate ()
    {
        if (Input.GetButtonDown(InputType.SHIFT_VIEW))
        {
            activeOffset = new Vector3(-activeOffset.x, activeOffset.y, activeOffset.z);
            Debug.Log(activeOffset);
        }
        this.transform.position = player.position 
                                + this.transform.right * activeOffset.x
                                + this.transform.up * activeOffset.y
                                + this.transform.forward * activeOffset.z;

        //---------------------------------------------------
        //Testing area
        float intensityX = Input.GetAxis(InputType.MOUSE_X);
        float intensityY = Input.GetAxis(InputType.MOUSE_Y) * inversion;
        
        if (intensityX != 0)
        {
            this.transform.RotateAround(player.position + new Vector3(0, activeOffset.y, 0), this.transform.up, intensityX * sensitivity);
        }

        if (intensityY != 0)
        {
            if (this.transform.eulerAngles.x < 30 || this.transform.eulerAngles.x > 330/*(intensityY > 0 && this.transform.eulerAngles.x < 30) || intensityY < 0 && this.transform.eulerAngles.x > 330*/)
            {
                this.transform.RotateAround(player.position + new Vector3(0, activeOffset.y, 0), this.transform.right, -intensityY * sensitivity);
            }
            else if (this.transform.eulerAngles.x > 30 && intensityY > 0)
            {
                this.transform.RotateAround(player.position + new Vector3(0, activeOffset.y, 0), this.transform.right, -intensityY * sensitivity);
            }
            else if (this.transform.eulerAngles.x < 330 && intensityY < 0)
            {
                this.transform.RotateAround(player.position + new Vector3(0, activeOffset.y, 0), this.transform.right, -intensityY * sensitivity);
            }
        }
        //Following 2 lines need to be done every frame in case something else is causing the character to move
        if (target == null)
            transform.LookAt(player.position + this.transform.up * activeOffset.y + this.transform.right * activeOffset.x, Vector3.up);
        else
            transform.LookAt(target.position, Vector3.up);
        flashlight.rotation = this.transform.rotation;
        //---------------------------------------------------

        SetOffset(testOffset);
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
    public void SetOffset(CameraOffset newOffset)
    {
        switch (newOffset)
        {
            case CameraOffset.Default:  activeOffset = defaultOffset; break;
            case CameraOffset.Walk: activeOffset = walkOffset; break;
            case CameraOffset.Crouch: activeOffset = crouchOffset; break;
            case CameraOffset.PDA: activeOffset = PDA_Offset; break;
        }
    }

    void SmoothLook()
    {
        //looking ahead of the player
        Vector3 relPlayerPosition = player.transform.position + player.transform.forward * 100 + player.transform.up * offsetY - this.transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(relPlayerPosition, Vector3.up);
        transform.rotation = Quaternion.Lerp(this.transform.rotation, lookRotation, smoothness * Time.deltaTime);
    }

    
}
