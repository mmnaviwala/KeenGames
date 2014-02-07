using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum CameraOffset { Default, Aim, Crouch, PDA , Fighting, ClimbUp, ClimbDown, Hacking};
public enum CameraFollowSpeed { Default = 4, Aiming = 10 };

[AddComponentMenu("Scripts/Camera/Camera Movement 3D")]
public class CameraMovement3D : CameraMovement 
{
    public float followSpeed = 5;

    //various camera offsets
    public Vector3 defaultOffset = new Vector3(-0.5f, 1.5f, -1f); //relative to player. x = left/right, y = up/down, z = forward/backward
    public Vector3 aimOffset;
    public Vector3 crouchOffset;
    public Vector3 PDA_Offset;
    public Vector3 HackingOffset;
    public Vector3 fightingOffset;
    public Vector3 climbUpOffset, climbDownOffset;
    public Vector3 activeOffset;

    public float x_sensitivity = 5;             //mouse X sensitivity
    public float y_sensitivity = 2;             //mouse Y sensitivity
	public int invertLook = 1;                  //1 = not inverted, -1 = inverted (for mouse look)
	public Transform target = null;             //for following target other than player
	public bool atTargetPos = false;            //if atTargetPos = true, saves calculations

    private int invertOffset = 1;               //-1 = inversion of x-offset
	private float raycastDistance = 1;          //distance for raycast to avoid clipping through walls

    private Vector3 targetLookPos;              //targetLookPos = position over player's shoulder to look at
    private Vector3 camTargetPos;

    Transform player;
    Transform flashlight;
    public GameObject camRotationHelper;        //helping with camera rotation


	// Use this for initialization
	void Start () 
    {
        player = GameObject.FindGameObjectWithTag(Tags.PLAYER).transform;
		flashlight = player.GetComponent<PlayerStats>().flashlight.transform;

        camTargetPos = player.transform.position;

        SetOffset(CameraOffset.Default);
		if(camRotationHelper == null)
		{
			camRotationHelper = new GameObject();
			camRotationHelper.name = "camTarget";
		}
        camRotationHelper.transform.position = player.position + new Vector3(0, activeOffset.y, 0);
        camRotationHelper.transform.rotation = player.rotation;
	}
	
	// LateUpdate is called once per frame
	void LateUpdate ()
    {		
		if (Input.GetButtonDown(InputType.SHIFT_VIEW))
			InvertOffset();
		
		//Adjusting look rotation
		float intensityX = Input.GetAxis(InputType.MOUSE_X);
		float intensityY = Input.GetAxis(InputType.MOUSE_Y) * invertLook;
		
		if (intensityX != 0)
		{
			camRotationHelper.transform.rotation = Quaternion.Euler(camRotationHelper.transform.eulerAngles.x, camRotationHelper.transform.eulerAngles.y + intensityX * x_sensitivity, camRotationHelper.transform.eulerAngles.z);
		}
		if (intensityY != 0)
		{
			float angle = camRotationHelper.transform.eulerAngles.x - intensityY * y_sensitivity;
			if (angle > 180) angle -= 360;
			angle = Mathf.Clamp(angle, -80, 80);
			camRotationHelper.transform.rotation = Quaternion.Euler(angle, camRotationHelper.transform.eulerAngles.y, camRotationHelper.transform.eulerAngles.z);
		}
		
		//Deciding if we should manipulate the camera
		//Saving calculation/render time if camera doesn't have to move
		Vector3 lookTemp = targetLookPos;

		Vector3 lookPosOffset = player.up * activeOffset.y + camRotationHelper.transform.right * activeOffset.x;
		targetLookPos = player.position +  lookPosOffset/* + player.forward * .125f*/;
		RaycastHit hit1;
		if(Physics.Raycast (player.position + player.up * activeOffset.y, camRotationHelper.transform.right * activeOffset.x, out hit1, Mathf.Abs(activeOffset.x)))
		{
			InvertOffset();
		}

		if (lookTemp != targetLookPos)
			atTargetPos = false;

		if (!atTargetPos)
		{
			this.transform.rotation = camRotationHelper.transform.rotation;

			Vector3 offsetDirection = this.transform.forward * activeOffset.z;

			//Preventing clipping
			RaycastHit hit;

			Ray ray = new Ray(targetLookPos, offsetDirection);
			if(Physics.Raycast (ray, out hit, raycastDistance) && hit.collider.tag != Tags.MAIN_CAMERA)
			{
				camTargetPos = hit.point - ray.direction;
			}
			else
				camTargetPos = targetLookPos + offsetDirection;

			//moving flashlight
			Vector3 fOrigin = player.position + new Vector3(0, activeOffset.y, 0);

			flashlight.rotation = this.transform.rotation;
			if(Physics.Raycast (fOrigin, this.transform.forward, out hit, 0.5f))
				flashlight.position =  fOrigin + this.transform.forward / 8;
			else 
				flashlight.position =  fOrigin + this.transform.forward / 2;

			//Smoothly moving toward target
			if (Vector3.Distance(this.transform.position, camTargetPos) > .01f)
				this.transform.position = Vector3.Lerp(this.transform.position, camTargetPos, followSpeed * Time.deltaTime);
			else
			{
				this.transform.position = camTargetPos;
				atTargetPos = true;
			}
		}
	}

    /// <summary>
    /// Just inverts left/right offset
    /// </summary>
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
		raycastDistance = activeOffset.magnitude;
    }

}