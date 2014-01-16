using UnityEngine;  
using System.Collections; 
using System.Collections.Generic;

public class RaycastReflectionC : MonoBehaviour  
{  
	private Ray ray;   
	private RaycastHit hit;  
	
	//reflection direction  
	private Vector3 inDirection;  
	
	//the number of reflections  
	public int nReflections = 2;  

	public List<Vector3> reflectionPoints = new List<Vector3>();	// ----------------------------------------

	void Update ()  
	{  
		reflectionPoints.Clear();		// ----------------------------------------
		//reflectionPoints.Add(transform.position);		// ----------------------------------------

		//clamp the number of reflections between 1 and int capacity 
		nReflections = Mathf.Clamp(nReflections,1,nReflections); 

		//cast a new ray left, from the current attached game object position  
		ray = new Ray(transform.position, Vector3.left); 

		for(int i=0; i <= nReflections; i++)  
		{  
			if(i == 0)  
			{  
				if(Physics.Raycast(ray.origin,ray.direction, out hit, 100))  
				{   
					Debug.DrawLine(ray.origin, hit.point, Color.red);

					reflectionPoints.Add(ray.origin);		// ----------------------------------------
					reflectionPoints.Add(hit.point);		// ----------------------------------------

					inDirection = Vector3.Reflect(ray.direction, hit.normal);  
					ray = new Ray(hit.point, inDirection);  

					Debug.DrawRay(hit.point, inDirection*100, Color.magenta);

					// add points for the next possible point.
					// if the next point doesnt actually exist, then these points will stay
					// else these points will be replaced by actual hit points
					reflectionPoints.Add(hit.point);						// ~~~~
					reflectionPoints.Add((hit.point + inDirection*100));	// ~~~~
				}
				else
				{
					// if initally not hiting anything, then a default line will be displayed 
					reflectionPoints.Add(ray.origin);						// ----------------------------------------
					reflectionPoints.Add(new Vector3(-15, -3.8f, 0));		// ----------------------------------------
				}
			}  
			else  
			{   
				if(Physics.Raycast(ray.origin,ray.direction, out hit, 100))  
				{  
					reflectionPoints.RemoveAt(reflectionPoints.Count-1);	// ~~~~
					reflectionPoints.RemoveAt(reflectionPoints.Count-1);	// ~~~~

					Debug.DrawLine(ray.origin, hit.point, Color.red);

					reflectionPoints.Add(ray.origin);		// ----------------------------------------
					reflectionPoints.Add(hit.point);		// ----------------------------------------

					inDirection = Vector3.Reflect(inDirection,hit.normal);  
					ray = new Ray(hit.point,inDirection);  
					  
					Debug.DrawRay(hit.point, inDirection*100, Color.magenta);

					reflectionPoints.Add(hit.point);						// ~~~~
					reflectionPoints.Add((hit.point + inDirection*100));	// ~~~~

//					Debug.Log("i = " + i);
//					Debug.Log("Hit Point: " + hit.point);
//					Debug.Log("Direction: " + inDirection);

				}
			}  
		}  

//		Debug.Log(reflectionPoints.Count);
//		for(int i=0; i<reflectionPoints.Count; i++)
//			Debug.Log(reflectionPoints[i]);
	}  




}  