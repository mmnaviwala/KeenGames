using UnityEngine;
using System.Collections;

public class LightningFlash : MonoBehaviour {

	IEnumerator Start ()
	{
		while (true)
		{
			light.enabled = !(light.enabled); //toggle on/off the enabled property
			yield return new WaitForSeconds(Random.Range(0.05f, 0.5f));
		}
	}
}
