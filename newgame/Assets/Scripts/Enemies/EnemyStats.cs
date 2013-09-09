using UnityEngine;
using System.Collections;

public class EnemyStats : MonoBehaviour 
{
    public int enemyColor;
    private enum EnemyColor{ Green = 0, Blue = 1, Red = 2, Purple = 3 }

	// Use this for initialization
	void Start () 
    {
        //GameObject.FindGameObjectWithTag(Tags.PLAYER).GetComponent<CharacterStats>().vulnerableEnemies.Add(this.gameObject);
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}
}
