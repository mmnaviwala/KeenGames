using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Stats for the character (currently just the player)
/// </summary>
[AddComponentMenu("Scripts/Characters/Player Stats")]
public class PlayerStats : CharacterStats
{
    #region variables
    //visibility multipliers
    private const float CROUCH_MULTIPLIER = .5f;    //crouching reduces visibility by 50%
    public List<Light> affectingLights;             //will be used in calculating visibility
    public Flashlight flashlight;
    private float _visibility = 1;


    public DetectionSphere _closeQuarterEnemies; //enemies within melee range
    public DetectionSphere _nearbyEnemies;       //enemies within hearing range


    public AudioClip deathClip, meleeClip;
    private Animator anim;
    private PlayerMovementBasic playerMovement;
    private HUD_Stealth hud;

    public float attackSpeed = .25f;
    private float nextAttackTime = 0;
    private bool attacking;
    #endregion

    public float visibility { get { return visibility; } }//visibility multiplier; 1 = normal, 0 = invisible

    void Awake()
    {
        affectingLights = new List<Light>();

        playerMovement = this.GetComponent<PlayerMovementBasic>();
        hud = this.GetComponent<HUD_Stealth>();
        anim = this.GetComponent<Animator>();
        this.currentSecArea = GameObject.FindGameObjectWithTag(Tags.GAME_CONTROLLER).GetComponent<SecurityArea>(); //base security area

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
    }
    void Start()
    {
    }
    void Update()
    {
        this.RegainStamina(5 * Time.deltaTime);
        //These lists will never be too large, but should still be moved out of Update at some point
        _closeQuarterEnemies.charactersInRange.RemoveAll((CharacterStats enemy) => enemy == null); //Scans all nearby enemies each frame and removes those who have died, which wouldn't
        _nearbyEnemies.charactersInRange.RemoveAll((CharacterStats enemy) => enemy == null);       //trigger the OnTriggerExit function

        if (Input.GetButtonDown(InputType.MELEE) && Time.time > nextAttackTime && !this._isDead)
            PerformMelee();
    }

    public void PerformMelee()
    {
        if (_closeQuarterEnemies.charactersInRange.Count > 0)
        {
            //determining the enemy CLOSEST to the direction the player is facing
            CharacterStats nearestEnemy = _closeQuarterEnemies.charactersInRange[0];
            float lowestAngle = 180;
            foreach (EnemyStats enemy in _closeQuarterEnemies.charactersInRange)
            {
                if (!enemy.isDead)
                {
                    Vector3 relEnemyPos = enemy.transform.position - this.transform.position;
                    float angle = Vector3.Angle(relEnemyPos, this.transform.forward);
                    if (angle < lowestAngle)
                    {
                        lowestAngle = angle;
                        nearestEnemy = enemy;
                    }
                }
            }

            if (lowestAngle != 180)
            {
                //If the player is behind the target (60-degree area)
                Vector3 relPlayerPos = this.transform.position - nearestEnemy.transform.position;
                float enemyAngle = Vector3.Angle(nearestEnemy.transform.forward, new Vector3(relPlayerPos.x, 0, relPlayerPos.z));

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
        nextAttackTime = Time.time + attackSpeed;
        if (angle > 140)
        {
            targetP.TakeDamage(true);
        }
        else
        {
            this.audio.PlayOneShot(meleeClip);
            targetP.TakeDamage(_meleeDamage, this);
        }
    }
    public override void TakeDamage(bool instantKill)
    {
        this.Die();
    }
    protected override void Die()
    {
        this._isDead = true;
        anim.SetBool(HashIDs.dead_bool, isDead);
        playerMovement.enabled = false;
        GameObject.FindGameObjectWithTag(Tags.GAME_CONTROLLER).GetComponent<EndOfLevel>().EndLevel(true);
        //fade to black
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
            //return light_modifier * CROUCH_MULTIPLIER
            return 1;
        }
        else
            return 0; //complete darkness = invisible
    }
}
