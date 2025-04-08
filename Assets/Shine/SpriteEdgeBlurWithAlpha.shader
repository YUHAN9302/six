Shader "Custom/SpriteEdgeBlurWithAlpha"
{
    Properties
    {
        _MainTex ("Sprite Texture", 2D) = "white" {}
        _BlurAmount ("Blur Amount", Range(0, 10)) = 1
        _TintColor ("Tint Color", Color) = (1,1,1,1)
    }

    SubShader
    {
        Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }
        LOD 100
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _BlurAmount;
            float4 _TintColor;

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float blur = _BlurAmount * 0.001;

                // 累積 RGB 與 Alpha 以實現邊界柔化
                fixed4 col = tex2D(_MainTex, i.uv);
                col += tex2D(_MainTex, i.uv + float2( blur, 0));
                col += tex2D(_MainTex, i.uv - float2( blur, 0));
                col += tex2D(_MainTex, i.uv + float2(0, blur));
                col += tex2D(_MainTex, i.uv - float2(0, blur));
                col /= 5;

                // 模糊後的顏色與透明度再乘上 TintColor
                col *= _TintColor;

                return col;
            }
            ENDCG
        }
    }
}
