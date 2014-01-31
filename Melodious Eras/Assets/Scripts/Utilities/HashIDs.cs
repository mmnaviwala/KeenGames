using UnityEngine;
using System.Collections;

[AddComponentMenu("Scripts/Utilities/Hash IDs")]
public static class HashIDs {

	public static int 	dying_state, 
					  	dead_bool, 
						locomotion_state, 
						speed_float, 
						sneaking_bool, 
               			playerInSight_bool, 
						aiming_bool, 
						aimWeight_float, 
						angularSpeed_float, 
						open_bool,
						jump_float,
						jumpLeg_float;

    public static void Initialize()
    {
		dying_state = Animator.StringToHash("Player Animator.Dying");
		dead_bool = Animator.StringToHash("Dead");
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
    }
}
