using UnityEngine;
using System.Collections;

/// <summary>
/// Stats for the character (currently just the player)
/// </summary>
public class CharacterStats : MonoBehaviour
{
    public int notes; //health

    private bool attacking;
    public AudioClip deathClip;
    private Animator anim;
    private PlayerMovement playerMovement;
    private HashIDs hash;
    Camera mainCam;

    void Awake()
    {
        notes = 0;
        mainCam = Camera.main;
    }
    void Update()
    {

    }

    public void Attack(GameObject target)
    {

    }
}
