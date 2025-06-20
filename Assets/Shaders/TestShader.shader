Shader "Unlit/TestShader"
{
    Properties
    {
        [MainTexture] _BaseMap ("Texture", 2D) = "white" {}
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" "RenderPipeline" = "UniversalPipeline"}
        LOD 100
        ZWrite Off
        Cull Off

        Pass
        {
            Name "ColorBlitPass"

            HLSLPROGRAM

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            // The Blit.hlsl file provides the vertex shader (Vert),
            // input structure (Attributes) and output strucutre (Varyings)
            #include "Packages/com.unity.render-pipelines.core/Runtime/Utilities/Blit.hlsl"

            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            // #pragma multi_compile_fog

            // #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
                // float3 viewVector : TEXCOORD1;
            };

            v2f vert(appdata v) {
                v2f output;
                // output.pos = TransformObjectToHClip(v.vertex.xyz);
                output.pos = v.vertex;
                output.uv = v.uv;
                // Camera space matches OpenGL convention where cam forward is -z. In unity forward is positive z.
                // (https://docs.unity3d.com/ScriptReference/Camera-cameraToWorldMatrix.html)
                // float3 viewVector = mul(unity_CameraInvProjection, float4(v.uv * 2 - 1, 0, -1));
                // output.viewVector = mul(unity_CameraToWorld, float4(viewVector,0));
                return output;
            }

            // sampler2D _BaseMap;
            // float4 _BaseMap_ST;
            // float3 BoundsMin;
            // float3 BoundsMax;

            TEXTURE2D_X(_CameraOpaqueTexture);
            SAMPLER(sampler_CameraOpaqueTexture);

            // Returns (dstToBox, dstInsideBox). If ray misses box, dstInsideBox will be zero
            // float2 rayBoxDst(float3 boundsMin, float3 boundsMax, float3 rayOrigin, float3 invRaydir) {
            //     // Adapted from: http://jcgt.org/published/0007/03/04/
            //     float3 t0 = (boundsMin - rayOrigin) * invRaydir;
            //     float3 t1 = (boundsMax - rayOrigin) * invRaydir;
            //     float3 tmin = min(t0, t1);
            //     float3 tmax = max(t0, t1);
                
            //     float dstA = max(max(tmin.x, tmin.y), tmin.z);
            //     float dstB = min(tmax.x, min(tmax.y, tmax.z));

            //     // CASE 1: ray intersects box from outside (0 <= dstA <= dstB)
            //     // dstA is dst to nearest intersection, dstB dst to far intersection

            //     // CASE 2: ray intersects box from inside (dstA < 0 < dstB)
            //     // dstA is the dst to intersection behind the ray, dstB is dst to forward intersection

            //     // CASE 3: ray misses box (dstA > dstB)

            //     float dstToBox = max(0, dstA);
            //     float dstInsideBox = max(0, dstB - dstToBox);
            //     return float2(dstToBox, dstInsideBox);
            // }

            half4 frag(v2f input) : SV_Target
            {
                half4 col = SAMPLE_TEXTURE2D_X(_CameraOpaqueTexture, sampler_CameraOpaqueTexture, input.uv);
                // float3 rayOrigin = _WorldSpaceCameraPos;
                // float3 rayDir = normalize(input.viewVector);

                // float2 rayBoxInfo = rayBoxDst(BoundsMin, BoundsMax, rayOrigin, rayDir);
                // float dstToBox = rayBoxInfo.x;
                // float dstInsideBox = rayBoxInfo.y;

                // bool rayHitBox = dstInsideBox > 0;
                // if (rayHitBox) {
                //     col = 0;
                // }

                return col;
            }
            ENDHLSL
        }
    }
}
