using UnityEngine;
using System.Collections;

public enum WeaponClass {SmallArms, LargeArms, Melee, Thrown};
[AddComponentMenu("Scripts/Items/Weapons/Weapon")]
public class Weapon : Equipment
{
	public Vector3 wieldPosition;
	public Vector3 wieldRotation;

    protected SphereCollider soundSphere;
    protected Sound sound;
    public string weaponName;
    public GUIStyle hudStyle;
	public WeaponClass _class;
	
	public int ammoInClip, extraAmmo, maxAmmo, clipSize, ammoPerShot,
	damage;
    public float fireRate;

    void Start()
    {
    }

    void Update()
    {

    }


    public virtual void Initialize()
    {
        //soundSphere = this.GetComponent<SphereCollider>();
        //soundSphere.enabled = false;
        sound = this.GetComponent<Sound>();
    }

    /// <summary>
    /// Used by the player. Fires where the crosshair is centered.
    /// </summary>
    /// <returns></returns>
    public virtual bool Fire()
    {
        return false;
    }

    /// <summary>
    /// Meant for use by enemies. Attacks the target.
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
    public virtual bool Fire(CharacterStats target)
    {
        return false;
    }
    /// <summary>
    /// Also meant for enemies. Shoots at this position
    /// </summary>
    /// <param name="targetPos"></param>
    /// <returns></returns>
	public virtual bool Fire(Vector3 targetPos) {return false;}


    /// <summary>
    /// For melee and instantaneous projectiles
    /// </summary>
    /// <param name="enemy"></param>
    /// <param name="damage"></param>
    public virtual void Damage(CharacterStats enemy, int damage)
    {
        
    }

	public virtual void Equip(CharacterStats wielder, Transform rightHand)
	{
        this.wielder = wielder;
		this.transform.parent = rightHand;
		this.transform.localPosition = wieldPosition;
		this.transform.localEulerAngles = wieldRotation;
	}

    public virtual IEnumerator Reload()
    {
        return null;
    }

    /// <summary>
    /// String used in HUD.
    /// </summary>
    /// <returns></returns>
    public virtual string HudString()
    {
        return weaponName;
    }
}
