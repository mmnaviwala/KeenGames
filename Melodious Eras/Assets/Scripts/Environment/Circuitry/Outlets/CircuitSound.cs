using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class CircuitSound : CircuitOutlet 
{
	public AudioClip soundClip;
	public bool useTrigger = false;
	public DetectionSpherePlayer triggerArea;

	public float minVolume = 0f;
	public float maxVolume = 1f;
	public float soundFadeSpeed = 5f;
	YieldInstruction endOfFrame = new WaitForEndOfFrame();

	// Use this for initialization
	void Awake () 
	{
        this.PlugIn(electricGrid);
		//this.audio.clip = soundClip;
		if(useTrigger)
		{
			this.GetComponent<AudioSource>().Stop();
			this.GetComponent<AudioSource>().volume = minVolume;
		}
		else
		{
			this.GetComponent<AudioSource>().Play();
			this.GetComponent<AudioSource>().volume = maxVolume;
		}
	}
	void FixedUpdate()
	{
		if(triggerArea && triggerArea.playerInRange && !this.GetComponent<AudioSource>().isPlaying)
		{
			StartCoroutine(FadeSound (true));
		}
		else if (triggerArea && !this.triggerArea.playerInRange && this.GetComponent<AudioSource>().isPlaying)
		{
			StartCoroutine(FadeSound (false));
		}
	}

    /// <summary>
    /// Sound fades/increases smoothly, to feel realistic.
    /// </summary>
    /// <param name="upDown"></param>
    /// <returns></returns>
	IEnumerator FadeSound(bool upDown)
	{
		float goal = minVolume + (maxVolume - minVolume) * (upDown ? .95f : .05f);

		if(upDown)
		{
			this.GetComponent<AudioSource>().Play();
			while(this.GetComponent<AudioSource>().volume < goal)
			{
				this.GetComponent<AudioSource>().volume = Mathf.Lerp(this.GetComponent<AudioSource>().volume, maxVolume, soundFadeSpeed * Time.deltaTime);
				yield return endOfFrame;
			}
			this.GetComponent<AudioSource>().volume = maxVolume;
		}
		else
		{
			while(this.GetComponent<AudioSource>().volume > goal)
			{
				this.GetComponent<AudioSource>().volume = Mathf.Lerp (this.GetComponent<AudioSource>().volume, minVolume, soundFadeSpeed * Time.deltaTime);
				yield return endOfFrame;
			}
			this.GetComponent<AudioSource>().volume = minVolume;
			this.GetComponent<AudioSource>().Stop ();
		}
	}
}