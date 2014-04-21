using UnityEngine;
using System.Collections;

public enum Faction { Enemy1, Enemy2, Neutral, Player }

[AddComponentMenu("Scripts/Characters/Character Stats")]
public class CharacterStats : MonoBehaviour 
{
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

    [SerializeField]
    protected int _health = 100;
    [SerializeField]
    protected int _maxHealth = 100;
    [SerializeField]
    protected float _stamina = 100;
    [SerializeField]
    protected float _maxStamina = 100;
    [SerializeField]
    protected float _adrenalineMultiplier = 1;
    protected bool _isDead = false;

    [SerializeField]
    protected int _meleeDamage = 10; //damage modifier could be calculated by melee weapons


    private YieldInstruction _eof = new WaitForEndOfFrame();
    protected bool _exhausted = false;
    


    #region accessors & mutators
    public bool exhausted { get { return _exhausted; } }
    public int health { get { return _health; } }
    public int maxHealth { get { return _maxHealth; } }
    public float stamina { get { return _stamina; } }
    public float maxStamina { get { return _maxStamina; } }
    public bool isDead { get { return _isDead; } }
    #endregion

    // Use this for initialization
    void Start() { }
	// Update is called once per frame
    void Update() { }

    /// <summary>
    /// Deals damage to this character.
    /// </summary>
    /// <param name="damage"></param>
    public virtual void TakeDamage(int damage) 
    {
        if (this.suit == null || this.suit.armor == 0)
        {
            this._health -= (_health > damage) ? damage : _health;
        }
        else
        {
            this.suit.armor -= damage;
            if (suit.armor < 0)
            {
                int leftoverDamage = Mathf.Abs(suit.armor);
                this._health -= (_health > leftoverDamage) ? leftoverDamage : _health;
                this.suit.armor = 0;
            }
        }

        if (health == 0)
            this.Die();
    }
    /// <summary>
    /// Deals damage to this character, letting them know who hit them.
    /// </summary>
    /// <param name="damage"></param>
    /// <param name="source"></param>
    public virtual void TakeDamage(int damage, CharacterStats source) { }
    /// <summary>
    /// Instantly kills this character.
    /// </summary>
    public virtual void TakeDamage(bool instantKill) { }
    /// <summary>
    /// Deals damage to this character, ignoring any armor.
    /// </summary>
    /// <param name="damage"></param>
    public virtual void TakeDamageThroughArmor(int damage)
    {
        this._health -= (_health > damage) ? damage : _health;
        if (this._health == 0)
            this.Die();
    }
    /// <summary>
    /// Deals damage to this character, ignoring any armor and telling them who hit them.
    /// </summary>
    /// <param name="damage"></param>
    /// <param name="source"></param>
    public virtual void TakeDamageThroughArmor(int damage, CharacterStats source) { }
    /// <summary>
    /// Instantly kills this character, ignoring any protections
    /// </summary>
    /// <param name="instantKill"></param>
    public virtual void Kill() 
    { 
        this.Die(); 
    }
    protected virtual void Die() { }

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
    /// Marks the character as Exhausted until their stamina >= recoveryNeeded amount
    /// </summary>
    /// <param name="recoveryNeeded"></param>
    /// <returns></returns>
    protected IEnumerator BecomeExhausted(float recoveryNeeded)
    {
        this._stamina = 0;
        this._exhausted = true;
        float adjustedMinimum = recoveryNeeded / _adrenalineMultiplier;
        while (this.stamina < adjustedMinimum)
            yield return _eof;

        this._exhausted = false;
    }
}
