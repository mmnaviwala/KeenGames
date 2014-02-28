using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(SphereCollider))]
public class EnemySight : MonoBehaviour {

    public List<CharacterStats> charactersInRange;
    public EnemyAI ai;
    public float fovAngle = 160f;
    // Use this for initialization
    void Awake()
    {
        charactersInRange = new List<CharacterStats>();
        if (ai == null)
        {
            Transform temp = this.transform.parent;
            while (temp.parent != null && temp.GetComponent<EnemyAI>() == null)
                temp = temp.parent;
            ai = temp.GetComponent<EnemyAI>();
        }
        ai.sight = this;
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.isTrigger && (other.tag == Tags.ENEMY || other.tag == Tags.PLAYER) && other.transform != ai.transform) //last check prevents this character from seeing itself
        {
            this.charactersInRange.Add(other.GetComponent<CharacterStats>());
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (!other.isTrigger && (other.tag == Tags.ENEMY || other.tag == Tags.PLAYER) && other.transform != ai.transform)
        {
            this.charactersInRange.Remove(other.GetComponent<CharacterStats>());
        }
    }
}
