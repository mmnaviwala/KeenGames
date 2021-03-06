﻿Shader "Custom/Fullscreen/Grayscale Fade" {
Properties {
	_MainTex ("Base (RGB)", 2D) = "white" {}
	_RampTex ("Base (RGB)", 2D) = "grayscaleRamp" {}
	_EffectAmount ("Effect Amount", Range(0, 1)) = 1.0
}

SubShader {
	Pass {
		ZTest Always Cull Off ZWrite Off
		Fog { Mode off }
				
		CGPROGRAM
		#pragma vertex vert_img
		#pragma fragment frag
		#pragma fragmentoption ARB_precision_hint_fastest 
		#include "UnityCG.cginc"

		uniform sampler2D _MainTex;
		uniform sampler2D _RampTex;
		uniform half _RampOffset;
		uniform float _EffectAmount;

		fixed4 frag (v2f_img i) : COLOR
		{
			fixed4 original = tex2D(_MainTex, i.uv);
			fixed grayscale = Luminance(original.rgb);
			half2 remap = half2 (grayscale + _RampOffset, .5);
			fixed4 output = tex2D(_RampTex, remap);
			output.a = original.a;
			return lerp(original, output, _EffectAmount);
		}
		ENDCG

	}
}

Fallback off

}