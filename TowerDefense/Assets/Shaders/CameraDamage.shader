Shader "Custom/CameraDamage"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _effectColor ("Color", Color) = (1,0,0,1)
        _colorValue("Color Value", Range(0.0, 1.0)) = 0.1
        _colorSaturation("Color Saturation", Range(0.0, 1.0)) = 0.1
        _centerDistance("Center Distance", Range(0.0, 1.0)) = 1
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass
        {
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

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
            fixed4 _effectColor;
            float _colorValue;
            float _colorSaturation;
            float _centerDistance;

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);

                // Make a vignette in the middle of the screen
                float d = length(i.uv - float2(0.5, 0.5));
                float c = 1.0 - _centerDistance * d;
                float3 color = _effectColor.rgb * sin(2) / ((1- _colorValue) * 10) + (1 - _colorSaturation);

                // Output to screen
                col.rgb *= float3(c,c,c) + (color / c);
                
                return col;
            }
            ENDCG
        }
    }
}
