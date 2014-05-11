using UnityEngine;
using System.Collections;

[AddComponentMenu("Scripts/Environment/Environment")]
public static class Environment
{
    public static Vector3 wind; //wind direction/speed
    private static float turbulence; //variation in wind speed

    private static float magX, magY, magZ; //for wind.
    public static float globalDustLevel = 0f;

    public static void SetWind(Vector3 windDirection)
    {
        wind = windDirection;
        magX = wind.x;
        magY = wind.y;
        magZ = wind.z;
        turbulence = 0;
    }
    public static void SetWind(Vector3 windDirection, float turbulenceP)
    {
        wind = windDirection;
        magX = wind.x;
        magY = wind.y;
        magZ = wind.z;
        turbulence = turbulenceP;
    }

	static YieldInstruction waitTime = new WaitForSeconds(Time.fixedDeltaTime * 100);
    public static IEnumerator BlowWind()
    {
        while (!wind.Equals(Vector3.zero))
        {
            wind = new Vector3(magX + Random.Range(-turbulence, turbulence), magY + Random.Range(-turbulence, turbulence), magZ + Random.Range(-turbulence, turbulence));
            yield return waitTime;
        }
    }
}
