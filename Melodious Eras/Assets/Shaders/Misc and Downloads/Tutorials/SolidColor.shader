Shader "Tutorials/SolidColor" {
	Properties {
	_SolidColor ("Solid Color", Color) = (1, 0, 0)
	}
	SubShader {
		Color [_SolidColor]
		Pass {
			
		}
		
	} 
	FallBack "Diffuse"
}
