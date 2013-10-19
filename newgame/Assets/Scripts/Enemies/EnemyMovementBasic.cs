using UnityEngine;
using System.Collections;

public class EnemyMovementBasic : MonoBehaviour 
{
    private Camera mainCam;
    private EnemyStats stats;
    private HUD hud;
    private GameObject player;

    //private float[] barCache = new float[4];
    private bool isVulnerable = false;

    private bool isDead = false;

    public bool isFlying = false;

	// Use this for initialization
	void Start()
    {
        this.tag = Tags.ENEMY;
        stats = this.GetComponent<EnemyStats>();
        player = GameObject.FindGameObjectWithTag(Tags.PLAYER);
        hud = player.GetComponent<HUD>();
        mainCam = Camera.main;

	}

    // Update is called once per frame
    void Update() 
    {
        if (isDead)
        {
            GameObject.FindGameObjectWithTag(Tags.PLAYER).GetComponent<CharacterStats>().vulnerableEnemies.Remove(this.gameObject);
            Destroy(this.gameObject);
        }
        else
        {
            //this.transform.position = new Vector3(transform.position.x - 2f * Time.deltaTime, transform.position.y, transform.position.z);

            float myScreenXPos = mainCam.WorldToScreenPoint(this.transform.position).x;
            float myScreenYPos = mainCam.WorldToScreenPoint(this.transform.position).y;
            //if target is (approx) touching corresponding bar
            if (myScreenXPos < hud.barCache[(int)stats.enemyColor] + Screen.width / 10 && 
                myScreenXPos > hud.barCache[(int)stats.enemyColor] - Screen.width / 10 && 
                myScreenYPos > 0 && myScreenYPos < Screen.height)
            {
                //if they've just entered
                if (isVulnerable == false)
                {
                    isVulnerable = true;
                    this.light.enabled = true;
                    player.GetComponent<CharacterStats>().vulnerableEnemies.Add(this.gameObject);
                }
            }
            //when they leave
            else if (isVulnerable == true)
            {
                //if difficulty is on Easy (yes for now)
                this.light.color = new Color(1, 1, 1);
                this.renderer.materials[0] = Resources.LoadAssetAtPath("Assets/Materials/mat_white.mat", typeof(Material)) as Material;
                stats.enemyColor = EnemyStats.EnemyColor.White;
                //stats._color = this.color = "white";

                ///On other difficulties
                //isVulnerable = false;
                //this.light.enabled = false;
                //player.GetComponent<CharacterStats>().vulnerableEnemies.Remove(this.gameObject);
            }
            if (isFlying && myScreenXPos < hud.barCache[0])
            {
                Debug.Log("Flying enemy aggro'd");
            }
        }
	}

    public void TakeDamage()
    {
        if (this.isVulnerable)
        {
            isDead = true;
            GameObject.FindGameObjectWithTag(Tags.PLAYER).GetComponent<CharacterStats>().notes++;
        }
    }
}
