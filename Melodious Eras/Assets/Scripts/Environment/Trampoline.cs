using UnityEngine;
using System.Collections;

public class Trampoline : MonoBehaviour 
{
    public float intensity = 300f;
    void OnCollisionEnter(Collision collision)
    {
        //if landing on the top
        if (collision.contacts[0].normal.y < -.9f) 
            StartCoroutine(collision.gameObject.GetComponent<PlayerMovementBasic>().Launch(intensity));

        Debug.Log("Trampoline " + collision.contacts[0].normal.y);
        Debug.Log(collision.contacts.Length + " - " + collision.contacts[0].normal.y);
    }
}
