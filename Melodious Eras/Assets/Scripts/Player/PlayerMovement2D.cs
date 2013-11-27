using UnityEngine;
using System.Collections;

public enum FacingDirection2D { Left, Right };
public class PlayerMovement2D : MonoBehaviour 
{
    public bool autoRun = true;
    public float speed = 7f;
    private bool moving = false;
    public FacingDirection2D facingDirection2D = FacingDirection2D.Right;
    public Vector3 runDirection = Vector3.right;
    private Animator anim;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (!autoRun)
        {
            speed = 0;
            //All movement inputs in here are temporary
            moving = false;
            if (Input.GetKey(KeyCode.D))
            {
                speed = 7;
                this.transform.position += Vector3.right * speed * Time.deltaTime;
                if (facingDirection2D != FacingDirection2D.Right)
                    this.transform.Rotate(Vector3.up, 180f);
                facingDirection2D = FacingDirection2D.Right;
                this.anim.SetFloat("Speed", 7);
                moving = true;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                speed = 7;
                this.transform.position -= Vector3.right * speed * Time.deltaTime;
                if (facingDirection2D != FacingDirection2D.Left)
                    this.transform.Rotate(Vector3.up, 180f);
                facingDirection2D = FacingDirection2D.Left;
                this.anim.SetFloat("Speed", 7);
                moving = true;
            }
            if (Input.GetKey(KeyCode.W))
            {
                speed = 7;
                this.transform.position += Vector3.forward * speed * Time.deltaTime;
                this.anim.SetFloat("Speed", speed);
                moving = true;
            }
            if (Input.GetKey(KeyCode.S))
            {
                speed = 7;
                this.transform.position -= Vector3.forward * speed * Time.deltaTime;
                this.anim.SetFloat("Speed", speed);
                moving = true;
            }
        }
        else
        {
            this.transform.position += runDirection * speed * Time.deltaTime; //autorun
        }
	}
}
