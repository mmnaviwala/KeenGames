using UnityEngine;
using System.Collections;

[AddComponentMenu("Scripts/Items/Weapons/Laser Pistol")]
public class LaserPistol : SemiAutoWeapon
{
	LineRenderer laser;
	YieldInstruction waitp1 = new WaitForSeconds(.1f);    //render time for laser
	YieldInstruction endOfFrame = new WaitForEndOfFrame();

    // Use this for initialization
    void Start()
    {
        laser = barrelExit.GetComponent<LineRenderer>();
        laser.enabled = false;
        this.Initialize();
    }

    public override bool Fire()
    {        
        if (!reloading && Time.time > nextShotTime) //if capable of firing yet
        {
            if (ammoInClip > 0 || infiniteAmmo)
            {
                shotFiredTime = Time.time;
                nextShotTime = shotFiredTime + fireRate;

                Ray rayFromCam = mainCam.ViewportPointToRay(new Vector3(.5f, .5f, 0)); //casts a ray straight out of the center of the camera
                RaycastHit hit;
                
                if (Physics.Raycast(rayFromCam, out hit, range, ignorePlayer))
                {
                    StartCoroutine(ShootLaser(barrelExit.position, hit.point));
                    if (!hit.collider.isTrigger) //hopefully isn't necessary anymore, since all triggers should now be on ignored layers
                    {
                        if (hit.collider.tag == Tags.ENEMY)
                        {
                            hit.collider.transform.GetComponent<EnemyStats>().TakeDamage((int)(damage * GameController.difficulty_playerDamageMultiplier));
                        }
                        else if (hit.collider.tag == Tags.BREAKABLE)
                        {
                            //Deals damage to the object. Some objects (light lamps) have multiple breakable components (such as CircuitLight & FlipSwitch)
                            BreakableObject[] components = hit.collider.transform.GetComponents<BreakableObject>();
                            for (int i = 0; i < components.Length; i++)
                                components[i].TakeDamage((int)(damage * GameController.difficulty_playerDamageMultiplier));

                            if (hit.rigidbody && !hit.rigidbody.isKinematic) //applies force, if able
                                hit.rigidbody.AddForce(rayFromCam.direction * 10);
                        } 
                    }
                }
                else
                {   //Shoots a laser forward, if no collision
                    StartCoroutine(ShootLaser(barrelExit.position, barrelExit.position + rayFromCam.direction * range));
                }

                if(!infiniteAmmo)
                    ammoInClip -= ammoPerShot;

                this.audio.PlayOneShot(shootingSound);
                return true;
            }
            else //if no ammo in the clip
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

    public override bool Fire(CharacterStats target)
    {
        if (!reloading && Time.time > nextShotTime) 
        {
            if (ammoInClip > 0 || infiniteAmmo)
            {
                shotFiredTime = Time.time;
                nextShotTime = shotFiredTime + fireRate;

                StartCoroutine(ShootLaser(this.barrelExit.position, target.transform.position + Vector3.up));
                target.TakeDamage((int)(damage * GameController.difficulty_enemyDamageMultiplier));

                if (!infiniteAmmo)
                    ammoInClip -= ammoPerShot;

                this.audio.PlayOneShot(shootingSound);
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Displays the laser shot.
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <returns></returns>
    IEnumerator ShootLaser(Vector3 start, Vector3 end)
    {
        laser.SetPosition(0, start);
        laser.SetPosition(1, end);
        laser.enabled = true;
		StartCoroutine(Flash ());
        yield return waitp1;
        laser.enabled = false;
    }
    /// <summary>
    /// Light flash at the barrel of the gun.
    /// </summary>
    /// <returns></returns>
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
