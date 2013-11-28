using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Stats for the character (currently just the player)
/// </summary>
public class CharacterStats : MonoBehaviour
{
    public int health = 100;
    public List<EnemyStats> nearbyEnemies;
    public int threshold = 5;

    private bool attacking;
    public AudioClip deathClip;
    private Animator anim;
    private PlayerMovement playerMovement;
    private HashIDs hash;
    private HUD_Stealth hud;
    private CameraMovement3D mainCam;
    private bool inMeleeRange = false;
    private float meleeHeldDown = 0;


    void Awake()
    {
        nearbyEnemies = new List<EnemyStats>();
    }
    void Start()
    {
        hud = this.GetComponent<HUD_Stealth>();
        mainCam = Camera.main.GetComponent<CameraMovement3D>();
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
            if (Input.GetButtonDown(InputType.MELEE))
            {
                //Determining which angle the player's character is facing (the one they want to attack)
                EnemyStats nearestEnemy = nearbyEnemies[0]; //will actually with the smallest angle away from the player's facing direction
                float lowestAngle = 180;
                foreach (EnemyStats enemy in nearbyEnemies)
                {
                    float distance = Vector3.Distance(this.transform.position, enemy.transform.position);

                    Vector3 relEnemyPos = enemy.transform.position - this.transform.position;
                    float angle = Vector3.Angle(relEnemyPos, this.transform.forward);
                    if (angle < lowestAngle)
                    {
                        lowestAngle = angle;
                        nearestEnemy = enemy;
                    }
                }
                //If the player is behind the target (60-degree area)
                Vector3 relPlayerPos = this.transform.position - nearestEnemy.transform.position;
                float enemyAngle = Vector3.Angle(nearestEnemy.transform.forward, new Vector3(relPlayerPos.x, 0, relPlayerPos.z));
                Debug.Log("Enemy Angle: " + enemyAngle);
                Debug.Log("Enemy Forward: " + nearestEnemy.transform.forward +
                          "\nPlayer Relative Position: " + (this.transform.position - nearestEnemy.transform.position));
                if ( enemyAngle > 150)
                {
                    this.Attack(this, nearestEnemy);
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.isTrigger && other.tag == Tags.ENEMY)
        {
            nearbyEnemies.Add(other.GetComponent<EnemyStats>());
            Debug.Log("Nearby Enemy count: " + nearbyEnemies.Count);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.isTrigger && other.tag == Tags.ENEMY)
        {
            nearbyEnemies.Remove(other.GetComponent<EnemyStats>());
            Debug.Log("Nearby Enemy count: " + nearbyEnemies.Count);
        }
    }

    /// <summary>
    /// Instantly kills the target
    /// </summary>
    /// <param name="target"></param>
    public void Attack(CharacterStats attacker, EnemyStats target)
    {
        target.TakeDamage(true);
    }
    /// <summary>
    /// Used when picking up a note.
    /// </summary>
    /// <param name="noteColor"></param>
    /// <param name="value"></param>
    /*public void AddNotes(EnemyColor noteColor, int value)
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
    }*/

    /// <summary>
    /// Rewards player for killing an enemy.
    /// </summary>
    /// <param name="enemyColor"></param>
    /// <param name="whiteValue"></param>
    /*public void RewardNotes(EnemyColor enemyColor, int whiteValue, int colorValue)
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
    }*/
}
