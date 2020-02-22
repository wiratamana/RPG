Shader "Unlit/ItemShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Intensity("Intensity", Float) = 1

        _MaskColor("Mask Color", 2D) = "white" {}
        _MaskLeather("Mask Leather", 2D) = "white" {}
        _MaskMetal("Mask Metal", 2D) = "white" {}
        _CamDir("CameraDirection", Vector) = (.34, .85, .92)

       _ColorPrimary("Color Primary", Color) = (1,1,1,1)
       _ColorSecondary("Color Secondary", Color) = (1,1,1,1)

       _LeatherPrimary("Leather Primary", Color) = (1,1,1,1)
       _LeatherSecondary("Leather Secondary", Color) = (1,1,1,1)

       _MetalPrimary("Metal Primary", Color) = (1,1,1,1)
       _MetalSecondary("Metal Secondary", Color) = (1,1,1,1)
       _MetalDark("Metal Dark", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
                float3 normal : NORMAL;
            };

            sampler2D _MainTex;
            float _Intensity;

            sampler2D _MaskColor;
            sampler2D _MaskLeather;
            sampler2D _MaskMetal;

            float4 _MainTex_ST;
            float3 _CamDir;

            float4 _ColorPrimary;
            float4 _ColorSecondary;

            float4 _LeatherPrimary;
            float4 _LeatherSecondary;

            float4 _MetalPrimary;
            float4 _MetalDark;
            float4 _MetalSecondary;

            float4 setColor(float4 maskColor, float4 baseColor)
            {
                if (maskColor.r == 1 && maskColor.g == 0 && maskColor.b == 1)
                {
                    return _ColorSecondary;
                }

                if (maskColor.r == 0 && maskColor.g == 1 && maskColor.b == 1)
                {
                    return _ColorPrimary;
                }

                return baseColor;
            }

            float4 setLeather(float4 maskColor, float4 baseColor)
            {
                if (maskColor.r == 1 && maskColor.g == 0 && maskColor.b == 1)
                {
                    return _LeatherSecondary;
                }

                if (maskColor.r == 0 && maskColor.g == 1 && maskColor.b == 1)
                {
                    return _LeatherPrimary;
                }

                return baseColor;
            }

            float4 setMetal(float4 maskColor, float4 baseColor)
            {
                if (maskColor.r == 1 && maskColor.g == 0 && maskColor.b == 1)
                {
                    return _MetalSecondary;
                }

                if (maskColor.r == 0 && maskColor.g == 1 && maskColor.b == 1)
                {
                    return _MetalPrimary;
                }


                if (maskColor.r == 1 && maskColor.g == 1 && maskColor.b == 0)
                {
                    return _MetalDark;
                }

                return baseColor;
            }

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.normal = v.normal;
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                // sample the texture
                float4 col = tex2D(_MainTex, i.uv);
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                float3 normal = UnityObjectToWorldNormal(i.normal);
                float dotProduct = dot(normal, normalize(-_CamDir));
                float maxVal = max(dotProduct, 0.2);

                float4 maskColor = tex2D(_MaskColor, i.uv);
                float4 maskLeather = tex2D(_MaskLeather, i.uv);
                float4 maskMetal = tex2D(_MaskMetal, i.uv);

                col = setColor(maskColor, col);
                col = setLeather(maskLeather, col);
                col = setMetal(maskMetal, col);

                //if (maskColor.r == 1 && maskColor.g == 0 && maskColor.b == 1)
                //{
                //    col = _ColorSecondary;
                //}
                //
                //if (maskColor.r == 0 && maskColor.g == 1 && maskColor.b == 1)
                //{
                //    col = _ColorPrimary;
                //}

                return  float4(col.rgb * maxVal * _Intensity, 1);
            }

            ENDCG
        }
    }
}
