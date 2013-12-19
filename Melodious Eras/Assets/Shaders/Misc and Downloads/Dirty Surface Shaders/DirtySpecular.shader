Shader "_DirtySpecular"
{
	Properties 
	{
_DiffuseTexture("_DiffuseTexture", 2D) = "black" {}
_Dirt("_Dirt", 2D) = "black" {}
_MakeDirty("_MakeDirty", Range(0,1.5) ) = 1
_DiffuseIntensity("_DiffuseIntensity", Range(0,2) ) = 1
_SpecularPower("_SpecularPower", Range(0,2) ) = 0.5
_SpecularIntensity("_SpecularIntensity", Range(0,1) ) = 0.5
_SpecularColor("_SpecularColor", Color) = (1,1,1,1)
_DirtIntensity("_DirtIntensity", 2D) = "black" {}

	}
	
	SubShader 
	{
		Tags
		{
"Queue"="Geometry"
"IgnoreProjector"="False"
"RenderType"="Opaque"

		}

		
Cull Back
ZWrite On
ZTest LEqual
ColorMask RGBA
Fog{
}


		CGPROGRAM
#pragma surface surf BlinnPhongEditor  vertex:vert
#pragma target 2.0


sampler2D _DiffuseTexture;
sampler2D _Dirt;
float _MakeDirty;
float _DiffuseIntensity;
float _SpecularPower;
float _SpecularIntensity;
float4 _SpecularColor;
sampler2D _DirtIntensity;

			struct EditorSurfaceOutput {
				half3 Albedo;
				half3 Normal;
				half3 Emission;
				half3 Gloss;
				half Specular;
				half Alpha;
				half4 Custom;
			};
			
			inline half4 LightingBlinnPhongEditor_PrePass (EditorSurfaceOutput s, half4 light)
			{
half3 spec = light.a * s.Gloss;
half4 c;
c.rgb = (s.Albedo * light.rgb + light.rgb * spec);
c.a = s.Alpha;
return c;

			}

			inline half4 LightingBlinnPhongEditor (EditorSurfaceOutput s, half3 lightDir, half3 viewDir, half atten)
			{
				half3 h = normalize (lightDir + viewDir);
				
				half diff = max (0, dot ( lightDir, s.Normal ));
				
				float nh = max (0, dot (s.Normal, h));
				float spec = pow (nh, s.Specular*128.0);
				
				half4 res;
				res.rgb = _LightColor0.rgb * diff;
				res.w = spec * Luminance (_LightColor0.rgb);
				res *= atten * 2.0;

				return LightingBlinnPhongEditor_PrePass( s, res );
			}
			
			struct Input {
				float2 uv_DiffuseTexture;
float2 uv_Dirt;
float2 uv_DirtIntensity;

			};

			void vert (inout appdata_full v, out Input o) {
float4 VertexOutputMaster0_0_NoInput = float4(0,0,0,0);
float4 VertexOutputMaster0_1_NoInput = float4(0,0,0,0);
float4 VertexOutputMaster0_2_NoInput = float4(0,0,0,0);
float4 VertexOutputMaster0_3_NoInput = float4(0,0,0,0);


			}
			

			void surf (Input IN, inout EditorSurfaceOutput o) {
				o.Normal = float3(0.0,0.0,1.0);
				o.Alpha = 1.0;
				o.Albedo = 0.0;
				o.Emission = 0.0;
				o.Gloss = 0.0;
				o.Specular = 0.0;
				o.Custom = 0.0;
				
float4 Tex2D0=tex2D(_DiffuseTexture,(IN.uv_DiffuseTexture.xyxy).xy);
float4 Tex2D1=tex2D(_Dirt,(IN.uv_Dirt.xyxy).xy);
float4 Tex2D2=tex2D(_DirtIntensity,(IN.uv_DirtIntensity.xyxy).xy);
float4 Multiply3=Tex2D2 * _MakeDirty.xxxx;
float4 Lerp0=lerp(Tex2D0,Tex2D1,Multiply3);
float4 Multiply0=Lerp0 * _DiffuseIntensity.xxxx;
float4 Multiply2=_SpecularColor * _SpecularIntensity.xxxx;
float4 Tex2D3=tex2D(_Dirt,(IN.uv_Dirt.xyxy).xy);
float4 Tex2D4=tex2D(_DirtIntensity,(IN.uv_DirtIntensity.xyxy).xy);
float4 Multiply1=Tex2D4 * _MakeDirty.xxxx;
float4 Lerp1=lerp(Multiply2,Tex2D3,Multiply1);
float4 Master0_1_NoInput = float4(0,0,1,1);
float4 Master0_2_NoInput = float4(0,0,0,0);
float4 Master0_5_NoInput = float4(1,1,1,1);
float4 Master0_7_NoInput = float4(0,0,0,0);
float4 Master0_6_NoInput = float4(1,1,1,1);
o.Albedo = Multiply0;
o.Specular = _SpecularPower.xxxx;
o.Gloss = Lerp1;

				o.Normal = normalize(o.Normal);
			}
		ENDCG
	}
	Fallback ""
}