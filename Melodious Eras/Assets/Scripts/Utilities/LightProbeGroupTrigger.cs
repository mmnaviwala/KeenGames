using UnityEngine;
using System.Collections;

public class LightProbeGroupTrigger : MonoBehaviour 
{
	void OnTriggerEnter(Collider other)
	{
		if(!other.isTrigger && !other.gameObject.isStatic)
		{
			if(other.renderer != null)
				other.renderer.useLightProbes = true;
			else
			{
				Renderer[] renderers = other.GetComponentsInChildren<Renderer>();
				for(int r = 0; r < renderers.Length; r++)
					renderers[r].useLightProbes = true;
			}
		}
	}
	void OnTriggerExit(Collider other)
	{
		if(!other.isTrigger && !other.gameObject.isStatic)
		{
			if(other.renderer != null)
				other.renderer.useLightProbes = false;
			else
			{
				Renderer[] renderers = other.GetComponentsInChildren<Renderer>();
				for(int r = 0; r < renderers.Length; r++)
					renderers[r].useLightProbes = false;
			}
		}
	}
}
