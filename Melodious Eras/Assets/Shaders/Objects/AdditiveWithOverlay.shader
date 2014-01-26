Shader "Custom/Screen Flicker" {
	Properties {
		_TintColor ("Tint Color", Color) = (0.5, 0.5, 0.5)
		_MainTex ("Particle Texture", 2D) = "white" {}
		_Interlace ("Particle Texture", 2D) = "white" {}
	}
	Category {
		Tags {"Queue" = "Transparent"}
		Blend SrcAlpha One
		AlphaTest Greater .01
		ColorMask RGB
		Cull Off Lighting Off ZWrite Off Fog {Color (0, 0, 0, 0)}
		
		BindChannels {
			Bind "Color", color
			Bind "Vertex", vertex
			Bind "TexCoord", texcoord
		}
		
		SubShader {
			Pass {
				SetTexture [_MainTex] {
					constantColor [_TintColor]
					combine constant * primary
				}
				SetTexture [_MainTex] {
					combine texture * previous
				}
				SetTexture [_Interlace] {
					combine texture * previous
				}
			}
		}
		
		SubShader {
			Pass {SetTexture [_MainTex] {
				combine texture * primary
			}
		}
	}
	FallBack "Diffuse"
	}
}
