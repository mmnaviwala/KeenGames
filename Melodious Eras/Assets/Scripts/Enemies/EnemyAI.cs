using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour 
{
    public float hearingMultiplier = 1;     //0 = deaf, 1 = normal, >1 = dogs & security
    public float awarenessMultiplier = 1;   //
    private float lightDifferenceMultiplier; //optional feature. Enemies in high-light areas will find it harder to detect players in low-light areas.


    private Vector3 lastPlayerSighting;
    public NPCGroup group;

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    public void Listen()
    {
 
    }
}
