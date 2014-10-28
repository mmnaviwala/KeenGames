using UnityEngine;
using System.Collections;

public enum TriggerDirection {PosX, NegX, PosY, NegY, PosZ, NegZ}
[RequireComponent(typeof(BoxCollider))]
public class LightTrigger : MonoBehaviour 
{
	public bool startOn = false;
	public CircuitLight[] circuitLights;
	public Light[] lights;
	public TriggerDirection onTriggerDirection; //turns lights on
	public TriggerDirection offTriggerDirection;//turns lights off

	void Start()
	{
		Switch (startOn);
	}

	void OnTriggerEnter(Collider other)
	{
		if(other is CapsuleCollider && other.tag == Tags.PLAYER)
		{
			Vector3 relPosition = other.transform.position - this.transform.position;

			switch(onTriggerDirection)
			{
				case TriggerDirection.PosX: 
					if(relPosition.x > 0)
						Switch (true);
					break;
				case TriggerDirection.NegX: 
					if(relPosition.x < 0)
						Switch (true);
					break;
				case TriggerDirection.PosY: 
					if(relPosition.y > 0)
						Switch (true);
					break;
				case TriggerDirection.NegY: 
					if(relPosition.y< 0)
						Switch (true);
					break;
				case TriggerDirection.PosZ: 
					if(relPosition.z > 0)
						Switch (true);
					break;
				case TriggerDirection.NegZ: 
					if(relPosition.z < 0)
						Switch (true);
					break;
			}

			switch(offTriggerDirection)
			{
				case TriggerDirection.PosX: 
					if(relPosition.x > 0)
						Switch (false);
					break;
				case TriggerDirection.NegX: 
					if(relPosition.x < 0)
						Switch (false);
					break;
				case TriggerDirection.PosY: 
					if(relPosition.y > 0)
						Switch (false);
					break;
				case TriggerDirection.NegY: 
					if(relPosition.y< 0)
						Switch (false);
					break;
				case TriggerDirection.PosZ: 
					if(relPosition.z > 0)
						Switch (false);
					break;
				case TriggerDirection.NegZ: 
						if(relPosition.z < 0)
						Switch (false);
					break;
			}
		}
	}

	void OnTriggerExit(Collider other)
	{if(other is CapsuleCollider && other.tag == Tags.PLAYER)
		{
			Vector3 relPosition = other.transform.position - this.transform.position;
			
			switch(onTriggerDirection)
			{
			case TriggerDirection.PosX: 
				if(relPosition.x < 0)
					Switch (true);
				break;
			case TriggerDirection.NegX: 
				if(relPosition.x > 0)
					Switch (true);
				break;
			case TriggerDirection.PosY: 
				if(relPosition.y < 0)
					Switch (true);
				break;
			case TriggerDirection.NegY: 
				if(relPosition.y> 0)
					Switch (true);
				break;
			case TriggerDirection.PosZ: 
				if(relPosition.z < 0)
					Switch (true);
				break;
			case TriggerDirection.NegZ: 
				if(relPosition.z > 0)
					Switch (true);
				break;
			}
			
			switch(offTriggerDirection)
			{
			case TriggerDirection.PosX: 
				if(relPosition.x < 0)
					Switch (false);
				break;
			case TriggerDirection.NegX: 
				if(relPosition.x > 0)
					Switch (false);
				break;
			case TriggerDirection.PosY: 
				if(relPosition.y < 0)
					Switch (false);
				break;
			case TriggerDirection.NegY: 
				if(relPosition.y> 0)
					Switch (false);
				break;
			case TriggerDirection.PosZ: 
				if(relPosition.z < 0)
					Switch (false);
				break;
			case TriggerDirection.NegZ: 
				if(relPosition.z > 0)
					Switch (false);
				break;
			}
		}

	}

	void Switch(bool onOff)
	{
		for(int i = 0; i < circuitLights.Length; i++)
			circuitLights[i].AutoSwitch(onOff);
		for(int i = 0; i < lights.Length; i++)
			lights[i].enabled = onOff;
	}

	void OnDrawGizmos () {
		if (transform.parent) {
            Gizmos.color = new Color(0, .2f, .8f, .15f);//new Color (0.5f, 0.7f, 0.4f, 0.15f);
			Gizmos.DrawCube (GetComponent<Collider>().bounds.center, GetComponent<Collider>().bounds.size );
		}
	}
	
	void OnDrawGizmosSelected () {
		if (transform.parent) {
            Gizmos.color = new Color(0, .2f, .8f, .5f);//new Color (0.5f, 0.7f, .4f, 0.5f);
			Gizmos.DrawCube (GetComponent<Collider>().bounds.center, GetComponent<Collider>().bounds.size );
		}
	}
}