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
        else
        {
            //this.transform.position = new Vector3(transform.position.x - 2f * Time.deltaTime, transform.position.y, transform.position.z);

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
                    //if difficulty is on Easy (yes for now)
                    this.light.color = new Color(1, 1, 1);
                    this.renderer.materials[0] = Resources.LoadAssetAtPath("Assets/Materials/mat_white.mat", typeof(Material)) as Material;
                    stats.enemyColor = EnemyColor.White;
                    //stats._color = this.color = "white";

                    ///On other difficulties
                    //isVulnerable = false;
                    //this.light.enabled = false;
                    //player.GetComponent<CharacterStats>().vulnerableEnemies.Remove(this.gameObject);
                    if (isFlying)
                    {
                        Debug.Log("Flying enemy aggro'd");
                        //swoop towards player
                    }
                }
            }
        }
	}

}
