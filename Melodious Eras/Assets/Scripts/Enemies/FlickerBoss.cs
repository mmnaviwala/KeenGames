using UnityEngine;
using System.Collections;

public class FlickerBoss : MonoBehaviour {
	
	Transform boss;

	void Start () 
	{
		StartCoroutine(Flicker());
		boss = GameObject.FindGameObjectWithTag(Tags.TECHNO_BOSS).transform;
	}
	
	IEnumerator Flicker()
    {
		yield return new WaitForSeconds(2F);
		boss.transform.localScale = new Vector3(0,0,0);
		
		yield return new WaitForSeconds(1F);
		
		boss.transform.localScale = new Vector3(2,2,2);
		iTween.MoveTo(gameObject, new Vector3(281,14,0),1);
		boss.transform.localScale = new Vector3(0,0,0);
	}
	
}
