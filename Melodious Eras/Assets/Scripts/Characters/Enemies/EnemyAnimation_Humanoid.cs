using UnityEngine;
using System.Collections;

public class EnemyAnimation_Humanoid : MonoBehaviour 
{
	public float deadZone = 5f;

	private Transform player;
	private NavMeshAgent nav;
	private EnemyAI AI;
	private Animator anim;
	private AnimatorSetup animSetup;

	// Use this for initialization
	void Awake () {
        HashIDs.Initialize();
		player = GameObject.FindGameObjectWithTag(Tags.PLAYER).transform;
		AI = this.GetComponent<EnemyAI>();
		nav = this.GetComponent<NavMeshAgent>();
		anim = this.GetComponent<Animator>();

		nav.updateRotation = false; //done by animation
		animSetup = new AnimatorSetup(anim);
		anim.SetLayerWeight(1, 1f);
		anim.SetLayerWeight(2, 1f);

		deadZone *= Mathf.Deg2Rad;
	}

	// Update is called once per frame
	void Update()
	{
		NavAnimSetup();
	}

	void OnAnimatorMove()
	{
		nav.velocity = anim.deltaPosition / Time.deltaTime;
		this.transform.rotation = this.anim.rootRotation;
	}

	private void NavAnimSetup () 
	{
		float speed;
		float angle;

		if(this.AI.seesPlayer)
		{
			speed = 0f;
			angle = FindAngle (this.transform.forward, player.position - this.transform.position, transform.up);
		}
		else //handles speed during turning, to avoid having the enemy run in a wide arc (rather than make a sharp turn)
		{
			speed = Vector3.Project(nav.desiredVelocity, this.transform.forward).magnitude;

			angle = FindAngle (this.transform.forward, nav.desiredVelocity, this.transform.up);

			if(Mathf.Abs (angle) < deadZone)
			{
				this.transform.LookAt(this.transform.position, nav.desiredVelocity);
				angle = 0f;
			}
		}

		animSetup.Setup(speed, angle);
	}

	private float FindAngle(Vector3 facingDirection, Vector3 desiredDirection, Vector3 up)
	{
		if(desiredDirection == Vector3.zero)
			return 0f;

		float angle = Vector3.Angle (facingDirection, desiredDirection);
		Vector3 normal = Vector3.Cross (facingDirection, desiredDirection);

		angle *= Mathf.Sign (Vector3.Dot(normal, up));

		return angle * Mathf.Deg2Rad;
	}
}