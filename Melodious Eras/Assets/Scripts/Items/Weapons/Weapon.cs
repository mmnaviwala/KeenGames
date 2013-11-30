using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{
    protected SphereCollider soundSphere;
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
        soundSphere = this.GetComponent<SphereCollider>();
        soundSphere.enabled = false;
    }

    /// <summary>
    /// For melee and instantaneous projectiles
    /// </summary>
    /// <param name="enemy"></param>
    /// <param name="damage"></param>
    public virtual void Damage(EnemyStats enemy, int damage)
    {
        
    }
}
