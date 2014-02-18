Shader "Vertex Paint/Vertex Noise"
{
	properties {
		 _LightMap ("Brightness Light Map ", Float) = 0.4
		 _SpecColor ("Specular Color for Light Map", Color) = (0.5,0.5,0.5,1)
		 _Shininess ("Shininess for Light Map", Range(0.03,1)) = 1
		 _Color ("Diffuse Color", Color) = (0.5,0.5,0.5,0.5)

		_SpecPower1 ("Specular Power", Range(0.0,2.0)) = 1
		_SpecColor ("Specular Color", Color) = (1,1,1,1)
		
		_Shininess ("Shininess", Range(0.0,2.0)) = 1
		
		_Splat1 ("Texture 1 (R) (A = Spec)", 2D) = "white"{}
		_Splat1bump ("Texture 1 (R) bump", 2D) = "grey"{}
	}
	
	subshader {
		LOD 800
		Tags {"SplatCount" = "1" "RenderType" = "Opaque" }
		
		CGPROGRAM
		#pragma surface surf BlinnPhong vertex:vert 
		#pragma target 3.0
		
		
		sampler2D _Splat1;
		
		sampler2D _Splat1bump;
		
		float _SpecPower1;
		float _Shininess;
		
		struct Input {
        	float2 uv_Splat1;
        	float4 vertexColor;
	    };
	    
	    void vert (inout appdata_full v, out Input o) {
	        o.vertexColor = v.color;
	    }
			
		void surf (Input IN, inout SurfaceOutput o) {
			
			float4 splat_control = IN.vertexColor;
			fixed3 albedo;
			float gloss;
			float2 uv1 = IN.uv_Splat1;
			
			float4 Splat1 = tex2D(_Splat1, uv1);
			
			float4 tempNormal;
			float3 normal;
			
			// diffuse color
			albedo = Splat1.rgb;
			albedo += splat_control.rgb;
			
			//normals
			tempNormal = tex2D (_Splat1bump, uv1);
			normal = UnpackNormal(tempNormal);
			
			// specular based on alpha of the textures
			gloss = Splat1.a *  _SpecPower1;
			
			o.Normal = normal;
			o.Albedo = albedo;
			o.Gloss = gloss;
			o.Specular = _Shininess;
		}
		
		ENDCG
	}
	Fallback "Diffuse"
}