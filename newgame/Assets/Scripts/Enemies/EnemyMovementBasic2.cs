using UnityEngine;
using System.Collections;

public class EnemyMovementBasic2 : MonoBehaviour
{
    private Camera mainCam;
    private EnemyStats stats;
    private HUD2 hud;
    private Transform player;

    //private float[] barCache = new float[4];
    private bool isVulnerable = false;
    private bool isDead = false;
    public string color = "";

    // Use this for initialization
    void Start()
    {
        this.tag = Tags.ENEMY;
        stats = this.GetComponent<EnemyStats>();
        hud = GameObject.FindGameObjectWithTag(Tags.PLAYER).GetComponent<HUD2>();
        mainCam = Camera.main;
        player = GameObject.FindGameObjectWithTag(Tags.PLAYER).transform;

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

            //if target is near the player
            if(this.transform.position.x < player.position.x + 5f && this.transform.position.x > player.position.x)
            {
                //if they've just entered
                if (isVulnerable == false)
                {
                    isVulnerable = true;
                    this.light.enabled = true;
                    GameObject.FindGameObjectWithTag(Tags.PLAYER).GetComponent<CharacterStats>().vulnerableEnemies.Add(this.gameObject);
                }
            }
            //when they leave
            else if (isVulnerable == true)
            {
                isVulnerable = false;
                this.light.enabled = false;
                GameObject.FindGameObjectWithTag(Tags.PLAYER).GetComponent<CharacterStats>().vulnerableEnemies.Remove(this.gameObject);
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
