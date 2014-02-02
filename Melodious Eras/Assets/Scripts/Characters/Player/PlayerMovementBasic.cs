using UnityEngine;
using System.Collections;

[AddComponentMenu("Scripts/Characters/Player Movement Basic")]
public class PlayerMovementBasic : MonoBehaviour
{
    public float jumpForce = 20f;
    public float speed = 7f;
    public float snapDownThreshold = .25f;
    public float sound;

    public bool jumping = false;
    public bool isShooting = false;
    public bool useDefaultMovement = true;
    public bool isAiming = false;
	public bool isWalking = false;
	public bool isCrouching = false;
	public PhysicMaterial zeroFriction, 
						  fullFriction;
	public LayerMask ignorePlayer;
	public Vector3 targetPoint;

    private bool moving = false;

    private CameraMovement3D mainCam;
    private PlayerStats stats;
    private HUD_Stealth hud;
    private Animator anim;
	private CapsuleCollider capsule;

	private YieldInstruction diveTime, vaultTime, climbTime, slideTime;
	
	[SerializeField] AdvancedSettings advancedSettings;                 // Container for the advanced settings class , thiss allows the advanced settings to be in a foldout in the inspector
	
	
	[System.Serializable]
	public class AdvancedSettings
	{
		public float stationaryTurnSpeed = 180;				// additional turn speed added when the player is stationary (added to animation root rotation)
		public float movingTurnSpeed = 360;					// additional turn speed added when the player is moving (added to animation root rotation)
		public float headLookResponseSpeed = 2;				// speed at which head look follows its target
		public float crouchHeightFactor = 0.6f; 			// collider height is multiplied by this when crouching
		public float crouchChangeSpeed = 4;					// speed at which capsule changes height when crouching/standing
		public float autoTurnThresholdAngle = 100;			// character auto turns towards camera direction if facing away by more than this angle
		public float autoTurnSpeed = 2;						// speed at which character auto-turns towards cam direction
		public PhysicMaterial zeroFrictionMaterial;			// used when in motion to enable smooth movement
		public PhysicMaterial highFrictionMaterial;			// used when stationary to avoid sliding down slopes
		public float jumpRepeatDelayTime = 0.25f;			// amount of time that must elapse between landing and being able to jump again
		public float runCycleLegOffset = 0.2f;				// animation cycle offset (0-1) used for determining correct leg to jump off
		public float groundStickyEffect = 5f;				// power of 'stick to ground' effect - prevents bumping down slopes.
	}

    void Start()
    {
		diveTime = new WaitForSeconds(2.233f);
		vaultTime = new WaitForSeconds(1.4f);
		climbTime = new WaitForSeconds(3f);

        mainCam = Camera.main.GetComponent<CameraMovement3D>();
        stats = this.GetComponent<PlayerStats>();
        anim = this.GetComponent<Animator>();
        hud = this.GetComponent<HUD_Stealth>();
		capsule = this.GetComponent<CapsuleCollider>();

        anim.SetFloat(HashIDs.speed_float, 0f);
		anim.SetBool(HashIDs.aiming_bool, isShooting);
		anim.SetLayerWeight(1, 1f);
		//anim.SetLayerWeight(2, 1f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!jumping && useDefaultMovement)
        {
            MovementInputs();
			RaycastHit hit;
			if(Physics.Raycast (this.transform.position, Vector3.down, out hit, .25f))
			{
				//Debug.Log(hit.collider.name);
			}
        }
    }

	void Update()
	{
		if(!jumping && useDefaultMovement)
		{
			if (Input.GetButtonDown(InputType.CROUCH))
			{
				isCrouching = !isCrouching; //toggled instead of held
				mainCam.SetOffset(isCrouching ? CameraOffset.Crouch : CameraOffset.Default);
				this.anim.SetBool(HashIDs.sneaking_bool, isCrouching);
				//Perform crouch here
			}
			if (Input.GetButtonDown(InputType.JUMP))
			{
				this.StartCoroutine(Jump());
			}
			CombatInputs();
		}
	}
	#region Inputs
    void CombatInputs()
    {
        //----------------------------------------------
        //Determining camera offset
		//AIM (and WALK) offset
		//Walking is for PC only; speed is handled by analog sticks on consoles
		//Listening for all 3 to avoid camera shifting issues
		if (Input.GetButtonDown(InputType.WALK))
		{
			isWalking = true;
			mainCam.SetOffset(CameraOffset.Aim);
		}
		else if (Input.GetButtonUp(InputType.WALK))
		{
			isWalking = isAiming; //Will still be walking if the player is aiming
			mainCam.SetOffset(isCrouching ? CameraOffset.Crouch : CameraOffset.Default);
		}

        if (Input.GetButtonDown(InputType.RELOAD) && stats.equippedWeapon is Gun)
        {
            StartCoroutine(((Gun)stats.equippedWeapon).Reload());
        }
        if (Input.GetButton(InputType.AIM))
        {
            //Semi-auto
            if (Input.GetButtonDown(InputType.SHOOT) && stats.equippedWeapon != null /*&& (weapon is MeleeWeapon || weapon is SemiAuto)*/)
            {
                stats.equippedWeapon.Fire();
            }
        }
        else
        {
            if (Input.GetButtonDown(InputType.SHOOT))
                stats.PerformMelee();
        }
        if (Input.GetButtonDown(InputType.AIM))
        {
			if(!isCrouching)
            	mainCam.SetOffset(CameraOffset.Aim);
            mainCam.followSpeed = (float)CameraFollowSpeed.Aiming;
            isAiming = true;
			anim.SetBool(HashIDs.aiming_bool, isAiming);
        }
        else if (Input.GetButtonUp(InputType.AIM))
        {
            mainCam.followSpeed = (float)CameraFollowSpeed.Default;
            if (!isWalking) //Will only change offset if the player isn't holding down the Walk key
                mainCam.SetOffset(isCrouching ? CameraOffset.Crouch : CameraOffset.Default);
            isAiming = false;
			anim.SetBool(HashIDs.aiming_bool, isAiming);
        }
    }

