using UnityEngine;
using System.Collections;

public class SwitchPerspectives : MonoBehaviour 
{
    public enum NewPerspective { Sidescroller};
    public NewPerspective newPerspective;
    public Vector3 newOffset;
    public float smoothness = -1;
    
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == Tags.PLAYER)
        {
            switch(newPerspective)
            {
                case NewPerspective.Sidescroller:
                    CameraMovement2D perspective = Camera.main.GetComponent<CameraMovement2D>();
                    //Disabling other camera scripts if not already on the current one
                    if (!perspective.enabled)
                    {
                        CameraMovement[] perspectives = Camera.main.GetComponents<CameraMovement>();
                        for (int p = 0; p < perspectives.Length; p++)
                            perspectives[p].enabled = false;

                        perspective.enabled = true;
                    }
                    perspective.SetOffset(newOffset);
                    if(smoothness != -1)
                        perspective.smoothness = this.smoothness;
                    break;
            }
        }
    }

    /*void OnTriggerEnter(Collider other)
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
    }*/
}
