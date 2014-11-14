using UnityEngine;
using System.Collections;

public class Cain3D_Camera_Movement : MonoBehaviour {

	private float XRotationSpeed = 0.05f;
	private float YRotationSpeed = 0.05f;
	private float ZMovementSpeed = 0.5f;
	float speed = 40;
    YieldInstruction eof = new WaitForEndOfFrame();
	
	void Start ()
	{
		StartCoroutine(rotateX());
        StartCoroutine(Crescendo(this.GetComponent<AudioSource>(), 0.2f, 1f, 60f));
	}

    IEnumerator Crescendo(AudioSource music, float from, float to, float seconds)
    {
        music.volume = from;
        float increaseSpeed = (to - from) / seconds;

        while (music.volume < to)
        {
            music.volume += Time.deltaTime * increaseSpeed;
            yield return eof;
        }

        music.volume = to;
    }

	IEnumerator rotateY()
	{
		while(transform.eulerAngles.y > 36f)
		{
			transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y - YRotationSpeed, 0);
			yield return new WaitForSeconds(0.01f);
		}

		if (transform.eulerAngles.y <= 36f)
		{
			yield return new WaitForSeconds(0.015f);
			StartCoroutine(rotateX());
		}
	}

	IEnumerator rotateX ()
	{
		while(transform.eulerAngles.x < 356f)
		{
			transform.eulerAngles = new Vector3(transform.eulerAngles.x + XRotationSpeed, 47, 0);
			yield return new WaitForSeconds(0.015f);
		}

		if(transform.eulerAngles.x >= 356f)
		{
			yield return new WaitForSeconds(0.01f);
			StartCoroutine(rotateXnY());
		}
	}

	IEnumerator rotateXnY()
	{
		while(transform.eulerAngles.y >= 11 && transform.eulerAngles.x < 368)
		{
			transform.eulerAngles = new Vector3(transform.eulerAngles.x + XRotationSpeed, transform.eulerAngles.y - YRotationSpeed, 0);
			yield return new WaitForSeconds(0.009f);
		}

		if(transform.eulerAngles.y < 11 && transform.eulerAngles.x >= 8)
		{
			StartCoroutine(moveZ1());
		}
	}

	IEnumerator moveZ1()
	{
		while(transform.position.z < -1244)
		{
			transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + ZMovementSpeed);
			yield return new WaitForSeconds(0.009f);
		}

		if(transform.position.z >= -1244)
		{
			StartCoroutine(moveZ2());
		}
	}

	IEnumerator moveZ2()
	{
		Vector3 targetPosition = GameObject.Find("Sewer Black").transform.position;
		while (transform.position != targetPosition)
		{
			transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed*Time.deltaTime);
			yield return new WaitForSeconds(0.001f);
		}
		
		if (transform.position == targetPosition)
		{
			// transform.position = new Vector3(transform.position.x, transform.position.y-50, transform.position.z);
			// do something here
		}
	}

}
