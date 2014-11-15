using UnityEngine;
using System.Collections;

[AddComponentMenu("Scripts/Items/Weapons/Gun")]
public class Gun : Weapon
{
    #region Variables
    public LayerMask ignorePlayer;
    public Transform barrelExit;
    public GameObject projectile;   //(Optional?) projectile/ammo type
    public Animation attackAnimation, reloadAnimation;
    public WeaponMod[] mods;        //May need to be replaced with sub-transform Mod Slots on each gun model.
    public bool isAuto;
    public bool infiniteAmmo;

    public AudioClip shootingSound, emptySound, reloadSound;
	public float muzzleVelocity;

    public int durability, weight;
    public float
                 accuracy, //0 = perfect accuracy for now.
				 range,//Range projectile will travel, or range where reticle will turn red.
				 npcRange; //range NPCs will use for this weapon

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

                this.GetComponent<AudioSource>().PlayOneShot(shootingSound);

                return true;
            }
            else
            {
                this.GetComponent<AudioSource>().PlayOneShot(emptySound);
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

    public override IEnumerator Reload()
    {
        if (extraAmmo > 0 && this.ammoInClip < clipSize)
        {
            //Play animation, do this stuff once the animation is finished
            reloading = true;
            this.GetComponent<AudioSource>().PlayOneShot(reloadSound);

            yield return new WaitForSeconds(reloadSound.length); //until animation callback is implemented
            int roundsNeeded = clipSize - ammoInClip;

            int roundsToReload = (extraAmmo > roundsNeeded) ? roundsNeeded : extraAmmo;

            extraAmmo -= roundsToReload;
            ammoInClip += roundsToReload;
            reloading = false;
        }
    }
}
