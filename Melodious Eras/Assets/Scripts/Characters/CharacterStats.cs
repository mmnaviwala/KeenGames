using UnityEngine;
using System.Collections;

public class CharacterStats : MonoBehaviour 
{
    public int health = 100, maxHealth = 100;
    public bool isDead = false;
    public int meleeDamage = 10; //damage modifier could be calculated by melee weapons

    public Weapon equippedWeapon;
    public Weapon[] holsteredWeapons;
    public Transform leftHand, rightHand; //right hand holds gun; left hand could hold flashlight/energy shield/sword/secondary gun/etc

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public virtual void TakeDamage(int damage)
    {
 
    }

    public virtual void TakeDamage(int damage, CharacterStats source)
    {
 
    }

    public virtual void TakeDamage(bool instantKill)
    {
 
    }

    public virtual void Attack(CharacterStats target)
    {
 
    }

    public virtual void Attack(CharacterStats target, CharacterStats attacker, float angle)
    {

    }
}
