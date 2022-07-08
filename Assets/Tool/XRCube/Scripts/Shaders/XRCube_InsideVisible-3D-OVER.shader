Shader "XRCube/XRCube_InsideVisible-3D-OVER" {
Properties {
	_MainTex("Base (RGB)", 2D) = "black" {}
_Brightness("Brightness", Range(0, 10)) = 1
_Saturation("Saturation", Range(0, 10)) = 1
_Contrast("Contrast", Range(0, 10)) = 1
_distortionK1("distortion_K1", range(-100, 100)) = 0
_distortionK2("distortion_K2", range(-100, 100)) = 0
_distortionK3("distortion_K3", range(-100, 100)) = 0
_scale("scale", range(0, 3)) = 1
_reverse_X("reverse_X", range(0, 1)) = 1
_reverse_Y("reverse_Y", range(0, 1)) = 0
}

SubShader {
	Tags { "RenderType" = "Transparent" }
	Cull front
	LOD 100
	
	Pass {  
		CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"
					float 	_Intensity_x;
			float 	_Intensity_y;
			float _P_x;
			float _P_y;
			float _distortionK1;
			float _distortionK2;
			float _distortionK3;
			float _scale;
			float _reverse_X;
			float _reverse_Y;
			struct appdata_t {
				float4 vertex : POSITION;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f {
				float4 vertex : SV_POSITION;
				float2 texcoord : TEXCOORD0;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			half _Brightness, _Saturation, _Contrast;
			v2f vert (appdata_t v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				if (_reverse_X == 1)
				{
					v.texcoord.x = 1 - v.texcoord.x;
				}
				if (_reverse_Y == 1)
				{
					v.texcoord.y = 1 - v.texcoord.y;
				}
				
				o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
				return o;
			}
			float2 barrel(float2 uv)
			{

				float2 h = uv.xy - float2(0.5, 0.25);
				float r2 = h.x * h.x + h.y * h.y;
				float f = 1.0 + r2 * (_distortionK1 + _distortionK2 * r2 + _distortionK3 * r2 * r2);

				return f * _scale * h + float2(0.5, 0.25);
			}
			float4 frag (v2f i) : SV_Target
			{
				i.texcoord.y = (i.texcoord.y / 2) ;
			float4 col = tex2D(_MainTex, barrel(i.texcoord));
				col.rgb = saturate(col.rgb * _Brightness);                  // brightness
				fixed gray = dot(col.rgb, fixed3(0.3, 0.6, 0.1));           // gray
				col.rgb = lerp(gray, col.rgb, _Saturation);                 // saturation
				col.rgb = lerp(0.5, col.rgb, _Contrast);                    // contrast
				return col;
			}
		ENDCG
	}
}

}