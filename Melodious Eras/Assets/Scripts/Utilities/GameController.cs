using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum Difficulty { Easy = 0, Medium = 1, Hard = 2 }
public enum QualityLevel_5FS { VeryFast, Fast, Simple, Good, High, VeryHigh, Ultra};
[AddComponentMenu("Scripts/Utilities/Game Controller")]
public class GameController : MonoBehaviour
{
    public Difficulty difficulty = Difficulty.Medium;
    public Vector3 wind;
	public Color32 ambientLight = new Color32(51, 51, 51, 255);
    public float turbulence = 0;
    public float shadowDistance = -1;
    private static float defaultShadowDistance = 100;

    // Use this for initialization
    void Awake()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        //Physics.gravity = new Vector3(0f, -49f, 0f);
        AudioListener.pause = false;
        Time.timeScale = 1;

        InitializeEnvironment();
        XMLUtilities.Test();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void InitializeEnvironment()
    {
        Environment.SetWind(this.wind, this.turbulence);
        StartCoroutine(Environment.BlowWind());
    }

	[ContextMenu("Bake Settings")]
	public void BakeSettings()
	{
		RenderSettings.ambientLight = this.ambientLight;
	}
}
