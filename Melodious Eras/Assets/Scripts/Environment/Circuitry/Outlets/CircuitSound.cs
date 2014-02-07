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
			this.audio.Stop();
			this.audio.volume = minVolume;
		}
		else
		{
			this.audio.Play();
			this.audio.volume = maxVolume;
		}
	}
	void FixedUpdate()
	{
		if(triggerArea.playerInRange && !this.audio.isPlaying)
		{
			StartCoroutine(FadeSound (true));
		}
		else if (!this.triggerArea.playerInRange && this.audio.isPlaying)
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
			this.audio.Play();
			while(this.audio.volume < goal)
			{
				this.audio.volume = Mathf.Lerp(this.audio.volume, maxVolume, soundFadeSpeed * Time.deltaTime);
				yield return endOfFrame;
			}
			this.audio.volume = maxVolume;
		}
		else
		{
			while(this.audio.volume > goal)
			{
				this.audio.volume = Mathf.Lerp (this.audio.volume, minVolume, soundFadeSpeed * Time.deltaTime);
				yield return endOfFrame;
			}
			this.audio.volume = minVolume;
			this.audio.Stop ();
		}
	}
}