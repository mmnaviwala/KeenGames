﻿using UnityEngine;
using System.Collections;
using System.Runtime.CompilerServices;

public enum FacingDirection2D { Left, Right };
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
    public bool autoRun = true;
    public FacingDirection2D facingDirection2D = FacingDirection2D.Right;
    public Vector3 runDirection = Vector3.right;


    private Camera mainCam;
    private CameraMovement2D cam2d;
    private CameraMovement3D cam3d;
    private CharacterStats stats;
    private HUD hud;
    private Renderer[] meshRenderers;
    private float gravity;
    private Animator anim;
	
	// Use this for initialization
	void Start ()
    {
        mainCam = Camera.main;

        stats = this.GetComponent<CharacterStats>();
        cam2d = mainCam.GetComponent<CameraMovement2D>();
        cam3d = mainCam.GetComponent<CameraMovement3D>();
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
        if (!autoRun)
        {
            //All movement inputs in here are temporary
            moving = false;
            if (cam2d.enabled)
            {
                if (Input.GetKey(KeyCode.D))
                {
                    this.transform.position += Vector3.right * speed * Time.deltaTime;
                    if (facingDirection2D != FacingDirection2D.Right)
                        this.transform.Rotate(Vector3.up, 180f);
                    //this.transform.position = new Vector3(transform.position.x + (speed * Time.deltaTime), transform.position.y, transform.position.z);
                    facingDirection2D = FacingDirection2D.Right;
                    this.anim.SetFloat("Speed", 7);
                    moving = true;
                }
                else if (Input.GetKey(KeyCode.A))
                {
                    this.transform.position -= Vector3.right * speed * Time.deltaTime;
                    if (facingDirection2D != FacingDirection2D.Left)
                        this.transform.Rotate(Vector3.up, 180f);
                    //this.transform.position = new Vector3(transform.position.x - (speed * Time.deltaTime), transform.position.y, transform.position.z);
                    facingDirection2D = FacingDirection2D.Left;
                    this.anim.SetFloat("Speed", 7);
                    moving = true;
                }
            }
            else if (cam3d.enabled)
            {
                if (Input.GetKey(KeyCode.W))
                {
                    this.transform.position += this.transform.forward * speed * Time.deltaTime;
                    this.anim.SetFloat("Speed", 7);
                    moving = true;
                }
                if (Input.GetKey(KeyCode.S))
                {
                    this.transform.position -= this.transform.forward * speed * Time.deltaTime;
                    this.anim.SetFloat("Speed", 7);
                    moving = true;
                }
                if (Input.GetKey(KeyCode.A))
                {
                    this.transform.position -= this.transform.right * speed * Time.deltaTime;
                    this.anim.SetFloat("Speed", 7);
                    moving = true;
                }
                if (Input.GetKey(KeyCode.D))
                {
                    this.transform.position += this.transform.right * speed * Time.deltaTime;
                    this.anim.SetFloat("Speed", 7);
                    moving = true;
                }
            }
            if (!moving)
                this.anim.SetFloat("Speed", 0);
        }
        else
        {
            this.transform.position += runDirection * speed * Time.deltaTime;
        }
        //snapping the character down to the ground for smooth descent
        if (!jumping)
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
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        bool sneaking = Input.GetButton("Sneak");

        MovementManagement(h, v, sneaking);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == Tags.ENEMY)
        {
            stats.notes -= other.GetComponent<EnemyStats>().damageValue;
            if (stats.notes < 0) 
                stats.notes = 0;

            StartCoroutine(Blink());
            mainCam.GetComponent<CameraShake>().Shake();
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.contacts[0].normal.y > .7f)
            jumping = false;

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
}
