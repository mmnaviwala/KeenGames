using UnityEngine;
using System.Collections;

public class EndOfLevel : MonoBehaviour {

	public bool levelEnded = false;
	public Vector3 offset = new Vector3(1.26f, 1.45f, 0.6f);
	public float speed = 4;
	public GUIStyle levelCompleteGuiStyle, stasticsTextGuiStyle, guiButtonStyle;
	public string stasticsToDisplay;

	private Vector3 playerPos, cameraPos, cameraRotation;
	private bool alreadyGotPlayerPos = false, coroutineStarted = false, guiReadyToDisplay = false;
	private Rect levelCompleteRect, stasticsToDisplayRect;
	private string levelCompleteText = "Level Complete";
	private float xx, yy;


	void Start ()
	{
		xx = Screen.width / 10;
		yy = Screen.height / 10;

		levelCompleteGuiStyle.fontSize = System.Convert.ToInt32(Screen.height * .18f);
		levelCompleteGuiStyle.alignment = TextAnchor.MiddleRight;

		stasticsTextGuiStyle.fontSize = System.Convert.ToInt32(Screen.height * .06f);
		stasticsTextGuiStyle.alignment = TextAnchor.UpperLeft;
		stasticsTextGuiStyle.normal.textColor = Color.white;

		guiButtonStyle.fontSize = System.Convert.ToInt32(Screen.height * .06f);

		levelCompleteRect  = new Rect(xx * 8.7f, yy * 0.4f, xx, xx);
		stasticsToDisplayRect = new Rect(xx * 4.7f, yy * 3.0f, xx*4, xx);

		stasticsToDisplay = "Accuracy: 100%\nTime Elapsed: 09:30\nEnemies Killed: 4\nEnemies Knocked Out: 1\nEnemies Avoided: 5\nSpotted: 0 Times";

        //StartCoroutine(Wait());
	}

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2);
        EndLevel();
    }


    public void EndLevel()
    {
        var player = GameObject.FindGameObjectWithTag(Tags.PLAYER).GetComponent<PlayerMovementBasic>();
        var cam = GameObject.FindGameObjectWithTag(Tags.MAIN_CAMERA).GetComponent<CameraMovement3D>();
        //var hud = GameObject.FindGameObjectWithTag(Tags.PLAYER).GetComponent<HUD_Stealth>();
        player.enabled = false;
        cam.enabled = false;
        //hud.enabled = false;

        playerPos = player.transform.position;
        cameraPos = cam.transform.position;
        cameraRotation = cam.transform.rotation.eulerAngles;
        alreadyGotPlayerPos = true;

        foreach (Transform child in GameObject.Find("Flat_UI HUD").gameObject.transform)
		{
			if (child.GetComponent<FlatUI_HUD>() != null)
            	child.GetComponent<FlatUI_HUD>().enabled = false;
			if (child.GetComponent<DisplayObjectives>() != null)
				child.GetComponent<DisplayObjectives>().enabled = false;
		}

        StartCoroutine(moveCamera());
    }

	IEnumerator moveCamera()
	{
        Screen.showCursor = true;
        Screen.lockCursor = false;
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
			GUI.Label(levelCompleteRect, levelCompleteText, levelCompleteGuiStyle);
			GUI.Label(stasticsToDisplayRect, stasticsToDisplay, stasticsTextGuiStyle);
			if (GUI.Button (new Rect (xx * 3.2f, yy * 8.0f, xx * 7f, yy * 1.5f), "Go To Next Level", guiButtonStyle))
			{
				Debug.Log(Application.loadedLevel);
				if (Application.loadedLevel + 1 <= Application.levelCount)
					Application.LoadLevel(Application.loadedLevel + 1);
				else
					Application.LoadLevel(1);
			}
		}
	}
}
