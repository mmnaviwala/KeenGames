﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Stats for the character (currently just the player)
/// </summary>
public class CharacterStats : MonoBehaviour
{
    public int health = 100, maxHealth = 100;
    public bool isDead = false;
    public List<EnemyStats> closeQuarterEnemies, //will be available for melee attacks
                            nearbyEnemies;       //will be in range to hear
    public int threshold = 5;
    public int meleeDamage = 10; //damage modifier could be calculated by melee weapons

    public Suit suit;
    public Flashlight flashlight;
    public Weapon equippedWeapon;
    public Weapon[] holsteredWeapons;
    public Transform leftHand, rightHand; //right hand holds gun; left hand could hold flashlight/energy shield/sword/secondary gun/etc

    public AudioClip deathClip, meleeClip;
    private Animator anim;
    private PlayerMovement playerMovement;
    private HashIDs hash;
    private HUD_Stealth hud;
    private CameraMovement3D mainCam;

    public float attackSpeed = .25f, lastAttack = 0;
    private bool inMeleeRange = false;
    private bool attacking;
    private float meleeHeldDown = 0;


    void Awake()
    {
        //flashlight = this.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(2).GetChild(1).GetComponent<Flashlight>();
        //rightHand =  this.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0);
        equippedWeapon = rightHand.GetChild(0).GetComponent<Weapon>();
        //equippedWeapon.player = this;

        holsteredWeapons = new Weapon[3];
        closeQuarterEnemies = new List<EnemyStats>();
        lastAttack = Time.time;
    }
    void Start()
    {
        hud = this.GetComponent<HUD_Stealth>();
        mainCam = Camera.main.GetComponent<CameraMovement3D>();
        Debug.Log(Physics.gravity);
    }

    void Update()
    {
        closeQuarterEnemies.RemoveAll(enemy => enemy == null); //Scans all nearby enemies each frame and removes those who have died, which wouldn't
        nearbyEnemies.RemoveAll(enemy => enemy == null);       //trigger the OnTriggerExit function

        if (closeQuarterEnemies.Count > 0)
        {
            if (Input.GetButtonDown(InputType.MELEE))
            {
                //Determining which angle the player's character is facing (the one they want to attack)
                EnemyStats nearestEnemy = closeQuarterEnemies[0]; //will actually with the smallest angle away from the player's facing direction
                float lowestAngle = 180;
                foreach (EnemyStats enemy in closeQuarterEnemies)
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

                if (Time.time > lastAttack + attackSpeed)
                {
                    lastAttack = Time.time;
                    this.Attack(this, nearestEnemy, enemyAngle);
                }
                else
                    Debug.Log("Can't attack yet");

            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.isTrigger && other.tag == Tags.ENEMY)
        {
            closeQuarterEnemies.Add(other.GetComponent<EnemyStats>());
            Debug.Log("Nearby Enemy count: " + closeQuarterEnemies.Count);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.isTrigger && other.tag == Tags.ENEMY)
        {
            closeQuarterEnemies.Remove(other.GetComponent<EnemyStats>());
            Debug.Log("Nearby Enemy count: " + closeQuarterEnemies.Count);
        }
    }

    /// <summary>
    /// Instantly kills the target
    /// </summary>
    /// <param name="target"></param>
    public void Attack(CharacterStats attacker, EnemyStats target, float angle)
    {
        Debug.Log("Attack angle: " + angle);
        if (angle > 140)
        {
            target.TakeDamage(true);
        }
        else
        {
            this.audio.PlayOneShot(meleeClip);
            target.TakeDamage(meleeDamage, this);
        }
    }

    public void TakeDamage(int damage)
    {
        this.health -= (health > damage) ? damage : health;
        if (health == 0)
        {
            isDead = true;
            anim.SetBool("Dead", isDead);
        }
    }
}
