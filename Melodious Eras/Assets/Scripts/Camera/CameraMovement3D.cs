using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum CameraOffset { Default, Aim, Crouch, PDA , Fighting, ClimbUp, ClimbDown, Hacking, CrouchAim};
public enum CameraFollowSpeed { Default = 4, Aiming = 10 };

[AddComponentMenu("Scripts/Camera/Camera Movement 3D")]
public class CameraMovement3D : CameraMovement 
{
    [System.Serializable]
    public class Offsets
    {
        public Vector3 defaultOffset; //relative to player. x = left/right, y = up/down, z = forward/backward
        public Vector3 aim;
        public Vector3 crouch;
        public Vector3 PDA;
        public Vector3 Hacking;
        public Vector3 fightingt;
        public Vector3 climbUp, climbDown;
        public Vector3 crouchAim;

    }
    [SerializeField] public Offsets offsets;
    public Vector3 activeOffset;
    public float followSpeed = 5;

    //various camera offsets

    public float x_sensitivity = 5;             //mouse X sensitivity
    public float y_sensitivity = 2;             //mouse Y sensitivity
	public int invertLook = 1;                  //1 = not inverted, -1 = inverted (for mouse look)
	public bool atTargetPos = false;            //if atTargetPos = true, saves calculations

    private int invertOffset = 1;               //-1 = inversion of x-offset
	private float raycastDistance = 1;          //distance for raycast to avoid clipping through walls
    [SerializeField] private LayerMask raycastLayers;

    private Vector3 targetLookPos;              //targetLookPos = position over player's shoulder to look at
    private Vector3 camTargetPos;

    Transform player;
    Transform flashlight;
    public GameObject camRotationHelper;        //helping with camera rotation

    private bool shaking;
    private YieldInstruction eof;


    private Vector3 xz_direction = new Vector3(0, 0, 0); //using same vector to avoid garbage collection
    /// <summary>
    /// <para>Returns the X and Y components of the camera's facing direction.</para>
    /// <para>Used primarily for player movement, but figured it belongs
    /// in the camera class in case anything else needs it.</para>
    /// </summary>
    public Vector3 XZdirection
    {
        get {
            xz_direction.x = this.transform.forward.x;
            xz_direction.z = this.transform.forward.z;
            return xz_direction;
        }
    }

    void Awake()
    {
        camRotationHelper = new GameObject();
        camRotationHelper.name = "camTarget";

        GameController.ShowCursor(false);
    }
	// Use this for initialization
	void Start () 
    {
        player = GameObject.FindGameObjectWithTag(Tags.PLAYER).transform;
		flashlight = player.GetComponent<PlayerStats>().flashlight.transform;
        eof = new WaitForEndOfFrame();

        camTargetPos = player.transform.position;

        SetOffset(CameraOffset.Default);
		
		
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
			if (angle > 180) 
                angle -= 360;
			angle = Mathf.Clamp(angle, -80, 80);
			camRotationHelper.transform.rotation = Quaternion.Euler(angle, camRotationHelper.transform.eulerAngles.y, camRotationHelper.transform.eulerAngles.z);
		}
		
		//Deciding if we should manipulate the camera
		//Saving calculation/render time if camera doesn't have to move
		Vector3 lookTemp = targetLookPos;

		Vector3 lookPosOffset = player.up * activeOffset.y + camRotationHelper.transform.right * activeOffset.x;
		targetLookPos = player.position +  lookPosOffset;
		//RaycastHit hit1;
        if (Physics.Raycast(player.position + player.up * activeOffset.y, camRotationHelper.transform.right * activeOffset.x, Mathf.Abs(activeOffset.x), raycastLayers))
			InvertOffset();
		
		if (lookTemp != targetLookPos)
			atTargetPos = false;

		if (!atTargetPos)
		{
			this.transform.rotation = camRotationHelper.transform.rotation;

			Vector3 offsetDirection = this.transform.forward * activeOffset.z;

			//Preventing clipping
			RaycastHit hit;

			Ray ray = new Ray(targetLookPos, offsetDirection);
			if(Physics.Raycast (ray, out hit, raycastDistance, raycastLayers) && hit.collider.tag != Tags.MAIN_CAMERA)
				camTargetPos = hit.point - ray.direction;
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
    }

    /// <summary>
    /// Changes current camera offset.
    /// </summary>
    /// <param name="newOffset"></param>
    public void SetOffset(CameraOffset newOffset)
    {
        switch (newOffset)
        {
            case CameraOffset.Default:  activeOffset = offsets.defaultOffset;   break;
            case CameraOffset.Aim: activeOffset = offsets.aim; break;
            case CameraOffset.Crouch: activeOffset = offsets.crouch; break;
            case CameraOffset.PDA: activeOffset = offsets.PDA; break;
            case CameraOffset.Fighting: activeOffset = offsets.fightingt; break;
            case CameraOffset.ClimbUp: activeOffset = offsets.climbUp; break;
            case CameraOffset.ClimbDown: activeOffset = offsets.climbDown; break;
            case CameraOffset.Hacking: activeOffset = offsets.Hacking; break;
            case CameraOffset.CrouchAim: activeOffset = offsets.crouchAim; break;
        }
        activeOffset.x *= invertOffset;
        raycastDistance = activeOffset.magnitude;
    }

    public void Shake(float intensity, float speed, float duration)
    {
		if(!shaking)
        	this.StartCoroutine(shake(intensity, speed, duration));
    }
    private IEnumerator shake(float intensity, float speed, float duration)
    {
        shaking = true;
        float endTime = Time.time + duration;
        float shakeOffsetX, shakeOffsetY;
        while (Time.time < endTime)
        {
            this.atTargetPos = false;

            float _intensity = intensity * (endTime - Time.time);
            //shakeOffsetX = Mathf.PerlinNoise(Time.time * speed, Time.time * speed) * _intensity - _intensity / 2f;
            shakeOffsetY = (Mathf.PerlinNoise((Time.time + 1) * speed / 4, (Time.time + 1) * speed / 4) * _intensity - _intensity / 2f) * 2;
            shakeOffsetX = Mathf.Cos(Time.time * speed) * _intensity;
            //shakeOffsetY = Mathf.Sin(Time.time * speed * 2) * _intensity / 2;

            this.transform.position = camTargetPos + transform.right * shakeOffsetX + transform.up * shakeOffsetY;
            yield return eof;
        }
        shaking = false;
    }
}