using UnityEngine;
using System.Collections;

public class LaserPistol : SemiAutoWeapon {

	// Use this for initialization
    void Start()
    {
        if (barrelExit == null)
            barrelExit = this.transform.FindChild("barrel_exit");
        shotFiredTime = nextShotTime = Time.time;
        mainCam = Camera.main;
        Debug.Log(barrelExit.name);
        centerScreen = new Vector3(mainCam.pixelWidth / 2, mainCam.pixelHeight / 2, mainCam.nearClipPlane);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
