using UnityEngine;
using System.Collections;

enum Acrobatics { Vault, Dive, ClimbLedge };
/// <summary>
/// Monolothic character control class. I've had as much fun writing it as you'll have reading it.
/// </summary>
[AddComponentMenu("Scripts/Characters/Player Movement Basic")]
public class PlayerMovementBasic : MonoBehaviour
{
    #region Public variables
	private const float RAY_LOW = 0.75f;
	private const float RAY_HIGH = 1.5f;
	private const float MAX_CLIMB_HEIGHT = 3.5f;
    private const float CROUCH_HEIGHT_THRESHOLD = 1.225f;

    private const float m_VaultMatchTargetStart = 0.40f;
    private const float m_VaultMatchTargetStop = 0.51f;
    private const float m_ClimbMatchTargetStart = 0.1f;  //.19f;
    private const float m_ClimbMatchTargetStop = .3f;

    private const int AIM_LAYER = 2;		//animator aiming layer
    

    public float speed = 0f;                //current speed of character
    public float acceleration = 3f;         //rate that player speeds up, multiplied by adrenaline
    public float deceleration = 5f;         //rate that player slows down, divided by adrenaline

    public float snapDownThreshold = .25f;  //Length of downward raycasts for smooth descent of ramps
    public float soundwaveDistance;
    public bool useDefaultMovement = true;
    public PhysicMaterial zeroFriction,
                          fullFriction;
    public LayerMask ignorePlayer;          //used to prevent LookAt IK function from bugging out when camera's pointed at player
    public Vector3 targetPoint;             //center of camera. When aiming, this points the arm at the target
    public LayerMask obstacleLayers;
    #endregion

    #region Private variables
    private bool jumping = false;
    private bool isShooting = false;
    private bool isAiming = false;
	private bool isWalking = true;
	private bool isCrouching = false;
    private bool onGround = true;
    private bool falling = false;


    private Vector3 m_target;               //MatchTarget coords
    private Vector2 moveDirection;          //movement direction of the player. Cached here to minimize garbage collection.
    private bool moving = false;

    private CameraMovement3D mainCam;
    private PlayerStats stats;
    private HUD_Stealth hud;
    private Animator anim;
	private CapsuleCollider capsule;

	private YieldInstruction diveTime, vaultTime, climbTime, slideTime, endOfFrame;
    private float originalHeight;
    private float fallStartHeight, fallEndHeight;
    private float fallSpeed = 0f;
    private Ray groundCheckRay = new Ray(Vector3.zero, Vector3.down); //cached to avoid garbage collection
	
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
    #endregion

    #region Accessors & Mutators
    public bool IsAiming    { get { return isAiming; } }
    public bool IsWalking   { get { return isWalking; } }
    public bool IsCrouching { get { return isCrouching; } }
    #endregion

    void Awake()
    {
        HashIDs.Initialize();
        moveDirection = new Vector2();
        stats = this.GetComponent<PlayerStats>();
        anim = this.GetComponent<Animator>();
        hud = this.GetComponent<HUD_Stealth>();
        capsule = this.GetComponent<CapsuleCollider>();
    }

    void Start()
	{
		mainCam = Camera.main.GetComponent<CameraMovement3D>();

		diveTime = new WaitForSeconds(1.367f);
		vaultTime = new WaitForSeconds(1.220f);
		climbTime = new WaitForSeconds(2.367f);
		endOfFrame = new WaitForEndOfFrame();

		originalHeight = capsule.height;

        anim.SetFloat(HashIDs.speed_float, 0f); //initializes animator to idle stance
		anim.SetBool(HashIDs.aiming_bool, isShooting);
        anim.SetLayerWeight(AIM_LAYER, 1f);             //This makes sure aiming mask overrides default arm animations
        this.anim.SetFloat(HashIDs.jumpLeg_float, -1f); //leaving at -1 for now. Leads to weird falling when set to 1
		//anim.SetLayerWeight(2, 1f);           //optional gun grip, but too small to notice for now
    }

    void OnDisable()
    {
        this.GetComponent<Rigidbody>().velocity = Vector3.zero;
        anim.SetFloat(HashIDs.speed_float, 0f);
        anim.SetBool(HashIDs.sneaking_bool, false);
    }

