﻿using UnityEngine;
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
	public float crouchHeightFactor = 0.6f;
	public float crouchChangeSpeed = 4;
	public PhysicMaterial zeroFriction, 
						  fullFriction;

    private bool moving = false;

    private CameraMovement3D mainCam;
    private PlayerStats stats;
    private HUD_Stealth hud;
    private Animator anim;


    void Start()
    {
        mainCam = Camera.main.GetComponent<CameraMovement3D>();
        stats = this.GetComponent<PlayerStats>();
        anim = this.GetComponent<Animator>();
        hud = this.GetComponent<HUD_Stealth>();

        anim.SetFloat(HashIDs.speed_float, 0f);
		anim.SetBool(HashIDs.shooting_float, isShooting);
		anim.SetLayerWeight(1, 1f);
		anim.SetLayerWeight(2, 1f);
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
				this.Jump();
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

        if (Input.GetButtonDown(InputType.RELOAD) && stats.equippedWeapon is Gun)
        {
            StartCoroutine(((Gun)stats.equippedWeapon).Reload());
        }
        if (Input.GetButton(InputType.AIM))
        {
            //Semi-auto
            if (Input.GetButtonDown(InputType.SHOOT) && stats.equippedWeapon is Gun /*&& (weapon is MeleeWeapon || weapon is SemiAuto)*/)
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
            mainCam.SetOffset(CameraOffset.Aim);
            mainCam.followSpeed = (float)CameraFollowSpeed.Aiming;
            isAiming = true;
			anim.SetBool(HashIDs.shooting_float, isAiming);
        }
        else if (Input.GetButtonUp(InputType.AIM))
        {
            mainCam.followSpeed = (float)CameraFollowSpeed.Default;
            if (!isWalking) //Will only change offset if the player isn't holding down the Walk key
                mainCam.SetOffset(isCrouching ? CameraOffset.Crouch : CameraOffset.Default);
            isAiming = false;
			anim.SetBool(HashIDs.shooting_float, isAiming);
        }
    }

    void MovementInputs()
    {
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
        if (impactVelocity > 20)
        {
            stats.health -= 5 * (int)(impactVelocity - 20);
            anim.applyRootMotion = true;
            //emit noise
        }
        if (jumping && !(collision.rigidbody && collision.rigidbody.isKinematic) && (Mathf.Abs(collision.contacts[0].normal.x) > .5f || Mathf.Abs(collision.contacts[0].normal.z) > .5f))
        {
            Debug.Log("collision has no rigidbody: " + collision.transform.name);
        }

        anim.SetBool("IsGrinding", collision.collider.tag == Tags.SLIDE);
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.collider.tag == Tags.SLIDE)
            anim.SetBool("IsGrinding", false);
    }

    public void Jump()
    {
        if (!jumping)
        {
			//if no obstacles in front of player
            this.rigidbody.AddForce(Vector3.up * jumpForce);
            jumping = true;
            anim.SetBool("IsGrinding", false);
			//anim.SetBool("Jumping", true);
            anim.applyRootMotion = false;
			//if low obstacle (vault)
			//...
			//if high obstacle (climb wall)
			//...
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
}
