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
	private Vector3 m_target; //MatchTarget coords

    private bool moving = false;

    private CameraMovement3D mainCam;
    private PlayerStats stats;
    private HUD_Stealth hud;
    private Animator anim;
	private CapsuleCollider capsule;

	private YieldInstruction diveTime, vaultTime, climbTime, slideTime, endOfFrame;
	private float originalHeight;
	
	[SerializeField] AdvancedSettings advancedSettings;                 // Container for the advanced settings class , thiss allows the advanced settings to be in a foldout in the inspector

	
	private const float m_VaultMatchTargetStart 	= 0.40f;
	private const float m_VaultMatchTargetStop 		= 0.51f;
	
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
		diveTime = new WaitForSeconds(1.367f);
		vaultTime = new WaitForSeconds(1.4f);
		climbTime = new WaitForSeconds(3f);
		endOfFrame = new WaitForEndOfFrame();

        mainCam = Camera.main.GetComponent<CameraMovement3D>();
        stats = this.GetComponent<PlayerStats>();
        anim = this.GetComponent<Animator>();
        hud = this.GetComponent<HUD_Stealth>();
		capsule = this.GetComponent<CapsuleCollider>();
		originalHeight = capsule.height;

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
				if(!isCrouching)
				{
					isCrouching = true;
				}
				else
				{
					// prevent standing up in crouch-only zones
					Ray crouchRay = new Ray (rigidbody.position + Vector3.up * capsule.radius * .5f, Vector3.up);
					float crouchRayLength = originalHeight - capsule.radius * .5f;
					if (!Physics.SphereCast (crouchRay, capsule.radius * .5f, crouchRayLength)) 
						isCrouching = false;
				}
				//isCrouching = !isCrouching; //toggled instead of held
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
		//ProcessMatchTarget ();
		ScaleCapsuleForCrouching();
		//ScaleForVaulting();
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


    void MovementInputs()
    {

        float h = Input.GetAxis(InputType.HORIZONTAL);  //A(neg), D(pos), Left joystick left(neg)/right(pos)
        float v = Input.GetAxis(InputType.VERTICAL);    //S(neg), W(pos), Left joystick down(neg)/up(pos)
        Vector2 direction = new Vector2(h, v);

        MovementManager(direction);
	}
	
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
	#endregion

	#region animator IK
	void OnAnimatorIK(int layerIndex)
	{
		if (!(mainCam.activeOffset.Equals(mainCam.climbUpOffset) || mainCam.activeOffset.Equals(mainCam.climbDownOffset)))
			TurnPlayerHead();
		
		if(layerIndex == 1 && this.isAiming)
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
	#endregion

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
				else
					jumping = false;
			}
			else if(Physics.Raycast(low, out hitLow, raycastDistance))
			{
				//vault
				anim.SetBool(HashIDs.vault_bool, true);
				//StartCoroutine(ProcessMatchTarget(hitLow.collider, hitLow.point));
				m_target = hitLow.point;
				m_target.y = hitLow.collider.bounds.max.y;
				Debug.Log(m_target);
				StartCoroutine(ProcessMatchTarget(hitLow.collider, hitLow.point, this.transform.position));
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
		if(this.anim.GetCurrentAnimatorStateInfo(0).IsName("Player Animator.Vault"))
		{
			//this.collider.enabled = false;
			//this.rigidbody.useGravity = false;
			this.anim.MatchTarget(m_target, new Quaternion(), AvatarTarget.LeftHand, new MatchTargetWeightMask(Vector3.one, 0), m_VaultMatchTargetStart, m_VaultMatchTargetStop);
			//this.anim.MatchTarget(m_target, new Quaternion(), AvatarTarget.LeftHand, new MatchTargetWeightMask(Vector3.one, 0), m_VaultMatchTargetStart, m_VaultMatchTargetStop);
		}
		else
		{
			this.collider.enabled = true;
			this.rigidbody.useGravity = true;
		}
	}
	/// <summary>
	/// Matching target for vaulting
	/// </summary>
	/// <returns>The match target.</returns>
	/// <param name="hitCol">Hit col.</param>
	/// <param name="hitPoint">Hit point.</param>
	/// <param name="startPos">Start position.</param>
	IEnumerator ProcessMatchTarget(Collider hitCol, Vector3 hitPoint, Vector3 startPos)
	{
		Vector3 matchTarget = hitPoint;
		matchTarget.y = hitCol.bounds.max.y;


		float distance = Vector3.Distance(hitPoint, startPos);
		float startTime = m_VaultMatchTargetStart * (distance / 4); //trying to avoid clipping animation
		float endTime = m_VaultMatchTargetStop * (distance / 4);

		while(this.anim.GetBool(HashIDs.vault_bool))
		{
			AnimatorStateInfo state = this.anim.GetCurrentAnimatorStateInfo(0);
			if(state.IsName("Player Animator.Vault")&& state.normalizedTime > startTime)
			{
				this.rigidbody.isKinematic = true;
				this.anim.MatchTarget(matchTarget, new Quaternion(), AvatarTarget.LeftHand, new MatchTargetWeightMask(Vector3.one, 0), startTime, endTime);
				
			}yield return endOfFrame;
		}
		this.rigidbody.isKinematic = false;
	}
	
	void ScaleCapsuleForCrouching ()
	{
		// scale the capsule collider according to
		// if crouching ...
		if ( isCrouching && (capsule.height != originalHeight * advancedSettings.crouchHeightFactor)) {
			capsule.height = Mathf.MoveTowards (capsule.height, originalHeight * advancedSettings.crouchHeightFactor, Time.deltaTime * 4);
			capsule.center = Vector3.MoveTowards (capsule.center, Vector3.up * originalHeight * advancedSettings.crouchHeightFactor * .5f, Time.deltaTime * 2);
		}
		// ... everything else 
		else
		if (capsule.height != originalHeight && capsule.center != Vector3.up * originalHeight * .5f) {
			capsule.height = Mathf.MoveTowards (capsule.height, originalHeight, Time.deltaTime * 4);
			capsule.center = Vector3.MoveTowards (capsule.center, Vector3.up * originalHeight * .5f, Time.deltaTime * 2);
		}
	}

	void ScaleForVaulting()
	{
		if ( anim.GetBool(HashIDs.vault_bool) && (capsule.height != originalHeight * advancedSettings.crouchHeightFactor)) {
			capsule.height = Mathf.MoveTowards (capsule.height, originalHeight * advancedSettings.crouchHeightFactor, Time.deltaTime * 4);
			capsule.center = Vector3.MoveTowards (capsule.center, Vector3.up * (1 - originalHeight * advancedSettings.crouchHeightFactor * .5f), Time.deltaTime * 2);
		}
		// ... everything else 
		else
		if (capsule.height != originalHeight && capsule.center != Vector3.up * originalHeight * .5f) {
			capsule.height = Mathf.MoveTowards (capsule.height, originalHeight, Time.deltaTime * 4);
			capsule.center = Vector3.MoveTowards (capsule.center, Vector3.up * originalHeight * .5f, Time.deltaTime * 2);
		}
	}
}
