using UnityEngine;
using System.Collections;

public class moviePlayer : MonoBehaviour {
	
	//[RequireComponent (typeof(AudioSource))]
	// Use this for initialization
	
	public bool LoopMovie   = true;
	public bool PlayOnStart = true;
	
	void Start () {
	
		if(PlayOnStart)
		{
		MovieTexture movie = renderer.material.mainTexture as MovieTexture;
		//audio.clip = movie.audioClip;
		movie.loop = LoopMovie;
		//audio.loop = LoopMovie;
		//audio.Play();
		movie.Play();
		}
		
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
