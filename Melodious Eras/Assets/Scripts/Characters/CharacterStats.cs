using UnityEngine;
using System.Collections;

public enum Faction { Enemy1, Enemy2, Neutral, Player }

[AddComponentMenu("Scripts/Characters/Character Stats")]
public class CharacterStats : MonoBehaviour 
{
    public Faction faction;
    public SecurityArea currentSecArea;
    public int health = 100, maxHealth = 100;
    public float stamina = 100, maxStamina = 100;
    [SerializeField]
    protected float adrenalineMultiplier = 1;
    public bool isDead = false;
    public int meleeDamage = 10; //damage modifier could be calculated by melee weapons

    public Weapon equippedWeapon;
	public Weapon sidearm;
	public Weapon largeWeapon;
	public Weapon thrown;
	public Weapon explosive;
    public Transform leftHand, rightHand; //right hand holds gun; left hand could hold flashlight/energy shield/sword/secondary gun/etc
	public Inventory inventory = new Inventory(15);
	public Inventory tempInventory = new Inventory(); //for level-specific items like keys
	public Transform lookatTarget;

	// Use this for initialization
    void Start() { }
	// Update is called once per frame
    void Update() { }

    public virtual void TakeDamage(int damage) { }
    public virtual void TakeDamage(int damage, CharacterStats source) { }
    public virtual void TakeDamage(bool instantKill) { }
    public virtual void TakeDamageThroughArmor(int damage) { }
    public virtual void TakeDamageThroughArmor(int damage, CharacterStats source) { }
    public virtual void TakeDamageThroughArmor(bool instantKill) { }
    protected virtual void Die() { }

    public virtual void Attack(CharacterStats target) { }
    public virtual void Attack(CharacterStats target, CharacterStats attacker, float angle) { }

    public virtual void PickUp(Item item) { }
    public virtual void SwitchWeapon(WeaponClass slot) { }
    public virtual void HolsterWeapon() { }

    public virtual void ReduceStamina(float value)
    {
        this.stamina -= value / adrenalineMultiplier; //higher adrenaline = slower stamina reduction
    }
    public virtual void ReduceStaminaAbsolute(float value)
    {
        this.stamina -= value;
    }
    public void RegainStamina(float value)
    {
        if (this.stamina < this.maxStamina)
        {
            this.stamina += value * adrenalineMultiplier; //higher adrenaline = faster stamina regeneration
            if (this.stamina > 100)
                this.stamina = 100;
        }
    }
}
