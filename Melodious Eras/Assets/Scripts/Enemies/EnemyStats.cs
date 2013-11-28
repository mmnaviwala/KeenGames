using UnityEngine;
using System.Collections;

public enum EnemyColor { Green = 0, Blue = 1, Red = 2, Purple = 3, White = 4 }
public class EnemyStats : MonoBehaviour 
{
    public bool isVulnerable = false;
    public bool isDead = false;

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
    /// Instantly kills this enemy.
    /// </summary>
    public void TakeDamage(bool instantKill)
    {
        isDead = true;
    }
}
