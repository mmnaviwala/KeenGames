using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour 
{
    public GameObject player;
    public GameObject enemyToSpawn;
    Camera mainCam;
    public float spawnDelayMin = 5.0f;
    public float spawnDelayMax = 8.0f;
	// Use this for initialization
	void Start () 
    {
        player = GameObject.FindGameObjectWithTag(Tags.PLAYER);
        for (float x = -10f; x <= 90f; x += 10f)
        {
            GameObject marker = GameObject.CreatePrimitive(PrimitiveType.Cube);
            marker.renderer.material.color = Color.blue;
            marker.transform.localScale = new Vector3(1f, 3f, 1f);
            marker.transform.position = new Vector3(x, 1f, 1.5f);
        }
        Debug.Log("Playing audio");
        mainCam = Camera.main;
        Debug.Log(mainCam.WorldToScreenPoint(new Vector3(8, 1, 0)));
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    void SpawnEnemy()
    {
        
    }
    void SpawnEnemy(int color) 
    {
        
    }
}