	void OnAnimatorIK(int layerIndex)
	{
		if (!(mainCam.activeOffset.Equals(mainCam.climbUpOffset) || mainCam.activeOffset.Equals(mainCam.climbDownOffset)))
			TurnPlayerHead();

		if(this.isAiming)
		{

			anim.SetIKPosition(AvatarIKGoal.RightHand, targetPoint);
			anim.SetIKPositionWeight(AvatarIKGoal.RightHand, anim.GetFloat(HashIDs.aimWeight_float));
		}
	}

    void TurnPlayerHead()
    {
        Ray ray = mainCam.camera.ViewportPointToRay(new Vector3(.5f, .5f, 0));
        RaycastHit hit;


		anim.SetLookAtWeight(1, .25f, 2);
		if(stats.lookatTarget != null)
		{
			//playerAnim.SetLookAtWeight(1, .5f, 1, 1, 1);
			anim.SetLookAtPosition(stats.lookatTarget.position);
		}
        else 
		{			
			//playerAnim.SetLookAtWeight(1, 1, 1, 1, 1);
			if (Physics.Raycast(ray, out hit, 100, ignorePlayer) && hit.collider.tag != Tags.PLAYER && Vector3.Distance(hit.point, mainCam.transform.position) > 1)
			{
				targetPoint = hit.point;
				anim.SetLookAtPosition(targetPoint);
			}
			else
			{
				targetPoint = mainCam.transform.position + mainCam.transform.forward * 100;
				anim.SetLookAtPosition(targetPoint);
			}
        }
    }

    void MovementInputs()
    {

        float h = Input.GetAxis(InputType.HORIZONTAL);  //A(neg), D(pos), Left joystick left(neg)/right(pos)
        float v = Input.GetAxis(InputType.VERTICAL);    //S(neg), W(pos), Left joystick down(neg)/up(pos)
        Vector2 direction = new Vector2(h, v);

        MovementManager(direction);
    }
	#endregion

    void MovementManager(Vector2 direction)
    {
        moving = !direction.Equals(Vector2.zero);

        //Determining speed
        speed = (isWalking || isAiming) ? 2 : 5.657f;
        speed *= ((direction.magnitude < 1) ? direction.magnitude : 1);
        this.anim.SetFloat(HashIDs.speed_float, speed);

		float runCycle = Mathf.Repeat (anim.GetCurrentAnimatorStateInfo (0).normalizedTime + advancedSettings.runCycleLegOffset, 1);
		float jumpLeg = (runCycle < .5f ? 1 : -1) /** forwardAmount*/;
		if (!jumping) {
			anim.SetFloat ("JumpLeg", jumpLeg);
		}

		if(speed > 4 && !isCrouching)
		{
			if(!this.audio.isPlaying)
				this.audio.Play();
		}
		else if(this.audio.isPlaying)
			this.audio.Stop ();

        // Facing and running the desired direction
        float angle = Vector2.Angle(Vector2.up, direction);
        if (direction.x < 0)
            angle = -angle;

        if (!isAiming && moving && !jumping)
        {
            this.transform.eulerAngles = new Vector3(this.transform.eulerAngles.x, mainCam.transform.eulerAngles.y + angle, this.transform.eulerAngles.z);
			this.anim.applyRootMotion = true;

            //this.transform.eulerAngles = Vector3.Slerp(this.transform.eulerAngles, 
            //    new Vector3(this.transform.eulerAngles.x, mainCam.transform.eulerAngles.y + angle, this.transform.eulerAngles.z), 
            //    1 * Time.deltaTime);
        }
        else if (isAiming)
        {
            this.transform.eulerAngles = new Vector3(this.transform.eulerAngles.x, mainCam.transform.eulerAngles.y, this.transform.eulerAngles.z);

            this.anim.applyRootMotion = false;
            this.rigidbody.velocity = (this.transform.right * direction.x + this.transform.forward * direction.y) * speed + new Vector3(0, this.rigidbody.velocity.y, 0);

            /*if (direction.x < 0)
                mainCam.SetSideView(.6f);
            else if(direction.x > 0)
                mainCam.SetSideView(-.6f);*/
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.contacts[0].normal.y > .7f)
        {
			jumping = false;

            anim.applyRootMotion = true;
        }
        float impactVelocity = Vector3.Magnitude(collision.relativeVelocity);
        if (impactVelocity > 12.5f)
        {
            stats.health -= 5 * (int)(impactVelocity - 12.5f);
            anim.applyRootMotion = true;
            //emit noise
        }
        if (jumping && !(collision.rigidbody && collision.rigidbody.isKinematic) && (Mathf.Abs(collision.contacts[0].normal.x) > .5f || Mathf.Abs(collision.contacts[0].normal.z) > .5f))
        {
            Debug.Log("collision has no rigidbody: " + collision.transform.name);
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.collider.tag == Tags.SLIDE)
            anim.SetBool("IsGrinding", false);
    }

