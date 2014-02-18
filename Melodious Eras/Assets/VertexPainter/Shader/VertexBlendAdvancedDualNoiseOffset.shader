Shader "Vertex Paint/Vertex Blend Advanced Dual/Offset"
{
	properties {
		 _LightMap ("Brightness Light Map ", Float) = 0.4
		 _SpecColor ("Specular Color for Light Map", Color) = (0.5,0.5,0.5,1)
		 _Shininess ("Shininess for Light Map", Range(0.03,1)) = 1
		 _Color ("Diffuse Color", Color) = (0.5,0.5,0.5,0.5)

		_SpecPower ("Specular Power", Range(0.0,2.0)) = 1
		
		_Splat1 ("Texture 1 (R) (A = Spec)", 2D) = "white"{}
		_Splat1bump ("Texture 1 (R) bump", 2D) = "grey"{}
		_Splat2 ("Texture 2 (G) (A = Spec)", 2D) = "white"{}
		_Splat2bump ("Texture 2 (R) bump", 2D) = "grey"{}

		_Noise ("Noise  (A)", 2D) = "white" {}
		_NoiseTile ("Noise Tiling Factor", float) = 2.0

		_OffsetStrength ("OffsetStrength (Vertex R)", Range(-2,2)) = 0.3
	}
	
	subshader {
		LOD 1000
		Tags {"Queue" = "Geometry" "SplatCount" = "2" "RenderType" = "Opaque" }
		
		CGPROGRAM
		#pragma surface surf BlinnPhong addshadow vertex:vert
		#pragma target 3.0
		
		sampler2D _Splat1;
		sampler2D _Splat2;
		
		sampler2D _Splat1bump;
		sampler2D _Splat2bump;
		
		uniform sampler2D  _Noise;

		float _SpecPower;
		float _Shininess;

		half4 _Color;
		float _NoiseTile;
		float _OffsetStrength;
		
		struct Input {
        	float2 uv_Splat1;
        	float2 uv_Splat2;
        	float4 vertexColor;
			float4 color      : COLOR;
	    };

	    void vert (inout appdata_full v, out Input o) {
	        o.vertexColor = v.color;
			v.vertex.y += v.color.b*_OffsetStrength;
	    }

		void surf (Input IN, inout SurfaceOutput o) {
			float4 splat_control = IN.vertexColor;
			fixed3 albedo;
			fixed gloss;
			float2 uv1 = IN.uv_Splat1;
			float2 uv2 = IN.uv_Splat2;
			
			float4 Splat1 = tex2D(_Splat1, uv1);
			float4 Splat2 = tex2D(_Splat2, uv2);

			float4 splat2Normal = tex2D(_Splat2bump, uv2);
			
			fixed4 tempNormal;
			fixed3 normal;

			//Noise
			fixed2 uv_Noise = IN.uv_Splat1 * _NoiseTile;
			fixed noise = tex2D( _Noise, uv_Noise).a;
			fixed term = pow( (splat_control.r + ( (1-splat_control.r) * lerp(noise,splat2Normal.r,splat_control.g)) + ( lerp(noise,splat2Normal.r,splat_control.g) * splat_control.r) * 1 ), splat_control.a*25 );
			term = clamp( term, 0.0, 1.0 );
			
			// diffuse color
			albedo = lerp(Splat1.rgb,Splat2.rgb,term);
			//normals
			tempNormal = lerp(tex2D(_Splat1bump, uv1),splat2Normal,term);
			normal = UnpackNormal(tempNormal);
			
			// specular based on alpha of the textures
			gloss = lerp(Splat1.a,Splat2.a,term) * _SpecPower;
			
			o.Normal = normal;
			o.Albedo = albedo * _Color;
			o.Gloss = gloss;
			o.Specular = _Shininess;
		}
		
		ENDCG
	}
	Fallback "Diffuse"
}