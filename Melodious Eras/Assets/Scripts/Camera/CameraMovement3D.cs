using UnityEngine;
using System.Collections;

public enum CameraOffset { Default, Aim, Crouch, PDA , Fighting, ClimbUp, ClimbDown, Hacking};
public class CameraMovement3D : CameraMovement 
{
    public float followSpeed = 5;

    public Vector3 defaultOffset = new Vector3(-0.5f, 1.5f, -1f);
    public Vector3 aimOffset;
    public Vector3 crouchOffset;
    public Vector3 PDA_Offset;
    public Vector3 HackingOffset;
    public Vector3 fightingOffset;
    public Vector3 climbUpOffset, climbDownOffset;
    private Vector3 activeOffset;

    public float x_sensitivity = 5;
    public float y_sensitivity = 2;
    public int invertLook = 1; //1 = not inverted, -1 = inverted (for mouse look)
    private int invertOffset = 1; //-1 = inversion of x-offset

    private Vector3 targetPos, targetLookPos;
    private GameObject camTargetPos, camLookPos;

    Transform player;
    Transform flashlight;
    public Transform target = null;

    private Animator playerAnim;

	// Use this for initialization
	void Start () 
    {
        player = GameObject.FindGameObjectWithTag(Tags.PLAYER).transform;
        playerAnim = player.GetComponent<Animator>();
        flashlight = player.GetComponent<PlayerStats>().flashlight.transform;

        camTargetPos = new GameObject();
        camTargetPos.transform.position = player.transform.position;

        this.transform.position = player.transform.position + defaultOffset;

        targetPos = player.transform.position + defaultOffset;

        SetOffset(CameraOffset.Default);
	}
	
	// Update is called once per frame
	void LateUpdate ()
    {
        /*if(!(activeOffset.Equals(crouchOffset) || activeOffset.Equals(climbUpOffset) || activeOffset.Equals(climbDownOffset)))
            TurnPlayerHead();

        if (Input.GetButtonDown(InputType.SHIFT_VIEW))
        {
            InvertOffset();
            //AdjustOffset(new Vector3(-activeOffset.x, activeOffset.y, activeOffset.z));
            Debug.Log(activeOffset);
        }
        this.transform.position = player.position 
                                + this.transform.right * activeOffset.x
                                + this.transform.up * activeOffset.y
                                + this.transform.forward * activeOffset.z;

        //---------------------------------------------------
        //Testing area
        float intensityX = Input.GetAxis(InputType.MOUSE_X);
        float intensityY = Input.GetAxis(InputType.MOUSE_Y) * invertLook;
        
        if (intensityX != 0)
        {
            this.transform.RotateAround(player.position + new Vector3(0, activeOffset.y, 0), this.transform.up, intensityX * x_sensitivity);
        }

        if (intensityY != 0)
        {
            if (this.transform.eulerAngles.x < 30 || this.transform.eulerAngles.x > 330)
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
        {
            transform.LookAt(player.position + this.transform.up * activeOffset.y + this.transform.right * activeOffset.x, Vector3.up);
            //transform.LookAt(player.position + Vector3.up * activeOffset.y + this.transform.right * activeOffset.x, Vector3.up);
        }
        else
            transform.LookAt(target.position, Vector3.up);
        flashlight.rotation = this.transform.rotation;
        //---------------------------------------------------

        //SetOffset(testOffset);
        */


        if (!(activeOffset.Equals(crouchOffset) || activeOffset.Equals(climbUpOffset) || activeOffset.Equals(climbDownOffset)))
            TurnPlayerHead();

        if (Input.GetButtonDown(InputType.SHIFT_VIEW))
        {
            InvertOffset();
            //AdjustOffset(new Vector3(-activeOffset.x, activeOffset.y, activeOffset.z));
            Debug.Log(activeOffset);
        }
        camTargetPos.transform.position = player.position
                                + camTargetPos.transform.right * activeOffset.x
                                + camTargetPos.transform.up * activeOffset.y
                                + camTargetPos.transform.forward * activeOffset.z;

        //---------------------------------------------------
        //Testing area
        float intensityX = Input.GetAxis(InputType.MOUSE_X);
        float intensityY = Input.GetAxis(InputType.MOUSE_Y) * invertLook;

        if (intensityX != 0)
        {
            camTargetPos.transform.RotateAround(player.position + new Vector3(0, activeOffset.y, 0), camTargetPos.transform.up, intensityX * x_sensitivity);
        }

        if (intensityY != 0)
        {
            if (this.transform.eulerAngles.x < 30 || this.transform.eulerAngles.x > 330)
            {
                camTargetPos.transform.RotateAround(player.position + new Vector3(0, activeOffset.y, 0), camTargetPos.transform.right, -intensityY * y_sensitivity);
            }
            else if (this.transform.eulerAngles.x > 30 && intensityY > 0)
            {
                camTargetPos.transform.RotateAround(player.position + new Vector3(0, activeOffset.y, 0), camTargetPos.transform.right, -intensityY * y_sensitivity);
            }
            else if (this.transform.eulerAngles.x < 330 && intensityY < 0)
            {
                camTargetPos.transform.RotateAround(player.position + new Vector3(0, activeOffset.y, 0), camTargetPos.transform.right, -intensityY * y_sensitivity);
            }
        }
        //Following 2 lines need to be done every frame in case something else is causing the character to move
        if (target == null)
        {
            
            camTargetPos.transform.LookAt(player.position + camTargetPos.transform.up * activeOffset.y + camTargetPos.transform.right * activeOffset.x, Vector3.up);
            this.transform.rotation = camTargetPos.transform.rotation;
            //transform.LookAt(player.position + Vector3.up * activeOffset.y + this.transform.right * activeOffset.x, Vector3.up);
        }
        else
            transform.LookAt(target.position, Vector3.up);
        flashlight.rotation = camTargetPos.transform.rotation;
        this.transform.position = Vector3.Lerp(this.transform.position, camTargetPos.transform.position, followSpeed * Time.deltaTime);
        //---------------------------------------------------
        
        //SetOffset(testOffset);
	}
    void NewCameraRotation()
    {
        RaycastHit hit;
        float range = activeOffset.magnitude;
        Vector3 direction = this.transform.right * activeOffset.x + this.transform.up * activeOffset.y + this.transform.forward * activeOffset.z;

        if (Physics.Raycast(player.transform.position, direction, out hit, range))
        {
            camTargetPos.transform.position = hit.point;
            this.transform.position = Vector3.Lerp(this.transform.position, hit.point, 5 * Time.deltaTime);
        }
        else
        {
            
        }
    }

