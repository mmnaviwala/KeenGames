using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//TODO: Currently only works with Point Lights
[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Light))]
public class CharacterIllumination : MonoBehaviour
{
    [SerializeField] LayerMask lightLayers;
    List<CharacterStats> affectedCharacters = new List<CharacterStats>();
    Light myLight;

    //cheating on update costs
    int lol_hax = 0;
    const int EVERY_N_FRAMES = 5;
    float ambientLightIntensity = 0f;

    void Awake()
    {
        var myCollider = GetComponent<SphereCollider>();
        myLight = GetComponent<Light>();

        myCollider.isTrigger = true;
        myCollider.radius = myLight.range;
        
        Color ambient = RenderSettings.ambientLight;
        ambientLightIntensity = Mathf.Max(ambient.r, ambient.g, ambient.b);
    }


	void FixedUpdate ()
    {
        if(lol_hax == 0) //only do this every N = 5 physics frames, effectively cutting cost by 80%
        {
            if (!myLight.enabled)
            {
                this.enabled = false;
                affectedCharacters.ForEach(character => DeIlluminate(character));
                affectedCharacters.Clear();
            }

            affectedCharacters.ForEach(character => Illuminate(character));
        }
        lol_hax = (lol_hax + 1) % EVERY_N_FRAMES;
	}

    void OnTriggerEnter(Collider other)
    {
        var character = other.GetComponent<CharacterStats>();
        if(character)
        {
            affectedCharacters.Add(character);
            Illuminate(character);
        }
    }

    void OnTriggerExit(Collider other)
    {
        var character = other.GetComponent<CharacterStats>();
        if (character)
        {
            affectedCharacters.Remove(character);
            DeIlluminate(character);
        }
    }

    void Illuminate(CharacterStats character)
    {
        var charCollider = character.GetComponent<CapsuleCollider>();

        Vector3 charPosCenter = charCollider.bounds.center;
        Vector3 charPosUpper = charCollider.bounds.max + Vector3.down * 0.2f;
        /*
        if(character.name.Equals("Agent Cipher"))
        {
            Debug.Log(charPosCenter + "\n" + charPosUpper);
        }*/

        if (myLight.shadows == LightShadows.None ||
            Physics.Linecast(transform.position, charPosCenter, lightLayers) ||
            Physics.Linecast(transform.position, charPosUpper, lightLayers))
        {
            //illuminate the character, based on light intensity
            float relativeDistance = 1f - Vector3.Distance(transform.position, charPosCenter) / myLight.range;
            float illumination = myLight.intensity * relativeDistance * relativeDistance;
            if (illumination > character.visibility)
                character.visibility = illumination;
            /*
            if (character.name.Equals("Agent Cipher"))
                Debug.Log(string.Format("Illumination on {0}: {1}", character.name, illumination));
             * */
        }
    }

    void DeIlluminate(CharacterStats character)
    {
        character.visibility = ambientLightIntensity; //if there's another light on the player, it'll fix this in the next physics update
    }
}
