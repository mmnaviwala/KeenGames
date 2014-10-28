using UnityEngine;
using System.Collections;

[AddComponentMenu("Scripts/Items/Weapons/Lightning Gun")]
public class LightningGun : Gun 
{
    ChainLightning lightningBolt;
    public LineRenderer[] lightningForks;

    int forkCount, maxForkCount;
    // Use this for initialization
	void Start () 
    {
        this.Initialize();
        maxForkCount = 4;
        forkCount = 0;
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}
    public override bool Fire()
    {
        if (!reloading && Time.time > nextShotTime)
        {
            if (ammoInClip > 0)
            {
                shotFiredTime = Time.time;
                nextShotTime = shotFiredTime + fireRate;

                Ray ray = mainCam.ViewportPointToRay(new Vector3(.5f, .5f, 0));
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, range, ignorePlayer))
                {
                    //StartCoroutine(ShootLaser(barrelExit.position, hit.point, .1f));
                    if (hit.collider.tag == Tags.ENEMY && !hit.collider.isTrigger)
                    {
                        hit.collider.GetComponent<EnemyStats>().TakeDamage(damage);
                    }
                    else if (hit.collider.tag == Tags.BREAKABLE && !hit.collider.isTrigger)
                    {
                        hit.collider.GetComponent<BreakableObject>().TakeDamage(damage);
                    }
                }
                else
                {
                    //StartCoroutine(ShootLaser(barrelExit.position, barrelExit.position + ray.direction * range, .1f));
                }
                ammoInClip -= ammoPerShot;

                this.GetComponent<AudioSource>().PlayOneShot(shootingSound);

                return true;
            }
            else
            {
                this.GetComponent<AudioSource>().PlayOneShot(emptySound);
                if (this.extraAmmo > 0)
                    StartCoroutine(this.Reload());
                return false;
            }
        }
        else
        {
            return true;
        }
    }

    IEnumerator ForkLightning(Vector3 start, Vector3 end, float renderTime)
    {
        //Physics.OverlapSphere(
        yield return new WaitForSeconds(.05f);
    }
}
