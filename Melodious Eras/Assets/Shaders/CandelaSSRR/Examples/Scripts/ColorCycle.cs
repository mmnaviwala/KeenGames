using UnityEngine;
using System.Collections;

public class ColorCycle : MonoBehaviour {

	public float cycleSpeed = 1.0f;

	private float timer = 0.0f;
	private Color currentColor;
	private Color newColor;

	void Start () 
	{
		currentColor = GetComponent<Renderer>().material.color;
		newColor     = new Color( Random.value, Random.value, Random.value, 1.0f );
	}
	
	
	void Update () 
	{
		timer += Time.deltaTime*cycleSpeed;
		if (timer >= 1.0f)//change the float value here to change how long it takes to switch.
		{
			// pick a random color
			currentColor = GetComponent<Renderer>().material.color;
			newColor = new Color( Random.value, Random.value, Random.value, 1.0f );
			timer = 0;
		}
		GetComponent<Renderer>().material.color = Color.Lerp(currentColor, newColor, timer);
	}
}
