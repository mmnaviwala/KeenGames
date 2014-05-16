using UnityEngine;
using System.Collections;

public enum EnemyColor { Green = 0, Blue = 1, Red = 2, Purple = 3, White = 4 }

[AddComponentMenu("Scripts/Characters/Enemy Stats")]
public class EnemyStats : CharacterStats
{
    public bool isVulnerable = false;
    private bool attacking;
    public float meleeSpeed = .5f, shootSpeed = .5f;
    private float lastAttackTime;
    

    private EnemyAI ai;
    private EnemySight _sight;
    public EnemyAI AI       { get { return ai; }
                              set { ai = value; } }
    public EnemySight sight { get { return _sight; }
                              set { _sight = value; } }

    void Awake()
    {
        this.anim = this.GetComponent<Animator>();
        this.SetAnimLayers();
    }
    void Start()
    {
        ai = this.GetComponent<EnemyAI>();
        lastAttackTime = Time.time;
		this.currentSecArea = GameObject.FindGameObjectWithTag(Tags.GAME_CONTROLLER).GetComponent<SecurityArea>();

        if (this.equippedWeapon is Gun)
            ((Gun)equippedWeapon).infiniteAmmo = true;
    }

    public void Update()
    {
        this.RegenerateHealth();
    }

    #region Damage taking and stat changes
    public override void TakeDamage(int damage, CharacterStats source)
    {
        if(ai.currentEnemy == null)
            ai.currentEnemy = source;

        if (suit != null && suit.armor > 0)
            suit.armor -= damage;
        else
            this._health -= damage;

        if (this.isVulnerable && this.health <= 0)
            this.Die();
        else
        {
            regenWaitModifier = (health > 50) ? 1 : Mathf.Sqrt(health / 50);
        }
    }
    public override void TakeDamage(bool instantKill)
    {
        if (this.isVulnerable)
            this.Die();
    }
    /// <summary>
    /// Deals damage to invulnerable enemy. Use this to tell the enemy who just attacked it
    /// </summary>
    /// <param name="damage"></param>
    /// <param name="source"></param>
    public override void TakeDamageThroughArmor(int damage, CharacterStats source)
    {
        if(ai.currentEnemy == null)
            ai.currentEnemy = source;

        this._health -= damage;

        if (this.health <= 0)
            this.Die();
        else
            regenWaitModifier = Mathf.Max(1, Mathf.Lerp(0, 50, this.health)); //going with constant 50, rather than half of max health
    }

    protected override void RegenerateHealth()
    {
        if (this._health < maxHealth && Time.time > lastHitTakenTime + Difficulty.enemyRegenWait / regenWaitModifier)
        {
            _health += Difficulty.enemyRegenSpeed * Time.deltaTime;
            if (_health > maxHealth)
                _health = maxHealth;
        }
    }


    /// <summary>
    /// <para>Processes the enemy's death. For this type of enemy, it disables their AI, then shrinks and disables their sight's sphere collider.</para>
    /// <para>Avoiding destroying any components, in case we want enemies to be capable of being revived at some point.</para>
    /// </summary>
    protected override void Die()
    {
        _isDead = true;
        this.anim.SetBool(HashIDs.dead_bool, true);
        this.anim.SetBool(HashIDs.aiming_bool, false);
        this.ai.enabled = false;
        SphereCollider col = this.ai.sight.GetComponent<SphereCollider>();
        col.radius = 0;
        col.enabled = false;
    }

    #endregion
}
