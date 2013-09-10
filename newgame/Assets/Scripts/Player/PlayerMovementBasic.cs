using UnityEngine;
using System.Collections;

public class PlayerMovementBasic : MonoBehaviour 
{
    Camera mainCam;
    enum CharacterState {   Idle = 0,
                            Walking = 1,
                            Trotting = 2,
                            Running = 3,
                            Jumping = 4 }
	// Use this for initialization
	void Start () 
    {
        this.transform.Rotate(Vector3.up, 90f);
        mainCam = Camera.main;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetKey(KeyCode.D))
        {
            this.transform.position = new Vector3(transform.position.x + (3f * Time.deltaTime), transform.position.y, transform.position.z);
            mainCam.transform.position = new Vector3(this.transform.position.x + 5f, mainCam.transform.position.y, mainCam.transform.position.z);
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == Tags.ENEMY)
        {
            this.GetComponent<CharacterStats>().notes--;
            Camera.main.GetComponent<CameraShake>().Shake();
        }
    }
}
