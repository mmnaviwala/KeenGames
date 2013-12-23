using UnityEngine;
using System.Collections;

[AddComponentMenu("Scripts/Environment/Spawn Point")]
public class SpawnPoint : MonoBehaviour 
{
    public Transform objectToSpawn;
    public bool setOnTrigger = false,
                playerOnly = false;
    public float delay = 0f;

    void OnTriggerEnter(Collider other)
    {
        if (setOnTrigger && !(playerOnly && other.tag != Tags.PLAYER))
            //StartCoroutine(SpawnObject());
            Invoke("SpawnObject2", delay);
    }

    public IEnumerator SpawnObject()
    {
        if (delay != 0f)
            yield return new WaitForSeconds(delay);
        Instantiate(objectToSpawn, new Vector3(113.5f, 60f, 6f), objectToSpawn.transform.rotation);
    }
    public IEnumerator SpawnObject(float delayP)
    {
        if (delayP != 0f)
            yield return new WaitForSeconds(delayP);
        Instantiate(objectToSpawn, new Vector3(113.5f, 60f, 6f), objectToSpawn.transform.rotation);
    }
    private void SpawnObject2()
    {
        Instantiate(objectToSpawn, new Vector3(113.5f, 60f, 6f), objectToSpawn.transform.rotation);
    }
}
