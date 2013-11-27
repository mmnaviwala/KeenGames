using UnityEngine;
using System.Collections;
using System.Runtime.CompilerServices;

public class PlayerMovementBasic : MonoBehaviour 
{
    enum CharacterState {   Idle = 0,
                            Walking = 1,
                            Trotting = 2,
                            Running = 3,
                            Jumping = 4 }
    public float jumpForce = 20f;
    public float speed = 7f;
    private bool moving = false;
    public bool jumping = false;
    public float snapDownThreshold = .25f;
    public bool isShooting = false;
    public bool useDefaultMovement = true;
    public float sound;


    private Camera mainCam;
    public Light flashlight;
    private CharacterStats stats;
    private HUD hud;
    private Renderer[] meshRenderers;
    private float gravity;
    private Animator anim;
	
	// Use this for initialization
    void Awake()
    {
        flashlight = this.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(2).GetChild(1).light;
    }
	void Start ()
    {
        mainCam = Camera.main;
        stats = this.GetComponent<CharacterStats>();
        anim = this.GetComponent<Animator>();
        hud = this.GetComponent<HUD>();
        meshRenderers = this.transform.GetComponentsInChildren<Renderer>();

        gravity = Mathf.Abs(Physics.gravity.y);
        anim.SetFloat("Speed", 7.0f);
        anim.SetBool("IsShooting", isShooting);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetButtonDown(InputType.TOGGLE_FLASHLIGHT))
        {
            ToggleFlashlight();
        }
        moving = false;
        if (!jumping && useDefaultMovement)
        {
            Vector2 direction = new Vector2();
            if (Input.GetKey(KeyCode.W))
            {
                direction += new Vector2(0, 1);
                moving = true;
            }
            if (Input.GetKey(KeyCode.S))
            {
                direction -= new Vector2(0, 1);
                moving = true;
            }
            if (Input.GetKey(KeyCode.A))
            {
                direction -= new Vector2(1, 0);
                moving = true;
            }
            if (Input.GetKey(KeyCode.D))
            {
                direction += new Vector2(1, 0);
                moving = true;
            }
            if (moving)
            {
                speed = Input.GetButton(InputType.SNEAK) ? 2 : 7;
                this.anim.SetFloat("Speed", speed);
                //this.animation["Locomotion"].speed = (speed > 5.667f) ? 7/5.667f : 1;
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

    void FixedUpdate()
    {
        float h = Input.GetAxis(InputType.HORIZONTAL);
        float v = Input.GetAxis(InputType.VERTICAL);
        bool sneaking = Input.GetButton(InputType.SNEAK);

        MovementManagement(h, v, sneaking);
    }

    /*void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == Tags.ENEMY)
        {
            stats.notes -= other.GetComponent<EnemyStats>().damageValue;
            if (stats.notes < 0) 
                stats.notes = 0;

            StartCoroutine(Blink());
            mainCam.GetComponent<CameraShake>().Shake();
        }
    }*/
    void OnCollisionEnter(Collision collision)
    {
        if (collision.contacts[0].normal.y > .7f)
        {
            jumping = false;
            float impactVelocity = Vector3.Magnitude(collision.relativeVelocity);
            if ( impactVelocity> 30)
            {
                Debug.Log("Collision impact high (relative velocity = " + impactVelocity + " > 30; Player taking damage.");
                //Take damage
                //emit noise
            }
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
    /// Goes through each of the player's renderers (3 total) and flashes them on and off.
    /// </summary>
    /// <returns></returns>
    public IEnumerator Blink()
    {
        for (int i = 0; i < 5; i++)
        {
            for (int r = 0; r < 3; r++)
                meshRenderers[r].enabled = false;
            yield return new WaitForSeconds(.1f);

            for (int r = 0; r < 3; r++)
                meshRenderers[r].enabled = true;
            yield return new WaitForSeconds(.25f);
        }
    }

    /// <summary>
    /// Determines if the transform is grounded (less than 0.25f off the ground).
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

    void MovementManagement(float horizontal, float vertical, bool isSneaking)
    {
 
    }

    public void ToggleFlashlight()
    {
        flashlight.enabled = !flashlight.enabled;
    }
}
