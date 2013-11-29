using UnityEngine;
using System.Collections;

public enum AmmoType { Pistol, Shotgun, SMG, AssaultRifle, StunGun};
public enum AmmoAttribute { Normal, ArmorPiercing, HollowPoint, LowVelocity, Polonium, Incendiary, Cryo, Electric };

public class Projectile : MonoBehaviour 
{
    public float damageModifier = 1;
    public AmmoType ammoType;
    public AmmoAttribute attribute;

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    public void Propel(Vector3 direction, float force)
    {
        this.rigidbody.AddForce(direction * force * damageModifier, ForceMode.Force);
    }

    void OnCollisionEnter(Collision col)
    {
        Debug.Log(col.contacts[0].point);
    }
}
