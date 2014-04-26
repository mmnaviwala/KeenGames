using UnityEngine;
using System.Collections;

public class EndOfLevel : MonoBehaviour {

	public bool levelEnded = false;
	public Vector3 offset = new Vector3(1.26f, 1.45f, 0.6f);
	public float speed = 4;
	public GUIStyle levelCompleteGuiStyle, stasticsTextGuiStyle, guiButtonStyle;
	public string stasticsToDisplay;

	private Vector3 playerPos, cameraPos, cameraRotation;
	private bool guiReadyToDisplay = false;
	private Rect levelCompleteRect, stasticsToDisplayRect;
    private Rect nextLevelRect, replayRect;
	private string levelCompleteText = "Level Complete";
	private float xx, yy;

    private YieldInstruction eof = new WaitForEndOfFrame();


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
        //nextLevelRect = new Rect(xx * 3.2f, yy * 8.0f, xx * 7f, yy * 1.5f);
        nextLevelRect = new Rect(xx * 6.2f, yy * 8.0f, xx * 3.5f, yy * 1.5f);
        replayRect = new Rect(xx * 3, yy * 8, xx * 3, yy * 1.5f);

		stasticsToDisplay = "Accuracy: 100%\nTime Elapsed: 09:30\nEnemies Killed: 4\nEnemies Knocked Out: 1\nEnemies Avoided: 5\nSpotted: 0 Times";
        
	}

    IEnumerator test()
    {
        yield return new WaitForSeconds(2);
        EndLevel(false);
    }


    public void EndLevel(bool didPlayerDie)
    {
        Screen.showCursor = true;
        Screen.lockCursor = false;
        var player = GameObject.FindGameObjectWithTag(Tags.PLAYER).GetComponent<PlayerMovementBasic>();
        var cam = GameObject.FindGameObjectWithTag(Tags.MAIN_CAMERA).GetComponent<CameraMovement3D>();
        //var hud = GameObject.FindGameObjectWithTag(Tags.PLAYER).GetComponent<HUD_Stealth>();
        player.enabled = false;
        cam.enabled = false;
        //hud.enabled = false;

        playerPos = player.transform.position;
        cameraPos = cam.transform.position;
        cameraRotation = cam.transform.eulerAngles;

        Transform hud = GameObject.FindGameObjectWithTag(Tags.GAME_CONTROLLER).transform.FindChild("Flat_UI HUD");
        foreach (Transform child in hud)
		{
			if (child.GetComponent<FlatUI_HUD>() != null)
            	child.GetComponent<FlatUI_HUD>().enabled = false;
			if (child.GetComponent<DisplayObjectives>() != null)
				child.GetComponent<DisplayObjectives>().enabled = false;
		}

        StartCoroutine(moveCamera(player, cam, didPlayerDie));
    }

	IEnumerator moveCamera(PlayerMovementBasic player, CameraMovement3D cam, bool didPlayerDie)
	{
        Screen.showCursor = true;
        Screen.lockCursor = false;

		Vector3 targetPosition = playerPos + 
                                 player.transform.forward * 2 + 
                                 Vector3.up * 1.5f;

        Vector3 _offset = didPlayerDie ? 
            -player.transform.right : //looks down at player if he's dead
            Vector3.up * 1.5f - player.transform.right; //looks up at player


		while ((cameraPos - targetPosition).sqrMagnitude > .1f)
		{
			cameraPos = Vector3.Lerp(cameraPos, targetPosition, speed * Time.deltaTime);
            cam.transform.position = cameraPos;
            cam.transform.LookAt(player.transform.position + _offset);
			yield return eof;
		}

		guiReadyToDisplay = true;
	}


	void OnGUI ()
	{
		if (guiReadyToDisplay)
		{
			GUI.Label(levelCompleteRect, levelCompleteText, levelCompleteGuiStyle);
			GUI.Label(stasticsToDisplayRect, stasticsToDisplay, stasticsTextGuiStyle);

            if (GUI.Button(nextLevelRect, "Go To Next Level", guiButtonStyle))
			{
				if (Application.loadedLevel + 1 <= Application.levelCount)
					Application.LoadLevel(Application.loadedLevel + 1);
				else
					Application.LoadLevel(1);
			}
            if (GUI.Button(replayRect, "Replay", guiButtonStyle))
            {
                Application.LoadLevel(Application.loadedLevel);
            }
		}
	}
}
