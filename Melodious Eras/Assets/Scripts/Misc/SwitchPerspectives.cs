using UnityEngine;
using System.Collections;

public class SwitchPerspectives : MonoBehaviour 
{
    public enum NewPerspective { Sidescroller, ThirdPerson};
    public NewPerspective newPerspective;
    public bool useDefaultOffset = false;
    public Vector3 newOffset;
    public Vector3 newRotation = new Vector3(-1, -1, -1);
    public Transform target;
    public float smoothness = -1;
    public bool stopPlayer = false;
    public bool disableHUD = false, hideHUD;
    
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == Tags.PLAYER)
        {
            if (stopPlayer)
                other.GetComponent<PlayerMovementBasic>().Stop(true);
            switch(newPerspective)
            {
                case NewPerspective.Sidescroller:
                    CameraMovement2D perspective2d = Camera.main.GetComponent<CameraMovement2D>();
                    //Disabling other camera scripts if not already on the current one
                    if (!perspective2d.enabled)
                    {
                        CameraMovement[] perspectives = Camera.main.GetComponents<CameraMovement>();
                        for (int p = 0; p < perspectives.Length; p++)
                            perspectives[p].enabled = false;

                        perspective2d.enabled = true;
                    }
                    perspective2d.SetOffset(useDefaultOffset ? CameraMovement2D.defaultOffset : newOffset);

                    if(smoothness != -1)
                        perspective2d.smoothness = this.smoothness;
                    break;

                case NewPerspective.ThirdPerson:
                    CameraMovement3D perspective3d = Camera.main.GetComponent<CameraMovement3D>();
                    //Disabling other camera scripts if not already on the current one
                    if (!perspective3d.enabled)
                    {
                        CameraMovement[] perspectives = Camera.main.GetComponents<CameraMovement>();
                        for (int p = 0; p < perspectives.Length; p++)
                            perspectives[p].enabled = false;
                        perspective3d.enabled = true;
                    }
                    if(target == null)
                        perspective3d.SetOffset(useDefaultOffset ? CameraMovement3D.defaultOffset : newOffset);
                    else
                        perspective3d.SetOffset(useDefaultOffset ? CameraMovement3D.defaultOffset : newOffset, target);


                    if(smoothness != -1)
                        perspective3d.smoothness = this.smoothness;
                    break;
            }
            other.GetComponent<HUD>().enabled = !disableHUD;
            other.GetComponent<HUD>().hide = hideHUD;   
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
