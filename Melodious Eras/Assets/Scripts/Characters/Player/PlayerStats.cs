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

    public Flashlight flashlight;
    private float _visibility = 1;


    public DetectionSphere _closeQuarterEnemies; //enemies within melee range
    public DetectionSphere _nearbyEnemies;       //enemies within hearing range


    public AudioClip deathClip, meleeClip;
    private PlayerMovementBasic _playerMovement;
    private HUD_Stealth hud;
    private CameraMovement3D cam;

    public float attackSpeed = .25f;
    private float nextAttackTime = 0;
    private bool attacking;
    private const float BACKSTAB_STRIKE_TIME = 1.0f;


    #endregion

    public float visibility { get { return _visibility; } }//visibility multiplier; 1 = normal, 0 = invisible
    public PlayerMovementBasic playerMovement { get { return _playerMovement; } }

    void Awake()
    {
        affectingLights = new List<Light>();

        _playerMovement = this.GetComponent<PlayerMovementBasic>();
        hud = this.GetComponent<HUD_Stealth>();
        anim = this.GetComponent<Animator>();
        this.SetAnimLayers();

        this.currentSecArea = GameObject.FindGameObjectWithTag(Tags.GAME_CONTROLLER).GetComponent<SecurityArea>(); //base security area

        //registering an equipped weapon, if any
        if (equippedWeapon == null)
        {
            equippedWeapon = rightHand.GetComponentInChildren<Weapon>();
        }
        equippedWeapon.Equip(this, rightHand);

        _closeQuarterEnemies.charactersInRange = new List<CharacterStats>();
    }

    void Start()
    {
        cam = Camera.main.GetComponent<CameraMovement3D>();
    }

    void Update()
    {
        this.BlendWoundLayer();
        this.RegainStamina(5 * Time.deltaTime);
        this.RegenerateHealth(Difficulty.playerRegenWait, Difficulty.playerRegenSpeed);
        //These lists will never be too large, but should still be moved out of Update at some point
        _closeQuarterEnemies.charactersInRange.RemoveAll((CharacterStats enemy) => enemy == null); //Scans all nearby enemies each frame and removes those who have died, which wouldn't
        _nearbyEnemies.charactersInRange.RemoveAll((CharacterStats enemy) => enemy == null);       //trigger the OnTriggerExit function

        if (Input.GetButtonDown(InputType.MELEE) && Time.time > nextAttackTime && !this._isDead)
            PerformMelee();
    }

    #region Combat
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
            this.StartCoroutine(PerformBackstab(targetP));
        }
        else
        {
            this.GetComponent<AudioSource>().PlayOneShot(meleeClip);
            targetP.TakeDamage(_meleeDamage, this);
        }
    }
    IEnumerator PerformBackstab(CharacterStats targetP)
    {
        anim.SetBool(HashIDs.backstab_bool, true);
        yield return new WaitForSeconds(BACKSTAB_STRIKE_TIME);
        targetP.TakeDamage(true);

        anim.SetBool(HashIDs.backstab_bool, false);
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        ShakeCamera(damage);
    }
    public override void TakeDamage(int damage, CharacterStats source)
    {
        base.TakeDamage(damage, source);
        ShakeCamera(damage);
    }
    public override void TakeDamageThroughArmor(int damage)
    {
        base.TakeDamageThroughArmor(damage);
        ShakeCamera(damage);
    }
    public override void TakeDamageThroughArmor(int damage, CharacterStats source)
    {
        base.TakeDamageThroughArmor(damage, source);
        ShakeCamera(damage);
    }
    public override void TakeDamage(bool instantKill)
    {
        lastHitTakenTime = Time.time;
        this.Die();
    }
    protected override void Die()
    {
		base.Die();
        _playerMovement.enabled = false;
        GameObject.FindGameObjectWithTag(Tags.GAME_CONTROLLER).GetComponent<EndOfLevel>().EndLevel(true);
    }
    #endregion

    private void ShakeCamera(int damage)
    {
        cam.Shake(damage / 100f, 5f, 3f);
    }
}
