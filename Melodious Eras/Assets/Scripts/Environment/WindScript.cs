using UnityEngine;
using System.Collections;

[AddComponentMenu("Scripts/Environment/Wind Script")]
public class WindScript : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        StartCoroutine(Wind());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Wind()
    {
        while (true)
        {
            this.rigidbody.AddForce(Environment.wind);
            float yieldTime = Random.Range(0, 2f);
            yield return new WaitForSeconds(yieldTime * yieldTime);
        }
    }
}
