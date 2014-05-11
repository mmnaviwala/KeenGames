using UnityEngine;
using System.Collections;

[AddComponentMenu("Scripts/Environment/Circuitry/Circuit Light")]
public class CircuitLight : CircuitNode
{
    public Light lightbulb;
	public LensFlare lensFlare;                             //Optional lens flare
    public bool flickering = false;
    public float frequency = .5f;                           //flicker frequency

	private Transform cam;                                  //position used when calculating flare intensity
	public float flareDistance = 10, flareBrightness = 1;   //optional flare stats
    public Light[] ambientLight;                            //Optional (faked) ambient lighting for this light; functionally identical to the lightbulb
    private LightShafts[] lightbulbShafts;

	// Use this for initialization

    void Awake()
    {
		cam = Camera.main.transform;

        if (this.electricGrid != null) //Plugging this node into the electric grid
            this.PlugIn(electricGrid);

        if (lightbulb == null) 
		{
            lightbulb = this.light;
			if(lightbulb == null)
				lightbulb = this.transform.GetComponentInChildren<Light>();
		}
        lightbulbShafts = this.GetComponentsInChildren<LightShafts>();
        for (int s = 0; s < lightbulbShafts.Length; s++)
            lightbulbShafts[s].m_Brightness = Environment.globalDustLevel;

		if(lensFlare == null)
			lensFlare = this.GetComponent<LensFlare>();
		//this.GetComponent<SphereCollider>().radius = lightbulb.range;
    }
	void Start () 
    {
        /*if(this.lightbulb != null)
            lightbulb.enabled = this.hasPower && this.activated && !this.isBroken;
        if (this.lightbulb != null && this.hasPower && !isBroken && this.activated && flickering)
            StartCoroutine(Flicker());*/
        for (int a = 0; a < ambientLight.Length; a++)
            ambientLight[a].color = this.lightbulb.color;
        this.PerformSwitchAction(this.activated); //initializing

	}

	void Update()
	{
		if(this.lightbulb.enabled && lensFlare != null)
		{
			float distance = Vector3.Distance(lightbulb.transform.position, cam.position);
			if(distance < lightbulb.range)
			{
				float intensity = lightbulb.range / 2 / distance;//flareBrightness / distance;
				lensFlare.enabled = true;
				lensFlare.brightness = intensity * intensity;
			}
			else
				lensFlare.enabled = false;
		}
		else if(lensFlare != null)
			lensFlare.enabled = false;
	}

    //Triggers will be used for detecting light levels on player
	void OnTriggerEnter(Collider col)
	{

	}
	void OnTriggerExit(Collider col)
	{

	}

    IEnumerator Flicker()
    {
        float defaultIntensity = this.lightbulb.intensity;
		float[] defaultAmbientIntensities = new float[ambientLight.Length];
		for(int a = 0; a < ambientLight.Length; a++)
			defaultAmbientIntensities[a] = ambientLight[a].intensity;

        while (this.hasPower && activated && !isBroken)
        {
			float f = Random.value;
            this.lightbulb.intensity = f * defaultIntensity;
			for(int a = 0; a < ambientLight.Length; a++)
				ambientLight[a].intensity = f * defaultAmbientIntensities[a];

            yield return new WaitForSeconds(.1f);

			this.lightbulb.intensity = defaultIntensity;
			for(int a = 0; a < ambientLight.Length; a++)
				ambientLight[a].intensity = f * defaultAmbientIntensities[a];

			yield return new WaitForSeconds(Random.Range(0f, frequency));
        }
        this.lightbulb.enabled = false; //light needs to turn off once power is lost
    }
    /// <summary>
    /// If signal == true, activate light. Only enables lightbulb if there's power.
    /// Starts flickering light, if "flickering" is true
    /// </summary>
    /// <param name="signal"></param>
    /// <returns></returns>
    public override bool PerformSwitchAction(bool signal)
    {
        activated = signal && !isBroken;
        this.lightbulb.enabled = activated && hasPower;

		for(int a = 0; a < ambientLight.Length; a++)
			ambientLight[a].enabled = this.lightbulb.enabled;
        for (int s = 0; s < lightbulbShafts.Length; s++)
            lightbulbShafts[s].enabled = this.lightbulb.enabled;

        if (lightbulb.enabled && flickering)
        {
            StartCoroutine(Flicker());
        }
        return false;
    }

    public override void TurnOnOff(bool on)
    {
        PerformSwitchAction(on);
    }

    public override void TakeDamage(int damage)
    {
        if (durability != -1)
        {
            durability -= damage;
            if (durability <= 0)
            {
                //TODO: play breaking sound
                this.lightbulb.enabled = false;
                this.isBroken = true;
				for(int a = 0; a < ambientLight.Length; a++)
                    ambientLight[a].enabled = false;
                for (int s = 0; s < lightbulbShafts.Length; s++)
                    lightbulbShafts[s].enabled = false;
            }
        }
    }

	/// <summary>
	/// Automatically turns lightbulbs on/off when triggered. Avoids updating Activated attribute 
	/// so the lights can be reverted to their previous state if the player comes back to the area
	/// </summary>
	/// <param name="onOff">If set to <c>true</c> on off.</param>
	public void AutoSwitch(bool onOff)
	{
		this.lightbulb.enabled = onOff && hasPower && activated && !isBroken;
		
		for(int a = 0; a < ambientLight.Length; a++)
            ambientLight[a].enabled = this.lightbulb.enabled;
        for (int s = 0; s < lightbulbShafts.Length; s++)
            lightbulbShafts[s].enabled = this.lightbulb.enabled;
		
		if (lightbulb.enabled && flickering)
			StartCoroutine(Flicker());
	}
}
