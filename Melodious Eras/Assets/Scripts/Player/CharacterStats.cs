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

    public Flashlight flashlight;
    public Weapon equippedWeapon;
    public Weapon[] holsteredWeapons;
    public Transform rightHand;

    public AudioClip deathClip;
    private Animator anim;
    private PlayerMovement playerMovement;
    private HashIDs hash;
    private HUD_Stealth hud;
    private CameraMovement3D mainCam;

    private bool inMeleeRange = false;
    private bool attacking;
    private float meleeHeldDown = 0;


    void Awake()
    {
        //flashlight = this.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(2).GetChild(1).GetComponent<Flashlight>();
        //rightHand =  this.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0);
        equippedWeapon =  rightHand.GetChild(0).GetComponent<Weapon>();
        //equippedWeapon.player = this;

        holsteredWeapons = new Weapon[3];
        nearbyEnemies = new List<EnemyStats>();
    }
    void Start()
    {
        hud = this.GetComponent<HUD_Stealth>();
        mainCam = Camera.main.GetComponent<CameraMovement3D>();
        Debug.Log(Physics.gravity);
    }

    void Update()
    {
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
}