    /// <summary>
    /// Movement is handled under FixedUpdate() to avoid jittering. 
    /// Everything else is handled under Update() for correct input detection
    /// </summary>
    void FixedUpdate()
    {
        if (!jumping && useDefaultMovement)
        {
            ProcessFalling();
            MovementInputs();

			RaycastHit hit; //snapping character to ground for smooth slope descent 
			if(Physics.Raycast (this.transform.position, Vector3.down, out hit, .125f))
                this.transform.position = hit.point;
        }
    }

	void Update()
	{
		if(useDefaultMovement) //if default movement controls aren't being overridden
		{
			if (Input.GetButtonDown(InputType.CROUCH))
			{
                if (!isCrouching)
                {
                    isCrouching = true;
                    mainCam.SetOffset(CameraOffset.Crouch);
                }
                else
                {
                    // prevent standing up in crouch-only areas (under desks, in vents, etc)
                    Ray crouchRay = new Ray(GetComponent<Rigidbody>().position + Vector3.up * capsule.height * .5f, Vector3.up);
                    float crouchRayLength = originalHeight - capsule.radius * .5f;
                    if (!Physics.SphereCast(crouchRay, capsule.radius * .5f, crouchRayLength))
                    {
                        isCrouching = false;
                        mainCam.SetOffset(CameraOffset.Default);
                    }
                }
				mainCam.SetOffset(isCrouching ? CameraOffset.Crouch : CameraOffset.Default);
				this.anim.SetBool(HashIDs.sneaking_bool, isCrouching);
			}

			if (Input.GetButtonDown(InputType.JUMP))
				this.StartCoroutine(Jump());

			CombatInputs();
		}
		//ProcessMatchTarget ();
		ScaleCapsuleForCrouching();
		//ScaleForVaulting();
	}
	#region Inputs
    /// <summary>
    /// Covers: Aiming, Walking, Reloading, Shooting
    /// </summary>
    void CombatInputs()
    {
        //Determining camera offset
		//AIM (and WALK) offset
		//Walking is for PC only; speed is handled by analog sticks on consoles
		//Listening for all 3 to avoid camera shifting issues
		if (Input.GetButton(InputType.RUN) && isAiming == false && this.stats.stamina > 0 && !this.stats.exhausted)
		{
			isWalking = false;
			mainCam.SetOffset(CameraOffset.Default);
		}
		else
		{
			//isWalking = isAiming; //Will still be walking if the player is aiming
            isWalking = true;
			mainCam.SetOffset(isCrouching ? CameraOffset.Crouch : CameraOffset.Default);
		}

        //AIM input
        if(Input.GetButton(InputType.AIM))
        {
            if (!isCrouching)
                mainCam.SetOffset(CameraOffset.Aim);
            else
                mainCam.SetOffset(CameraOffset.CrouchAim);

            //May need to go outside of parent if-statement, depending on how Unity inputs work per-frame
            if (Input.GetButtonDown(InputType.AIM)) //When AIM is pressed
            {
                mainCam.followSpeed = (float)CameraFollowSpeed.Aiming;
                isAiming = true;
                anim.SetBool(HashIDs.aiming_bool, isAiming);
            }
        }
        else if (Input.GetButtonUp(InputType.AIM))//When AIM is released
        {
            mainCam.followSpeed = (float)CameraFollowSpeed.Default;
            if (!isWalking) //Will only change offset if the player isn't holding down the Walk key
                mainCam.SetOffset(isCrouching ? CameraOffset.Crouch : CameraOffset.Default);
            isAiming = false;
			anim.SetBool(HashIDs.aiming_bool, isAiming);
        }

        //SHOOT input
        if (Input.GetButtonDown(InputType.SHOOT))
        {
            if (isAiming && stats.equippedWeapon != null)
                stats.equippedWeapon.Fire();
            else
                stats.PerformMelee();
        }

        //RELOAD input
        if (Input.GetButtonDown(InputType.RELOAD))
        {
            StartCoroutine(stats.equippedWeapon.Reload());
        }
    }


