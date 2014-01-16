using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum CameraOffset { Default, Aim, Crouch, PDA , Fighting, ClimbUp, ClimbDown, Hacking};
public enum CameraFollowSpeed { Default = 4, Aiming = 10 };

[AddComponentMenu("Scripts/Camera/Camera Movement 3D")]
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
	public Transform target = null;
	public bool atTargetPos = false;

    private int invertOffset = 1; //-1 = inversion of x-offset
	private float raycastDistance = 1;

    private Vector3 targetPos, targetLookPos;
    private GameObject camTargetPos, camLookPos;

    Transform player;
    Transform flashlight;
    GameObject go;
	ImageEffectBase nightVision, fisheye, noiseAndGrain;

    private Animator playerAnim; //making the player look in the camera's direction

	// Use this for initialization
	void Start () 
    {
        player = GameObject.FindGameObjectWithTag(Tags.PLAYER).transform;
        Debug.Log(player == null);
        playerAnim = player.GetComponent<Animator>();
        flashlight = player.GetComponent<PlayerStats>().flashlight.transform;

		nightVision = this.GetComponent<NightVisionTestCS>();
		fisheye = this.GetComponent ("Fisheye") as ImageEffectBase;
		noiseAndGrain = this.GetComponent ("NoiseAndGrain") as ImageEffectBase;

        camTargetPos = new GameObject();
        camTargetPos.transform.position = player.transform.position;

        SetOffset(CameraOffset.Default);

        go = new GameObject();
        go.name = "camTarget";
        go.transform.position = player.position + new Vector3(0, activeOffset.y, 0);
        go.transform.rotation = player.rotation;
	}
	
	// Update is called once per frame
	void LateUpdate ()
    {
		if(Input.GetButtonDown(InputType.TOGGLE_NIGHTVISION))
		{
			nightVision.enabled = !nightVision.enabled;
			fisheye.enabled = !fisheye.enabled;
			noiseAndGrain.enabled = !noiseAndGrain.enabled;
		}

		if (!(activeOffset.Equals(crouchOffset) || activeOffset.Equals(climbUpOffset) || activeOffset.Equals(climbDownOffset)))
			TurnPlayerHead();
		
		if (Input.GetButtonDown(InputType.SHIFT_VIEW))
			InvertOffset();
		
		//Adjusting look rotation
		float intensityX = Input.GetAxis(InputType.MOUSE_X);
		float intensityY = Input.GetAxis(InputType.MOUSE_Y) * invertLook;
		
		if (intensityX != 0)
		{
			//atTargetPos = false;
			go.transform.rotation = Quaternion.Euler(go.transform.eulerAngles.x, go.transform.eulerAngles.y + intensityX * x_sensitivity, go.transform.eulerAngles.z);
		}
		if (intensityY != 0)
		{
			//atTargetPos = false;
			float angle = go.transform.eulerAngles.x - intensityY * y_sensitivity;
			if (angle > 180) angle -= 360;
			angle = Mathf.Clamp(angle, -80, 80);
			go.transform.rotation = Quaternion.Euler(angle, 
			                                         go.transform.eulerAngles.y,
			                                         go.transform.eulerAngles.z);
		}
		
		//Deciding if we should manipulate the camera
		//Saving calculation/render time if camera doesn't have to move
		Vector3 lookTemp = targetLookPos;
		targetLookPos = player.position + player.up * activeOffset.y + go.transform.right * activeOffset.x /* + player.forward * .125f*/;
		if (lookTemp != targetLookPos)
			atTargetPos = false;
		
		if (!atTargetPos)
		{
			this.transform.rotation = go.transform.rotation;
			flashlight.rotation = this.transform.rotation;
			Vector3 offsetDirection = this.transform.forward * activeOffset.z;

			//Preventing clipping
			RaycastHit hit;
			if(Physics.Raycast (targetLookPos, offsetDirection, out hit, raycastDistance))
			{
				/*this.transform.position = */camTargetPos.transform.position = hit.point + this.transform.forward;
				Debug.DrawLine(targetLookPos, camTargetPos.transform.position);
				//return;
			}
			else
				camTargetPos.transform.position = targetLookPos + offsetDirection + player.forward * .125f;

			//Smoothly moving toward target
			if (Vector3.Distance(this.transform.position, camTargetPos.transform.position) > .01f)
				this.transform.position = Vector3.Lerp(this.transform.position, camTargetPos.transform.position, followSpeed * Time.deltaTime);
			else
			{
				this.transform.position = camTargetPos.transform.position;
				atTargetPos = true;
			}
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

    public void InvertOffset()
    {
        invertOffset = -invertOffset;
        activeOffset.x = -activeOffset.x;
    }

    public void SetSideView(float xOffset)
    {
        activeOffset.x = xOffset;
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