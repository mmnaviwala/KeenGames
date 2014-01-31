using UnityEngine;
using System.Collections;

[AddComponentMenu("Scripts/Items/Weapons/Laser Pistol")]
public class LaserPistol : SemiAutoWeapon
{
	LineRenderer laser;
	YieldInstruction waitp1 = new WaitForSeconds(.1f); //render time for laser
	YieldInstruction endOfFrame = new WaitForEndOfFrame();

    // Use this for initialization
    void Start()
    {
        laser = barrelExit.GetComponent<LineRenderer>();
        laser.enabled = false;
        this.Initialize();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override bool Fire()
    {
        if (!reloading && Time.time > nextShotTime)
        {
            if (ammoInClip > 0 || infiniteAmmo)
            {
                shotFiredTime = Time.time;
                nextShotTime = shotFiredTime + fireRate;

                Ray ray = mainCam.ViewportPointToRay(new Vector3(.5f, .5f, 0));
                RaycastHit hit;
                
                if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, range, ignorePlayer))
                {
                    StartCoroutine(ShootLaser(barrelExit.position, hit.point));
                    if (hit.collider.tag == Tags.ENEMY && !hit.collider.isTrigger)
                    {
                        hit.collider.transform.GetComponent<EnemyStats>().TakeDamage(damage);
                    }
                    else if (hit.collider.tag == Tags.BREAKABLE && !hit.collider.isTrigger)
                    {
                        BreakableObject[] components = hit.collider.transform.GetComponents<BreakableObject>();
                        for (int i = 0; i < components.Length; i++)
                            components[i].TakeDamage(damage);

                        if (hit.rigidbody && !hit.rigidbody.isKinematic)
                        {
                            hit.rigidbody.AddForce(ray.direction * 10);
                            Debug.Log("Applying force to " + hit.rigidbody.name);
                        }
                        //hit.collider.transform.GetComponent<BreakableObject>().TakeDamage(damage);
                    }

                    //if (hit.rigidbody != null)
                    //{
                    //    hit.rigidbody.AddForce(ray.direction * 100);
                    //    Debug.Log("Adding force");
                    //}
                }
                else
                {
                    StartCoroutine(ShootLaser(barrelExit.position, barrelExit.position + ray.direction * range));
                }

                //laser.GetComponent<Laser>().Shoot(barrelExit.position, hit.point, hit.collider.GetComponent<EnemyStats>(), damage, .1f);
                if(!infiniteAmmo)
                    ammoInClip -= ammoPerShot;

                this.audio.PlayOneShot(shootingSound);

                return true;
            }
            else
            {
                this.audio.PlayOneShot(emptySound);
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

    IEnumerator ShootLaser(Vector3 start, Vector3 end)
    {
        laser.SetPosition(0, start);
        laser.SetPosition(1, end);
        laser.enabled = true;
		StartCoroutine(Flash ());
        yield return waitp1;
        laser.enabled = false;
    }

	IEnumerator Flash()
	{
		barrelExit.light.intensity = 1;
		while(barrelExit.light.intensity > .01f)
		{
			barrelExit.light.intensity = Mathf.Lerp (barrelExit.light.intensity, 0f, 10 * Time.deltaTime);
			yield return endOfFrame;
		}
	}
}
