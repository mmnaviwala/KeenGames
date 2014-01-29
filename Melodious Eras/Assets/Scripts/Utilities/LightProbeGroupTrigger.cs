using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LightProbeGroupTrigger : MonoBehaviour 
{
	List<DynamicObject> dynamicObjects = new List<DynamicObject>();

	public class DynamicObject {
		public GameObject go;
		public int triggerCount;
		public DynamicObject(GameObject go)
		{
			this.go = go;
			triggerCount = 0;
		}
	}
	void OnTriggerEnter(Collider other)
	{
		if(!other.isTrigger && !other.gameObject.isStatic && dynamicObjects.Find((DynamicObject d) => d.go == other.gameObject) == null)
			dynamicObjects.Add (new DynamicObject(other.gameObject));
	}
	void OnTriggerStay(Collider other)
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
//	void OnTriggerEnter(Collider other)
//	{
//		if(!other.isTrigger && !other.gameObject.isStatic)
//		{
//			if(other.renderer != null)
//				other.renderer.useLightProbes = true;
//			else
//			{
//				Renderer[] renderers = other.GetComponentsInChildren<Renderer>();
//				for(int r = 0; r < renderers.Length; r++)
//					renderers[r].useLightProbes = true;
//			}
//		}
//	}
//	void OnTriggerExit(Collider other)
//	{
//		if(!other.isTrigger && !other.gameObject.isStatic)
//		{
//			if(other.renderer != null)
//				other.renderer.useLightProbes = false;
//			else
//			{
//				Renderer[] renderers = other.GetComponentsInChildren<Renderer>();
//				for(int r = 0; r < renderers.Length; r++)
//					renderers[r].useLightProbes = false;
//			}
//		}
//	}
}
