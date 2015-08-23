Shader "Custom/Vignette" {
	Properties {
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Power("Power", Float) = 5.0
	}
	SubShader {
		Pass {
			CGPROGRAM

			#pragma vertex vert_img
			#pragma fragment frag

			#include "UnityCG.cginc"

			sampler2D _MainTex;
			float _Power;

			float4 frag(v2f_img v) : COLOR
			{
				float2 uv = v.uv;
				float4 tex = tex2D(_MainTex, uv);
				tex *= 1 - dot(uv - 0.5f, uv - 0.5f) * _Power;
				return tex;
			}

			ENDCG
		}
	} 
	FallBack "Diffuse"
}