    public IEnumerator Jump()
    {
        if (!jumping || anim.GetBool(HashIDs.onGround_bool))
        {
			//if no obstacles in front of player
            //this.rigidbody.AddForce(Vector3.up * jumpForce);
            jumping = true;

			//anim.SetBool(HashIDs.onGround_bool, false);
            //anim.applyRootMotion = false;


			//Determining action
			Ray low = new Ray(this.transform.position + Vector3.up * .75f, this.transform.forward);
			Ray high = new Ray(this.transform.position + Vector3.up * 1.5f, this.transform.forward);
			RaycastHit hitLow, hitHigh;

			float raycastDistance = Mathf.Max(1f, anim.GetFloat(HashIDs.speed_float));
			Debug.DrawLine(low.origin, low.origin + low.direction * raycastDistance, Color.red);
			Debug.DrawLine(high.origin, high.origin + high.direction * raycastDistance, Color.red);

			//if low hit and high miss
			if(Physics.Raycast (high, out hitHigh, raycastDistance))
			{
				Ray climbHeight = new Ray(this.transform.position + Vector3.up * 2.5f, this.transform.forward);
				if(!Physics.Raycast(climbHeight, raycastDistance))
				{
					//climb wall
					//need to match hand placement with top of wall
					anim.SetBool(HashIDs.climbeLedge_bool, true);
					yield return climbTime;
					anim.SetBool(HashIDs.climbeLedge_bool, false);
					anim.SetBool(HashIDs.onGround_bool, true);
				}
			}
			else if(Physics.Raycast(low, out hitLow, raycastDistance))
			{
				//vault
				anim.SetBool(HashIDs.vault_bool, true);
				yield return vaultTime;
				anim.SetBool(HashIDs.vault_bool, false);
				anim.SetBool(HashIDs.onGround_bool, true);
			}
			else
			{
				//either jump or dive
				anim.SetBool(HashIDs.dive_bool, true);
				yield return diveTime;
				anim.SetBool(HashIDs.dive_bool, false);
				anim.SetBool(HashIDs.onGround_bool, true);
			}
        }
    }
    public void Jump(float force)
    {
        if (!jumping)
        {
            this.rigidbody.AddForce(Vector3.up * force);
            jumping = true;
            anim.SetBool("IsGrinding", false);
            anim.applyRootMotion = false;
        }
    }

	YieldInstruction waitp1 = new WaitForSeconds(.1f);
    public IEnumerator Launch(float force)
    {
        anim.SetBool("IsGrinding", false);
        this.rigidbody.AddForce(Vector3.up * force);

        yield return waitp1; //Giving the player time to add more force to the jump
        jumping = true;
    }


    /// <summary>
    /// Determines if the transform is grounded (less than 0.25f off the ground) for smooth descents.
    /// </summary>
    /// <returns></returns>
    public bool IsGrounded()
    {
        return Physics.Raycast(this.transform.position, Vector3.down, 0.25f);
    }

	void ProcessMatchTarget()
	{
		AnimatorStateInfo info = this.anim.GetCurrentAnimatorStateInfo(0);
//		if(info.IsName(HashIDs.vault_bool))
//		{			
//			if(MatchTarget) 
//			{
//				m_Animator.MatchTarget(m_Target,new Quaternion(),AvatarTarget.LeftHand,new MatchTargetWeightMask(Vector3.one,0),m_VaultMatchTargetStart,m_VaultMatchTargetStop); // start and stop time 
//			}
//		}
//		else if(info.IsName(HashIDs)) // always do match targeting.
//		{
//			m_Animator.MatchTarget(m_Target,new Quaternion(),AvatarTarget.Root,new MatchTargetWeightMask(new Vector3(1,0,1),0),m_SlideMatchTargetStart,m_SlideMatchTargetStop);				
//		}
	}
}
