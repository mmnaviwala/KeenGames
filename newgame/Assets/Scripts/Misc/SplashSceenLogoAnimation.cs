using UnityEngine;
using System.Collections;

public class SplashSceenLogoAnimation : MonoBehaviour {
	
	public Texture2D[] logoFrames;
	private AudioSource ding;
    void Awake()
    {
        Application.targetFrameRate = -1; //default value
        Time.timeScale = 1;
    }
	// Use this for initialization
	void Start () 
	{		
		ding = Camera.main.audio;	
		logoFrames = new Texture2D[34];

        for (int frame = 1; frame < 35; frame++)
        {
            string name = (frame < 10 ? "logo/0" : "logo/") + frame;
            logoFrames[frame - 1] = Resources.Load(name, typeof(Texture2D)) as Texture2D;
        }

        StartCoroutine(Animate());
	}
	
	IEnumerator Animate()
	{
		for(int i=0; i<34; i++)
        {
            guiTexture.texture = logoFrames[i];
            if (i == 23)
                ding.Play();

            yield return new WaitForSeconds(i == 0 ? .3f : .1f);
		}
		yield return new WaitForSeconds(2.0F);
		Application.LoadLevel("testTitleMenuQuads");
	}
}
