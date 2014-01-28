using UnityEngine;
using System.Collections;

public class SecurityCamera : CircuitOutlet 
{
	public SecurityTerminal terminal;
	public Transform pivot;
	[SerializeField][Range(-90, 0)] public float minRotation;
	[SerializeField][Range(0, 90)] public float maxRotation;
	[SerializeField][Range(0, 10)] public float speed = 3;
	public Camera cam;
	[SerializeField][Range(0, 5)]public float waitTime = 1;

	// Use this for initialization
	void Start () {
		cam.enabled = false;
		StartCoroutine(Sweep ());
		//cam.enabled = this.activated && this.hasPower && !this.isBroken;
	}

	IEnumerator Sweep()
	{
		while(this.activated)
		{
			pivot.Rotate (Vector3.up, speed * Time.deltaTime, Space.Self);
			if(pivot.localEulerAngles.y < minRotation || pivot.localEulerAngles.y > maxRotation)
			{
				yield return new WaitForSeconds(waitTime);
				speed = -speed;
			}
			yield return new WaitForEndOfFrame();
		}
	}
}
