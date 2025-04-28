Shader "Custom/Blur"
{
    Properties
    {
        _MainTex("Base (RGBA)", 2D) = "white" {}
        _BlurSize("Blur Amount", Range(0.0, 1.0)) = 1.0
    }
    SubShader
    {
        Tags
        {
            "Queue" = "Transparent"
            "IgnoreProjector" = "True"
            "RenderType" = "Transparent"
            "PreviewType" = "Plane"
            "CanUseSpriteAtlas" = "True"
        }

        Cull Off
        Lighting Off
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 2.0
            #include "UnityCG.cginc"
            #include "UnityUI.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float4 color : COLOR;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                fixed4 color : COLOR;
                float2 texcoord : TEXCOORD0;
            };

            sampler2D _MainTex;
            float4 _MainTex_TexelSize;
            float4 _MainTex_ST;
            float _BlurSize;

            v2f vert(appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
                o.color = v.color;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float2 uv = i.texcoord;
                float2 offset = _MainTex_TexelSize.xy * _BlurSize;

                fixed4 col = tex2D(_MainTex, uv) * 0.2;

                col += tex2D(_MainTex, uv + float2(offset.x, 0)) * 0.125;
                col += tex2D(_MainTex, uv - float2(offset.x, 0)) * 0.125;
                col += tex2D(_MainTex, uv + float2(0, offset.y)) * 0.125;
                col += tex2D(_MainTex, uv - float2(0, offset.y)) * 0.125;

                col += tex2D(_MainTex, uv + offset.xy) * 0.1;
                col += tex2D(_MainTex, uv - offset.xy) * 0.1;
                col += tex2D(_MainTex, uv + float2(-offset.x, offset.y)) * 0.1;
                col += tex2D(_MainTex, uv + float2(offset.x, -offset.y)) * 0.1;

                col.a = 1.0;
                return col * i.color;
            }
            ENDCG
        }
    }
}
