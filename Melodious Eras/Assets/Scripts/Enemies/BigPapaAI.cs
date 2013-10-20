using UnityEngine;
using System.Collections;

public class BigPapaAI : MonoBehaviour 
{
    Transform eye;
    private int hp = 3;
	// Use this for initialization
	void Start () 
    {
        eye = this.transform.FindChild("Diskmen1:Giant_Disk_Droup").FindChild("Diskmen1:Giant_Eye");
	}
	
	// Update is called once per frame
	void Update () 
    {
        //keeping eye orientation stable
        eye.rotation = new Quaternion(0.0f, -0.7f, 0.7f, 0.0f);
        Debug.Log(eye.localRotation);
        if (hp <= 0)
            Destroy(this.gameObject);
	}

    void OnCollisionEnter(Collision col)
    {
        Debug.Log("Big Papa Collision with " + col.gameObject.tag);
        if (col.gameObject.tag == Tags.ENEMY)
        {
            Destroy(col.gameObject);
            hp--;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == Tags.ENEMY)
        {
            Destroy(other.gameObject);
            hp--;
        }
    }
}
