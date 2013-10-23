using UnityEngine;
using System.Collections;

public class ObstacleScript : MonoBehaviour 
{
    public float timeDelay = 0f;
    bool falling = false;
    float velocity = 0f;
	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (falling)
        {
            velocity -= Time.deltaTime * 9.81f;
            this.transform.position = Vector3.Lerp(this.transform.position, this.transform.position + Vector3.down, velocity * Time.deltaTime);
        }
	}
    IEnumerator Fall()
    {
        yield return new WaitForSeconds(timeDelay);
        falling = true;
    }
}
