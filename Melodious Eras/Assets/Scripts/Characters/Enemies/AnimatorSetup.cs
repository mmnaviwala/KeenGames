using UnityEngine;

public class AnimatorSetup
{
	public float speedDampTime = 0.1f;				// Damping time for the Speed parameter.
	public float angularSpeedDampTime = 0.7f;		// Damping time for the AngularSpeed parameter
	public float angleResponseTime = 0.6f;			// Response time for turning an angle into angularSpeed.
	
	
	private Animator anim;							// Reference to the animator component.
	
	
	// Constructor
	public AnimatorSetup(Animator animator)
	{
		anim = animator;
	}
	
	
	public void Setup(float speed, float angle)
	{
		// Angular speed is the number of degrees per second.
		float angularSpeed = angle / angleResponseTime;
		
		// Set the mecanim parameters and apply the appropriate damping to them.
		anim.SetFloat(HashIDs.speed_float, speed, speedDampTime, Time.deltaTime);
		anim.SetFloat(HashIDs.angularSpeed_float, angularSpeed, angularSpeedDampTime, Time.deltaTime);
	}	
}