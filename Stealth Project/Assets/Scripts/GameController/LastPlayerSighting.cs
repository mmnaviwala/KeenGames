using UnityEngine;
using System.Collections;

public class LastPlayerSighting : MonoBehaviour 
{
    public Vector3 position = new Vector3(1000f, 1000f, 1000f); //default; means enemies don't know where the player is
    public static Vector3 resetPosition = new Vector3(1000f, 1000f, 1000f);

    public float lightHighIntensity = 0.25f;
    public float lightLowIntensity = 0f;
    public float fadeSpeed = 7f;
    public float musicFadeSpeed = 1f;

    private AlarmLight alarm;
    private Light mainLight;
    private AudioSource panicAudio;
    private AudioSource[] sirens;

    void Awake()
    {
        //Setup the reference to the alarm light.
        alarm = GameObject.FindGameObjectWithTag(Tags.ALARM).GetComponent<AlarmLight>();
                                                                  //"light" is built-in; don't need to use GetComponent (for custom scripts)
        mainLight = GameObject.FindGameObjectWithTag(Tags.MAIN_LIGHT).light; //mainLight = GameObject.FindGameObjectWithTag(Tags.mainLight).GetComponent<Light>();
        panicAudio = transform.Find("secondaryMusic").audio; //finding the "gameController" GameObject's child ("secondaryMusic"); all transforms contain audio, animation, colliders, etc.

        GameObject[] sirenGameObjects = GameObject.FindGameObjectsWithTag(Tags.SIREN);
        sirens = new AudioSource[sirenGameObjects.Length];

        for (int i = 0; i < sirens.Length; i++)
        {
            sirens[i] = sirenGameObjects[i].audio; //assigning the Audio for each GameObject in sirenGameObjects to the sirens array
        }
    }

    void Update()
    {
        SwitchAlarms();
        MusicFading();
    }

    void SwitchAlarms()
    {
        alarm.alarmOn = (position != resetPosition);
        float newIntensity = alarm.alarmOn ? lightLowIntensity : lightHighIntensity;

        mainLight.intensity = Mathf.Lerp(mainLight.intensity, newIntensity, fadeSpeed * Time.deltaTime);

        for (int i = 0; i < sirens.Length; i++)
        {
            if (alarm.alarmOn && !sirens[i].isPlaying)
                sirens[i].Play();
            else if (position == resetPosition)
                sirens[i].Stop();
        }
    }

    void MusicFading()
    {
        if (position != resetPosition)
        {
            audio.volume = Mathf.Lerp(audio.volume, 0f, musicFadeSpeed * Time.deltaTime);
            panicAudio.volume = Mathf.Lerp(panicAudio.volume, 0.8f, musicFadeSpeed * Time.deltaTime);
        }
        else
        {
            audio.volume = Mathf.Lerp(audio.volume, 0.8f, musicFadeSpeed * Time.deltaTime);
            panicAudio.volume = Mathf.Lerp(panicAudio.volume, 0f, musicFadeSpeed * Time.deltaTime);
        }
    }
}
