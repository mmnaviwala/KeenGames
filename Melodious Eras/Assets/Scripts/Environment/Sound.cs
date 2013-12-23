using UnityEngine;
using System.Collections;

[AddComponentMenu("Scripts/Environment/Sound")]
public class Sound : MonoBehaviour 
{
    SphereCollider soundSphere;
	// Use this for initialization
	void Start () {
        soundSphere = this.GetComponent<SphereCollider>();
        soundSphere.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    /// <summary>
    /// Emits a sound with a radius of 10
    /// </summary>
    public void Emit()
    {
        
    }
    /// <summary>
    /// Emits a sound with a certain radius
    /// </summary>
    /// <param name="radius"></param>
    public void Emit(float radius)
    {
        soundSphere.enabled = true;
        soundSphere.radius = radius;
    }
}
