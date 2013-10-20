using UnityEngine;
using System.Collections;

public class NoteScript : MonoBehaviour 
{
    public EnemyColor noteColor = EnemyColor.White;
    public int value = 1;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        this.transform.Rotate(Vector3.up, 90f * Time.deltaTime);
	}
    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision with " + other.gameObject.tag);
        if (other.gameObject.tag == Tags.PLAYER)
        {
            other.GetComponent<CharacterStats>().AddNotes(noteColor, value);
            GameObject.Destroy(this.gameObject);
        }
    }
}
