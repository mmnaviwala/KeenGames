using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum Difficulty { Easy = 0, Medium = 1, Hard = 2 , Realistic = 3}
public enum QualityLevel_5FS { VeryFast, Fast, Simple, Good, High, VeryHigh, Ultra};
[AddComponentMenu("Scripts/Utilities/Game Controller")]
public class GameController : MonoBehaviour
{
    public Difficulty difficulty = Difficulty.Medium;

	//Level settings; consider putting in seperate class
    public Vector3 wind;
	public Color32 ambientLight = new Color32(51, 51, 51, 255);
    public float turbulence = 0;
    public float shadowDistance = -1;
    private static float defaultShadowDistance = 100;
	public float farClipDistance = -1;

    private static float _difficulty_attackSpeedMultiplier; //lower = harder
    private static float _difficulty_playerDamageMultiplier;//lower = harder
    private static float _difficulty_enemyDamageMultiplier; //higher = harder

    public static float difficulty_attackSpeedMultiplier  { get { return _difficulty_attackSpeedMultiplier;  } }
    public static float difficulty_playerDamageMultiplier { get { return _difficulty_playerDamageMultiplier; } }
    public static float difficulty_enemyDamageMultiplier  { get { return _difficulty_enemyDamageMultiplier;  } }

    // Use this for initialization
    void Awake()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
		this.ambientLight = RenderSettings.ambientLight;
        //Physics.gravity = new Vector3(0f, -49f, 0f);
        AudioListener.pause = false;
        Time.timeScale = 1;
		QualitySettings.shadowDistance = (shadowDistance == -1) ? defaultShadowDistance : shadowDistance;
		if(farClipDistance != -1)
			Camera.main.farClipPlane = farClipDistance;

        InitializeEnvironment();
        XMLUtilities.Test();
		HashIDs.Initialize();

        AdjustDifficultSettings(difficulty);
    }


    void InitializeEnvironment()
    {
        Environment.SetWind(this.wind, this.turbulence);
        StartCoroutine(Environment.BlowWind());
    }

    public static void ShowCursor(bool show)
    {
        Screen.showCursor = show;
        Screen.lockCursor = !show;
    }

    public void AdjustDifficultSettings(Difficulty _difficulty)
    {
        switch (_difficulty)
        {
            case Difficulty.Easy:
                _difficulty_attackSpeedMultiplier =  1.50f;
                _difficulty_enemyDamageMultiplier =  0.75f;

                _difficulty_playerDamageMultiplier = 1.50f;
                break;
            case Difficulty.Medium:
                _difficulty_attackSpeedMultiplier =  1.00f;
                _difficulty_enemyDamageMultiplier =  1.00f;

                _difficulty_playerDamageMultiplier = 1.00f;
                break;
            case Difficulty.Hard:
                _difficulty_attackSpeedMultiplier =  0.75f;
                _difficulty_enemyDamageMultiplier =  1.50f;

                _difficulty_playerDamageMultiplier = 0.75f;
                break;
            case Difficulty.Realistic:
                _difficulty_attackSpeedMultiplier =  0.50f;
                _difficulty_enemyDamageMultiplier =  2.00f;

                _difficulty_playerDamageMultiplier = 1.50f;
                break;
        }
 
    }

    /// <summary>
    /// Modular calculation that takes negative numbers into consideration (-1 % 10 = 9)
    /// </summary>
    /// <param name="x"></param>
    /// <param name="m"></param>
    /// <returns></returns>
    public static int Mod(int x, int m)
    {
        return x < 0 ? x + m : x % m;
    }
}
