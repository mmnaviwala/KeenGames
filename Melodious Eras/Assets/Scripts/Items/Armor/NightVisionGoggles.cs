using UnityEngine;
using System.Collections;

public class NightVisionGoggles : MonoBehaviour {
	public Suit playerSuit;
	public float efficiency = 5;

	private NightVisionTestCS nightvision;
	private Bloom bloom;
	private Fisheye fisheye;
	private DepthOfFieldScatter dof;
	public bool activated = false;
	public Camera cam;

	private YieldInstruction endOfFrame = new WaitForEndOfFrame();

	// Use this for initialization
	void Start () {
		if(playerSuit == null)
		{
			Transform temp_transform = this.transform;
			while(temp_transform.tag != Tags.PLAYER)
			{
				temp_transform = temp_transform.parent;
			}
			playerSuit = temp_transform.GetComponent<Suit>();
		}

		if(cam == null) 
			cam = Camera.main;

		nightvision = cam.GetComponent<NightVisionTestCS>();
		bloom = cam.GetComponent<Bloom>();
		fisheye = cam.GetComponent<Fisheye>();
		dof = cam.GetComponent<DepthOfFieldScatter>();
	}
	
	// Update is called once per frame
	void Update () 
	{		
		if(Input.GetButtonDown(InputType.TOGGLE_NIGHTVISION) && playerSuit.batteryLife > 0)
		{
			activated = !activated;
			this.StopAllCoroutines();
			StartCoroutine(Toggle ());
			nightvision.enabled = activated;
			//bloom.enabled = activated;
			fisheye.enabled = activated;
			//dof.enabled = activated;
			/*if(activated)
			{
				bloom.bloomIntensity = 15;
				bloom.bloomThreshhold = .02f;
				bloom.bloomBlurIterations = 1;
				bloom.hollyStretchWidth = 1.25f;
			}
			else
			{
				bloom.bloomIntensity = .5f;
				bloom.bloomThreshhold = .3f;
				bloom.bloomBlurIterations = 2;
				bloom.hollyStretchWidth = 2.5f;

			}*/
            bloom.enabled = this.activated;
		}
		if(activated)
		{
			if(playerSuit.batteryLife == 0)
			{
				activated = false;
				nightvision.enabled = false;
				//bloom.enabled = false;
				fisheye.enabled = false;
				//dof.enabled = false;

				bloom.bloomIntensity = .5f;
				bloom.bloomThreshhold = .5f;
				bloom.bloomBlurIterations = 2;
				bloom.hollyStretchWidth = 2.5f;
				//Emit noise
			}
			else
			{
				float drain = Time.deltaTime / efficiency;
				playerSuit.batteryLife = (playerSuit.batteryLife > drain) ? playerSuit.batteryLife - drain : 0;
			}
		}
	}

	IEnumerator Toggle()
	{
		if(activated)
		{
			while(this.light.intensity < .09f)
			{
				this.light.intensity = Mathf.Lerp (this.light.intensity, .1f, 2 * Time.deltaTime);
				yield return endOfFrame;
			}
		}
		else
		{
			this.light.intensity = 0;
		}
	}
}
