using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Stats for the character (currently just the player)
/// </summary>
[AddComponentMenu("Scripts/Characters/Player Stats")]
public class PlayerStats : CharacterStats
{
    public DetectionSphere _closeQuarterEnemies; //enemies within melee range
    public DetectionSphere _nearbyEnemies;       //enemies within hearing range

    public Suit suit;
    public Flashlight flashlight;

    public AudioClip deathClip, meleeClip;
    private Animator anim;
    private PlayerMovement playerMovement;
    private HUD_Stealth hud;
    private CameraMovement3D mainCam;

    public float attackSpeed = .25f, lastAttackTime = 0;
    private bool attacking;

    public List<Light> affectingLights;             //will be used in calculating visibility
    public float visibility;                        //visibility multiplier; 1 = normal, 0 = invisible

    void Awake()
    {
        affectingLights = new List<Light>();

        //registering an equipped weapon, if any
        if (equippedWeapon == null)
        {
            for (int c = 0; c < rightHand.childCount; c++)
            {
                if (rightHand.GetChild(c).tag == Tags.WEAPON)
                {
                    equippedWeapon = rightHand.GetChild(c).GetComponent<Weapon>();
                    break;
                }
            }
        }

        _closeQuarterEnemies.charactersInRange = new List<CharacterStats>();
        lastAttackTime = Time.time;
    }
    void Start()
    {
        hud = this.GetComponent<HUD_Stealth>();
        anim = this.GetComponent<Animator>();
        mainCam = Camera.main.GetComponent<CameraMovement3D>();
        this.currentSecArea = GameObject.FindGameObjectWithTag(Tags.GAME_CONTROLLER).GetComponent<SecurityArea>(); //base security area
    }
    void Update()
    {
        this.RegainStamina(5 * Time.deltaTime);
        //These lists will never be too large, but should still be moved out of Update at some point
        _closeQuarterEnemies.charactersInRange.RemoveAll((CharacterStats enemy) => enemy == null); //Scans all nearby enemies each frame and removes those who have died, which wouldn't
        _nearbyEnemies.charactersInRange.RemoveAll((CharacterStats enemy) => enemy == null);       //trigger the OnTriggerExit function

        if (_closeQuarterEnemies.charactersInRange.Count > 0 && Input.GetButtonDown(InputType.MELEE))
            PerformMelee();
    }

    public void PerformMelee()
    {
        if (_closeQuarterEnemies.charactersInRange.Count > 0)
        {
            //Determining which angle the player's character is facing (the one they want to attack)
            CharacterStats nearestEnemy = _closeQuarterEnemies.charactersInRange[0];
            float lowestAngle = 180;
            foreach (EnemyStats enemy in _closeQuarterEnemies.charactersInRange)
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

            if (Time.time > lastAttackTime + attackSpeed)
            {
                this.Attack(nearestEnemy, this, enemyAngle);
            }
        }
        else
        {
            //Possibly just attack the air, once we get an animation
        }
    }

    /// <summary>
    /// Melee attack. Instantly kills the target if attacking from behind.
    /// </summary>
    /// <param name="attackerP"></param>
    public override void Attack(CharacterStats targetP, CharacterStats attackerP, float angle)
    {
        lastAttackTime = Time.time;
        if (angle > 140)
        {
            targetP.TakeDamage(true);
        }
        else
        {
            this.audio.PlayOneShot(meleeClip);
            targetP.TakeDamage(meleeDamage, this);
        }
    }

    /// <summary>
    /// Shared by all characters
    /// </summary>
    /// <param name="damage"></param>
    public override void TakeDamage(int damage)
    {
        this.health -= (health > damage) ? damage : health;
        if (health == 0)
        {
            this.isDead = true;
            anim.SetBool(HashIDs.dead_bool, isDead);
        }
    }
    protected override void Die()
    {
        this.isDead = true;
        //wait for animation, then fade to black
        //reload last checkpoint
    }

    /// <summary>
    /// Calculates visibility based on lighting
    /// </summary>
    /// <returns></returns>
    public float VisibilityMultiplier()
    {
        if (affectingLights.Count > 0)
        {
            foreach (Light _light in affectingLights)
            {
                float relativeDistance = Vector3.Distance(_light.transform.position, this.transform.position);
                if (_light.intensity * relativeDistance * relativeDistance > .5f)
                    return 1;
            }
            //calculate lighting visibility
            return 1;
        }
        else
            return 0; //complete darkness = invisible
    }
}
