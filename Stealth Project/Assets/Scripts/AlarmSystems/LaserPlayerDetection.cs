using UnityEngine;
using System.Collections;

public class LaserPlayerDetection : MonoBehaviour 
{
    private GameObject player;
    private LastPlayerSighting lps;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag(Tags.PLAYER);
        lps = GameObject.FindGameObjectWithTag(Tags.GAME_CONTROLLER).GetComponent<LastPlayerSighting>();
    }

    void OnTriggerStay(Collider other)
    {
        if (renderer.enabled)
        {
            if (other.gameObject == player && other is CapsuleCollider)
                lps.position = player.transform.position;
        }
    }
}
