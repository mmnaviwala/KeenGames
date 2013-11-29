﻿using UnityEngine;
using System.Collections;
using System.Runtime.CompilerServices;

public class PlayerMovementBasic : MonoBehaviour 
{
    public float jumpForce = 20f;
    public float speed = 7f;
    public float snapDownThreshold = .25f;
    public float sound;
    private float gravity;

    public bool jumping = false;
    public bool isShooting = false;
    public bool useDefaultMovement = true;
    public bool isAiming = false;
    public bool isCrouching = false;
    public bool isWalking = false;
    private bool moving = false;


    private CameraMovement3D mainCam;
    private CharacterStats stats;
    private HUD_Stealth hud;
    private Animator anim;
	
	void Start ()
    {
        mainCam = Camera.main.GetComponent<CameraMovement3D>();
        stats = this.GetComponent<CharacterStats>();
        anim = this.GetComponent<Animator>();
        hud = this.GetComponent<HUD_Stealth>();

        gravity = Mathf.Abs(Physics.gravity.y);
        anim.SetFloat("Speed", 7.0f);
        anim.SetBool("IsShooting", isShooting);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!jumping && useDefaultMovement)
        {
            ListenForInputs();

            float h = Input.GetAxis(InputType.HORIZONTAL);  //A(neg), D(pos), Left joystick left(neg)/right(pos)
            float v = Input.GetAxis(InputType.VERTICAL);    //S(neg), W(pos), Left joystick down(neg)/up(pos)
            Vector2 direction = new Vector2(h, v);

            MovementManager(direction);
        }
	}

    /// <summary>
    /// <para>Listens for all the necessary inputs associated with character movement. 
    /// Putting input detection in here since the Update function was getting pretty monolithic.</para>
    /// <para>Inputs: Aim, Shoot, Walk, Crouch, Jump</para>
    /// </summary>
    void ListenForInputs()
    {
        //----------------------------------------------
        //Determining camera offset
        //AIM (and WALK) offset
        if (Input.GetButton(InputType.AIM))
        {
            //Semi-auto
            if (Input.GetButtonDown(InputType.SHOOT) /*&& (weapon is MeleeWeapon || weapon is SemiAuto)*/)
            {
                stats.equippedWeapon.PullTrigger();
            }
            /*if (Input.GetButton(InputType.SHOOT) && weapon is Automatic)
            {
 
            }*/
        }
        if (Input.GetButtonDown(InputType.AIM))
        {
            mainCam.SetOffset(CameraOffset.Walk);
            isAiming = true;
            anim.SetBool("IsShooting", isAiming);
        }
        else if (Input.GetButtonUp(InputType.AIM))
        {
            if(!isWalking) //Will only change offset if the player isn't holding down the Walk key
                mainCam.SetOffset(isCrouching ? CameraOffset.Crouch : CameraOffset.Default);
            isAiming = false;
            anim.SetBool("IsShooting", isAiming);
        }

        //Walking is for PC only; speed is handled by analog sticks on consoles
        //Listening for all 3 to avoid camera shifting issues
        if (Input.GetButtonDown(InputType.WALK))
        {
            isWalking = true;
            mainCam.SetOffset(CameraOffset.Walk);
        }
        else if (Input.GetButtonUp(InputType.WALK))
        {
            isWalking = isAiming; //Will still be walking if the player is aiming
            mainCam.SetOffset(isCrouching ? CameraOffset.Crouch : CameraOffset.Default);
        }
        else if (Input.GetButtonDown(InputType.CROUCH))
        {
            isCrouching = !isCrouching; //toggled instead of held
            mainCam.SetOffset(isCrouching ? CameraOffset.Crouch : CameraOffset.Default);
            //Perform crouch here
        }
        if (Input.GetButtonDown(InputType.JUMP))
        {
            this.Jump();
        }
 
    }

    void MovementManager(Vector2 direction)
    {
        moving = !direction.Equals(Vector2.zero);

        //Determining speed
        speed = (isWalking || isAiming) ? 2 : 5.657f;
        speed *= ((direction.magnitude < 1) ? direction.magnitude : 1);
        this.anim.SetFloat("Speed", speed);

        // Facing and running the desired direction
        float angle = Vector2.Angle(Vector2.up, direction);
        if (direction.x < 0)
            angle = -angle;

        if (!isAiming && moving)
        {
            //this.transform.eulerAngles = new Vector3(this.transform.eulerAngles.x, mainCam.transform.eulerAngles.y + angle, this.transform.eulerAngles.z);
            this.transform.eulerAngles = Vector3.Lerp(this.transform.eulerAngles, 
                new Vector3(this.transform.eulerAngles.x, mainCam.transform.eulerAngles.y + angle, this.transform.eulerAngles.z), 
                10 * Time.deltaTime);
            this.rigidbody.velocity = this.transform.forward * speed + new Vector3(0, this.rigidbody.velocity.y, 0);
        }
        else if (isAiming)
        {
            this.transform.eulerAngles = new Vector3(this.transform.eulerAngles.x, mainCam.transform.eulerAngles.y, this.transform.eulerAngles.z);
            
            this.rigidbody.velocity = (this.transform.right * direction.x + this.transform.forward * direction.y) * speed + 
                                       new Vector3(0, this.rigidbody.velocity.y, 0);
        }
        //Snapping character down to the ground on slopes. (May no longer be necessary if moving the player with Velocity rather than translating)
        /*RaycastHit hit;
        Physics.Raycast(this.transform.position, Vector3.down, out hit, snapDownThreshold);
        if (hit.point != Vector3.zero)
        {
            this.transform.position = new Vector3(this.transform.position.x, hit.point.y, this.transform.position.z);
        }*/
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.contacts[0].normal.y > .7f)
        {
            jumping = false;
        }
        float impactVelocity = Vector3.Magnitude(collision.relativeVelocity);
        if (impactVelocity > 30)
        {
            Debug.Log("Collision impact high (relative velocity = " + impactVelocity + " > 30; Player taking damage.");
            stats.health -= (int)(impactVelocity - 30);
            //Take damage
            //emit noise
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
            this.rigidbody.AddForce(Vector3.up * gravity * jumpForce);
            jumping = true;
            anim.SetBool("IsGrinding", false);
        }
    }
    public void Jump(float force)
    {
        if (!jumping)
        {
            this.rigidbody.AddForce(Vector3.up * gravity * force);
            jumping = true;
            anim.SetBool("IsGrinding", false);
        }
    }
    public IEnumerator Launch(float force)
    {
        anim.SetBool("IsGrinding", false);
        this.rigidbody.AddForce(Vector3.up * gravity * force);

        yield return new WaitForSeconds(0.1f); //Giving the player time to add more force to the jump
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

    public void Stop(bool stopShooting)
    {
        this.speed = 0;
        this.anim.SetFloat("Speed", 0);
        this.anim.SetBool("IsShooting", !stopShooting);
    }
}
