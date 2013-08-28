using UnityEngine;
using System.Collections;

public class DoorAnimation : MonoBehaviour 
{
    public bool requiresKey;
    public AudioClip doorSwishClip, accessDeniedClip;

    private Animator anim;
    private HashIDs hash;
    private GameObject player;
    private PlayerInventory pInventory;
    private int triggerCount;

    void Awake()
    {
        triggerCount = 0;
        anim = GetComponent<Animator>();
        hash = GameObject.FindGameObjectWithTag(Tags.GAME_CONTROLLER).GetComponent<HashIDs>();
        player = GameObject.FindGameObjectWithTag(Tags.PLAYER);
        pInventory = player.GetComponent<PlayerInventory>();

        Debug.Log(hash.openBool + " " + triggerCount);
    }

    void Update()
    {
        anim.SetBool(hash.openBool, triggerCount > 0);

        if (anim.IsInTransition(0) && !audio.isPlaying)
        {
            this.audio.clip = doorSwishClip;
            this.audio.Play();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player && other is CapsuleCollider)
        {
            if (this.requiresKey)
            {
                if (pInventory.hasKey)
                    triggerCount++;
                else
                {
                    this.audio.clip = accessDeniedClip;
                    this.audio.Play();
                }
            }
            else
                triggerCount++;
        }
        //if an enemy collider hits the door's sphere collider, AND if that is a capsule collider 
        //(since enemies will also have sphere colliders for sight and sound)
        else if (other.gameObject.tag == Tags.ENEMY && other is CapsuleCollider)
        {
            triggerCount++;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if ((other.gameObject == player || other.gameObject.tag == Tags.ENEMY) && other is CapsuleCollider)
            triggerCount = Mathf.Max(0, triggerCount - 1); // if(triggerCount > 0) triggerCount--;
    }

    
}
