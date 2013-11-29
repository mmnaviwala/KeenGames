using UnityEngine;
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
            isAiming = false;
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
            }
            else if (Input.GetButtonUp(InputType.AIM))
            {
                mainCam.SetOffset(isCrouching ? CameraOffset.Crouch : CameraOffset.Default);
            }
            //CROUCH offset
            if (Input.GetButtonDown(InputType.CROUCH))
            {
                isCrouching = !isCrouching; //toggled instead of held
                mainCam.SetOffset(isCrouching ? CameraOffset.Crouch : CameraOffset.Default);
                //Perform crouch here
            }
            //----------------------------------------------

            float h = Input.GetAxis(InputType.HORIZONTAL);  //A(neg), D(pos), Left joystick left(neg)/right(pos)
            float v = Input.GetAxis(InputType.VERTICAL);    //S(neg), W(pos), Left joystick down(neg)/up(pos)

            Vector2 direction = new Vector2(h, v);
            moving = !direction.Equals(Vector2.zero);

            if (moving)
            {
                //Walking is for PC only; speed is handled by analog sticks on consoles
                //Listening for all 3 to avoid camera shifting issues
                if (Input.GetButtonDown(InputType.WALK))
                {
                    mainCam.SetOffset(CameraOffset.Walk);
                }
                else if (Input.GetButtonUp(InputType.WALK))
                {
                    mainCam.SetOffset(isCrouching ? CameraOffset.Crouch : CameraOffset.Default);
                }

                //Determining speed
                speed = (Input.GetButton(InputType.WALK) || Input.GetButton(InputType.AIM)) ? 2 : 5.657f;
                speed *= ((direction.magnitude < 1) ? direction.magnitude : 1);
                this.anim.SetFloat("Speed", speed);

                // Facing and running the desired direction
                float angle = Vector2.Angle(Vector2.up, direction);
                if (direction.x < 0)
                    angle = -angle;
                this.transform.eulerAngles = new Vector3(this.transform.eulerAngles.x, mainCam.transform.eulerAngles.y + angle, this.transform.eulerAngles.z);
                this.rigidbody.velocity = this.transform.forward * speed + new Vector3(0, this.rigidbody.velocity.y, 0);
            }
            if (Input.GetButtonDown(InputType.JUMP))
            {
                this.Jump();
            }
        }
        if (!moving)
            this.anim.SetFloat("Speed", 0);
        //snapping the character down to the ground for smooth descent
        if (!jumping && useDefaultMovement)
        {
            RaycastHit hit;
            Physics.Raycast(this.transform.position, Vector3.down, out hit, snapDownThreshold);
            if (hit.point != Vector3.zero)
            {
                this.transform.position = new Vector3(this.transform.position.x, hit.point.y, this.transform.position.z);
            }
        }
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
