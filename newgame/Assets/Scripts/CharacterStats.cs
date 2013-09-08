using UnityEngine;
using System.Collections;

/// <summary>
/// Stats for the character (currently just the player)
/// </summary>
public class CharacterStats : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth = 100;
    public float resetTimeAfterDeath = 5f;

    public AudioClip deathClip;
    private Animator anim;
    private PlayerMovement playerMovement;
    private HashIDs hash;
    

    void Awake()
    {
 
    }
    void Update()
    {
 
    }

    public void AdjustHealth(int amount)
    {
 
    }
}
