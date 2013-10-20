using UnityEngine;
using System.Collections;

public enum EnemyColor { Green = 0, Blue = 1, Red = 2, Purple = 3, White = 4 }
public class EnemyStats : MonoBehaviour 
{
    public int pointValue = 1,
               colorValue = 0,
               damageValue = 2;
    public bool isVulnerable = false;
    public bool isDead = false;
    public EnemyColor enemyColor;

    /// <summary>
    /// Instantly kills this enemy.
    /// </summary>
    public void TakeDamage()
    {
        if (this.isVulnerable)
        {
            isDead = true;
            GameObject.FindGameObjectWithTag(Tags.PLAYER).GetComponent<CharacterStats>().RewardNotes(enemyColor, pointValue, colorValue);
        }
    }
}
