using UnityEngine;
using System.Collections;

public enum AmmoType { Pistol, Shotgun, SMG, AssaultRifle, StunGun, Laser };
public enum AmmoAttribute { Normal, ArmorPiercing, HollowPoint, LowVelocity, Polonium, Incendiary, Cryo, Electric };

public class Projectile : MonoBehaviour
{
    public float damageModifier = 1;
    public AmmoType ammoType;
    public AmmoAttribute attribute;
    protected int baseDamage;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public virtual void Shoot(Vector3 direction, int damage, float velocity)
    {
        if (ammoType != AmmoType.Laser)
        {
            this.rigidbody.velocity = direction * velocity;
            this.baseDamage = damage;
        }
        else //Lasers are instantaneous
        {

        }
    }
    public virtual void Shoot(Vector3 start, Vector3 end, EnemyStats target, int damage, float renderTime)
    {
 
    }

    void OnCollisionEnter(Collision col)
    {
        //Debug.Log("Collision with " + col.collider.name + "at " + col.contacts[0].point);
        if (col.collider.tag == Tags.ENEMY)
        {
            col.collider.GetComponent<EnemyStats>().TakeDamage((int)(baseDamage * damageModifier));
        }
        Destroy(this.gameObject);
    }

    public void ShootLaser()
    {
 
    }
    IEnumerator shootLaser()
    {
        yield return new WaitForSeconds(.1f);
    }
}
