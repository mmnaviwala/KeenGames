using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GLLineRenderer : MonoBehaviour {

	public static Material lineMaterial;
	public List<Vector3> reflectionPoints;

	void Start()
	{
		reflectionPoints = GameObject.Find("BeamSource").GetComponent<RaycastReflectionC>().reflectionPoints;
	}

	static void CreateLineMaterial()
	{
		if( !lineMaterial ) {
			lineMaterial = new Material( "Shader \"Lines/Colored Blended\" {" +
			                            "SubShader { Pass { " +
			                            "    Blend SrcAlpha OneMinusSrcAlpha " +
			                            "    ZWrite Off Cull Off Fog { Mode Off } " +
			                            "    BindChannels {" +
			                            "      Bind \"vertex\", vertex Bind \"color\", color }" +
			                            "} } }" );
			lineMaterial.hideFlags = HideFlags.HideAndDontSave;
			lineMaterial.shader.hideFlags = HideFlags.HideAndDontSave;
		}
	}
	
	void OnPostRender()
	{
		CreateLineMaterial();
		lineMaterial.SetPass( 0 );

		for(int i=0; i<reflectionPoints.Count; i=i+2)
		{
			GL.Begin(GL.LINES);
			GL.Color(Color.red);
			GL.Vertex3(reflectionPoints[i].x, reflectionPoints[i].y, reflectionPoints[i].z);		// from point A
			GL.Vertex3(reflectionPoints[i+1].x, reflectionPoints[i+1].y, reflectionPoints[i+1].z);	// to point B
			GL.End();
		}

		/*GL.Begin(GL.QUADS);
		GL.Color(Color.yellow);
		GL.Vertex3(5, 1, 0);
		GL.Vertex3(5, 2, 0);
		GL.Vertex3(0, 2, 0);
		GL.Vertex3(0, 1, 0);
		GL.End();*/
	}
}
