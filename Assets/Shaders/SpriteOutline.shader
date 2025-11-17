Shader "Custom/SpriteOutlineHoverGlow"
{
    Properties
    {
        _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)
        _OutlineColor ("Outline Color", Color) = (1,0,0,1)
        _OutlineSize ("Outline Size", Float) = 1
        _GlowIntensity ("Glow Intensity", Float) = 1
        _Hover ("Hover", Float) = 0
    }

    SubShader
    {
        Tags {
            "Queue"="Transparent"
            "RenderType"="Transparent"
            "IgnoreProjector"="True"
            "CanUseSpriteAtlas"="True"
        }

        Cull Off
        Lighting Off
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            sampler2D _MainTex;
            float4 _MainTex_TexelSize;
            float4 _Color;
            float4 _OutlineColor;
            float _OutlineSize;
            float _GlowIntensity;
            float _Hover;

            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
                OUT.uv = IN.uv;
                return OUT;
            }

            float4 frag(Varyings IN) : SV_Target
            {
                float4 main = tex2D(_MainTex, IN.uv);

                // Draw sprite normally
                if (main.a > 0.01)
                    return main * _Color;

                // If not hovering, no outline
                if (_Hover < 0.5)
                    return float4(0,0,0,0);

                float2 px = _MainTex_TexelSize.xy * _OutlineSize;

                // 3x3 soft outline sampling
                float outlineAlpha = 0;
                for (int x = -1; x <= 1; x++)
                {
                    for (int y = -1; y <= 1; y++)
                    {
                        float2 offset = float2(x * px.x, y * px.y);
                        outlineAlpha += tex2D(_MainTex, IN.uv + offset).a;
                    }
                }

                outlineAlpha = saturate(outlineAlpha * 0.25 * _GlowIntensity); // smooth and glow

                if (outlineAlpha > 0)
                    return float4(_OutlineColor.rgb, outlineAlpha);

                return float4(0,0,0,0);
            }

            ENDHLSL
        }
    }
}
