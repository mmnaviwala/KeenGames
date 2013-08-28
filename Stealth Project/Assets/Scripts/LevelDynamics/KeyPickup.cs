using UnityEngine;
using System.Collections;

public class KeyPickup : MonoBehaviour 
{
    public AudioClip keyGrab;
    private GameObject player;
    private PlayerInventory pInventory;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag(Tags.PLAYER);
        pInventory = player.GetComponent<PlayerInventory>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player && other is CapsuleCollider)
        {
            AudioSource.PlayClipAtPoint(keyGrab, this.transform.position);
            pInventory.hasKey = true;
            Destroy(this.gameObject);
        }
    }
}
