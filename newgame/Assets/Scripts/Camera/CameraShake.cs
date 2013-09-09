using UnityEngine;
using System.Collections;
public class CameraShake : MonoBehaviour
{
    
    private Vector3 originPosition;
    private Quaternion originRotation;
    public float shakeDecay;
    public float shakeIntensity;

    void OnGUI()
    {
        if (GUI.Button(new Rect(20, 40, 80, 20), "Shake"))
        {
            //Mathf.PerlinNoise(
            Shake();
        }
    }

    void Update()
    {
        if (shakeIntensity > 0)
        {
            transform.position = originPosition + Random.insideUnitSphere * shakeIntensity;
            transform.rotation = new Quaternion(
                originRotation.x + Random.Range(-shakeIntensity, shakeIntensity) * .05f,
                originRotation.y + Random.Range(-shakeIntensity, shakeIntensity) * .05f,
                originRotation.z + Random.Range(-shakeIntensity, shakeIntensity) * .05f,
                originRotation.w + Random.Range(-shakeIntensity, shakeIntensity) * .05f);
            shakeIntensity -= shakeDecay;
        }
    }

    public void Shake()
    {
        originPosition = transform.position;
        originRotation = transform.rotation;
        shakeIntensity = .5f;
        shakeDecay = 0.005f;
    }
}