    void MovementInputs()
    {
        moveDirection.x = Input.GetAxis(InputType.HORIZONTAL);  //A(neg), D(pos), Left joystick left(neg)/right(pos)
        moveDirection.y = Input.GetAxis(InputType.VERTICAL);    //S(neg), W(pos), Left joystick down(neg)/up(pos)
        moving = !moveDirection.Equals(Vector2.zero);

        //Determining speed
        if (moving)
        {
            float targetSpeed = (isWalking || isAiming) ? 2 : 5.657f;
            speed = Mathf.Lerp(speed, targetSpeed, acceleration * stats.adrenalineMultiplier * Time.deltaTime);
        }
        else
        {
            speed = Mathf.Lerp(speed, 0          , deceleration / stats.adrenalineMultiplier * Time.deltaTime);
        }
        //speed *= ((moveDirection.magnitude < 1) ? moveDirection.magnitude : 1); //used for analog input. Disabled for now, since stamina is being added
        this.anim.SetFloat(HashIDs.speed_float, speed);

        //if running
        if (speed > 4f && !isCrouching)
        {
            this.stats.ReduceStamina(15 * Time.deltaTime); 
            if (!this.GetComponent<AudioSource>().isPlaying) //play footsteps
                this.GetComponent<AudioSource>().Play();
            foreach(EnemyStats enemy in stats._nearbyEnemies.charactersInRange)
            {
                enemy.AI.Listen(this.transform.position, 10);
            }
        }
        else if (this.GetComponent<AudioSource>().isPlaying)
            this.GetComponent<AudioSource>().Stop();

        // Facing and running the desired direction. If-condition is there because Vector2.Angle only returns positive 0-180
        float angle = Vector2.Angle(Vector2.up, moveDirection);
        mainCam.followSpeed = (angle <= 90 || speed < 3) ? (float)CameraFollowSpeed.Default : (float)CameraFollowSpeed.Aiming * 2;


        if (moveDirection.x < 0)
        {
            angle = -angle;
        }


        //Rotates character to face desired direction, if not aiming
        if (!isAiming && moving && !jumping)
        {
            this.transform.eulerAngles = new Vector3(this.transform.eulerAngles.x, mainCam.transform.eulerAngles.y + angle, this.transform.eulerAngles.z);
            this.anim.applyRootMotion = true;
        }
        else if (isAiming) //overrides root motion for more accurate movement when aiming (player can't see legs when aiming anyways)
        {
            this.transform.eulerAngles = new Vector3(this.transform.eulerAngles.x, mainCam.transform.eulerAngles.y, this.transform.eulerAngles.z);

            this.anim.applyRootMotion = false;
            this.GetComponent<Rigidbody>().velocity = (this.transform.right * moveDirection.x + this.transform.forward * moveDirection.y) * speed + new Vector3(0, this.GetComponent<Rigidbody>().velocity.y, 0);
        }
	}
	#endregion

	#region animator IK
	void OnAnimatorIK(int layerIndex)
	{
        if (!(mainCam.activeOffset == mainCam.offsets.climbUp || mainCam.activeOffset == mainCam.offsets.climbDown))
			TurnPlayerHead();
		
        //Points arm toward target when aiming
		if(layerIndex == 1 && this.isAiming)
		{
			anim.SetIKPosition(AvatarIKGoal.RightHand, targetPoint);
			anim.SetIKPositionWeight(AvatarIKGoal.RightHand, anim.GetFloat(HashIDs.aimWeight_float));
		}
	}
	
