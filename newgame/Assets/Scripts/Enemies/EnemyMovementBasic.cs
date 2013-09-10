using UnityEngine;
using System.Collections;

public class EnemyMovementBasic : MonoBehaviour 
{
    private Camera mainCam;
    private EnemyStats stats;

    private float[] barCache = new float[4];
    private bool isVulnerable = false;

    private bool isDead = false;

    public string color = "";

	// Use this for initialization
	void Start ()
    {
        this.tag = Tags.ENEMY;
        stats = this.GetComponent<EnemyStats>();
        mainCam = Camera.main;

        float xx = Screen.width / 10;

        barCache[0] = xx * 3f + xx / 8f;    //green bar center
        barCache[1] = xx * 4.25f + xx / 8f; //blue bar center
        barCache[2] = xx * 5.5f + xx / 8f;  //red bar center
        barCache[3] = xx * 6.75f + xx / 8f; //purple bar center

        switch (stats.enemyColor)
        {
            case 0: color = "green";  
                    
                break;
            case 1: color = "blue";     break;
            case 2: color = "red";      break;
            case 3: color = "purple";   break;
        }
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
            this.transform.position = new Vector3(transform.position.x - 2f * Time.deltaTime, transform.position.y, transform.position.z);

            float myScreenXPos = mainCam.WorldToScreenPoint(this.transform.position).x;
            //if target is (approx) touching corresponding bar
            if (myScreenXPos < barCache[stats.enemyColor] + Screen.width / 20 && myScreenXPos > barCache[stats.enemyColor] - Screen.width / 20)
            {
                //if they've just entered
                if (isVulnerable == false)
                {
                    isVulnerable = true;
                    Debug.Log(color + " is vulnerable");
                    this.transform.localScale = new Vector3(1, .1f, 1);
                    GameObject.FindGameObjectWithTag(Tags.PLAYER).GetComponent<CharacterStats>().vulnerableEnemies.Add(this.gameObject);
                }
            }
            //when they leave
            else if (isVulnerable == true)
            {
                isVulnerable = false;
                Debug.Log(color + " is no longer vulnerable");
                this.transform.localScale = new Vector3(1, 1, 1);
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
