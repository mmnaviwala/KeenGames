using UnityEngine;
using System.Collections;

[AddComponentMenu("Scripts/Environment/Circuitry/Circuit Light")]
public class CircuitLight : CircuitNode
{
    public Light lightbulb;
	public LensFlare lensFlare;
    public bool flickering = false;
    public float frequency = .5f;
	private Transform cam;
	public float flareDistance = 10, flareBrightness = 1;
	// Use this for initialization

    void Awake()
    {
		cam = Camera.main.transform;
        if (this.electricGrid != null)
            electricGrid.connectedObjects.Add(this);
        if (lightbulb == null) 
		{
            lightbulb = this.light;
			if(lightbulb == null)
				lightbulb = this.transform.GetComponentInChildren<Light>();
		}
		if(lensFlare == null)
			lensFlare = this.GetComponent<LensFlare>();
		//this.GetComponent<SphereCollider>().radius = lightbulb.range;
    }
	void Start () 
    {
        if(this.lightbulb != null)
            lightbulb.enabled = this.hasPower && this.activated && !this.isBroken;
        if (this.lightbulb != null && this.hasPower && !isBroken && this.activated && flickering)
            StartCoroutine(Flicker());
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

	void OnTriggerEnter(Collider col)
	{

	}
	void OnTriggerExit(Collider col)
	{

	}

    IEnumerator Flicker()
    {
        float defaultIntensity = this.lightbulb.intensity;
        while (this.hasPower && activated && !isBroken)
        {
            this.lightbulb.intensity = Random.value * defaultIntensity;
            //this.lightbulb.enabled = false;
            yield return new WaitForSeconds(.1f);
            //this.lightbulb.enabled = true;
            this.lightbulb.intensity = defaultIntensity;
            yield return new WaitForSeconds(Random.Range(0f, frequency));
        }
        this.lightbulb.enabled = false; //light needs to turn off once power is lost
    }
    public override bool PerformSwitchAction(bool signal)
    {
        activated = signal && !isBroken;
        this.lightbulb.enabled = hasPower && signal && !isBroken;
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
                this.lightbulb.enabled = false;
                this.isBroken = true;
            }
        }
    }
}
