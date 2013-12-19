Shader "Tutorials/VertexColorsLerp" {
	Properties {
		_Color ("Solid Color (A - Blend)", Color) = (1, 1, 1)
		_Number ("Number", Range(0, 1)) = 1
		//Range(0, 1) - 1
	}
	SubShader {
		BindChannels {
			Bind "vertex", vertex
			Bind "color", color
		}
		Pass {
			/*SetTexture[_] { //anything can be in the square brackets
				ConstantColor(1, 1, 1, [_Number])
				Combine constant Lerp(constant) primary
			}*/
		}
	}
}
