using UnityEngine;
using System.Collections;

public class NoteScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision with " + other.gameObject.tag);
        if (other.gameObject.tag == Tags.PLAYER)
        {
            other.GetComponent<CharacterStats>().notes++;
            GameObject.Destroy(this.gameObject);
        }
    }
}
