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
    public EnemyAI AI { 
        get { return ai; }
        set { ai = value; }
    }

    void Start()
    {
        ai = this.GetComponent<EnemyAI>();
        lastAttackTime = Time.time;
		this.currentSecArea = GameObject.FindGameObjectWithTag(Tags.GAME_CONTROLLER).GetComponent<SecurityArea>();
    }

    public void Update()
    {
        if (isDead)
        {
            ((PlayerStats)(ai.currentEnemy))._closeQuarterEnemies.charactersInRange.Remove(this);
            Destroy(this.gameObject);
        }
    }

    #region Damage taking and stat changes
    /// <summary>
    /// Deals damage to the enemy.
    /// </summary>
    /// <param name="damage"></param>
    public override void TakeDamage(int damage)
    {
        this.health -= damage;
        if (this.isVulnerable && this.health <= 0)
        {
            this.isDead = true;
            Destroy(this.gameObject);
        }
    }
    public override void TakeDamage(int damage, CharacterStats source)
    {
        ai.currentEnemy = source;
        this.health -= damage;
        if (this.isVulnerable && this.health <= 0)
        {
            this.isDead = true;
            Destroy(this.gameObject);
        }
 
    }
    /// <summary>
    /// Instantly kills this enemy.
    /// </summary>
    public override void TakeDamage(bool instantKill)
    {
        if (this.isVulnerable)
        {
            isDead = true;
            Destroy(this.gameObject);
        }
    }
    /// <summary>
    /// Deals damage to invulnerable enemy.
    /// </summary>
    /// <param name="damage"></param>
    public override void TakeDamageThroughArmor(int damage)
    {
        this.health -= damage;
        if (this.health <= 0)
        {
            this.isDead = true;
            Destroy(this.gameObject);
        }
    }
    /// <summary>
    /// Deals damage to invulnerable enemy. Use this to tell the enemy who just attacked it
    /// </summary>
    /// <param name="damage"></param>
    /// <param name="source"></param>
    public override void TakeDamageThroughArmor(int damage, CharacterStats source)
    {
        ai.currentEnemy = source;
        this.health -= damage;
        if (this.health <= 0)
        {
            this.isDead = true;
            Destroy(this.gameObject);
        }

    }
    /// <summary>
    /// Instantly kills this enemy, even if invulnerable.
    /// </summary>
    public override void TakeDamageThroughArmor(bool instantKill)
    {
        isDead = true;
        Destroy(this.gameObject);
    }
    #endregion
}
