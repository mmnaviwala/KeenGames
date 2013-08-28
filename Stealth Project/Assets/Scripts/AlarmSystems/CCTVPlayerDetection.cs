using UnityEngine;
using System.Collections;

public class CCTVPlayerDetection : MonoBehaviour 
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
        if (other.gameObject == player && other is CapsuleCollider)
        {
            Vector3 relPlayerPos = player.transform.position - transform.position;
            RaycastHit hit;

            if (Physics.Raycast(this.transform.position, relPlayerPos, out hit))
            {
                if(hit.collider.gameObject == player)
                {
                    lps.position = player.transform.position; //spotted!
                }
            }
        }
    }
}
