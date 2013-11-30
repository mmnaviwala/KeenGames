using UnityEngine;
using System.Collections;

public class Gun : Weapon
{
    #region Variables
    public Transform barrelExit;
    public GameObject projectile;   //(Optional?) projectile/ammo type
    public Animation attackAnimation, reloadAnimation;
    public WeaponMod[] mods;        //May need to be replaced with sub-transform Mod Slots on each gun model.
    public bool isAuto;

    public AudioClip shootingSound, emptySound;

    public int ammoCapacity, ammoInClip, extraAmmo, maxAmmo, clipSize, ammoPerShot,
               damage, muzzleVelocity;
    public int durability, weight;
    public float fireRate, //Time between shots.
                 accuracy, //0 = perfect accuracy for now.
                 range;    //Range projectile will travel, or range where reticle will turn red.

    protected float shotFiredTime, nextShotTime;
    protected bool reloading = false;

    protected Camera mainCam;
    protected Vector3 centerScreen;
    #endregion

    // Use this for initialization
    void Start()
    {
        this.Initialize();
    }

    public override void Initialize()
    {
        base.Initialize();
        if (barrelExit == null)
            barrelExit = this.transform.FindChild("barrel_exit");
        shotFiredTime = nextShotTime = Time.time;
        mainCam = Camera.main;
        centerScreen = new Vector3(mainCam.pixelWidth / 2, mainCam.pixelHeight / 2, mainCam.nearClipPlane);

    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// <para>Called when player Shoots. Possibly returns the damage dealt as an int?
    /// Alternatively, Instantiate a projectile to shoot towards an enemy.</para>
    /// <para>Returns FALSE if gun is empty.</para>
    /// </summary>
    /// <returns></returns>
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
                Vector3 direction;

                if (Physics.Raycast(ray, out hit))
                {
                    direction = (hit.point - barrelExit.position).normalized; //will go toward the target
                }
                else
                {
                    direction = ray.direction; //will just shoot forward; barrelExit.forward should work too
                }
                Quaternion rot = Quaternion.FromToRotation(projectile.transform.up, direction);

                GameObject proj = (GameObject)Instantiate(projectile, barrelExit.position, rot);
                proj.GetComponent<Projectile>().Shoot(direction, damage, muzzleVelocity);
                ammoInClip -= ammoPerShot;

                this.audio.PlayOneShot(shootingSound);

                return true;
            }
            else
            {
                this.audio.PlayOneShot(emptySound);
                if(this.extraAmmo > 0)
                    StartCoroutine(this.Reload());
                return false;
            }
        }
        else
        {
            return true;
        }
    }

    public virtual void HoldTrigger()
    {

    }

    public virtual IEnumerator Reload()
    {
        //Play animation, do this stuff once the animation is finished
        reloading = true;
        yield return new WaitForSeconds(1); //until animation callback is implemented

        int roundsNeeded = clipSize - ammoInClip;
        Debug.Log("Rounds needed: " + roundsNeeded);

        int roundsToReload = (extraAmmo > roundsNeeded) ? roundsNeeded : extraAmmo;
        Debug.Log("Rounds to reload: " + roundsToReload);

        extraAmmo -= roundsToReload;
        ammoInClip += roundsToReload;
        reloading = false;
    }
}
