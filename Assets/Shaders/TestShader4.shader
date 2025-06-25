Shader "Unlit/TestShader4"
{
    Properties
    {
        _BoundsMin("Bounds Min", Vector) = (0, 0, 0)
        _BoundsMax("Bounds Max", Vector) = (1, 1, 1)
        _NumSteps("Number of Steps", Integer) = 3
        _TestDensity("Test Density", Float) = 0.1
    }

    SubShader
    {
        Tags { "RenderType" = "Opaque" "RenderPipeline" = "UniversalPipeline"}        
        LOD 100

        HLSLINCLUDE
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
        
        CBUFFER_START(UnityPerMaterial)
            float3 _BoundsMin;
            float3 _BoundsMax;
            int _NumSteps;
            float _TestDensity;
        CBUFFER_END

        TEXTURE2D_X(_BlitTexture);
        SAMPLER(sampler_BlitTexture);

        TEXTURE2D(_CameraDepthTexture);
        SAMPLER(sampler_CameraDepthTexture);

        struct appdata
        {
            uint vertexID : SV_VertexID;
        };

        struct v2f
        {
            float4 position : SV_POSITION;
            float2 uv : TEXCOORD0;
            float3 viewVector : TEXCOORD1;
        };

        ENDHLSL

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            v2f vert(appdata input)
            {
                v2f output;
                output.position = GetFullScreenTriangleVertexPosition(input.vertexID);
                output.uv = GetFullScreenTriangleTexCoord(input.vertexID);

                float2 uvClip = output.uv * 2 - 1;
                float4 viewPos = mul(unity_CameraInvProjection, float4(uvClip, 0, -1));
                output.viewVector = mul(unity_CameraToWorld, float4(viewPos.xyz, 0)).xyz;

                return output;
            }

            // Returns (dstToBox, dstInsideBox). If ray misses box, dstInsideBox will be zero
            float2 rayBoxDst(float3 boundsMin, float3 boundsMax, float3 rayOrigin, float3 invRaydir) {
                // Adapted from: http://jcgt.org/published/0007/03/04/
                float3 t0 = (boundsMin - rayOrigin) * invRaydir;
                float3 t1 = (boundsMax - rayOrigin) * invRaydir;
                float3 tmin = min(t0, t1);
                float3 tmax = max(t0, t1);
                
                float dstA = max(max(tmin.x, tmin.y), tmin.z);
                float dstB = min(tmax.x, min(tmax.y, tmax.z));

                // CASE 1: ray intersects box from outside (0 <= dstA <= dstB)
                // dstA is dst to nearest intersection, dstB dst to far intersection

                // CASE 2: ray intersects box from inside (dstA < 0 < dstB)
                // dstA is the dst to intersection behind the ray, dstB is dst to forward intersection

                // CASE 3: ray misses box (dstA > dstB)

                float dstToBox = max(0, dstA);
                float dstInsideBox = max(0, dstB - dstToBox);
                return float2(dstToBox, dstInsideBox);
            }

            float sampleDensity(float3 position) {
                return _TestDensity;
            }

            float4 frag(v2f input) : SV_Target
            {
                float4 col = SAMPLE_TEXTURE2D_X(_BlitTexture, sampler_BlitTexture, input.uv);
                float rawDepth = SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, sampler_CameraDepthTexture, input.uv);
                float depth = LinearEyeDepth(rawDepth, _ZBufferParams);

                float3 rayOrigin = _WorldSpaceCameraPos;
                float3 rayDir = normalize(input.viewVector);
                float3 invRayDir = 1 / input.viewVector;    

                float2 rayBoxInfo = rayBoxDst(_BoundsMin, _BoundsMax, rayOrigin, invRayDir);
                float dstToBox = rayBoxInfo.x;
                float dstInsideBox = rayBoxInfo.y;

                float dstTravelled = 0;
                float dstLimit = min(depth - dstToBox, dstInsideBox);
                float stepSize = dstLimit / _NumSteps;

                float totalDensity = 0;
                while(dstTravelled < dstLimit) {
                    float3 rayPos = rayOrigin + rayDir * (dstToBox + dstTravelled);
                    totalDensity += sampleDensity(rayPos) * dstTravelled;
                    dstTravelled += stepSize;
                }

                float transmittance = exp(-totalDensity);                
                return col * transmittance;
            }

            ENDHLSL
        }
    }
}
