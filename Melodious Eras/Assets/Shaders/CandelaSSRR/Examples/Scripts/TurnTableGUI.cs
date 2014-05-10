using UnityEngine;
using System.Collections;

public class TurnTableGUI : MonoBehaviour {

	public GameObject BaseObject;
	public Camera MainCamera;
	public Material mat1;
	public Material mat2;
	public Material mat3;
	public Material mat4;
	public Material mat5;
	public GameObject obj1;
	public GameObject obj2;
	public GameObject obj3;

	//Internal Use
	public GUIStyle labelStyle;
	private bool SSRRToggle = true;
	private bool HQblurToggle = false;
	private bool physicallyAccurateMix = true;

	private CandelaSSRR candelassrr;
	private float SliderRoughness = 1.0F;
	private float SliderReflectivity = 1.0F;
	private float SliderGlobalBlur = 3.77f;

	private Color tablecolor;
	private int currentMatID = 0;
	private int currentObjID = 0;
	private GameObject lastVisibleObject;
	private string materialDescription;

	private string matinfo1;
	private string matinfo2;
	private string matinfo3;
	private string matinfo4;
	private string composeModeType = "(Physically Accurate)";

	void Start ()
	{
		candelassrr = MainCamera.GetComponentInChildren<CandelaSSRR>();
		BaseObject.renderer.material = mat1;
		tablecolor = BaseObject.renderer.material.color;
		currentMatID = 0;
		currentObjID = 0;
		lastVisibleObject = obj1;

		matinfo1 = "Material Based Roughness. A roughness map is applied to the specular map";
		matinfo2 = "Material Based Reflection Masking. Also includes a roughness map applied to the specular map";
		matinfo3 = "Demonstrating Normal Maps, Increase roughness via the left slider & Select HQ Blur to see the difference with a more rough material";
		matinfo4 = "Demonstrating Bumped Cubemap Reflections mixed with SSRR, Physically Accurate Blending mode will mask (occlude) Cubemap reflections"; //cubemap

		materialDescription = matinfo1;
	}


	void OnGUI() 
	{

		GUI.Box(new Rect(Screen.width-200, 20, 150,200),materialDescription,labelStyle);

		GUI.BeginGroup(new Rect(10, 20, 250, 300));
		//TOGGLE SSRR
		SSRRToggle = GUI.Toggle(new Rect(10, 10, 100, 30), SSRRToggle, "SSRR On/Off");
		candelassrr.enabled = SSRRToggle;
		//Roughness Slider
		GUI.Label(new Rect(5, 45, 100, 20), "Mat Roughness",labelStyle);
		SliderRoughness = GUI.HorizontalSlider(new Rect(100, 45, 150, 20), SliderRoughness, 0.03F, 1.0F);
		//Reflectivity Slider
		GUI.Label(new Rect(5, 65, 100, 20), "Mat Reflectivity",labelStyle);
		SliderReflectivity = GUI.HorizontalSlider(new Rect(100, 65, 150, 20), SliderReflectivity, 0.0F, 1.0F);

		//MATERIAL SELECTION
		if (GUI.Button(new Rect(5, 85, 120, 20), "NextMaterial"))
		{
			currentMatID++;
			if(currentMatID > 4) currentMatID = 0;

			switch (currentMatID)
			{
			case 0:
				BaseObject.renderer.material = mat1;
				tablecolor = BaseObject.renderer.material.color;
				materialDescription = matinfo1;
				break;
			case 1:
				BaseObject.renderer.material = mat2;
				tablecolor = BaseObject.renderer.material.color;
				materialDescription = matinfo2;
				break;
			case 2:
				BaseObject.renderer.material = mat3;
				tablecolor = BaseObject.renderer.material.color;
				materialDescription = matinfo2;
				break;
			case 3://NORMAL MAP
				BaseObject.renderer.material = mat4;
				tablecolor = BaseObject.renderer.material.color;
				materialDescription = matinfo3;
				break;
			case 4://CUBEMAP MAP
				BaseObject.renderer.material = mat5;
				tablecolor = BaseObject.renderer.material.color;
				materialDescription = matinfo4;
				break;
			}
		}

		//OBJECT SELECTION
		if (GUI.Button(new Rect(5, 110, 120, 20), "NextObject"))
		{
			currentObjID++;
			if(currentObjID > 2) currentObjID = 0;
			
			switch (currentObjID)
			{
			case 0:
				lastVisibleObject.SetActive(false);
				obj1.SetActive(true);
				lastVisibleObject = obj1;
				break;
			case 1:
				lastVisibleObject.SetActive(false);
				obj2.SetActive(true);
				lastVisibleObject = obj2;
				break;
			case 2:
				lastVisibleObject.SetActive(false);
				obj3.SetActive(true);
				lastVisibleObject = obj3;
				break;
			
			}
		}

		//HIGH QUALITY BLUR TOGGLE
		HQblurToggle = GUI.Toggle(new Rect(5, 140, 120, 30), HQblurToggle, "HQ Blur On/Off");
		candelassrr.BlurQualityHigh = HQblurToggle;
		//GLOBAL BLUR CONTROL
		GUI.Label(new Rect(5, 170, 100, 20), "Global Blur",labelStyle);
		SliderGlobalBlur = GUI.HorizontalSlider(new Rect(100, 170, 150, 20), SliderGlobalBlur, 0.7F, 3.8F);

		physicallyAccurateMix = GUI.Toggle(new Rect(5, 195, 140, 30), physicallyAccurateMix, "Compose Mode");
		GUI.Label(new Rect(118, 201, 150, 30),composeModeType,labelStyle);
		GUI.EndGroup();


		if(GUI.changed)
		{
			BaseObject.renderer.material.SetFloat("_Shininess", SliderRoughness);
			BaseObject.renderer.material.color = new Color(tablecolor.r,tablecolor.g,tablecolor.b,SliderReflectivity);
			candelassrr.GlobalBlurRadius = SliderGlobalBlur;
			if(physicallyAccurateMix)
			{
				candelassrr.SSRRcomposeMode = 1.0f;
				composeModeType = "(Physically Accurate)";
			}
			else
			{
				candelassrr.SSRRcomposeMode = 0.0f;
				composeModeType = "(Additive)";
			}
		}


	}

}
