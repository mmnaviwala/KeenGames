using UnityEngine;
using System.Collections;
public class CameraShake : MonoBehaviour
{
    
    private Vector3 originPosition;
    private Quaternion originRotation;
    public float shakeDecay;
    public float shakeIntensity;
    Vector3 cameraOffset;

    Transform player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag(Tags.PLAYER).transform;
        cameraOffset = this.transform.position - player.position - new Vector3(0f, 0f, 0f);
        Debug.Log(cameraOffset);
        this.audio.Play();
    }

    void Update()
    {
        if (shakeIntensity > 0)
        {
            //transform.position = player.position + cameraOffset + Random.insideUnitSphere * shakeIntensity;
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
        shakeIntensity = .75f;
        shakeDecay = 0.01f;
    }
}