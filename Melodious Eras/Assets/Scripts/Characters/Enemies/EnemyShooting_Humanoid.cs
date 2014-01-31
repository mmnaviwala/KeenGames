using UnityEngine;
using System.Collections;

public class EnemyShooting_Humanoid : MonoBehaviour 
{
	Gun gun;
	private Animator anim;
	CharacterStats targetStats;

	private Transform player;
	private bool shooting;

	void Awake()
	{
		this.anim = this.GetComponent<Animator>();
		player = GameObject.FindGameObjectWithTag(Tags.PLAYER).transform;
		targetStats = player.GetComponent<CharacterStats>();
	}
	// Use this for initialization
	void Start () 
	{
		gun = this.GetComponent<EnemyStats>().equippedWeapon as Gun;
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
