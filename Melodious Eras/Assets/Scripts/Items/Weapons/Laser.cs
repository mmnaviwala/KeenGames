using UnityEngine;
using System.Collections;

public class Laser : Projectile
{
    LineRenderer laser;
	// Use this for initialization
    void Awake()
    {

        laser = this.GetComponent<LineRenderer>();
        laser.enabled = false;
        laser.useWorldSpace = true;
        Debug.Log("Laser start");
    }
	void Start () 
    {
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    public override void Shoot(Vector3 start, Vector3 end, EnemyStats target, int damage, float renderTime)
    {

        laser.SetPosition(0, start);
        laser.SetPosition(1, end);

        if (target != null)
        {
            target.TakeDamage((int)(damage * damageModifier));
            StartCoroutine(AnimateLaser(renderTime));
        }
    }
    IEnumerator AnimateLaser(float renderTime)
    {
        laser.enabled = true;
        yield return new WaitForSeconds(renderTime);
        laser.enabled = false;
    }
}
