using UnityEngine;
using System.Collections;

public enum EnemyColor { Green = 0, Blue = 1, Red = 2, Purple = 3, White = 4 }
public class EnemyStats : MonoBehaviour
{
    public bool isVulnerable = false;
    public bool isDead = false;
    public int health = 100, maxHealth = 100;

    private EnemyAI AI;

    void Start()
    {
        AI = this.GetComponent<EnemyAI>();
    }

    public void Update()
    {
        if (isDead)
        {
            AI.playerStats.nearbyEnemies.Remove(this);
            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// Deals damage to the enemy.
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(int damage)
    {
        this.health -= damage;
        if (this.health <= 0)
        {
            this.isDead = true;
        }
    }
    /// <summary>
    /// Instantly kills this enemy.
    /// </summary>
    public void TakeDamage(bool instantKill)
    {
        isDead = true;
    }

}
