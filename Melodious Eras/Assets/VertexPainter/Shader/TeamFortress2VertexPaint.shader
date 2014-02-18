    Shader "Vertex Paint/Toon/Team Fortress 2" {
 Properties {
        _Color ("Main Color (A = Alpha)", Color) = (1,1,1,1)
        _RimColor ("Rim Color", Color) = (0.97,0.88,1,0.75)
        _RimPower ("Rim Power", Float) = 1.5

        _MainTex ("Diffuse 1 (RGB) Alpha (A)", 2D) = "white" {}
        _BumpMap ("Normal 1 (Normal)", 2D) = "bump" {}
        _SpecularTex ("Specular 1 Level (R) Gloss (G) Rim Mask (B) Illumn (A)", 2D) = "gray" {}

		_MainTex2 ("Diffuse 2 (RGB) Alpha (A)", 2D) = "white" {}
        _BumpMap2 ("Normal 2 (Normal)", 2D) = "bump" {}
        _SpecularTex2 ("Specular 2 Level (R) Gloss (G) Rim Mask (B) Illumn (A)", 2D) = "gray" {}

		_BlendTex ("Noise Texture (A)", 2D) = "gray" {}
		_BlendTile ("Noise Tiling Factor", float) = 2.0

        _RampTex ("Toon Ramp (RGB)", 2D) = "white" {}
		_Illum ("Illumination", Range(0, 3)) = 0
    }

    SubShader{
        Tags { "RenderType" = "Opaque" "SplatCount" = "2"}
		Lighting On

        CGPROGRAM
            #pragma surface surf TF2 vertex:vert fullforwardshadows exclude_path:prepass
            #pragma target 3.0

            struct Input
            {
                half2 uv_MainTex;
                //float3 worldNormal;
				//float3 viewDir;
				fixed4 vertexColor;
                INTERNAL_DATA
            };          

            sampler2D _MainTex, _SpecularTex, _BumpMap, _RampTex, _MainTex2, _SpecularTex2, _BumpMap2, _BlendTex;
            fixed4 _RimColor, _Color;
            fixed _RimPower;
			fixed _Illum;
			fixed _BlendTile;

            inline fixed4 LightingTF2 (SurfaceOutput s, fixed3 lightDir, fixed3 viewDir, fixed atten)
            {	
				#ifndef USING_DIRECTIONAL_LIGHT
					lightDir = normalize(lightDir);
				#endif

                fixed3 h = normalize (lightDir + viewDir);
                fixed NdotL = dot(s.Normal, lightDir) * 0.5 + 0.5;
                fixed3 ramp = tex2D(_RampTex, fixed2(NdotL * atten)).rgb; 
                fixed nh = max (0, dot (s.Normal, h));
                fixed spec = pow (nh, s.Gloss * 64) * s.Specular;
                fixed4 c;
                c.rgb = (s.Albedo * _Color.rgb * ramp * _LightColor0.rgb + _LightColor0.rgb * spec) * atten;
                c.a = s.Alpha * _Color.a;
                return c;
            }

			void vert (inout appdata_full v, out Input o) {
				o.vertexColor = v.color;
			}

            void surf (Input IN, inout SurfaceOutput o)
            {
				fixed4 splat_control = IN.vertexColor;

				fixed4 Splat1 = tex2D(_MainTex, IN.uv_MainTex);
				fixed4 Splat2 = tex2D(_MainTex2, IN.uv_MainTex);

				fixed2 uv_Noise = IN.uv_MainTex * _BlendTile;
				fixed noise = tex2D( _BlendTex, uv_Noise).a;
				fixed term = pow( (splat_control.r + ( (1-splat_control.r) * lerp(noise,Splat1.a,splat_control.g)) + ( lerp(noise,Splat2.a,splat_control.g) * splat_control.r) * 1 ), splat_control.a*25 );
				term = clamp( term, 0.0, 1.0 );

				o.Albedo = lerp(Splat1.rgb,Splat2.rgb,term);
				o.Normal = UnpackNormal(lerp(tex2D(_BumpMap, IN.uv_MainTex),tex2D(_BumpMap2, IN.uv_MainTex),term));
                fixed4 specGloss = lerp(tex2D(_SpecularTex, IN.uv_MainTex),tex2D(_SpecularTex2, IN.uv_MainTex),term);
                o.Specular = specGloss.r;
                o.Gloss = specGloss.g;
                fixed3 rim = pow(max(0, dot(fixed3(0, 1, 0), WorldNormalVector (IN, o.Normal))), _RimPower) * _RimColor.rgb * _RimColor.a * specGloss.b;
				o.Emission = (o.Albedo*specGloss.a) * _Illum;
                o.Albedo += rim;
            }
			
        ENDCG
    }
	
    Fallback "Transparent/Cutout/Bumped Specular"
}