// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/VertexSin" {
	Properties{
		_Frequency("Frequency",Float) = 0
		_Amplification("Amplification",Float) = 0
		_WidthFactor("Width Factor",Float) = 0
		_HeightFactor("Height Factor",Float) = 0
		_RotateFrequency("Rotate Frequency",Float)=0
		_RotateAngle("Rotate Angle",Float)=0
		_IsRotate("IsRotate",Int)=1
		_Color("Emission", Color) = (1,1,1,1)
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_MaskTex("Mask", 2D) = "white" {}

	}
		SubShader{
			Tags{ "DisableBatching" = "True" }
			//Tags { "RenderType"="Opaque" }
		Pass{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			sampler2D _MainTex;
			sampler2D _MaskTex;
			fixed4 _Color;
			float _Frequency;
			float _Amplification;
			float _WidthFactor;
			float _HeightFactor;
			float _RotateFrequency;
			float _RotateAngle;
			bool _IsRotate;

			struct a2v {
				float4 pos : POSITION;
				float4 texcoord : TEXCOORD0;
			};

			struct v2f {
				float4 pos:SV_POSITION;
				float2 uv:TEXCOORD0;
			};

			v2f vert(a2v v) {
				v2f o;
				const float xmax = 0.6409438f;
				const float xmin = -0.8110144f;
				float4 maskUV = float4((xmax - v.pos.x) / (xmax - xmin),0,0 ,0);
				float maskValue = tex2Dlod(_MaskTex, maskUV).r;

				const float PI = 3.14159;
				float deltaZ = maskValue * _Amplification * sin(_WidthFactor * v.pos.x * PI + _HeightFactor * abs(v.pos.y) / 0.005f * PI + _Time.y * _Frequency * PI);
				v.pos.z += deltaZ;

				if (_IsRotate==1) {
					float angle = _RotateAngle*sin(_Time.y*_RotateFrequency*PI)*PI/180.0;
					float4x4 rotateMatrix = float4x4(
						cos(angle), 0, sin(angle), 0,
						0, 1, 0, 0,
						-sin(angle), 0, cos(angle), 0,
						0, 0, 0, 0);
					v.pos = mul(rotateMatrix, v.pos);
				}
				o.pos = UnityObjectToClipPos(v.pos);
				o.uv = v.texcoord;
				return o;
			}

			fixed4 frag(v2f i) :SV_Target{
				fixed4 c = tex2D(_MainTex,i.uv);
				return c;
			}
			ENDCG
		}
	}
		FallBack "Diffuse"
}
