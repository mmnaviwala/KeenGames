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
            selections[counter].transform.position = new Vector3(radius * Mathf.Sin(angle * Mathf.Deg2Rad),
                                      -10 + radius * Mathf.Cos(angle * Mathf.Deg2Rad),
                                      -1);

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
        if (centerPosY < -.25f)
        {
            centerPosY += 3 * Time.deltaTime;

            for (int i = 0; i < 8; i++)
                selections[i].transform.position += new Vector3(0, 3 * Time.deltaTime, 0);
        }
        else if (centerPosY != 0)
        {
            centerPosY = 0;
            for(int angle = 0; angle < 360; angle+= 45)
                selections[angle / 45].transform.position = new Vector3(radius * Mathf.Sin(angle * Mathf.Deg2Rad),
                                      radius * Mathf.Cos(angle * Mathf.Deg2Rad),
                                      -1);
        }
        if (Input.GetButtonDown("Fire1")) //mouse click
        {
            Debug.Log(Input.mousePosition);
        }
       
	}

    void OnMouseDown()
    {
        Debug.Log("OnMouseDown " + Input.mousePosition);
    }
}
