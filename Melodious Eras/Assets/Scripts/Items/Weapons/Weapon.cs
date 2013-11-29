using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{
    #region Variables
    public Projectile projectile;   //(Optional?) projectile/ammo type
    public Animation attackAnimation;
    public WeaponMod[] mods;        //May need to be replaced with sub-transform Mod Slots on each gun model.

    public AudioClip shootingSound, emptySound;

    public int ammoCapacity, ammoInClip, totalAmmo, maxAmmo, clipSize, ammoPerShot,
               damage;
    public int durability, weight;
    public float fireRate, //Time between shots.
                 accuracy, //0 = perfect accuracy for now.
                 range;    //Range projectile will travel, or range where reticle will turn red.

    private float shotFiredTime, nextShotTime;
    #endregion

    // Use this for initialization
	void Start () 
    {
        shotFiredTime = nextShotTime = Time.time;
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
    public virtual int PullTrigger()
    {
        if (Time.time > nextShotTime)
        {
            shotFiredTime = Time.time;
            nextShotTime = shotFiredTime + fireRate;
            Debug.Log("Shot fired at " + shotFiredTime);
        }
        else
        {
            Debug.Log("Can't fire yet");
        }
        return 0;
    }
}
