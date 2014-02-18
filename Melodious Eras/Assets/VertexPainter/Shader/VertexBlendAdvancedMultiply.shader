Shader "Vertex Paint/Vertex Blend Advanced 4 Textures/Multiply"
{
	properties {
		 _LightMap ("Brightness Light Map ", Float) = 0.4
		 _SpecColor ("Specular Color for Light Map", Color) = (0.5,0.5,0.5,1)
		 _Shininess ("Shininess for Light Map", Range(0.03,1)) = 1
		 _Color ("Diffuse Color", Color) = (0.5,0.5,0.5,0.5)

		_SpecPower ("Specular Power", Range(0.0,2.0)) = 1
		
		_Splat1 ("Texture 1 (R) (A = Spec)", 2D) = "white"{}
		_Splat1bump ("Texture 1 (R) Normal", 2D) = "grey"{}
		_Splat2 ("Texture 2 (G) (A = Spec)", 2D) = "white"{}
		_Splat2bump ("Texture 2 (G) Normal", 2D) = "grey"{}
		_Splat3 ("Texture 3 (B) (A = Spec)", 2D) = "white"{}
		_Splat3bump ("Texture 3 (B) Normal", 2D) = "grey"{}
		_Splat4 ("Texture 4 (Default) (A = Spec)", 2D) = "white"{}
		_Splat4bump ("Texture 4 (Default) Normal", 2D) = "grey"{}

		_Noise ("Noise  (A)", 2D) = "white" {}
		_NoiseTile ("Noise Tiling Factor", float) = 2.0
	}
	
	subshader {
		LOD 900
		Tags {"Queue" = "Geometry" "SplatCount" = "4" "RenderType" = "Opaque" }
		
		CGPROGRAM
		#pragma surface surf BlinnPhong vertex:vert 
		#pragma target 3.0
		
		sampler2D _Splat1;
		sampler2D _Splat2;
		sampler2D _Splat3;
		sampler2D _Splat4;
		
		sampler2D _Splat1bump;
		sampler2D _Splat2bump;
		sampler2D _Splat3bump;
		sampler2D _Splat4bump;

		uniform sampler2D  _Noise;
		
		float _SpecPower;
		float _Shininess;

		half4 _Color;
		float _NoiseTile;
		
		struct Input {
        	float2 uv_Splat1;
        	float2 uv_Splat2;
        	float2 uv_Splat3;
        	float2 uv_Splat4;
        	float4 vertexColor;
			float4 color      : COLOR;
	    };

		inline fixed4 Blend (fixed4 val, fixed2 uv_Noise, fixed r, fixed noise)
        {
			fixed term = pow( (r + ( (1-r) * noise) + (noise * r) * 1 ), 16 );
			term = clamp( term, 0.0, 1.0 );
			return (val* term);
        }  

	    void vert (inout appdata_full v, out Input o) {
	        o.vertexColor = v.color;
	    }

		void surf (Input IN, inout SurfaceOutput o) {
			float4 splat_control = IN.vertexColor;
			fixed3 albedo;
			float gloss;
			float2 uv1 = IN.uv_Splat1;
			float2 uv2 = IN.uv_Splat2;
			float2 uv3 = IN.uv_Splat3;
			float2 uv4 = IN.uv_Splat4;
			
			float4 Splat1 = tex2D(_Splat1, uv1);
			float4 Splat2 = tex2D(_Splat2, uv2);
			float4 Splat3 = tex2D(_Splat3, uv3);
			float4 Splat4 = tex2D(_Splat4, uv4);
			
			fixed4 tempNormal;
			fixed3 normal;

			//Noise
			fixed2 uv_Noise = IN.uv_Splat1 * _NoiseTile;
			fixed r = 1 - IN.color.r;
			fixed noise = tex2D( _Noise, uv_Noise).a;
			
			// diffuse color
			fixed4 test =  Blend(Splat1.rgba,uv_Noise,splat_control.r,noise);
			test += Blend(Splat2.rgba,uv_Noise,splat_control.g,noise);
			test += Blend(Splat3.rgba,uv_Noise,splat_control.b,noise);
			test += Blend(Splat4.rgba,uv_Noise,abs(1-splat_control.a),noise);
			albedo = test;
			//normals
			tempNormal = Blend(tex2D(_Splat1bump, uv1),uv_Noise,splat_control.r,noise);
			test += Blend(tex2D(_Splat2bump, uv2),uv_Noise,splat_control.g,noise);
			test += Blend(tex2D(_Splat3bump, uv3),uv_Noise,splat_control.b,noise);
			test += Blend(tex2D(_Splat4bump, uv4),uv_Noise,abs(1-splat_control.a),noise);
			normal = UnpackNormal(tempNormal);
			
			// specular based on alpha of the textures
			gloss = test.a * _SpecPower;				
			
			o.Normal = normal;
			o.Albedo = albedo * _Color;
			o.Gloss = gloss;
			o.Specular = _Shininess;
		}
		
		ENDCG
	}
	Fallback "Diffuse"
}