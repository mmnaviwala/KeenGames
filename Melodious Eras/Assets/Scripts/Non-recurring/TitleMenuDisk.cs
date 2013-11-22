using UnityEngine;
using System.Collections;

public class TitleMenuDisk : MonoBehaviour 
{
    float bobbingWeight = 4;
    public int fragmentNum;
	// Use this for initialization
	void Start () 
    {
	}
	
	// Update is called once per frame
	void Update () 
    {
        float zdepth = (Mathf.PerlinNoise(Time.time / 10 + fragmentNum, 0.0f) - .5f) * bobbingWeight / 500;
        this.transform.Translate(this.transform.forward * zdepth);
        //this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, zdepth);
	}

    void OnMouseDown()
    {
        Debug.Log("Clicked on " + this.name);
    }
}
