Shader "Custom/ice" {
	SubShader{
		Tags{ "Queue" = "Transparent" }
		LOD 200

		CGPROGRAM
#pragma surface surf Standard alpha:fade
#pragma target 3.0

		struct Input {
		float3 worldNormal;
		float3 viewDir;
	};

	void surf(Input IN, inout SurfaceOutputStandard o) {
		o.Albedo = fixed4(0.66, 0.8, 0.96, 1);
		float alpha = 1 - (abs(dot(IN.viewDir, IN.worldNormal))) + 0.3f;
		o.Alpha = alpha * 1.5f;
	}
	ENDCG
	}
		FallBack "Diffuse"
}