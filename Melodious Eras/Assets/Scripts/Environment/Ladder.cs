using UnityEngine;
using System.Collections;

[AddComponentMenu("Scripts/Environment/Ladder")]
public class Ladder : MonoBehaviour 
{
    public float bottom, top;
    GameObject player;
    Vector3 topPos;
    CameraMovement3D cam;
    public Vector3 snapDirection;

    bool climbing = false;
    bool canClimb = false;
	// Use this for initialization
	void Start () 
    {
        bottom = this.transform.position.y - this.transform.localScale.y / 2;
        top = bottom + this.transform.localScale.y; //temporary (for cubes)
        cam = Camera.main.GetComponent<CameraMovement3D>();

        bottom = this.GetComponent<Renderer>().bounds.min.y;
        top = this.GetComponent<Renderer>().bounds.max.y;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (canClimb)
        {
            if (Input.GetButtonDown(InputType.USE))
            {
                climbing = true;
                player.GetComponent<PlayerMovementBasic>().useDefaultMovement = false;
                player.GetComponent<Rigidbody>().useGravity = false;
            }
        }
        if (climbing)
        {
            if (Input.GetAxis(InputType.VERTICAL) > 0)
            {
                cam.SetOffset(CameraOffset.ClimbUp);
                player.transform.position += Vector3.up * 1.5f * Time.deltaTime;
            }
            else if (Input.GetAxis(InputType.VERTICAL) < 0)
            {
                cam.SetOffset(CameraOffset.ClimbDown);
                player.transform.position -= Vector3.up * 1.5f * Time.deltaTime;

                if (player.transform.position.y < bottom + .5f)
                {
                    cam.SetOffset(CameraOffset.Default);
                    StartCoroutine(FreezeControls());
                    climbing = false;
                    player.GetComponent<PlayerMovementBasic>().useDefaultMovement = true;
                    player.GetComponent<Rigidbody>().useGravity = true;
                }
            }
            if (Input.GetButtonDown(InputType.JUMP))
            {
                climbing = false;
                player.GetComponent<PlayerMovementBasic>().useDefaultMovement = true;
                player.GetComponent<Rigidbody>().useGravity = true;
            }
        }
	}
    void OnTriggerEnter(Collider other)
    {
        if (other is CapsuleCollider && other.tag == Tags.PLAYER)
        {
            player = other.gameObject;
            canClimb = true;
            Debug.Log("Player near ladder");
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other is CapsuleCollider && other.tag == Tags.PLAYER)
        {
            if (other.transform.position.y >= top - .5f)
            {
                cam.SetOffset(CameraOffset.Default);
                other.transform.position = new Vector3(other.transform.position.x, top, other.transform.position.z) + snapDirection;
                StartCoroutine(FreezeControls());
            }
            canClimb = false;
            climbing = false;
            player.GetComponent<PlayerMovementBasic>().useDefaultMovement = true;
            player.GetComponent<Rigidbody>().useGravity = true;
            Debug.Log("Player leaving ladder");
        }
    }

	private YieldInstruction freezeWait = new WaitForSeconds(.5f);
    IEnumerator FreezeControls()
    {
        player.GetComponent<PlayerMovementBasic>().enabled = false;
		yield return freezeWait;
        player.GetComponent<PlayerMovementBasic>().enabled = true;
    }
}
