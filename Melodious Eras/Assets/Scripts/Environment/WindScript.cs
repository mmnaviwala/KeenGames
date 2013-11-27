using UnityEngine;
using System.Collections;

public class WindScript : MonoBehaviour 
{
    public float force;
	// Use this for initialization
	void Start () 
    {
        StartCoroutine(Wind());
	}
	
	// Update is called once per frame
	void Update () 
    {
	    
	}

    IEnumerator Wind()
    {
        while (true)
        {
            this.rigidbody.AddForce(force, 0, force);
            yield return new WaitForSeconds(Random.Range(0, .5f));
        }
    }
}
