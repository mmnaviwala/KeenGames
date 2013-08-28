using UnityEngine;
using System.Collections;

public class LaserSwitchDeactivation : MonoBehaviour 
{
    public GameObject laser;
    public Material unlockedMat;

    private GameObject player;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag(Tags.PLAYER);
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject == player)
        {
            if (Input.GetButton("Switch"))
            {
                DeactivateLaser();
            }
        }
    }

    void DeactivateLaser()
    {
        laser.SetActive(false);

        Renderer switchConsoleScreen = transform.Find("prop_switchUnit_screen").renderer;

        switchConsoleScreen.material = unlockedMat;
        this.audio.Play();
    }
}
