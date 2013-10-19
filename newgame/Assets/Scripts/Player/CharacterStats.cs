using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Stats for the character (currently just the player)
/// </summary>
public class CharacterStats : MonoBehaviour
{
    public int notes, greenNotes, blueNotes, redNotes, purpleNotes; //health
    public List<GameObject> vulnerableEnemies;

    private bool attacking;
    public AudioClip deathClip;
    private Animator anim;
    private PlayerMovement playerMovement;
    private HashIDs hash;
    Camera mainCam;

    enum InstantKill { NONE = 0, GREEN = 1, BLUE = 2, RED = 3, PURPLE = 4 };
    int specialAttack = 0;

    void Awake()
    {
        notes = 0;
        greenNotes = 0;
        blueNotes = 0;
        redNotes = 0;
        purpleNotes = 0;
        mainCam = Camera.main;
        vulnerableEnemies = new List<GameObject>();
    }
    void Update()
    {

    }

    public void Attack(GameObject target)
    {
        target.GetComponent<EnemyMovementBasic>().TakeDamage();
    }

    public void AddNote(string color, int value)
    {
        switch (color)
        {
            case "green":
                greenNotes += value;
                if (greenNotes == 5)
                {
                    greenNotes = 0;
                    specialAttack = (int)InstantKill.GREEN;
                }
                break;
            case "blue":
                blueNotes += value;
                if (blueNotes == 5)
                {
                    blueNotes = 0;
                    specialAttack = (int)InstantKill.BLUE;
                } 
                break;
            case "red":
                redNotes += value;
                if (redNotes == 5)
                {
                    redNotes = 0;
                    specialAttack = (int)InstantKill.RED;
                }
                break;
            case "purple":
                purpleNotes += value;
                if (purpleNotes == 5)
                {
                    purpleNotes = 0;
                    specialAttack = (int)InstantKill.PURPLE;
                }
                break;
            default:
                notes+= value; break;
        }
    }


}
