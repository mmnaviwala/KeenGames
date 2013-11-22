using UnityEngine;
using System.Collections;

public class titleMenu3D : MonoBehaviour 
{
    public GameObject objectType;
    GameObject[] selections;

    float radius = 2;
    public float centerPosY = -10;
	// Use this for initialization
	void Start () 
    {
        selections = new GameObject[8];
        int counter = 0;
        for (int angle = 0; angle < 360; angle += 45)
        {
            selections[counter] = GameObject.CreatePrimitive(PrimitiveType.Cube);
            selections[counter].transform.parent = this.transform;

            selections[counter].AddComponent<Rigidbody>();
            selections[counter].rigidbody.isKinematic = true;

            selections[counter].AddComponent<TitleMenuDisk>();
            selections[counter].GetComponent<TitleMenuDisk>().fragmentNum = counter;
            //selections[counter].transform.position = new Vector3(this.transform.position.x + radius * Mathf.Sin(angle * Mathf.Deg2Rad),
            //                                                     this.transform.position.y + radius * Mathf.Cos(angle * Mathf.Deg2Rad),
            //                                                     this.transform.position.z);
            selections[counter].GetComponent<TitleMenuDisk>().offset = new Vector3(radius * Mathf.Sin(angle * Mathf.Deg2Rad),
                                                                         radius * Mathf.Cos(angle * Mathf.Deg2Rad),
                                                                         0);
            //selections[counter].transform.localPosition = new Vector3(radius * Mathf.Sin(angle * Mathf.Deg2Rad),
            //                                                             radius * Mathf.Cos(angle * Mathf.Deg2Rad),
            //                                                             0);
            selections[counter].transform.localScale = new Vector3(2, 1, .1f);
            selections[counter].renderer.material.color = new Color((angle + 45) / 360f, (angle + 45) / 360f, (angle + 45) / 360f);
            selections[counter].name = "Cube" + (angle / 45);
            selections[counter].transform.Rotate(Vector3.forward, 90 - angle);
            counter++;
        }
	}

    // Update is called once per frame
    void Update() 
    {
        if (this.transform.position.y < .5f)
        {
            this.transform.position += new Vector3(0, 5 * Time.deltaTime, 0);
        }
        else if (this.transform.position.y < .75f)
        {
            this.transform.position = new Vector3(this.transform.position.x, .75f, -2);
        }
        this.transform.Rotate(Vector3.right, (Mathf.PerlinNoise(Time.time / 10, 0.0f) - .5f) * .25f);
        this.transform.Rotate(Vector3.forward, (Mathf.PerlinNoise(Time.time / 10, 0.0f) - .5f) * .5f);
	}


    void OnMouseDown()
    {
        Debug.Log("OnMouseDown " + Input.mousePosition);
    }
}
