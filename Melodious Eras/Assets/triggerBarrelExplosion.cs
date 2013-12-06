using UnityEngine;
using System.Collections;

public class triggerBarrelExplosion : BreakableObject 
{
    Transform explosion;
	// Use this for initialization
	void Start () 
	{
        //explosion = this.transform.FindChild("explosion");
	}

	IEnumerator startExplosion()
	{
		yield return new WaitForSeconds(1.5f);
		Destroy(this.gameObject);
	}
    public override void TakeDamage(int damage)
    {
        this.durability -= damage;
        if (durability <= 0)
        {
            StartCoroutine(startExplosion());
        }
    }
}
