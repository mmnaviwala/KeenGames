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
    public EnemyAI AI       { get { return ai; } }

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
		this.RegenerateHealth(Difficulty.enemyRegenWait, Difficulty.enemyRegenSpeed);
    }

    #region Damage taking and stat changes
    public override void TakeDamage(int damage, CharacterStats source, bool ignoreArmor = false)
    {
        if (ai.currentEnemy == null)
            ai.Notice(source);

        base.TakeDamage(damage, source, ignoreArmor);
    }
    public override void TakeDamage(bool instantKill)
    {
        if (this.isVulnerable)
            this.Die();
    }    


    /// <summary>
    /// <para>Processes the enemy's death. For this type of enemy, it disables their AI, then shrinks and disables their sight's sphere collider.</para>
    /// <para>Avoiding destroying any components, in case we want enemies to be capable of being revived at some point.</para>
    /// </summary>
    protected override void Die()
    {
		base.Die();
        this.ai.enabled = false;
        SphereCollider col = this.ai.sight.GetComponent<SphereCollider>();
        col.radius = 0;
        col.enabled = false;
    }

    #endregion
}
