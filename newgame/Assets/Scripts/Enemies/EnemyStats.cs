using UnityEngine;
using System.Collections;

public class EnemyStats : MonoBehaviour 
{
    public int pointValue = 1,
               damageValue = 2;
    public enum EnemyColor{ Green = 0, Blue = 1, Red = 2, Purple = 3, White = 4}
    public EnemyColor enemyColor;

}
