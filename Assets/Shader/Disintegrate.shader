Shader "Unlit/Disintegrate"
{
 Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _NoiseScale ("Noise Scale", Float) = 10.0
        _SubValue ("Subtract", Range(-2, 4)) = 0.0
        _Thickness ("Thickness", Range(0, 1)) = 0.0

        [HDR] _Color ("Color", Color) = (1,1,1,1)
        _ColorIntensity ("Color Intensity", Range(0, 10)) = 1

    }
    SubShader
    {
        Tags { 
            "RenderType"="Transparent"
            "Queue"="Transparent"
        }

        Pass
        {
            Cull Off
            ZWrite Off
            Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _NoiseScale;
            float4 _Color;
            float _SubValue;
            float _ColorIntensity;
            float _Thickness;

            // Gradient Noise functions
            float2 unity_gradientNoise_dir(float2 p)
            {
                // Rotation matrix to rotate the gradient
                p = p % 289;
                float x = (34 * p.x + 1) * p.x % 289 + p.y;
                x = (34 * x + 1) * x % 289;
                x = frac(x / 41) * 2 - 1;
                return normalize(float2(x - floor(x + 0.5), abs(x) - 0.5));
            }

            float unity_gradientNoise(float2 p)
            {
                float2 ip = floor(p);
                float2 fp = frac(p);
                float d00 = dot(unity_gradientNoise_dir(ip), fp);
                float d01 = dot(unity_gradientNoise_dir(ip + float2(0, 1)), fp - float2(0, 1));
                float d10 = dot(unity_gradientNoise_dir(ip + float2(1, 0)), fp - float2(1, 0));
                float d11 = dot(unity_gradientNoise_dir(ip + float2(1, 1)), fp - float2(1, 1));
                fp = fp * fp * fp * (fp * (fp * 6 - 15) + 10);
                return lerp(lerp(d00, d01, fp.y), lerp(d10, d11, fp.y), fp.x);
            }

            float GradientNoise(float2 UV, float Scale)
            {
                return unity_gradientNoise(UV * Scale) + 0.5;
            }

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                // Sample the texture
                float4 col = tex2D(_MainTex, i.uv);

                float2 uv = i.uv;
                
                // Generate noise
                float noise = GradientNoise(i.uv, _NoiseScale);
                noise = saturate(noise);

                uv.y = pow(uv.y, 2) * 3;
                float heightCalc = noise + uv.y;
                //return float4(heightCalc.xxx, 1);
                //return float4(heightCalc.xxx - _SubValue, 1);
                //heightCalc = step(heightCalc, 0);

                float stepFn = step(heightCalc - _SubValue, 0); // Smaller
                float stepFnOff = step(heightCalc - (_SubValue + _Thickness), 0); // Use this as the mask // Larger
                float lines = stepFnOff - stepFn;
                float4 lineColor = lines * _Color * _ColorIntensity;

                //return float4(colorOut.rgb, stepFnOff);
                float4 maskedOffTex = col * stepFn;
                //return maskedOffTex;
                return float4(maskedOffTex.rgb + lineColor.rgb, stepFnOff * col.a);
            }
                
            ENDCG
        }
    }
}
