using UnityEngine;
using System.Collections;

public class NoteScript : MonoBehaviour 
{
    public EnemyColor noteColor = EnemyColor.White;
    public int value = 1;
    //private CharacterStats player;
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
        if (other.gameObject.tag == Tags.PLAYER)
        {
            //AddNotes(other.GetComponent<CharacterStats>());
        }
    }
    /*public void AddNotes(CharacterStats player)
    {
        player.AddNotes(this.noteColor, value);
        Destroy(this.gameObject);
    }*/
}