    void TurnPlayerHead()
    {
        Ray ray = this.camera.ViewportPointToRay(new Vector3(.5f, .5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100) && hit.collider.tag != Tags.PLAYER && Vector3.Distance(hit.point, player.position) > 1)
        {
            
            playerAnim.SetLookAtWeight(1, .5f, 1, 1, 1);
            playerAnim.SetLookAtPosition(hit.point);
        }
        else
        {
            playerAnim.SetLookAtWeight(1, .5f, 1, 1, 1);
            playerAnim.SetLookAtPosition(this.transform.position + this.transform.forward * 100);
        } 
    }

    void CalculateLookPos()
    {
        Vector3 faceHeight = player.transform.position + player.transform.up * 1.5f;

        RaycastHit hit;
        if(Physics.Raycast(faceHeight, player.transform.right * invertOffset, out hit, Mathf.Abs(activeOffset.x)))
        {

        }
    }
    void CalculateCamPos()
    {
        RaycastHit hit;

        this.transform.position = Vector3.Lerp(this.transform.position, targetPos, 5 * Time.deltaTime);
        this.transform.LookAt(targetLookPos);
    }

    public void InvertOffset()
    {
        invertOffset = -invertOffset;
        activeOffset.x = -activeOffset.x;
    }

    public void SetOffset(CameraOffset newOffset, Transform targetP)
    {
        SetOffset(newOffset);
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
            case CameraOffset.Aim:     activeOffset = aimOffset;      break;
            case CameraOffset.Crouch:   activeOffset = crouchOffset;    break;
            case CameraOffset.PDA:      activeOffset = PDA_Offset;      break;
            case CameraOffset.Fighting: activeOffset = fightingOffset;  break;
            case CameraOffset.ClimbUp:  activeOffset = climbUpOffset;   break;
            case CameraOffset.ClimbDown:activeOffset = climbDownOffset; break;
            case CameraOffset.Hacking: activeOffset = HackingOffset; break;
        }
        activeOffset.x *= invertOffset;
    }
}