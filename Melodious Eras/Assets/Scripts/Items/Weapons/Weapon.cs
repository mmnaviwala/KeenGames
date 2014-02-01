using UnityEngine;
using System.Collections;

[AddComponentMenu("Scripts/Items/Weapons/Weapon")]
public class Weapon : Item
{
    protected SphereCollider soundSphere;
    protected Sound sound;
    public string weaponName;
    public GUIStyle hudStyle;

	
	public int ammoInClip, extraAmmo, maxAmmo, clipSize, ammoPerShot,
	damage;

    void Start()
    {
    }

    void Update()
    {

    }

    /// <summary>
    /// Used by the player. Fires where the crosshair is centered.
    /// </summary>
    /// <returns></returns>
    public virtual bool Fire()
    {
        Debug.Log("Base trigger");
        return false;
    }

    /// <summary>
    /// Generally used by enemies. Attacks the target.
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
    public virtual bool Fire(CharacterStats target)
    {
        return false;
    }

    public virtual void Initialize()
    {
        //soundSphere = this.GetComponent<SphereCollider>();
        //soundSphere.enabled = false;
        sound = this.GetComponent<Sound>();
    }

    /// <summary>
    /// For melee and instantaneous projectiles
    /// </summary>
    /// <param name="enemy"></param>
    /// <param name="damage"></param>
    public virtual void Damage(EnemyStats enemy, int damage)
    {
        
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
