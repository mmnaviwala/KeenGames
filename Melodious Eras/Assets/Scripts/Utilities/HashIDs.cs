using UnityEngine;
using System.Collections;

[AddComponentMenu("Scripts/Utilities/Hash IDs")]
public static class HashIDs {

	public static int 	dying_state, 
					  	dead_bool, 
						locomotion_state, 
						shout_state, 
						speed_float, 
						sneaking_bool, 
						shouting_bool,
               			playerInSight_bool, 
						shooting_float, 
						aimWeight_float, 
						angularSpeed_float, 
						open_bool,
						jump_float,
						jumpLeg_float;

    public static void Initialize()
    {
		dying_state = Animator.StringToHash("Base Layer.Dying");
		dead_bool = Animator.StringToHash("Dead");
		locomotion_state = Animator.StringToHash("Base Layer.Locomotion");
		shout_state = Animator.StringToHash("Shouting.Shout");
		speed_float = Animator.StringToHash("Speed");
		sneaking_bool = Animator.StringToHash("Sneaking");
		shouting_bool = Animator.StringToHash("Shouting");
		playerInSight_bool = Animator.StringToHash("PlayerInSight");
		shooting_float = Animator.StringToHash("IsShooting");
		aimWeight_float = Animator.StringToHash("AimWeight");
		angularSpeed_float = Animator.StringToHash("AngularSpeed");
		open_bool = Animator.StringToHash("Open");
		jump_float = Animator.StringToHash("Jump");
		jumpLeg_float = Animator.StringToHash("JumpLeg");
    }
}
