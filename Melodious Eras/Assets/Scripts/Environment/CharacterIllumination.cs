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

    void Awake()
    {
        var myCollider = GetComponent<SphereCollider>();
        myLight = GetComponent<Light>();

        myCollider.isTrigger = true;
        myCollider.radius = myLight.range;
    }

	// Update is called once per frame
	void Update ()
    {
        if (!myLight.enabled)
            this.enabled = false;
        
	    foreach(var character in affectedCharacters)
        {
            Collider charCollider = character.GetComponent<Collider>();

            Vector3 charPosCenter = charCollider.bounds.center;
            Vector3 charPosUpper = charCollider.bounds.max;

            if (Physics.Linecast(transform.position, charPosCenter, lightLayers) ||
                Physics.Linecast(transform.position, charPosUpper, lightLayers))
            {
                //illuminate the character, based on light intensity
            }
        }
	}

    void OnTriggerEnter(Collider other)
    {
        var character = other.GetComponent<CharacterStats>();
        if(character)
        {
            affectedCharacters.Add(character);            
        }
    }
    void OnTriggerExit(Collider other)
    {
        var character = other.GetComponent<CharacterStats>();
        if (character)
        {
            affectedCharacters.Remove(character);
        }
    }
}
