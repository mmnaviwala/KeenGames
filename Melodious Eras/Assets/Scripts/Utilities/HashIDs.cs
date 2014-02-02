using UnityEngine;
using System.Collections;

[AddComponentMenu("Scripts/Utilities/Hash IDs")]
public static class HashIDs 
{
	//states
	public static int dying_state;
	public static int locomotion_state;

	//booleans
	public static int dead_bool;
	public static int sneaking_bool;
	public static int slide_bool;
	public static int playerInSight_bool;
	public static int aiming_bool;
	public static int open_bool;
	public static int onGround_bool;
	public static int vault_bool;
	public static int dive_bool;
	public static int climbUp_bool;
	public static int climbDown_bool;
	public static int climbeLedge_bool;

	//floats
	public static int speed_float;
	public static int aimWeight_float;
	public static int angularSpeed_float;
	public static int jump_float;
	public static int jumpLeg_float;

    public static void Initialize()
    {
		dying_state = Animator.StringToHash("Player Animator.Dying");
		dead_bool = Animator.StringToHash("Player Animator.Dead");
		locomotion_state = Animator.StringToHash("Player Animator.Locomotion");
		speed_float = Animator.StringToHash("Speed");
		sneaking_bool = Animator.StringToHash("Sneaking");
		playerInSight_bool = Animator.StringToHash("PlayerInSight");
		aiming_bool = Animator.StringToHash("Aiming");
		aimWeight_float = Animator.StringToHash("AimWeight");
		angularSpeed_float = Animator.StringToHash("AngularSpeed");
		open_bool = Animator.StringToHash("Open");
		jump_float = Animator.StringToHash("Jump");
		jumpLeg_float = Animator.StringToHash("JumpLeg");
		onGround_bool = Animator.StringToHash("OnGround");
		vault_bool = Animator.StringToHash("Vault");
		dive_bool = Animator.StringToHash("Dive");
		climbUp_bool = Animator.StringToHash("ClimbUp");
		climbDown_bool = Animator.StringToHash("ClimbDown");
		climbeLedge_bool = Animator.StringToHash("ClimbLedge");
		slide_bool = Animator.StringToHash("Slide");
    }
}
