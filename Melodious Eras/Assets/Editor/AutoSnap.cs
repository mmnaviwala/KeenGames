using UnityEngine;
using UnityEditor;

public class AutoSnap : EditorWindow
{
	private Vector3 prevPosition;
	private Vector3 prevScale;
	private bool doSnap = true;
	private float snapValue = 1;
	private bool avoidZeroScale = true;
	
	[MenuItem( "Edit/Auto Snap %_l" )]
	
	static void Init()
	{
		var window = (AutoSnap)EditorWindow.GetWindow( typeof( AutoSnap ) );
		window.maxSize = new Vector2( 200, 100 );
	}
	
	public void OnGUI()
	{
		doSnap = EditorGUILayout.Toggle( "Auto Snap", doSnap );
		snapValue = EditorGUILayout.FloatField( "Snap Value", snapValue );
		avoidZeroScale = EditorGUILayout.Toggle ("Avoid Zero Scale", avoidZeroScale);
	}
	
	public void Update()
	{
		if ( doSnap
		    && !EditorApplication.isPlaying
		    && Selection.transforms.Length > 0)
		{
			if(Selection.transforms[0].position != prevPosition)
			{
				SnapPosition();
				//prevPosition = Selection.transforms[0].position;
			}
			if(Selection.transforms[0].localScale != prevScale)
			{
				SnapScale();
				//prevScale = Selection.transforms[0].localScale;
			}
		}
	}
	
	private void SnapPosition()
	{
		foreach ( var transform in Selection.transforms )
		{
			var t = transform.transform.position;
			t.x = Round( t.x );
			t.y = Round( t.y );
			t.z = Round( t.z );
			transform.transform.position = t;
		}
		prevPosition = Selection.transforms[0].position;
	}

	private void SnapScale()
	{
		for(int t = 0; t < Selection.transforms.Length; t++)
		{
			var tScale = Selection.transforms[t].localScale;
			var tPos = Selection.transforms[t].position;

			tScale.x = Round (tScale.x);
			if(tScale.x != prevScale.x)
				tPos.x += (tScale.x > prevScale.x ? snapValue : -snapValue)/ 2;

			tScale.y = Round (tScale.y);
			if(tScale.y != prevScale.y)
				tPos.y += (tScale.y > prevScale.y ? snapValue : -snapValue)/ 2;

			tScale.z = Round (tScale.z);
			if(tScale.z != prevScale.z)
				tPos.z += (tScale.z > prevScale.z ? snapValue : -snapValue)/ 2;

			Selection.transforms[t].localScale = tScale;
			Selection.transforms[t].position = tPos;

			prevScale = tScale;
			prevPosition = tPos;

		}
		//SnapPosition();
	}
	
	private float Round( float input )
	{
		return snapValue * Mathf.Round( ( input / snapValue ) );
	}
}