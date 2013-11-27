using UnityEngine;
using System.Collections;

public class Ladder : MonoBehaviour 
{
    public float bottom, top;
    GameObject player;
    Vector3 topPos;
    public Vector3 snapDirection;

    bool climbing = false;
	// Use this for initialization
	void Start () 
    {
        bottom = this.transform.position.y - this.transform.localScale.y / 2;
        top = bottom + this.transform.localScale.y; //temporary (for cubes)
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (climbing)
        {
            if (Input.GetKey(KeyCode.W))
            {
                player.transform.position += Vector3.up * 1.5f * Time.deltaTime;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                player.transform.position -= Vector3.up * 1.5f * Time.deltaTime;

                if (player.transform.position.y < bottom + .5f)
                {
                    StartCoroutine(FreezeControls());
                    climbing = false;
                    player.GetComponent<PlayerMovementBasic>().useDefaultMovement = true;
                    player.rigidbody.useGravity = true;
                }
            }
            if (Input.GetButtonDown(InputType.JUMP))
            {
                climbing = false;
                player.GetComponent<PlayerMovementBasic>().useDefaultMovement = true;
                player.rigidbody.useGravity = true;
            }
        }
	}
    void OnTriggerEnter(Collider other)
    {
        if (other is CapsuleCollider && other.tag == Tags.PLAYER)
            player = other.gameObject;
    }
    void OnTriggerStay(Collider other)
    {
        if (other is CapsuleCollider && Input.GetButtonDown(InputType.USE) && other.name == player.name)
        {
            climbing = true;
            player.GetComponent<PlayerMovementBasic>().useDefaultMovement = false;
            player.rigidbody.useGravity = false;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other is CapsuleCollider)
        {
            if (other.transform.position.y >= top - .5f)
            {
                other.transform.position = new Vector3(other.transform.position.x, top, other.transform.position.z) + snapDirection;
                StartCoroutine(FreezeControls());
            }
            climbing = false;
            player.GetComponent<PlayerMovementBasic>().useDefaultMovement = true;
            player.rigidbody.useGravity = true;
        }

    }

    IEnumerator FreezeControls()
    {
        player.GetComponent<PlayerMovementBasic>().enabled = false;
        yield return new WaitForSeconds(.5f);
        player.GetComponent<PlayerMovementBasic>().enabled = true;
    }
}
