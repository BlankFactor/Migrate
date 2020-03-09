Shader "Unlit/SkyUnlit"
{
    Properties
    {
		
        _MainTex ("_MainTex", 2D) = "white" {}
		_TinyColor("TinyColor",Color) = (1,1,1,1)
		_FogColor("FogColor",Color) = (1,1,1,1)
		_Speed_X("Speed_X",Range(0,1)) = 0.05
		_Speed_Y("Speed_Y",Range(0,1)) = 0.03
    }

    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue" = "Transparent"}

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

			sampler2D _MainTex;
			fixed4 _MainTex_ST;
			fixed4 _TinyColor;
			fixed4 _FogColor;
			fixed _Speed_X;
			fixed _Speed_Y;

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
                o.uv = TRANSFORM_TEX(v.uv, _MainTex) + frac(float2(_Speed_X,_Speed_Y) * _Time.y);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex,i.uv);
				col *= _TinyColor;
				col = lerp(col,_FogColor,_FogColor.a);
                return col;
            }
            ENDCG
        }
    }
}
