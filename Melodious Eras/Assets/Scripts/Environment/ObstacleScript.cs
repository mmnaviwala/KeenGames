using UnityEngine;
using System.Collections;

public class ObstacleScript : MonoBehaviour 
{
    public float timeDelay = 0f;
    public bool falling = false;
    float velocity = 0f;
    EnemyStats stats;
    CharacterStats player;
    HUD hud;
	// Use this for initialization
	void Start () 
    {
        player = GameObject.FindGameObjectWithTag(Tags.PLAYER).GetComponent<CharacterStats>();
        hud = player.GetComponent<HUD>();
        stats = this.GetComponent<EnemyStats>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (stats.isDead && !falling)
        {
            StartCoroutine(this.Fall());
        }
        else
        {
            float myScreenXPos = Camera.main.WorldToScreenPoint(this.transform.position).x;
            float myScreenYPos = Camera.main.WorldToScreenPoint(this.transform.position).y;
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

                }
            }
        }
	}
    void FixedUpdate()
    {
        if (falling)
        {
            //velocity -= Time.fixedDeltaTime * 9.81f;
            //Debug.Log("Velocity: " + velocity);
            //this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + velocity, this.transform.position.z);
        }
    }
    IEnumerator Fall()
    {
        yield return new WaitForSeconds(timeDelay);
        this.rigidbody.useGravity = true;
    }
}
