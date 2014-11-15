using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum DifficultyLevel { Easy = 0, Medium = 1, Hard = 2 , Realistic = 3}
public enum QualityLevel_5FS { VeryFast, Fast, Simple, Good, High, VeryHigh, Ultra};
[AddComponentMenu("Scripts/Utilities/Game Controller")]
public class GameController : MonoBehaviour
{
    public DifficultyLevel difficulty = DifficultyLevel.Medium;

	//Level settings; consider putting in seperate class
    public Vector3 wind;
	public Color32 ambientLight = new Color32(51, 51, 51, 255);
    public float turbulence = 0;
    public float shadowDistance = -1;
    private static float defaultShadowDistance = 100;
	public float farClipDistance = -1;
    [Range(0, 1)] public float dustLevel = 0f;



    // Use this for initialization
    void Awake()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
		//this.ambientLight = RenderSettings.ambientLight;
        RenderSettings.ambientLight = this.ambientLight;
        //Physics.gravity = new Vector3(0f, -49f, 0f);
        AudioListener.pause = false;
        Time.timeScale = 1;
		QualitySettings.shadowDistance = (shadowDistance == -1) ? defaultShadowDistance : shadowDistance;
		if(farClipDistance != -1)
			Camera.main.farClipPlane = farClipDistance;

        InitializeEnvironment();
        XMLUtilities.Test();
		HashIDs.Initialize();

        Difficulty.AdjustDifficultSettings(difficulty);
    }


    void InitializeEnvironment()
    {
        Environment.SetWind(this.wind, this.turbulence);
        Environment.globalDustLevel = this.dustLevel;
        StartCoroutine(Environment.BlowWind());
    }

    public static void ShowCursor(bool show)
    {
        Cursor.visible = show;
        Screen.lockCursor = !show;
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

public static class Difficulty
{
    private static float _attackSpeedMultiplier; //enemies only; lower = harder
    private static float _playerDamageMultiplier,   _enemyDamageMultiplier;
    private static float _playerRegenWait,          _enemyRegenWait;
    private static float _playerRegenSpeed,         _enemyRegenSpeed;



    public static float attackSpeedMultiplier { get { return _attackSpeedMultiplier; } }

    public static float playerDamageMultiplier { get { return _playerDamageMultiplier; } }
    public static float enemyDamageMultiplier { get { return _enemyDamageMultiplier; } }

    public static float playerRegenWait { get { return _playerRegenWait; } }
    public static float enemyRegenWait { get { return _enemyRegenWait; } }

    public static float playerRegenSpeed { get { return _playerRegenSpeed; } }
    public static float enemyRegenSpeed { get { return _enemyRegenSpeed; } }

    public static void AdjustDifficultSettings(DifficultyLevel _difficulty)
    {
        switch (_difficulty)
        {
            case DifficultyLevel.Easy:
                _attackSpeedMultiplier = 1.50f;

                _enemyDamageMultiplier = 0.5f;
                _playerDamageMultiplier = 1.50f;

                _playerRegenWait = 5f;
                _enemyRegenWait = 15f;

                _playerRegenSpeed = 10f;
                _enemyRegenSpeed = 5f;
                break;
            case DifficultyLevel.Medium:
                _attackSpeedMultiplier = 1.00f;

                _enemyDamageMultiplier = 0.75f;
                _playerDamageMultiplier = 1.00f;

                _playerRegenWait = 10f;
                _enemyRegenWait = 10f;

                _playerRegenSpeed = 7.5f;
                _enemyRegenSpeed = 7.5f;
                break;
            case DifficultyLevel.Hard:
                _attackSpeedMultiplier = 0.75f;

                _enemyDamageMultiplier = 1.50f;
                _playerDamageMultiplier = 0.75f;

                _playerRegenWait = 12.5f;
                _enemyRegenWait = 7.5f;

                _playerRegenSpeed = 5f;
                _enemyRegenSpeed = 10f;
                break;
            case DifficultyLevel.Realistic:
                _attackSpeedMultiplier = 0.5f;

                _enemyDamageMultiplier = 1.75f;
                _playerDamageMultiplier = 1.50f;

                _playerRegenWait = 20f;
                _enemyRegenWait = 20f;

                _playerRegenSpeed = 2.5f;
                _enemyRegenSpeed = 2.5f;
                break;
        }
    }
}