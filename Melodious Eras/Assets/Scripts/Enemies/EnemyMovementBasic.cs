using UnityEngine;
using System.Collections;

public class EnemyMovementBasic : MonoBehaviour 
{
    private Camera mainCam;
    private EnemyStats stats;
    private HUD hud;
    private CharacterStats player;

    //private float[] barCache = new float[4];
    public bool isFlying = false;
    public bool aggro = false;

	// Use this for initialization
	void Start()
    {
        this.tag = Tags.ENEMY;
        stats = this.GetComponent<EnemyStats>();
        player = GameObject.FindGameObjectWithTag(Tags.PLAYER).GetComponent<CharacterStats>();
        hud = player.GetComponent<HUD>();
        mainCam = Camera.main;

	}

    // Update is called once per frame
    void Update() 
    {
        if (stats.isDead)
        {
            player.vulnerableEnemies.Remove(this.stats);
            Destroy(this.gameObject);
        }
        else if(hud.enabled)
        {
            float myScreenXPos = mainCam.WorldToScreenPoint(this.transform.position).x;
            float myScreenYPos = mainCam.WorldToScreenPoint(this.transform.position).y;
            //if target is (approx) touching corresponding bar
            if (stats.enemyColor != EnemyColor.White)
            {
                if (myScreenXPos < hud.barCache[(int)stats.enemyColor] + Screen.width / 10 &&
                    myScreenXPos > hud.barCache[(int)stats.enemyColor] - Screen.width / 10 &&
                    myScreenYPos > 0 && myScreenYPos < Screen.height)
                {
                    //if they've just entered
                    if (stats.isVulnerable == false)
                    {
                        stats.isVulnerable = true;
                        this.light.enabled = true;
                        player.vulnerableEnemies.Add(this.stats);
                    }
                }
                //when they leave
                else if (stats.isVulnerable == true)
                {
                    ///On non-easy difficulties
                    stats.isVulnerable = false;
                    this.light.enabled = false;
                    player.GetComponent<CharacterStats>().vulnerableEnemies.Remove(this.stats);

                    if (isFlying)
                        aggro = true;
                }
            }

            //swoops down at the player
            if (aggro  /*&& myScreenXPos < hud.barCache[0]*/)
            {
                SmoothLook();
                this.transform.position = Vector3.Lerp(this.transform.position, player.transform.position + player.transform.forward, 5 * Time.deltaTime);

            }
        }
	}

    void OnTriggerEnter(Collider other)
    {
        aggro = false;
        isFlying = false;
    }
    void SmoothLook()
    {
        Vector3 relPlayerPosition = player.transform.position - this.transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(relPlayerPosition, Vector3.up);
        transform.rotation = Quaternion.Lerp(this.transform.rotation, lookRotation, 10 * Time.deltaTime);
    }
}
