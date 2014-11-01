﻿using UnityEngine;
using System.Collections;

public enum Faction { Enemy1, Enemy2, Neutral, Player }

[AddComponentMenu("Scripts/Characters/Character Stats")]
public class CharacterStats : MonoBehaviour
{
    protected int baseLayer, woundedLayer, aimLayer;

    public Faction faction;
    public SecurityArea currentSecArea;
    public Suit suit;
    public Weapon equippedWeapon;
    public Weapon sidearm;
    public Weapon largeWeapon;
    public Weapon thrown;
    public Weapon explosive;
    public Transform leftHand, rightHand; //right hand holds gun; left hand could hold flashlight/energy shield/sword/secondary gun/etc
    public Inventory inventory = new Inventory(15);
    public Inventory tempInventory = new Inventory(); //for level-specific items like keys
    public Transform lookatTarget;
    protected Animator anim;

    [SerializeField]
    protected float _health = 100;
    [SerializeField]
    protected float _maxHealth = 100;
    [SerializeField]
    protected float _stamina = 100;
    [SerializeField]
    protected float _maxStamina = 100;
    [SerializeField]
    protected float _adrenalineMultiplier = 1;
    [SerializeField][Range(0,1)]
    protected float _resilience = 1; //multiplier for Wounded Layer blending. 0 = no Wounded animation at all
    protected bool _isDead = false;

    protected float lastHitTakenTime = 0;
    protected float regenWaitModifier = 1; //the lower this character's health, the longer it takes before they start regenerating
    

    [SerializeField]
    protected int _meleeDamage = 10; //damage modifier could be calculated by melee weapons


    private YieldInstruction _eof = new WaitForEndOfFrame();
    protected bool _exhausted = false;
    


    #region accessors & mutators
    public bool exhausted   { get { return _exhausted;  } }
    public float health     { get { return _health; } }
    public float maxHealth  { get { return _maxHealth; } }
    public float stamina    { get { return _stamina;    } }
    public float maxStamina { get { return _maxStamina; } }
    public bool isDead      { get { return _isDead;     } }
    public float adrenalineMultiplier { get { return _adrenalineMultiplier; } set { _adrenalineMultiplier = value; } }
    #endregion

    // Use this for initialization
    void Start() {
        anim = this.GetComponent<Animator>();
        if (equippedWeapon)
            equippedWeapon.Equip(this, rightHand);
    }
	// Update is called once per frame
    void Update() { }

    /// <summary>
    /// Deals damage to this character.
    /// </summary>
    public virtual void TakeDamage(int damage) 
    {
        lastHitTakenTime = Time.time;
        if (this.suit == null || this.suit.armor == 0)
        {
            this._health -= (_health > damage) ? damage : _health;
        }
        else
        {
            this.suit.armor -= damage;
            if (suit.armor < 0)
			{
				this.suit.armor = 0;
                int leftoverDamage = Mathf.Abs(suit.armor);
                this._health -= (_health > leftoverDamage) ? leftoverDamage : _health;
            }
        }

        if (health == 0)
            this.Die();
        else
            regenWaitModifier = (health > 50) ? 1 : Mathf.Sqrt(health / 50); //going with constant 50, rather than half of max health
    }
    /// <summary>
    /// Deals damage to this character, letting them know who hit them.
    /// </summary>
    public virtual void TakeDamage(int damage, CharacterStats source) 
    {
        TakeDamage(damage);
    }
    /// <summary>
    /// Instantly kills this character.
    /// </summary>
    public virtual void TakeDamage(bool instantKill) { }
    /// <summary>
    /// Deals damage to this character, ignoring any armor.
    /// </summary>
    public virtual void TakeDamageThroughArmor(int damage)
    {
        lastHitTakenTime = Time.time;
        this._health -= (_health > damage) ? damage : _health;
        if (this._health == 0)
            this.Die();
        else
            regenWaitModifier = (health > 50) ? 1 : Mathf.Sqrt(health / 50);
    }
    /// <summary>
    /// Deals damage to this character, ignoring any armor and telling them who hit them.
    /// </summary>
    public virtual void TakeDamageThroughArmor(int damage, CharacterStats source) { }
    /// <summary>
    /// Instantly kills this character, ignoring any protections
    /// </summary>
    public virtual void Kill() 
    { 
        this.Die(); 
    }
    protected virtual void Die() 
	{
		_isDead = true;
		this.anim.SetBool(HashIDs.dead_bool, true);
		this.anim.SetBool(HashIDs.aiming_bool, false);
	}
	
	public virtual void Attack(CharacterStats target) { }
    public virtual void Attack(CharacterStats target, CharacterStats attacker, float angle) { }

    public virtual void PickUp(Item item) { }
    public virtual void SwitchWeapon(WeaponClass slot) { }
    public virtual void HolsterWeapon() { }

    public virtual void ReduceStamina(float value)
    {
        this._stamina -= value / _adrenalineMultiplier; //higher adrenaline = slower stamina reduction
        if (!this._exhausted && this.stamina < 0)
        {
            this.StartCoroutine(BecomeExhausted(10));
        }
    }
    public virtual void ReduceStaminaAbsolute(float value)
    {
        this._stamina -= value;
    }
    public void RegainStamina(float value)
    {
        if (this.stamina < this.maxStamina)
        {
            this._stamina += value * _adrenalineMultiplier; //higher adrenaline = faster stamina regeneration
            if (this._stamina > 100)
                this._stamina = 100;
        }
    }
    /// <summary>
    /// Regenerates health at rate per second.
    /// </summary>
    public virtual void RegenerateHealth(float rate)
    {
 
    }
    /// <summary>
    /// Regenerates health each frame under certain conditions. Higher difficulty increases rate for enemies, reduces rate for player.
    /// </summary>
	protected void RegenerateHealth( float regen_wait, float regen_speed)
	{
		if (this._health < maxHealth && Time.time > lastHitTakenTime + regen_wait / regenWaitModifier)
		{
			_health += regen_speed * Time.deltaTime;
			if (_health > maxHealth)
				_health = maxHealth;
		}
	}
	
	
	/// <summary>
	/// <para>Formula to blend Base Layer with Wounded Layer based on current health and resilience.</para>
    /// <para>Single statement, should be inlined by compiler</para>
    /// <para>Blending won't go higher than 75%</para>
    /// </summary>
    protected void BlendWoundLayer()
    {
        anim.SetLayerWeight(woundedLayer, (Mathf.Min(0.5f, 1.0f - ((float)health / maxHealth)) * _resilience));
    }

    /// <summary>
    /// Marks the character as Exhausted until their stamina >= recoveryNeeded amount
    /// </summary>
    protected IEnumerator BecomeExhausted(float recoveryNeeded)
    {
        this._stamina = 0;
        this._exhausted = true;
        float adjustedMinimum = recoveryNeeded / _adrenalineMultiplier;
        while (this.stamina < adjustedMinimum)
            yield return _eof;

        this._exhausted = false;
    }

    public virtual int FindAnimLayer(string name)
    {
        for (int a = 0; a < anim.layerCount; a++)
            if (anim.GetLayerName(a) == name)
                return a;
        return -1;
    }
    protected virtual void SetAnimLayers()
    {
        this.baseLayer = FindAnimLayer("Base Layer");
        this.woundedLayer = FindAnimLayer("Wounded");
        this.aimLayer = FindAnimLayer("Shooting");
    }
}
