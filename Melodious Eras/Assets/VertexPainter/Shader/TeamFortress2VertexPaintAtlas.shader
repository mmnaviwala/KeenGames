// Upgrade NOTE: replaced 'glstate.matrix.mvp' with 'UNITY_MATRIX_MVP'

    Shader "Vertex Paint/Toon/Team Fortress 2 Atlas" {
 Properties {
        _Color ("Main Color (A = Alpha)", Color) = (1,1,1,1)
        _RimColor ("Rim Color", Color) = (0.97,0.88,1,0.75)
        _RimPower ("Rim Power", Float) = 1.5

        _MainTex ("Diffuse 2x2 (RGB) Alpha (A)", 2D) = "white" {}
        _BumpMap ("Normal 2x2 (Normal)", 2D) = "bump" {}
        _SpecularTex ("Specular Level 2x2 (R) Gloss (G) Rim Mask (B) Illumn (A)", 2D) = "gray" {}

		_NoiseTex ("Noise Texture (A)", 2D) = "gray" {}

        _RampTex ("Toon Ramp (RGB)", 2D) = "white" {}
		_Illum ("Illumination", Range(0, 3)) = 0
    }

    SubShader{
        Tags { "RenderType" = "Opaque"}
		Lighting On

        CGPROGRAM
            #pragma surface surf TF2 vertex:vert fullforwardshadows exclude_path:prepass
            #pragma target 3.0

            struct Input
            {
                half2 uv_MainTex;
				half2 uv_NoiseTex;
                //float3 worldNormal;
				//float3 viewDir;
				fixed4 vertexColor;
                INTERNAL_DATA
            };          

            sampler2D _MainTex, _SpecularTex, _BumpMap, _RampTex, _NoiseTex;
            fixed4 _RimColor, _Color;
            fixed _RimPower;
			fixed _Illum;

            inline fixed4 LightingTF2 (SurfaceOutput s, fixed3 lightDir, fixed3 viewDir, fixed atten)
            {	
				#ifndef USING_DIRECTIONAL_LIGHT
					lightDir = normalize(lightDir);
				#endif

                fixed3 h = normalize (lightDir + viewDir);
                fixed NdotL = dot(s.Normal, lightDir) * 0.5 + 0.5;
                fixed3 ramp = tex2D(_RampTex, float2(NdotL * atten)).rgb; 
                float nh = max (0, dot (s.Normal, h));
                float spec = pow (nh, s.Gloss * 64) * s.Specular;
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

				fixed2 UV3 = half2(frac(IN.uv_MainTex.x) * 0.5, frac(IN.uv_MainTex.y) * 0.5);
				fixed2 UV1 = UV3+ half2(0.0, 0.5);
				fixed2 UV2 = UV3+ half2(0.5, 0.5);
				fixed2 UV4 = UV3+ half2(0.5, 0.0);

				fixed4 Splat1 = tex2D(_MainTex, UV1);
				fixed4 Splat2 = tex2D(_MainTex, UV2);
				fixed4 Splat3 = tex2D(_MainTex, UV3);
				fixed4 Splat4 = tex2D(_MainTex, UV4);

				fixed noise = tex2D( _NoiseTex, IN.uv_NoiseTex).a;
				fixed splatControl = splat_control.a*25;
				fixed noiseLerp = lerp(noise,Splat1.a,splat_control.a);
				fixed Blend1 = pow( (splat_control.r + ( (1-splat_control.r) * noiseLerp) + ( noiseLerp * splat_control.r) * 1 ), splatControl );
				Blend1 = clamp( Blend1, 0.0, 1.0 );
				noiseLerp = lerp(noise,Splat2.a,splat_control.a);
				fixed Blend2 = pow( (splat_control.g + ( (1-splat_control.g) * noiseLerp) + ( noiseLerp * splat_control.g) * 1 ), splatControl );
				Blend2 = clamp( Blend2, 0.0, 1.0 );

				noiseLerp = lerp(noise,Splat3.a,splat_control.a);
				fixed Blend3 = pow( (splat_control.b + ( (1-splat_control.b) * noiseLerp) + ( noiseLerp * splat_control.b) * 1 ), splatControl );
				Blend3 = clamp( Blend3, 0.0, 1.0 );

				fixed4 test = lerp(Splat4.rgba,Splat1.rgba,Blend1);
				test = lerp(test.rgba,Splat2.rgba,Blend2);
				test = lerp(test.rgba,Splat3.rgba,Blend3);			
				o.Albedo = test.rgb;

				fixed4 tempNormal = lerp(tex2D(_BumpMap, UV4),tex2D(_BumpMap, UV1).rgba,Blend1);
				tempNormal = lerp(tempNormal.rgba,tex2D (_BumpMap, UV2),Blend2);
				tempNormal = lerp(tempNormal.rgba,tex2D (_BumpMap, UV3),Blend3);
				o.Normal = UnpackNormal(tempNormal);

				fixed4 specGloss = lerp(tex2D(_SpecularTex, UV4),tex2D(_SpecularTex, UV1).rgba,Blend1);
				specGloss = lerp(specGloss.rgba,tex2D (_SpecularTex, UV2),Blend2);
				specGloss = lerp(specGloss.rgba,tex2D (_SpecularTex, UV3),Blend3);

				o.Specular = specGloss.r;
				o.Gloss = specGloss.g;
                half3 rim = pow(max(0, dot(float3(0, 1, 0), WorldNormalVector (IN, o.Normal))), _RimPower) * _RimColor.rgb * _RimColor.a * specGloss.b;
				o.Emission = (o.Albedo*specGloss.a) * _Illum;
                o.Albedo += rim;
            }
			
        ENDCG
    }
	
    Fallback "Transparent/Cutout/Bumped Specular"
}