Shader "Custom/Fullscreen/NightVisionTest" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_ColorAdjustment("Color Adjustment (RGBA)", Vector) = (-.10, .010, .010, 0.0)
	}
	
	SubShader {
		Pass {
			ZTest Always ZWrite Off
			Fog {Mode Off}
			
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag
			#pragma fragmentoption ARB_precision_hint_fastest
			#include "UnityCG.cginc"
			
			uniform sampler2D _MainTex;
			uniform float4 _ColorAdjustment;
			
			fixed4 frag (v2f_img i) : COLOR
			{
				fixed4 original = tex2D(_MainTex, i.uv);
				
				//get intensity value (Y part of YIQ color space)
				fixed Y = dot(fixed3(.2, 0.5, 0.2), original.rgb);
				
				//Convert to Night Vision Tone by adding constant
				//fixed4 nightVisionConvert = float4(-.10, .010, .010, 0.0);
				fixed4 output = Y + _ColorAdjustment; //Decide on "original" or "Y"
				
				//With Original: RGB(-.5, .005, .015)
				//With Y: RGB(-.05, .005, .015)
				
				output.a = original.a;
				
				return output;
			}
			ENDCG
		}
	}
	Fallback off
}
