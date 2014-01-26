    Shader "Custom/BPCEM with billboard reflection" {
    Properties {
        _Color ("Main Color", Color) = (1,1,1,1)
        _ReflectColor ("Reflection Color", Color) = (1,1,1,0.5)
        _MainTex ("Base (RGB) RefStrength (A)", 2D) = "white" {}
        _Cube ("Reflection Cubemap", Cube) = "_Skybox" { TexGen CubeReflect }
        _BumpMap ("Normalmap", 2D) = "bump" {}
        _BoxPosition ("Bounding Box Position", Vector) = (0, 0, 0)
        _BoxSize ("Bounding Box Size", Vector) = (10, 10, 10)
    }
     
    SubShader {
        Tags { "RenderType"="Opaque" }
        LOD 300
       
    CGPROGRAM
    #pragma target 3.0
    #pragma surface surf Lambert
     
    sampler2D _MainTex;
    sampler2D _BumpMap;
    sampler2D _Billboard;
    samplerCUBE _Cube;
     
    fixed4 _Color;
    fixed4 _ReflectColor;
    float3 _BoxSize;
    float3 _BoxPosition;
    float3 _QuadLLPos;
    fixed3 _QuadX;
    fixed3 _QuadY;
    float2 _QuadScale;
    fixed _Culling;
     
    struct Input {
        float2 uv_MainTex;
        float2 uv_BumpMap;
        fixed3 worldPos;
        float3 worldNormal;
        INTERNAL_DATA
    };
     
    void surf (Input IN, inout SurfaceOutput o) {
        // Base diffuse texture
        fixed4 tex = tex2D(_MainTex, IN.uv_MainTex);
        fixed4 c = tex * _Color;
        o.Albedo = c.rgb;
       
        fixed3 n = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
       
        // Reflection-ray
        float3 viewDir = IN.worldPos - _WorldSpaceCameraPos;
        float3 worldNorm = IN.worldNormal;
        worldNorm.xy -= n;
        float3 reflectDir = reflect (viewDir, worldNorm);
        fixed3 nReflDirection = normalize(reflectDir);
       
        // Parallax correction
        float3 boxStart = _BoxPosition - _BoxSize / 2.0;
        float3 firstPlaneIntersect = (boxStart + _BoxSize - IN.worldPos) / nReflDirection;
        float3 secondPlaneIntersect = (boxStart - IN.worldPos) / nReflDirection;
        float3 furthestPlane = (nReflDirection > 0.0) ? firstPlaneIntersect : secondPlaneIntersect;
        float3 intersectDistance = min(min(furthestPlane.x, furthestPlane.y), furthestPlane.z);
        float3 intersectPosition = IN.worldPos + nReflDirection * intersectDistance;
        fixed4 reflcol = texCUBE(_Cube, intersectPosition - _BoxPosition);
       
        // Ray-Plane intersection
        fixed3 quadNormal = cross(_QuadX, _QuadY);
        float planeIntersectDistance = (dot(IN.worldPos - _QuadLLPos, quadNormal) / dot(nReflDirection, quadNormal));
        intersectPosition = IN.worldPos - nReflDirection * planeIntersectDistance;
        float3 localPlaneIntersectPosition = intersectPosition - _QuadLLPos;
        float2 billboardUV = float2(dot(_QuadX, localPlaneIntersectPosition) / _QuadScale.x, dot(_QuadY, localPlaneIntersectPosition) / _QuadScale.y);
        fixed4 billboardCol = tex2D(_Billboard, billboardUV);
        float reflectDot = dot(nReflDirection, quadNormal);
       
        fixed w = billboardCol.a;
        w = billboardUV.x > 1.0 ? 0.0 : w;
        w = billboardUV.y > 1.0 ? 0.0 : w;
        w = billboardUV.x < 0.0 ? 0.0 : w;
        w = billboardUV.y < 0.0 ? 0.0 : w;
        w = reflectDot <= 0.0 && _Culling == 1.0 ? 0.0 : w;
       
        reflcol.rgb = lerp(reflcol.rgb, billboardCol.rgb, w);
       
        // Reflection display
        reflcol *= tex.a;
        o.Emission = reflcol.rgb * _ReflectColor.rgb;
        o.Alpha = reflcol.a * _ReflectColor.a;
    }
    ENDCG
    }
     
    FallBack "Reflective/VertexLit"
    }