using UnityEngine;
using System.Collections;

public class SwitchPerspectives : MonoBehaviour 
{
    public enum NewPerspective { Cam2D, Cam2DInverted };
    public NewPerspective newPerspective;

    void OnTriggerEnter(Collider other)
    {
		if(other.tag == Tags.PLAYER)
		{
	        CameraMovement[] perspectives = Camera.main.GetComponents<CameraMovement>();
	        switch (newPerspective)
	        {
	            case NewPerspective.Cam2D:
	                for (int p = 0; p < perspectives.Length; p++)
	                {
	                    if (perspectives[p].GetType() == typeof(CameraMovement2D))
	                        perspectives[p].enabled = true;
	                    else
	                        perspectives[p].enabled = false;
	                    
	                }
	                break;
	            case NewPerspective.Cam2DInverted:
	                for (int p = 0; p < perspectives.Length; p++)
	                {
	                    if (perspectives[p].GetType() == typeof(CameraMovement2DInverted))
	                        perspectives[p].enabled = true;
	                    else
	                        perspectives[p].enabled = false;
	                }
	                break;
	        }
	        Debug.Log("# Components: " + perspectives.Length);
		}
    }
}
