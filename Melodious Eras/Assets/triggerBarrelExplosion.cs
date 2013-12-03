using UnityEngine;
using System.Collections;

public class triggerBarrelExplosion : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		StartCoroutine(startExplosion());
	}

	IEnumerator startExplosion()
	{
		yield return new WaitForSeconds(1.5f);
		Destroy(this.gameObject);
	}
}
