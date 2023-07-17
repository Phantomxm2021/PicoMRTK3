Shader "PXR_SDK/PXR_UnderlayHole"
{
	SubShader
	{
		Tags {"Queue" = "Geometry+1" "RenderType" = "Transparent"}
		LOD 200

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
			};

			fixed4 _Transparent = fixed4(1, 1, 1, 0);

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				fixed4 col = _Transparent;
				return col;
			}
			ENDCG
		}
	}
}
