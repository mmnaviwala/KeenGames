using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{
    void Start()
    {
 
    }

    void Update()
    {
 
    }

    public virtual bool Fire()
    {
        Debug.Log("Base trigger");
        return false;
    }
}
