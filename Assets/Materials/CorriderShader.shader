Shader "Custom/CorriderShader" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
	}
	SubShader {
		
		//{ "RenderType" = "opaque" "Queue" = "opaque" }
		Tags{ "RenderType" = "opaque" }
		//cull off

		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows // alpha:fade

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
			// 카메라 뷰 벡터
			float3 viewDir;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_CBUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_CBUFFER_END

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color

			float rim = 1.0 - saturate(dot(normalize(IN.viewDir), o.Normal));
			rim = pow(rim, 13);

			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			
			// 줄무늬 넓이를 조절하기 위한 주기
			float frequency = 20 + (sin(_Time.y)*0.5) * 5;

			c.rgb = ( sin(IN.uv_MainTex.y *360 *0.5) > 0) ?

				// 가로줄이다.
				 sin(IN.uv_MainTex.x * frequency + _Time.y * 5)*5*rim :

				// 세로줄이다.
				 cos(IN.uv_MainTex.y * (30) + _Time.y * 2)*20 - (1- rim)*20.5; 


			

			o.Albedo = c.rgba;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
			//o.Emission = c.b*rim*0.5;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
