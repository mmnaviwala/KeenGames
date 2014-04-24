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

        if (this.equippedWeapon is Gun)
            ((Gun)equippedWeapon).infiniteAmmo = true;
    }

    /*public void Update()
    {
        if (isDead)
        {
            ((PlayerStats)(ai.currentEnemy))._closeQuarterEnemies.charactersInRange.Remove(this);
            Destroy(this.gameObject);
        }
    }*/

    #region Damage taking and stat changes
    public override void TakeDamage(int damage, CharacterStats source)
    {
        ai.currentEnemy = source;
        if (suit != null && suit.armor > 0)
            suit.armor -= damage;
        else
            this._health -= damage;

        if (this.isVulnerable && this.health <= 0)
        {
            this.Die();
        }
    }
    public override void TakeDamage(bool instantKill)
    {
        if (this.isVulnerable)
        {
            this.Die();
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
        this._health -= damage;
        if (this.health <= 0)
        {
            this.Die();
        }

    }

    /// <summary>
    /// <para>Processes the enemy's death. For this type of enemy, it disables their AI, then shrinks and disables their sight's sphere collider.</para>
    /// <para>Avoiding destroying any components, in case we want enemies to be capable of being revived at some point.</para>
    /// </summary>
    protected override void Die()
    {
        Debug.Log(this.name + " Dying");
        _isDead = true;
        this.ai.Anim.SetBool(HashIDs.dead_bool, true);
        this.ai.enabled = false;
        SphereCollider col = this.ai.sight.GetComponent<SphereCollider>();
        col.radius = 0;
        col.enabled = false;
    }
    #endregion
}