	void TurnPlayerHead()
	{
		Ray rayFromCam = mainCam.GetComponent<Camera>().ViewportPointToRay(new Vector3(.5f, .5f, 0));
		RaycastHit hit;
		
		
		anim.SetLookAtWeight(1f, .125f, .75f);
		if(stats.lookatTarget != null)
		{
			//playerAnim.SetLookAtWeight(1, .5f, 1, 1, 1);
			anim.SetLookAtPosition(stats.lookatTarget.position);
		}
		else 
		{			
			//playerAnim.SetLookAtWeight(1, 1, 1, 1, 1);
			if (Physics.Raycast(rayFromCam, out hit, 100, ignorePlayer) && hit.collider.tag != Tags.PLAYER && Vector3.Distance(hit.point, mainCam.transform.position) > 1)
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

    /// <summary>
    /// Matching target for vaulting/climbing ledge. Currently only vaulting has been tested
    /// </summary>
    /// <returns>The match target.</returns>
    /// <param name="hitCol">Hit col.</param>
    /// <param name="hitPoint">Hit point.</param>
    /// <param name="startPos">Start position.</param>
    IEnumerator ProcessMatchTarget(Collider hitCol, Vector3 hitPoint, Vector3 startPos, Acrobatics stunt)
    {
        Vector3 matchTarget = hitPoint;
        matchTarget.y = hitCol.bounds.max.y;
        float distance = Vector3.Distance(hitPoint, startPos);
        float startTime = 0, endTime = 0;
        AnimatorStateInfo state;
        

        switch (stunt)
        {
            case Acrobatics.Vault:  //IK for vaulting
                startTime = m_VaultMatchTargetStart /** (distance / 4)*/; //trying to avoid clipping animation
                endTime = m_VaultMatchTargetStop /** (distance / 4)*/;

                while (this.anim.GetBool(HashIDs.vault_bool))
                {
                    state = this.anim.GetCurrentAnimatorStateInfo(0);
                    
                    if (state.IsName("Vault") && state.normalizedTime > startTime)
                    {
                        this.GetComponent<Rigidbody>().isKinematic = true;
                        this.anim.MatchTarget(matchTarget, new Quaternion(), AvatarTarget.LeftHand, new MatchTargetWeightMask(Vector3.one, 0), startTime, endTime);
                    }
                    yield return endOfFrame;
                }
                break;

            case Acrobatics.ClimbLedge: //IK for climbing ledge. Not tested yet
                startTime = m_ClimbMatchTargetStart;
                endTime = m_ClimbMatchTargetStop;
                this.speed = 0f;
                while (this.anim.GetBool(HashIDs.climbLedge_bool))
                {
                    this.GetComponent<Rigidbody>().isKinematic = true;
                    state = this.anim.GetCurrentAnimatorStateInfo(0);
                    if (state.IsName("Jump to Ledge") && state.normalizedTime > startTime)
                    {
                        this.anim.MatchTarget(matchTarget, new Quaternion(), AvatarTarget.RightHand, new MatchTargetWeightMask(Vector3.one, 0), startTime, endTime);
                        this.anim.SetFloat(HashIDs.speed_float, 0f);
                    }
                    yield return endOfFrame;
                }
                break;
        }
        this.GetComponent<Rigidbody>().isKinematic = false;
    }
	#endregion

    void OnCollisionEnter(Collision collision)
    {
        //Determining if the player's landed on ground
        if (collision.contacts[0].normal.y > .7f)
        {
			jumping = false;
            anim.applyRootMotion = true;
        }

        //Calculating fall damage
        float impactVelocity = Vector3.Magnitude(collision.relativeVelocity);
        if (impactVelocity > 12.5f)
        {
            stats.TakeDamage(5 * (int)(impactVelocity - 12.5f), true);
            anim.applyRootMotion = true;
            //emit noise
        }
        if (jumping && !(collision.rigidbody && collision.rigidbody.isKinematic) && (Mathf.Abs(collision.contacts[0].normal.x) > .5f || Mathf.Abs(collision.contacts[0].normal.z) > .5f))
        {
            Debug.Log("collision has no rigidbody: " + collision.transform.name);
        }
    }

    /// <summary>
    /// Determines whether to dive, vault, or climb a wall based on raycasts.
    /// </summary>
    /// <returns></returns>
    public IEnumerator Jump()
    {
        if (!jumping && anim.GetBool(HashIDs.onGround_bool))
        {
            jumping = true;

			//Determining action
			Ray low = new Ray(this.transform.position + Vector3.up * RAY_LOW, /*mainCam.XZdirection*/this.transform.forward);
            Ray high = new Ray(this.transform.position + Vector3.up * RAY_HIGH, /*mainCam.XZdirection*/this.transform.forward);
            
			RaycastHit hitLowInfo, hitHighInfo;

			float raycastDistance = Mathf.Max(2f, anim.GetFloat(HashIDs.speed_float));


			//if high hit
			if(Physics.Raycast (high, out hitHighInfo, raycastDistance, obstacleLayers) && 
			   	(hitHighInfo.collider.GetComponent<MaterialPhysics>() == null || 
				 hitHighInfo.collider.GetComponent<MaterialPhysics>().canClimb))
			{

				Ray climbHeight = new Ray(this.transform.position + Vector3.up * MAX_CLIMB_HEIGHT, mainCam.XZdirection/*this.transform.forward*/);
				if(!Physics.Raycast(climbHeight, raycastDistance, obstacleLayers))
				{
					//climb wall
					//need to match hand placement with top of wall
					anim.SetBool(HashIDs.climbLedge_bool, true);
                    StartCoroutine(ProcessMatchTarget(hitHighInfo.collider, hitHighInfo.point, this.transform.position, Acrobatics.ClimbLedge));
					yield return climbTime;
					anim.SetBool(HashIDs.climbLedge_bool, false);
					//anim.SetBool(HashIDs.onGround_bool, true);
				}
				else
					jumping = false;
			}
			else if(Physics.Raycast(low, out hitLowInfo, raycastDistance, obstacleLayers) && 
			        (hitLowInfo.collider.GetComponent<MaterialPhysics>() == null || 
			 		 hitLowInfo.collider.GetComponent<MaterialPhysics>().canClimb))
			{
				//vault
				anim.SetBool(HashIDs.vault_bool, true);
				m_target = hitLowInfo.point;
				m_target.y = hitLowInfo.collider.bounds.max.y;
				StartCoroutine(ProcessMatchTarget(hitLowInfo.collider, hitLowInfo.point, this.transform.position, Acrobatics.Vault));
				yield return vaultTime;
				anim.SetBool(HashIDs.vault_bool, false);
				//anim.SetBool(HashIDs.onGround_bool, true);
			}
			else
			{
				//either jump or dive
				anim.SetBool(HashIDs.dive_bool, true);
				yield return diveTime;
				anim.SetBool(HashIDs.dive_bool, false);
				//anim.SetBool(HashIDs.onGround_bool, true);
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
    public void ProcessFalling()
    {
        //this.onGround = Physics.Raycast(this.transform.position + Vector3.up, Vector3.down, 1.5f, obstacleLayers);
        groundCheckRay.origin = this.transform.position + Vector3.up;
        this.onGround = Physics.SphereCast(groundCheckRay, 0.3f, 1.25f, obstacleLayers); 
        this.anim.SetBool(HashIDs.onGround_bool, this.onGround);

        if (!onGround)
        {
            this.anim.SetFloat(HashIDs.jump_float, this.GetComponent<Rigidbody>().velocity.y);
        }

        /*if (!onGround && !falling) //if fall just started
        {
            falling = true;
            this.fallStartHeight = this.transform.position.y;
        }
        else if (onGround && falling) //if player just landed
        {
            falling = false;
            this.fallEndHeight = this.transform.position.y;
            if (fallStartHeight - fallEndHeight > 5)
            {
                this.stats.TakeDamageThroughArmor(
            }
        }*/
    }

	
	void ScaleCapsuleForCrouching ()
	{
		// scale the capsule collider according to
		// if crouching ...
		if ( isCrouching && (capsule.height != originalHeight * advancedSettings.crouchHeightFactor)) {

            capsule.height = Mathf.Lerp(capsule.height, originalHeight * advancedSettings.crouchHeightFactor, Time.deltaTime * 4); 
            capsule.center = Vector3.Lerp(capsule.center, Vector3.up * originalHeight * advancedSettings.crouchHeightFactor * .5f, Time.deltaTime * 4);
		}
		// ... everything else 
        else if (capsule.height != originalHeight && capsule.center != Vector3.up * originalHeight * .5f)
        {
            capsule.height = Mathf.Lerp(capsule.height, originalHeight, Time.deltaTime * 4);
            capsule.center = Vector3.Lerp(capsule.center, Vector3.up * originalHeight * .5f, Time.deltaTime * 4);
		}
	}
}
