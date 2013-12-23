using UnityEngine;
using System.Collections;

[AddComponentMenu("Scripts/Environment/Light Flicker")]
public class LightFlicker : MonoBehaviour 
{
    public bool flickering = false;
    public float frequency = 0.5f;
	// Use this for initialization
	void Start () 
    {
        if (flickering)
            StartCoroutine(this.Flicker());
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    IEnumerator Flicker()
    {
        while (true)
        {
            this.light.enabled = !this.light.enabled;
            yield return new WaitForSeconds(Random.Range(0f, frequency));
        }
    }
}
