using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Stats for the character (currently just the player)
/// </summary>
public class CharacterStats : MonoBehaviour
{
    public int notes; //health
    public List<GameObject> vulnerableEnemies;

    private bool attacking;
    public AudioClip deathClip;
    private Animator anim;
    private PlayerMovement playerMovement;
    private HashIDs hash;
    Camera mainCam;

    void Awake()
    {
        notes = 0;
        mainCam = Camera.main;
        vulnerableEnemies = new List<GameObject>();
    }
    void Update()
    {

    }

    public void Attack(GameObject target)
    {
        target.GetComponent<EnemyMovementBasic>().TakeDamage();
    }
}
