using UnityEngine;
using System.Collections;

[AddComponentMenu("Scripts/Utilities/Screen Fade In Out")]
public class ScreenFadeInOut : MonoBehaviour
{
    public float fadeSpeed = 1.5f;
    private bool sceneStarting = true;

    void Awake()
    {
        GetComponent<GUITexture>().pixelInset = new Rect(0f, 0f, Screen.width, Screen.height);
    }
    void Update()
    {
        GetComponent<GUITexture>().pixelInset = new Rect(0f, 0f, Screen.width, Screen.height);
        if (sceneStarting)
            StartScene();
    }

    void FadeToClear()
    {
        GetComponent<GUITexture>().color = Color.Lerp(GetComponent<GUITexture>().color, Color.clear, fadeSpeed * Time.deltaTime);
    }

    void FadeToBlack()
    {
        GetComponent<GUITexture>().color = Color.Lerp(GetComponent<GUITexture>().color, Color.black, fadeSpeed * Time.deltaTime);
    }

    void StartScene()
    {
        FadeToClear();

        if (GetComponent<GUITexture>().color.a <= 0.05f) //see if screen is approx normal (guiTexture is clear)
        {
            GetComponent<GUITexture>().color = Color.clear;
            GetComponent<GUITexture>().enabled = false; //disables the top GuiTexture (now clear) to avoid bad performance
            sceneStarting = false;
        }
    }

    public void Endscene() //won't be called from this script; needs to be public
    {
        GetComponent<GUITexture>().enabled = true;
        FadeToBlack();

        if (GetComponent<GUITexture>().color.a >= 0.95f) //see if screen is approx black (guiTexture is black)
        {
            Application.LoadLevel(1);
        }
    }
	void OnGUI()
	{
		GUI.Button(new Rect(Screen.width/2 - Screen.width/20, Screen.height/2 - Screen.width/20, Screen.width/10, Screen.width/10), "Fade");
	}
}
