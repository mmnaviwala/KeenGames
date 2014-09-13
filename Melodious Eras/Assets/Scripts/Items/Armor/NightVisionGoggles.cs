using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NightVisionGoggles : MonoBehaviour {
	public Suit playerSuit;
	public float drainRate = 5;

	private NightVisionTestCS nightvision;
	private Bloom bloom;
	private Fisheye fisheye;
	private DepthOfFieldScatter dof;
	public bool activated = false;
	public Camera cam;

	private YieldInstruction endOfFrame = new WaitForEndOfFrame();

    private Shader outlineShader;
    private Dictionary<CharacterStats, Shader> charactersInRange = new Dictionary<CharacterStats, Shader>();


    private SphereCollider xraySphere;
    public LayerMask xrayLayer;

    private void Awake()
    {
        xraySphere = this.GetComponent<SphereCollider>();
        xraySphere.enabled = false;
    }

	// Use this for initialization
	void Start () 
    {
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
        outlineShader = Shader.Find("Outlined/Silhouetted Bumped Diffuse");


        
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
				float drain = Time.deltaTime * drainRate;
				playerSuit.batteryLife = (playerSuit.batteryLife > drain) ? playerSuit.batteryLife - drain : 0;
			}
		}
	}

    /// <summary> When entering the sphere collider, a character will become outlined </summary>
    void OnTriggerEnter(Collider other)
    {
        CharacterStats character = other.GetComponent<CharacterStats>();
        if (character)
        {
            Material mat = character.GetComponentInChildren<SkinnedMeshRenderer>().material;
            charactersInRange.Add(character, mat.shader); //Storing default shader for when the effect wears off
            OutlineCharacter(character, mat);
        }
    }

    /// <summary>  When exiting the sphere collider, a character's outline will disappear </summary>
    void OnTriggerExit(Collider other)
    {
        CharacterStats character = other.GetComponent<CharacterStats>();
        if (character && charactersInRange[character])
        {
            DeOutlineCharacter(character);
            charactersInRange.Remove(character);
        }
    }

    /// <summary> Sets the character's renderer to the outline renderer and stores their default renderer for when the effect wears off </summary>
    private void OutlineCharacter(CharacterStats character, Material mat)
    {
        mat.shader = Shader.Find("Outlined/Silhouetted Bumped Diffuse");
        mat.SetVector("_OutlineColor", new Vector4(0, 1, 1, 1)); //Cyan outline
        mat.SetFloat("_Outline", 0.0015f);
    }

    /// <summary> Sets the character's renderer to the outline renderer and stores their default renderer for when the effect wears off </summary>
    private void DeOutlineCharacter(CharacterStats character)
    {
        Shader default_shader = charactersInRange[character];
        Renderer current_renderer = character.GetComponentInChildren<SkinnedMeshRenderer>();
        current_renderer.material.shader = default_shader; // = default_shader;
    }

	IEnumerator Toggle()
	{
		if(activated)
        {
            xraySphere.enabled = true;
			while(this.light.intensity < .09f)
			{
				this.light.intensity = Mathf.Lerp (this.light.intensity, .1f, 2 * Time.deltaTime);
				yield return endOfFrame;
			}
		}
		else
		{
            xraySphere.enabled = false;
            foreach (CharacterStats c in charactersInRange.Keys)
                DeOutlineCharacter(c);
            charactersInRange.Clear();

			this.light.intensity = 0;
		}
	}
}
