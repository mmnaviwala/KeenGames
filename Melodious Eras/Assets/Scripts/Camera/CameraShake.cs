using UnityEngine;
using System.Collections;

[AddComponentMenu("Scripts/Camera/Camera Shake")]
public class CameraShake : MonoBehaviour
{
    
    private Vector3 originPosition;
    private Quaternion originRotation;
    public float shakeDecay;
    public float shakeIntensity;
    Vector3 cameraOffset;
    YieldInstruction eof;// = new WaitForEndOfFrame();
    CameraMovement3D cam3d;
    private bool shaking = false;

    Transform player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag(Tags.PLAYER).transform;
        cam3d = this.GetComponent<CameraMovement3D>();
        cameraOffset = this.transform.position - player.position - new Vector3(0f, 0f, 0f);
        this.audio.Play();
        eof = new WaitForEndOfFrame();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            StartCoroutine(Shake(5, 3));
        }
    }
    IEnumerator Shake(float intensity, float duration)
    {
        shaking = true;
        float endTime = Time.time + duration;
        while (Time.time < endTime)
        {
            
            yield return eof;
        }
        shaking = false;
    }
}