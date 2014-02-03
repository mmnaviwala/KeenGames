using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Stats for the character (currently just the player)
/// </summary>
[AddComponentMenu("Scripts/Characters/Player Stats")]
public class PlayerStats : CharacterStats
{
   // public List<EnemyStats> _CloseQuarterEnemies, //will be available for melee attacks
    //                        _CearbyEnemies;       //will be in range to hear
	public DetectionSphere _closeQuarterEnemies;
	public DetectionSphere _nearbyEnemies;
    public int threshold = 5;

    public Suit suit;
    public Flashlight flashlight;

    public AudioClip deathClip, meleeClip;
    private Animator anim;
    private PlayerMovement playerMovement;
    //private HashIDs hash;
    private HUD_Stealth hud;
    private CameraMovement3D mainCam;

    public float attackSpeed = .25f, lastAttack = 0;
    private bool inMeleeRange = false;
    private bool attacking;
    private float meleeHeldDown = 0;
	public List<Light> affectingLights;

    void Awake()
    {
		affectingLights = new List<Light>();
        //flashlight = this.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(2).GetChild(1).GetComponent<Flashlight>();
        //rightHand =  this.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0);
        if(equippedWeapon == null)
		{
			for(int c = 0; c < rightHand.childCount; c++)
			{
				if(rightHand.GetChild(c).tag == Tags.WEAPON)
				{
					equippedWeapon = rightHand.GetChild(c).GetComponent<Weapon>();
					break;
				}
			}
		}
		//equippedWeapon = rightHand.GetComponent<Weapon>();
        //equippedWeapon.player = this;

        _closeQuarterEnemies.charactersInRange = new List<CharacterStats>();
        lastAttack = Time.time;
    }
    void Start()
    {
        hud = this.GetComponent<HUD_Stealth>();
		anim = this.GetComponent<Animator>();
        mainCam = Camera.main.GetComponent<CameraMovement3D>();
    }

    void Update()
    {
        _closeQuarterEnemies.charactersInRange.RemoveAll(enemy => enemy == null); //Scans all nearby enemies each frame and removes those who have died, which wouldn't
        _nearbyEnemies.charactersInRange.RemoveAll(enemy => enemy == null);       //trigger the OnTriggerExit function

        if (_closeQuarterEnemies.charactersInRange.Count > 0)
        {
            if (Input.GetButtonDown(InputType.MELEE))
            {
                PerformMelee();
            }
        }
    }

    public void PerformMelee()
    {
        //Determining which angle the player's character is facing (the one they want to attack)
        if (_closeQuarterEnemies.charactersInRange.Count > 0)
        {
            CharacterStats nearestEnemy = _closeQuarterEnemies.charactersInRange[0]; //will actually be the enemy with the smallest angle away from the player's facing direction
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

            if (Time.time > lastAttack + attackSpeed)
            {
                this.Attack(nearestEnemy, this, enemyAngle);
            }
        }
        else
        {
            //Just perform melee attack
        }
    }

    /// <summary>
    /// Instantly kills the target if attacking from behind.
    /// </summary>
    /// <param name="attackerP"></param>
    public override void Attack(CharacterStats targetP, CharacterStats attackerP, float angle)
    {
        lastAttack = Time.time;
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

    public override void TakeDamage(int damage)
    {
        this.health -= (health > damage) ? damage : health;
        if (health == 0)
        {
            isDead = true;
            anim.SetBool(HashIDs.dead_bool, isDead);
        }
    }

	public float VisibilityMultiplier()
	{
		if(affectingLights.Count > 0)
		{
			foreach(Light _light in affectingLights)
			{
				float relativeDistance = Vector3.Distance(_light.transform.position, this.transform.position);
				if(_light.intensity * relativeDistance * relativeDistance > .5f)
					return 1;
			}
			//calculate lighting visibility
			return 1;
		}
		else
			return 0; //complete darkness = invisible
	}
}
