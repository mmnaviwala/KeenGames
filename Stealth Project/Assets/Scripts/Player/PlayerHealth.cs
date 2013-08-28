using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour 
{
    public float health = 100f;
    public float resetTimeAfterDeath = 5f;

    public AudioClip deathClip;
    private Animator anim;
    private PlayerMovement playerMovement;
    private HashIDs hash;
    private SceneFadeInOut sceneFader;
    private LastPlayerSighting lps;
    private float timer;
    private bool playerDead = false;

    void Awake()
    {
        anim = this.GetComponent<Animator>(); //should be same as this.gameObject.GetComponent<Animator>();
        playerMovement = this.GetComponent<PlayerMovement>();
        hash = GameObject.FindGameObjectWithTag(Tags.GAME_CONTROLLER).GetComponent<HashIDs>();
        sceneFader = GameObject.FindGameObjectWithTag(Tags.FADER).GetComponent<SceneFadeInOut>();
        lps = GameObject.FindGameObjectWithTag(Tags.GAME_CONTROLLER).GetComponent<LastPlayerSighting>();
    }

    void Update()
    {
        if (health <= 0f)
        {
            if (!playerDead)
            {
                PlayerDying();
            }
            else
            {
                PlayerDead();
                LevelReset();
            }
        }
    }

    void PlayerDying()
    {
        playerDead = true;
        anim.SetBool(hash.deadBool, true);
        AudioSource.PlayClipAtPoint(deathClip, transform.position);
    }

    void PlayerDead()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).nameHash == hash.dyingState)
            anim.SetBool(hash.deadBool, false);

        anim.SetFloat(hash.speedFloat, 0f);
        playerMovement.enabled = false;
        lps.position = LastPlayerSighting.resetPosition;
        this.audio.Stop();
    }

    void LevelReset()
    {
        timer += Time.deltaTime;
        if (timer >= resetTimeAfterDeath)
            sceneFader.Endscene();
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
    }
}
