using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NightVisionGoggles : MonoBehaviour {
	public Suit playerSuit;

	public float drainRate = 5;

	private NightVisionTestCS nightvision;
	private Fisheye fisheye;
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


	void Start () 
    {
		if(playerSuit == null)
			playerSuit = this.GetComponentInParent<Suit>();

		if(cam == null) 
			cam = Camera.main;

		nightvision = cam.GetComponent<NightVisionTestCS>();
		fisheye = cam.GetComponent<Fisheye>();
        outlineShader = Shader.Find("Outlined/Silhouetted Bumped Diffuse");
	}
	

	void Update () 
	{		
		if(Input.GetButtonDown(InputType.TOGGLE_NIGHTVISION) && playerSuit.batteryLife > 0)
		{
			activated = !activated;
			this.StopAllCoroutines();
			StartCoroutine(Toggle ());

			nightvision.enabled = activated;
			fisheye.enabled = activated;

		}
		if(activated)
		{
			if(playerSuit.batteryLife == 0)
			{
				activated = false;
				nightvision.enabled = false;
				fisheye.enabled = false;
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
        if (character && !charactersInRange.ContainsKey(character))
        {
            Material mat = character.GetComponentInChildren<SkinnedMeshRenderer>().material;
            charactersInRange.Add(character, mat.shader); //Storing default shader for when the effect wears off
            OutlineCharacter(character, mat);
        }
    }


    /// <summary> When exiting the sphere collider, a character's outline will disappear </summary>
    void OnTriggerExit(Collider other)
    {
        CharacterStats character = other.GetComponent<CharacterStats>();
        if (character && charactersInRange.ContainsKey(character))
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
        current_renderer.material.shader = default_shader;
    }


	IEnumerator Toggle()
	{
		if(activated)
        {
			xraySphere.enabled = true;
			OnTriggerEnter(playerSuit.GetComponent<Collider>());
			while(this.GetComponent<Light>().intensity < .09f)
			{
				this.GetComponent<Light>().intensity = Mathf.Lerp (this.GetComponent<Light>().intensity, .1f, 2 * Time.deltaTime);
				yield return endOfFrame;
			}
		}
		else
		{
			xraySphere.enabled = false;

            foreach (CharacterStats c in charactersInRange.Keys)
                DeOutlineCharacter(c);
			DeOutlineCharacter(playerSuit.wielder);
			charactersInRange.Clear();

			this.GetComponent<Light>().intensity = 0;
		}
	}
}
