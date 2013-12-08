using UnityEngine;
using System.Collections;

public class ChainLightning : Projectile 
{
    public int chainLength = 4;
    public LineRenderer[] lightningForks;
	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    public override void Shoot(Vector3 start, Vector3 end, EnemyStats target, int damage, float renderTime)
    {

    }

    void Chain()
    {
 
    }
}
