using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{
    #region Variables
    public Transform barrelExit;
    public GameObject projectile;   //(Optional?) projectile/ammo type
    public Animation attackAnimation;
    public WeaponMod[] mods;        //May need to be replaced with sub-transform Mod Slots on each gun model.

    public AudioClip shootingSound, emptySound;

    public int ammoCapacity, ammoInClip, totalAmmo, maxAmmo, clipSize, ammoPerShot,
               damage;
    public int durability, weight;
    public float fireRate, //Time between shots.
                 accuracy, //0 = perfect accuracy for now.
                 range;    //Range projectile will travel, or range where reticle will turn red.

    protected float shotFiredTime, nextShotTime;

    protected Camera mainCam;
    protected Vector3 centerScreen;
    #endregion

    // Use this for initialization
	void Start () 
    {
        if(barrelExit == null)
            barrelExit = this.transform.FindChild("barrel_exit");
        shotFiredTime = nextShotTime = Time.time;
        mainCam = Camera.main;
        Debug.Log(barrelExit.name);
        centerScreen = new Vector3(mainCam.pixelWidth / 2, mainCam.pixelHeight / 2, mainCam.nearClipPlane);
	}
	
	// Update is called once per frame
	void Update () 
    {
	    
	}

    /// <summary>
    /// Called when player Shoots. Possibly returns the damage dealt as an int?
    /// Alternatively, Instantiate a projectile to shoot towards an enemy.
    /// </summary>
    /// <returns></returns>
    public virtual void PullTrigger()
    {
        if (Time.time > nextShotTime)
        {
            shotFiredTime = Time.time;
            nextShotTime = shotFiredTime + fireRate;

            GameObject proj = (GameObject)Instantiate(projectile, barrelExit.position, mainCam.transform.rotation);
            proj.GetComponent<Projectile>().Propel(mainCam.transform.eulerAngles.normalized, 100);
            Debug.Log(proj.transform.eulerAngles);
            Debug.Log("Shot fired at " + shotFiredTime + "\nToward " + mainCam.ScreenToWorldPoint(centerScreen));
        }
        else
        {
            Debug.Log("Can't fire yet");
        }
    }

    public virtual void HoldTrigger()
    {
 
    }
}
