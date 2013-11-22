﻿using UnityEngine;
using System.Collections;

public class TitleMenuDisk : MonoBehaviour 
{
    Transform disk;
    public float bobbingWeight = .01f;
    public int fragmentNum;
    public float maxDistance;
    public bool forward = true;
    public Vector3 offset;
	// Use this for initialization
	void Start () 
    {
        this.transform.localPosition = offset;
        forward = Random.Range(-.5f, .5f) < 0 ? true : false;
        bobbingWeight = Random.Range(.1f, .5f);
        maxDistance = Random.Range(2.1f, 2.24f);
        disk = this.transform.parent;
        this.transform.position += disk.forward * Random.Range(-.5f, .5f);
        StartCoroutine(ChangeSpeed());
	}
	
	// Update is called once per frame
	void Update () 
    {
        //this.transform.position += disk.forward * (Mathf.PerlinNoise(Time.time * 5 + fragmentNum, 0) - .45f) * bobbingWeight;
        float distance = Vector3.Distance(this.transform.position, disk.position);

        //if (forward && distance > maxDistance)
        //{
        //    forward = false;
        //}
        //if (!forward && distance < 2f)
        //{
        //    forward = true;
        //}
        if (distance > maxDistance || distance < 2f)
        {
            forward = !forward;
        }
        if (forward)
            this.transform.position += Time.deltaTime * disk.forward * bobbingWeight; //* Mathf.PerlinNoise(Time.time + fragmentNum, 0) * bobbingWeight;
        else
            this.transform.position -= Time.deltaTime * disk.forward * bobbingWeight; //* Mathf.PerlinNoise(Time.time + fragmentNum, 0) * bobbingWeight;
        
        //float zdepth = (Mathf.PerlinNoise(Time.time / 5 + fragmentNum, 0.0f) - .5f) * bobbingWeight;
        //this.transform.Translate(disk.forward * zdepth);
        //this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, zdepth);
	}

    IEnumerator ChangeSpeed()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(3, 6));
            bobbingWeight = Random.Range(.1f, .5f);
            //maxDistance = Random.Range(2.1f, 2.25f);
        }
    }

    void OnMouseDown()
    {
        Debug.Log("Clicked on " + this.name);
    }
    void OnMouseOver()
    {
        this.transform.localPosition = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y, -maxDistance / 2);
    }
    void OnMouseExit()
    {
        this.transform.localPosition = offset;
    }
}
