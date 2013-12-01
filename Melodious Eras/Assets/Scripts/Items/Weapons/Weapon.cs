using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{
    protected SphereCollider soundSphere;
    protected Sound sound;
    public string weaponName;
    public GUIStyle hudStyle;
    void Start()
    {
    }

    void Update()
    {

    }

    public virtual bool Fire()
    {
        Debug.Log("Base trigger");
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
