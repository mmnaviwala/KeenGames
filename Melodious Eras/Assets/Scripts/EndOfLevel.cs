using UnityEngine;
using System.Collections;

public class EndOfLevel : MonoBehaviour {

	public bool levelEnded = false;
	public Vector3 offset = new Vector3(1.26f, 1.45f, 0.6f);
	public float speed = 4;
	public GUIStyle customGUIStyle;

	private Vector3 playerPos, cameraPos, cameraRotation;
	private bool alreadyGotPlayerPos = false, coroutineStarted = false, guiReadyToDisplay = false;
	private Rect levelCompleteRect;
	private string levelCompleteText = "Level Complete";


	void Start ()
	{
		float xx = Screen.width / 10;
		float yy = Screen.height / 10;
		customGUIStyle.fontSize = System.Convert.ToInt32(Screen.height * .18f);
		customGUIStyle.alignment = TextAnchor.MiddleRight;
		levelCompleteRect  = new Rect(xx * 8.7f, yy * 0.4f, xx, xx);
	}

	void Update () 
	{

		if (levelEnded)
		{
			if (!alreadyGotPlayerPos)
			{
				playerPos = GameObject.FindGameObjectWithTag(Tags.PLAYER).GetComponent<Transform>().position;
				cameraPos = GameObject.FindGameObjectWithTag(Tags.MAIN_CAMERA).GetComponent<Transform>().position;
				cameraRotation = GameObject.FindGameObjectWithTag(Tags.MAIN_CAMERA).GetComponent<Transform>().rotation.eulerAngles;
				alreadyGotPlayerPos = true;

				GameObject.FindGameObjectWithTag(Tags.PLAYER).GetComponent<PlayerMovementBasic>().enabled = false;
				GameObject.FindGameObjectWithTag(Tags.MAIN_CAMERA).GetComponent<CameraMovement3D>().enabled = false;
			}
			else
			{
				if (!coroutineStarted)
					StartCoroutine(moveCamera());
			}
		}
	}

	IEnumerator moveCamera()
	{
		coroutineStarted = true;
		Vector3 targetPosition = playerPos + offset;
		Vector3 targetRotation = new Vector3(0, 270, 0);
		GameObject.FindGameObjectWithTag(Tags.PLAYER).GetComponent<Transform>().rotation = Quaternion.Euler(0,90,0);
//		GameObject.FindGameObjectWithTag(Tags.MAIN_CAMERA).GetComponent<Transform>().rotation = Quaternion.Euler(0, 270, 0);

		while (cameraPos != targetPosition)
		{
			cameraPos = Vector3.MoveTowards(cameraPos, targetPosition, speed*Time.deltaTime);

			GameObject.FindGameObjectWithTag(Tags.MAIN_CAMERA).GetComponent<Transform>().position = cameraPos;
			yield return new WaitForSeconds(0.01f);
		}

		while (cameraRotation != targetRotation)
		{
			cameraRotation = Vector3.MoveTowards(cameraRotation, targetRotation, speed*Time.deltaTime*200);
			
			GameObject.FindGameObjectWithTag(Tags.MAIN_CAMERA).GetComponent<Transform>().rotation = Quaternion.Euler(cameraRotation);
			yield return new WaitForSeconds(0.01f);
		}

		guiReadyToDisplay = true;
	}


	void OnGUI ()
	{
		if (guiReadyToDisplay)
		{
			GUI.Label(levelCompleteRect, levelCompleteText, customGUIStyle);
		}
	}
}
