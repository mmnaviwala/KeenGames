using UnityEngine;
using System.Collections;

public class BigPapaAI : MonoBehaviour 
{
    Transform eye;
    public int hp = 6;
    public bool jumping = true;
    public float speed = 7;
    private Animator anim;
	// Use this for initialization
	void Start () 
    {
        eye = this.transform.FindChild("Diskmen1:Giant_Disk_Droup").FindChild("Diskmen1:Giant_Eye");
        jumping = true;
        anim = this.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        //keeping eye orientation stable
        //eye.rotation = new Quaternion(0.0f, -0.7f, 0.7f, 0.0f);

        //snapping the character down to the ground for smooth descent
        if (!jumping)
        {
            //this.rigidbody.useGravity = false;
            RaycastHit hit;
            this.transform.position = new Vector3(transform.position.x + (speed * Time.deltaTime), transform.position.y, transform.position.z);
            Physics.Raycast(this.transform.position, Vector3.down, out hit, 0.25f);
            if (hit.point != Vector3.zero)
            {
                this.transform.position = new Vector3(this.transform.position.x, hit.point.y, this.transform.position.z);
            }
        }
        if (hp <= 0)
        {
            StartCoroutine(this.Die());
        }
	}

    void OnCollisionEnter(Collision col)
    {
        Debug.Log("Big Papa Collision with " + col.gameObject.tag);
        jumping = false;
        if (col.gameObject.tag == Tags.OBSTACLE || col.gameObject.tag == Tags.ENEMY)
        {
            Destroy(col.gameObject);
            hp--;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == Tags.OBSTACLE || other.tag == Tags.ENEMY)
        {
            Destroy(other.gameObject);
            hp--;
        }
    }

    IEnumerator Die()
    {
        jumping = true;
        this.anim.SetBool("IsDead", true);
        yield return new WaitForSeconds(.958f);
        Destroy(this.gameObject);
    }
}
