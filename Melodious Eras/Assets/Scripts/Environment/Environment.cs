using UnityEngine;
using System.Collections;

public static class Environment
{
    public static Vector3 wind; //wind direction/speed
    public static float turbulence; //variation in wind speed

    private static float magX, magY, magZ; //for wind.


    public static void SetWind(Vector3 windDirection)
    {
        wind = windDirection;
        magX = wind.x;
        magY = wind.y;
        magZ = wind.z;
    }

    static IEnumerator BlowWind()
    {
        while (!wind.Equals(Vector3.zero))
        {
            wind = new Vector3(magX + Random.Range(-turbulence, turbulence), magY + Random.Range(-turbulence, turbulence), magZ + Random.Range(-turbulence, turbulence));
            yield return new WaitForSeconds(Time.fixedDeltaTime * 100);
            Debug.Log("Fixed delta time: " + Time.fixedDeltaTime);
        }
    }
}
