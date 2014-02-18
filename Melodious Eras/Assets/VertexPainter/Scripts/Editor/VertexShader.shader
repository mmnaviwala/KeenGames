    Shader "Hidden/Vertex Data" {
        Properties {
			_Color ("Colors", Vector) = (1,1,1,0)
        }
        SubShader {
            Tags { "RenderType"="Opaque" }
			Lighting Off
       
        CGPROGRAM
        #pragma surface surf Unlit
       
		float4 _Color;
       
        struct Input {
            float4 color : COLOR;
        };
       
	    half4 LightingUnlit(SurfaceOutput s, half3 lightDir, half atten)
		{
			return half4(s.Albedo, s.Alpha);
		}

        void surf (Input IN, inout SurfaceOutput o) {


			o.Albedo = float3(0,0,0);

			if(_Color.a >= 1)
			{
				o.Albedo.rgb = IN.color.a;
			}
			else
			{
				o.Albedo.r = IN.color.r * _Color.r;
				o.Albedo.g = IN.color.g * _Color.g;
				o.Albedo.b = IN.color.b * _Color.b;
			}
        }
        ENDCG
        }
    }