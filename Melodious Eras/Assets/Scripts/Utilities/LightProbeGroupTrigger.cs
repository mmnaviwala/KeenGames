using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LightProbeGroupTrigger : MonoBehaviour 
{
	List<DynamicObject> dynamicObjects = new List<DynamicObject>();

	public class DynamicObject {
		public GameObject go;
		public int triggerCount;
		public bool triggered = false;
		public DynamicObject(GameObject go)
		{
			this.go = go;
			triggerCount = 0;
		}

		public void TriggerLightProbes(bool onOff)
		{
			triggered = onOff;
			if(go.GetComponent<Renderer>() != null)
				go.GetComponent<Renderer>().useLightProbes = onOff;
			else
			{
				Renderer[] renderers = go.GetComponentsInChildren<Renderer>();
				for(int r = 0; r < renderers.Length; r++)
					renderers[r].useLightProbes = onOff;
			}
		}
	}
	void OnTriggerEnter(Collider other)
	{
		if(!other.isTrigger && !other.gameObject.isStatic)
		{
			DynamicObject dgo = dynamicObjects.Find((DynamicObject d) => d.go == other.gameObject);
			if( dgo == null)
			{
				dgo = new DynamicObject(other.gameObject);
				dgo.triggerCount++;
				dgo.TriggerLightProbes(true);
				dynamicObjects.Add (dgo);
			}
			else
			{
				dgo.triggerCount++;
				if(!dgo.triggered)
					dgo.TriggerLightProbes(true);
			}
		}
	}
	void OnTriggerExit(Collider other)
	{
		if(!other.isTrigger && !other.gameObject.isStatic)
		{
			DynamicObject dgo = dynamicObjects.Find((DynamicObject d) => d.go == other.gameObject);
			if(dgo != null && --dgo.triggerCount <= 0)
			{
				Debug.Log ("Triggers left: " + dgo.go.name + ", " + dgo.triggerCount);
				if(dgo.triggered)
					dgo.TriggerLightProbes(false);
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
