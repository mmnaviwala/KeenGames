Shader "Vertex Paint/Vertex Blend Advanced Dual/Rain Puddles"
{
	properties {
		 _LightMap ("Brightness Light Map ", Float) = 0.4
		 _SpecColor ("Specular Color for Light Map", Color) = (0.5,0.5,0.5,1)
		 _Shininess ("Shininess for Light Map", Range(0.03,10)) = 1
		 _Color ("Diffuse Color", Color) = (0.5,0.5,0.5,0.5)

		_SpecPower ("Specular Power", Range(0.00,2.00)) = 1
		
		_WaterColor ("Water Color (R)", Color) = (0.5,0.5,1,0.5)
		_Splat1 ("Puddle (R) Normal", 2D) = "grey"{}
		_Speed("_Speed", Range(0.1,5.0) ) = 0.5
		_BumpAmt  ("Distortion", range (-3.0,3.0)) = -1
		_CubeTex ("Reflection", Cube) = "_Skybox" { TexGen CubeReflect }
		_reflectivity ("ReflectionStrength", range (0.00,1.00)) = 0.5

		_OffsetStrength ("OffsetStrength (Vertex R)", Range(0,1)) = 0.3
		_Splat2 ("Ground (A = Spec)", 2D) = "white"{}
		_Splat2bump ("Ground Normal", 2D) = "grey"{}
	}
	
	subshader {
		LOD 1000
		Tags {"Queue" = "Geometry"}
		//UsePass "FX/Glass/Stained BumpDistort/BASE"
		
		CGPROGRAM
		#pragma surface surf BlinnPhong vertex:vert addshadow approxview  
		#pragma target 3.0

		samplerCUBE _CubeTex;
		float _BumpAmt;
		float _reflectivity;

		sampler2D _Splat2;
		
		sampler2D _Splat1;
		sampler2D _Splat2bump;
		
		float _SpecPower;
		float _Shininess;

		half4 _WaterColor;
		half4 _Color;

		fixed _Speed;
		float _OffsetStrength;
		
		struct Input {
			float2 uv_Splat1;
        	float2 uv_Splat2;
        	float4 vertexColor;
			float4 color      : COLOR;
			float3 worldRefl;
			INTERNAL_DATA
	    };

	    void vert (inout appdata_full v, out Input o) {
	        o.vertexColor = v.color;
			v.vertex.y -=  _OffsetStrength*0.5 - (v.color.r-0.5)*_OffsetStrength;
	    }

		void surf (Input IN, inout SurfaceOutput o) {
			float4 splat_control = IN.vertexColor;
			fixed3 albedo;
			float2 uv2 = IN.uv_Splat2;
			
			float4 Splat3 = tex2D(_Splat2, uv2);

			float4 splat2Normal = tex2D(_Splat2bump, uv2);
			
			fixed4 tempNormal;
			fixed3 normal;

			//Noise
			fixed term = pow( (splat_control.r + ( (1-splat_control.r) *  (1-splat2Normal.r)*splat_control.g)) + ( ((1-splat2Normal.r)*splat_control.g) * splat_control.r) , splat_control.a*25*clamp(sin(_Time*10)+1,0.3,1) );
			term = clamp( term, 0, 1.0 );

			float Multiply5= _Time * _Speed;
			float4 UV_Pan0=float4((IN.uv_Splat1.xyxy).x ,(IN.uv_Splat1.xyxy).y + Multiply5,(IN.uv_Splat1.xyxy).z,(IN.uv_Splat1.xyxy).w);
			float4 UV_Pan1=float4((IN.uv_Splat1.xyxy).x-(Multiply5*0.5),(IN.uv_Splat1.xyxy).y -(Multiply5*1.5),(IN.uv_Splat1.xyxy).z,(IN.uv_Splat1.xyxy).w);

			float4 Splat1 = tex2D(_Splat2, UV_Pan0.xy);

			//normals
			tempNormal = tex2D(_Splat1, UV_Pan0.xy) + tex2D(_Splat1, UV_Pan1.xy);
			tempNormal = lerp(tempNormal,splat2Normal,term);
			normal = UnpackNormal(tempNormal);

			// diffuse color
			float offset = (0.2 - term) * _BumpAmt;

			//Diffuse
			uv2.xy = float2(uv2.x+(offset*normal.x),uv2.y+(offset*normal.y));
			float4 Splat2 = tex2D(_Splat2, uv2);

			o.Gloss = lerp(pow(normal.r,5),normal.r,term) * _SpecPower;
			o.Specular = pow(lerp(normal.r,normal.r*Splat2.a,term),_Shininess);

			Splat2 *= (_WaterColor * (1-term));
			float3 reflectionVect = WorldReflectionVector(IN, o.Normal);
			Splat2 += (texCUBE(_CubeTex, float3(-reflectionVect.x + (offset*normal.x),reflectionVect.y + (offset*normal.y),reflectionVect.z)).rgba * _reflectivity * (1-term) * clamp(normal.y,0.7,1));
		    albedo =  lerp(Splat2.rgb,Splat3.rgb,term) * _Color;
			
			o.Albedo = albedo;
			
		}
		
		ENDCG
	}
	Fallback "Diffuse"
}