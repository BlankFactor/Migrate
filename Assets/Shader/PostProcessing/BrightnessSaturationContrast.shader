Shader "Hidden/BrightnessSaturationContrast"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_Brightness("Brightness",Float) = 1
		_Saturation("Saturation",Float) = 1
		_Contrast("Contrast",Float) = 1
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
			fixed _Brightness;
			fixed _Saturation;
			fixed _Contrast;

			fixed luminance(fixed3 col){
				return 0.21 * col.r + 0.71 * col.g + 0.08 * col.b;
			}

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
               
			   //Brightness
			   fixed3 finalCol = col.rgb * _Brightness;
			   
			   //Saturation
			   fixed lum = luminance(col);
			   finalCol = lerp(fixed3(lum,lum,lum),finalCol,_Saturation);
				
			   //Contrast
			   fixed3 argCol = fixed3(0.5,0.5,0.5);
			   finalCol = lerp(argCol,finalCol,_Contrast);

                return fixed4(1,1,1,1);
            }
            ENDCG
        }
    }
}
