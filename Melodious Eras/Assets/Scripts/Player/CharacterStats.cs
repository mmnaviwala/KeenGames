using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Stats for the character (currently just the player)
/// </summary>
public class CharacterStats : MonoBehaviour
{
    public int health = 100;
    public int notes, greenNotes, blueNotes, redNotes, purpleNotes; //points
    public List<EnemyStats> nearbyEnemies;
    public int threshold = 5;

    private bool attacking;
    public AudioClip deathClip;
    private Animator anim;
    private PlayerMovement playerMovement;
    private HashIDs hash;
    private bool inMeleeRange = false;
    private float meleeHeldDown = 0;

    enum InstantKill { NONE = 0, GREEN = 1, BLUE = 2, RED = 3, PURPLE = 4 };
    //int specialAttack = 0;
    EnemyColor specialAttack;
    EnemyColor[] specialAttacks;

    void Awake()
    {
        notes = 0;
        greenNotes = 0;
        blueNotes = 0;
        redNotes = 0;
        purpleNotes = 0;

        nearbyEnemies = new List<EnemyStats>();
        specialAttacks = new EnemyColor[5];
    }

    void Update()
    {
        //-------------------------------------------
        if (Input.GetButton(InputType.MELEE))
        {
            meleeHeldDown += Time.deltaTime;
            if (meleeHeldDown > 2)
            {
                meleeHeldDown = 0;
                Debug.Log("Holding Melee Button");
            }
        }
        else if (Input.GetButtonUp(InputType.MELEE))
        {
            meleeHeldDown = 0;
            Debug.Log("Melee Button up");
        }
        //-------------------------------------------


        if(nearbyEnemies.Count > 0)
        {
            EnemyStats nearestEnemy = nearbyEnemies[0]; //will actually with the smallest angle away from the player's facing direction
            

            if (Input.GetButtonDown(InputType.MELEE))
            {
                foreach (EnemyStats enemy in nearbyEnemies)
                {
                    float distance = Vector3.Distance(this.transform.position, enemy.transform.position);
                    float angle = Vector3.Angle(enemy.transform.forward, this.transform.position);
                    Debug.Log("Angle: " + angle);
                    if (distance < 2 && angle > 150) //leaving a 60-degree window for sneak attack
                    {

                    }
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == Tags.ENEMY)
        {
            nearbyEnemies.Add(other.GetComponent<EnemyStats>());
            Debug.Log("Nearby Enemy count: " + nearbyEnemies.Count);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == Tags.ENEMY)
        {
            nearbyEnemies.Remove(other.GetComponent<EnemyStats>());
            Debug.Log("Nearby Enemy count: " + nearbyEnemies.Count);
        }
    }

    /// <summary>
    /// Instantly kills the target
    /// </summary>
    /// <param name="target"></param>
    public void Attack(EnemyStats target)
    {
        target.TakeDamage();
    }
    /// <summary>
    /// Used when picking up a note.
    /// </summary>
    /// <param name="noteColor"></param>
    /// <param name="value"></param>
    public void AddNotes(EnemyColor noteColor, int value)
    {
        switch (noteColor)
        {
            case EnemyColor.Green:
                greenNotes += value;
                if (greenNotes >= threshold)
                {
                    greenNotes -= threshold;
                    specialAttack = EnemyColor.Green;
                }
                break;
            case EnemyColor.Blue:
                blueNotes += value;
                if (blueNotes >= threshold)
                {
                    blueNotes -= threshold;
                    specialAttack = EnemyColor.Blue;
                } 
                break;
            case EnemyColor.Red:
                redNotes += value;
                if (redNotes >= threshold)
                {
                    redNotes -= threshold;
                    specialAttack = EnemyColor.Red;
                }
                break;
            case EnemyColor.Purple:
                purpleNotes += value;
                if (purpleNotes >= threshold)
                {
                    purpleNotes -= threshold;
                    specialAttack = EnemyColor.Purple;
                }
                break;
            case EnemyColor.White:
                notes += value;
                break;
        }
    }

    /// <summary>
    /// Rewards player for killing an enemy.
    /// </summary>
    /// <param name="enemyColor"></param>
    /// <param name="whiteValue"></param>
    public void RewardNotes(EnemyColor enemyColor, int whiteValue, int colorValue)
    {
        switch (enemyColor)
        {
            case EnemyColor.Green:
                greenNotes += colorValue;
                if (greenNotes >= threshold)
                {
                    greenNotes -= threshold;
                    specialAttack = EnemyColor.Green;
                }
                break;
            case EnemyColor.Blue:
                blueNotes += colorValue;
                if (blueNotes >= threshold)
                {
                    blueNotes -= threshold;
                    specialAttack = EnemyColor.Blue;
                }
                break;
            case EnemyColor.Red:
                redNotes += colorValue;
                if (redNotes >= threshold)
                {
                    redNotes -= threshold;
                    specialAttack = EnemyColor.Red;
                }
                break;
            case EnemyColor.Purple:
                purpleNotes += colorValue;
                if (purpleNotes >= threshold)
                {
                    purpleNotes -= threshold;
                    specialAttack = EnemyColor.Purple;
                }
                break;
            case EnemyColor.White:
                break;
        }
        notes += whiteValue;
    }
    public void RewardNotes(int value)
    {
        this.notes += value;
    }
}
