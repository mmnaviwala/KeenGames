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

    private EnemyAI AI;

    void Start()
    {
        AI = this.GetComponent<EnemyAI>();
        lastAttackTime = Time.time;
    }

    public void Update()
    {
        if (isDead)
        {
            ((PlayerStats)(AI.currentEnemy)).closeQuarterEnemies.Remove(this);
            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// Deals damage to the enemy.
    /// </summary>
    /// <param name="damage"></param>
    public override void TakeDamage(int damage)
    {
        this.health -= damage;
        if (this.health <= 0)
        {
            this.isDead = true;
            Destroy(this.gameObject);
        }
    }
    public override void TakeDamage(int damage, CharacterStats source)
    {
        AI.currentEnemy = source;
        this.health -= damage;
        if (this.health <= 0)
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
        isDead = true;
        Destroy(this.gameObject);
    }

}
