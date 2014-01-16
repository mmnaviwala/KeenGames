using UnityEngine;
using System.Collections;

public class RotateOnClick : MonoBehaviour {

	public enum RotateWhatAxis { X, Y, Z }
	public RotateWhatAxis RWA;
	public float RotateBy = 45;

	void OnMouseDown() 
	{
		switch(RWA)
		{
			case RotateWhatAxis.X:
				transform.Rotate(Vector3.right, RotateBy, Space.World);
				break;
			case RotateWhatAxis.Y:
				transform.Rotate(Vector3.up, RotateBy, Space.World);
				break;
			case RotateWhatAxis.Z:
				transform.Rotate(Vector3.forward, RotateBy, Space.World);
				break;
		}
	}
}
