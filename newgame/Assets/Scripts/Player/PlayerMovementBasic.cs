using UnityEngine;
using System.Collections;

public class PlayerMovementBasic : MonoBehaviour 
{
    Camera mainCam;
    CharacterStats stats;
    enum CharacterState {   Idle = 0,
                            Walking = 1,
                            Trotting = 2,
                            Running = 3,
                            Jumping = 4 }
    public float jumpForce = 20f;
    //public float cameraSmoothness = 6f;
    //public float cameraOffsetX = 9f;
    public float speed = 7f;

    private float gravity;

    public bool jumping = false;
	// Use this for initialization
	void Start () 
    {
        stats = this.GetComponent<CharacterStats>();
        mainCam = Camera.main;
        gravity = Mathf.Abs(Physics.gravity.y);
	}
	
	// Update is called once per frame
	void Update () 
    {
        this.transform.position = new Vector3(transform.position.x + (speed * Time.deltaTime), transform.position.y, transform.position.z);
	}

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.gameObject.tag == Tags.ENEMY)
        {
            stats.notes = stats.notes == 0 ? 0 : stats.notes - 1;
            stats.notes--;
            if (stats.notes < 0) 
                stats.notes = 0;
            StartCoroutine(Blink());
            Camera.main.GetComponent<CameraShake>().Shake();
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.contacts[0].normal.y > .9f)
            jumping = false;
    }

    public void Jump()
    {
        if (!jumping)
        {
            this.rigidbody.AddForce(Vector3.up * gravity * jumpForce);
            jumping = true;
        }
    }
    public void Jump(float force)
    {
        if (!jumping)
        {
            this.rigidbody.AddForce(Vector3.up * gravity * force);
            jumping = true;
        }
    }
    public void JumpRegardless(float force)
    {
        this.rigidbody.AddForce(Vector3.up * gravity * force);
        jumping = true;
    }

    public IEnumerator Blink()
    {
        Debug.Log("Blinking");

        for (int i = 0; i < 3; i++)
        {
            this.transform.renderer.enabled = false;
            yield return new WaitForSeconds(.5f);
            this.transform.renderer.enabled = true;
            yield return new WaitForSeconds(.5f);
        }
    }
}
