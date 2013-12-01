using UnityEngine;
using System.Collections;

public class LaserPistol : SemiAutoWeapon
{
    LineRenderer laser;
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
            if (ammoInClip > 0)
            {
                shotFiredTime = Time.time;
                nextShotTime = shotFiredTime + fireRate;

                Ray ray = mainCam.ViewportPointToRay(new Vector3(.5f, .5f, 0));
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, range))
                {
                    StartCoroutine(ShootLaser(barrelExit.position, hit.point, .1f));
                    if (hit.collider.tag == Tags.ENEMY && !hit.collider.isTrigger)
                    {
                        hit.collider.GetComponent<EnemyStats>().TakeDamage(damage);
                    }
                }
                else
                {
                    StartCoroutine(ShootLaser(barrelExit.position, barrelExit.position + ray.direction * range, .1f));
                }

                //laser.GetComponent<Laser>().Shoot(barrelExit.position, hit.point, hit.collider.GetComponent<EnemyStats>(), damage, .1f);

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

    IEnumerator ShootLaser(Vector3 start, Vector3 end, float renderTime)
    {
        laser.SetPosition(0, start);
        laser.SetPosition(1, end);
        laser.enabled = true;
        yield return new WaitForSeconds(renderTime);
        laser.enabled = false;
    }
}
