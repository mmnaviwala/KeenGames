using UnityEngine;
using System.Collections;


//Used for turning ambient light up/down
public class AmbientLightTrigger : MonoBehaviour 
{
	public Color ambientLight1, ambientLight2;
	public TriggerDirection TriggerDirection1; //turns lights on
	public TriggerDirection TriggerDirection2;//turns lights off
	public float fadeSpeed = 2;
	YieldInstruction endOfFrame = new WaitForEndOfFrame();

	/// <summary>
	/// Just checks which direction the player is coming from, then switches appropriately. Same with OnTriggerExit
	/// </summary>
	/// <param name="other"></param>
	void OnTriggerEnter(Collider other)
	{
		Debug.Log("Triggered");
		if(other is CapsuleCollider && other.tag == Tags.PLAYER)
		{
			Vector3 relPosition = other.transform.position - this.transform.position;
			
			switch(TriggerDirection1)
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
			
			switch(TriggerDirection2)
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
	{
        if(other is CapsuleCollider && other.tag == Tags.PLAYER)
		{
			Vector3 relPosition = other.transform.position - this.transform.position;
			
			switch(TriggerDirection1)
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
			
			switch(TriggerDirection2)
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
	
	void Switch(bool oneOr2)
	{
		Color goal = oneOr2 ? ambientLight1 : ambientLight2;
		StartCoroutine(LerpColor (goal));
	}

    IEnumerator LerpColor(Color goal)
	{
		while(Mathf.Abs(RenderSettings.ambientLight.r - RenderSettings.ambientLight.r) > .005)
		{
			RenderSettings.ambientLight = Color.Lerp (RenderSettings.ambientLight, goal, fadeSpeed * Time.deltaTime);
			yield return endOfFrame;
		}
    }

    #region Editor functions
    void OnDrawGizmos()
    {
        if (transform.parent)
        {
            Gizmos.color = new Color(0.5f, 0.5f, 0.5f, 0.15f);
            Gizmos.DrawCube(collider.bounds.center, collider.bounds.size);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (transform.parent)
        {
            Gizmos.color = new Color(0.5f, 0.5f, .5f, 0.5f);
            Gizmos.DrawCube(collider.bounds.center, collider.bounds.size);
        }
    }
    #endregion
}
