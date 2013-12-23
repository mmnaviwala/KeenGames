using UnityEngine;
using System.Collections;

[AddComponentMenu("Scripts/Misc/iTween Mover")]
public class iTweenMover : MonoBehaviour {

	public string pathName;
	public int time;

	// Use this for initialization
	void Start () 
	{
		iTween.MoveTo(gameObject, iTween.Hash("path",iTweenPath.GetPath(pathName), "time",time));
	}
}
