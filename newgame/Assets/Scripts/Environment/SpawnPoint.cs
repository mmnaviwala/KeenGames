using UnityEngine;
using System.Collections;

public class SpawnPoint : MonoBehaviour 
{
    public Transform objectToSpawn;
    public bool setOnTrigger = false;
    public float delay = 0f;
	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (setOnTrigger && other.tag == Tags.PLAYER)
            StartCoroutine(SpawnObject());
    }

    public IEnumerator SpawnObject()
    {
        if (delay != 0f)
            yield return new WaitForSeconds(delay);
        Instantiate(objectToSpawn, new Vector3(113.5f, 60f, 6f), objectToSpawn.transform.rotation);
    }
}
