Shader "Custom/SilhoueteShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_BackColor("Backgrund Color", COLOR) = (0,0,0,1)
		_BackSlider("Back Color Slider", Range(0,1)) = 1
		_SilTex("Focus Texture", 2D) = "black" {}
		_SilColor("Silhouete Color", COLOR) = (1,1,1,1)
		_SilSlider("Silhouete Color Slider", Range(0,1)) = 0
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

				float2 uv_SilTex : TEXCOORD1;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;

				float2 uv_SilTex : TEXCOORD1;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
				o.uv_SilTex = v.uv_SilTex;
                return o;
            }

            sampler2D _MainTex;
			sampler2D _SilTex;
			float _BackSlider;
			float _SilSlider;
			float4 _BackColor;
			float4 _SilColor;

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 back = tex2D(_MainTex, i.uv);
				fixed4 target = tex2D(_SilTex, i.uv_SilTex);
				if (target.a > 0) {
					return lerp(target, _SilColor, _SilSlider);
				}
                return lerp(back, _BackColor, _BackSlider);
            }
            ENDCG
        }
    }
}
