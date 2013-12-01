using UnityEngine;
using System.Collections;

public enum CameraOffset { Default, Walk, Crouch, PDA , Fighting };
public class CameraMovement3D : CameraMovement 
{
    public float offsetX = 0, offsetY = 0, offsetZ = 0;
    public float smoothness = 5;

    public Vector3 defaultOffset = new Vector3(-0.5f, 1.5f, -1f);
    public Vector3 walkOffset;
    public Vector3 crouchOffset;
    public Vector3 PDA_Offset;
    public Vector3 fightingOffset;
    private Vector3 activeOffset;

    public float x_sensitivity = 5;
    public float y_sensitivity = 2;
    public int inversion = 1; //1 = not inverted, -1 = inverted (for mouse look)

    private Vector3 targetPos, targetLookPos;

    Transform player;
    Transform flashlight;
    public Transform target = null;

    public CameraOffset testOffset = CameraOffset.Default;

    private Animator playerAnim;

	// Use this for initialization
	void Start () 
    {
        player = GameObject.FindGameObjectWithTag(Tags.PLAYER).transform;
        playerAnim = player.GetComponent<Animator>();
        flashlight = player.GetComponent<CharacterStats>().flashlight.transform;

        this.transform.position = player.transform.position + defaultOffset;

        targetPos = player.transform.position + defaultOffset;

        SetOffset(CameraOffset.Default);
	}
	
	// Update is called once per frame
	void LateUpdate ()
    {
        Ray ray = this.camera.ViewportPointToRay(new Vector3(.5f, .5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
        {
            playerAnim.SetLookAtWeight(1, .6f, 1, 1, 1);
            playerAnim.SetLookAtPosition(hit.point);
        }
        else
        {
            playerAnim.SetLookAtWeight(1, .6f, 1, 1, 1);
            playerAnim.SetLookAtPosition(this.transform.position + this.transform.forward * 100);

        }

        if (Input.GetButtonDown(InputType.SHIFT_VIEW))
        {
            AdjustOffset(new Vector3(-activeOffset.x, activeOffset.y, activeOffset.z));
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
            this.transform.RotateAround(player.position + new Vector3(0, activeOffset.y, 0), this.transform.up, intensityX * x_sensitivity);
        }

        if (intensityY != 0)
        {
            if (this.transform.eulerAngles.x < 30 || this.transform.eulerAngles.x > 330/*(intensityY > 0 && this.transform.eulerAngles.x < 30) || intensityY < 0 && this.transform.eulerAngles.x > 330*/)
            {
                this.transform.RotateAround(player.position + new Vector3(0, activeOffset.y, 0), this.transform.right, -intensityY * y_sensitivity);
            }
            else if (this.transform.eulerAngles.x > 30 && intensityY > 0)
            {
                this.transform.RotateAround(player.position + new Vector3(0, activeOffset.y, 0), this.transform.right, -intensityY * y_sensitivity);
            }
            else if (this.transform.eulerAngles.x < 330 && intensityY < 0)
            {
                this.transform.RotateAround(player.position + new Vector3(0, activeOffset.y, 0), this.transform.right, -intensityY * y_sensitivity);
            }
        }
        //Following 2 lines need to be done every frame in case something else is causing the character to move
        if (target == null)
            transform.LookAt(player.position + this.transform.up * activeOffset.y + this.transform.right * activeOffset.x, Vector3.up);
        else
            transform.LookAt(target.position, Vector3.up);
        flashlight.rotation = this.transform.rotation;
        //---------------------------------------------------

        //SetOffset(testOffset);
	}

    /// <summary>
    /// Used for Shift View; Currently only used to switch from right-shoulder to left-shoulder view.
    /// </summary>
    /// <param name="newOffset"></param>
    public void AdjustOffset(Vector3 newOffset)
    {
        switch (testOffset)
        {
            case CameraOffset.Default:  defaultOffset = newOffset;  break;
            case CameraOffset.Walk:     walkOffset = newOffset;     break;
            case CameraOffset.Crouch:   crouchOffset = newOffset;   break;
            case CameraOffset.PDA:      PDA_Offset = newOffset;     break;
            case CameraOffset.Fighting: fightingOffset = newOffset; break;
        }
        activeOffset = newOffset;
        target = null;
    }

    public void SetOffset(Vector3 newOffset, Transform targetP)
    {
        offsetX = newOffset.x;
        offsetY = newOffset.y;
        offsetZ = newOffset.z;
        target = targetP;
    }
    /// <summary>
    /// Changes current camera offset.
    /// </summary>
    /// <param name="newOffset"></param>
    public void SetOffset(CameraOffset newOffset)
    {
        switch (newOffset)
        {
            case CameraOffset.Default:  activeOffset = defaultOffset;   break;
            case CameraOffset.Walk:     activeOffset = walkOffset;      break;
            case CameraOffset.Crouch:   activeOffset = crouchOffset;    break;
            case CameraOffset.PDA:      activeOffset = PDA_Offset;      break;
            case CameraOffset.Fighting: activeOffset = fightingOffset;  break;
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
