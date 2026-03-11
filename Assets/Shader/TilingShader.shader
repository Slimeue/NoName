Shader "Custom/WorldTilingWithTileSize"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _TileSize("Tile Size", Float) = 1
    }
    SubShader
    {
        Tags
        {
            "RenderType"="Opaque"
        }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float _TileSize; // size in world units for 1 tile

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);

                // Get world position of the vertex
                float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;

                // Divide by tile size to get repeated UV
                o.uv = worldPos.xz / _TileSize;

                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                return col;
            }
            ENDCG
        }
    }
}