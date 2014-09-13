using UnityEngine;
using System.Collections;

[AddComponentMenu("Scripts/Utilities/Screen Fade In Out")]
public class ScreenFadeInOut : MonoBehaviour
{
    public float fadeSpeed = 1.5f;
    private bool sceneStarting = true;

    void Awake()
    {
        guiTexture.pixelInset = new Rect(0f, 0f, Screen.width, Screen.height);
    }
    void Update()
    {
        guiTexture.pixelInset = new Rect(0f, 0f, Screen.width, Screen.height);
        if (sceneStarting)
            StartScene();
    }

    void FadeToClear()
    {
        guiTexture.color = Color.Lerp(guiTexture.color, Color.clear, fadeSpeed * Time.deltaTime);
    }

    void FadeToBlack()
    {
        guiTexture.color = Color.Lerp(guiTexture.color, Color.black, fadeSpeed * Time.deltaTime);
    }

    void StartScene()
    {
        FadeToClear();

        if (guiTexture.color.a <= 0.05f) //see if screen is approx normal (guiTexture is clear)
        {
            guiTexture.color = Color.clear;
            guiTexture.enabled = false; //disables the top GuiTexture (now clear) to avoid bad performance
            sceneStarting = false;
        }
    }

    public void Endscene() //won't be called from this script; needs to be public
    {
        guiTexture.enabled = true;
        FadeToBlack();

        if (guiTexture.color.a >= 0.95f) //see if screen is approx black (guiTexture is black)
        {
            Application.LoadLevel(1);
        }
    }
	void OnGUI()
	{
		GUI.Button(new Rect(Screen.width/2 - Screen.width/20, Screen.height/2 - Screen.width/20, Screen.width/10, Screen.width/10), "Fade");
	}
}
