using UnityEngine;
using System.Collections;

public class Waypoint : MonoBehaviour 
{
    [Range(0, 10)]
	public float waitTime = 3f;
	public Vector3 rotation = new Vector3(999, 999, 999);
	private Vector3 defaultRotation = new Vector3(999, 999, 999);
}
