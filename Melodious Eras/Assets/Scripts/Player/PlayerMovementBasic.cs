﻿using UnityEngine;
using System.Collections;

public class PlayerMovementBasic : MonoBehaviour 
{
    enum CharacterState {   Idle = 0,
                            Walking = 1,
                            Trotting = 2,
                            Running = 3,
                            Jumping = 4 }
    public float jumpForce = 20f;
    public float speed = 7f;
    public bool jumping = false;

    private Camera mainCam;
    private CharacterStats stats;
    private Renderer[] meshRenderers;
    private float gravity;
    private Animator anim;
	// Use this for initialization
	void Start () 
    {
        stats = this.GetComponent<CharacterStats>();
        mainCam = Camera.main;
        gravity = Mathf.Abs(Physics.gravity.y);
        meshRenderers = this.transform.GetComponentsInChildren<Renderer>();
        anim = this.GetComponent<Animator>();
        anim.SetFloat("Speed", 7.0f);
        anim.SetBool("IsShooting", true);
	}
	
	// Update is called once per frame
	void Update () 
    {
        this.transform.position = new Vector3(transform.position.x + (speed * Time.deltaTime), transform.position.y, transform.position.z);
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == Tags.ENEMY)
        {
            stats.notes = stats.notes == 0 ? 0 : stats.notes - 1;
            stats.notes--;
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

    //void OnCollisionExit(Collision collision)
    //{
    //    Debug.Log("Player leaving contact with " + collision.collider.tag);
    //    if (collision.collider.tag == Tags.SLIDE)
    //        anim.SetBool("IsGrinding", false);
    //}

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
}
