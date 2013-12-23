using UnityEngine;
using System.Collections;

[AddComponentMenu("Scripts/Environment/Breakable Object")]
public class BreakableObject : MonoBehaviour
{
    public int durability = -1; //-1 = invulnerable
    public AudioClip breakingSound;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public virtual void TakeDamage(int damage)
    {
        if (durability != -1)
        {
            durability -= (durability > damage) ? damage : durability;
            //break animation/sound
        }
    }
}
