Shader "Tutorials/TextureCombiner" {
	Properties {
		_Color ("Color", Color) = (0, 0, 1)
		_MainTex ("Texture", 2D) = "" {TexGen SphereMap} //TextGen (ObjectLinear, EyeLinear, SphereMap)
	}
	SubShader {
		Pass {
			Color [_Color]
			SetTexture[_MainTex] {Combine primary  * texture } //tints the texture
			//"Combine one - texture" inverts the texture
			//"Double" and "Quad" make the texture 2x or 4x as bright
		}
	} 
	FallBack "Diffuse"
}
