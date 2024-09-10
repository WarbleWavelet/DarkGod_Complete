// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/UI/FontTop"
{
    Properties
    {
        _MainTex("Base (RGB), Alpha (A)", 2D) = "white" {}
    }

    SubShader
    {
        LOD 200

        Tags
        {
        "Queue" = "Overlay"
        "IgnoreProjector" = "True"
        "RenderType" = "Transparent"
        }

        Pass
        {
            Cull Off
            Lighting Off
            ZWrite Off
            Fog{ Mode Off }
            ColorMask RGB
            AlphaTest Greater .01
            Blend SrcAlpha OneMinusSrcAlpha
            ColorMaterial AmbientAndDiffuse

        CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#include "UnityCG.cginc"

    sampler2D _MainTex;
    float4 _MainTex_ST;

    struct appdata_t
    {
        float4 vertex : POSITION;
        half4 color : COLOR;
        float2 texcoord : TEXCOORD0;
    };

    struct v2f
    {
        float4 vertex : POSITION;
        half4 color : COLOR;
        float2 texcoord : TEXCOORD0;
    };

    v2f vert(appdata_t v)
    {
        v2f o;
        o.vertex = UnityObjectToClipPos(v.vertex);
        o.color = v.color;
        o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
        return o;
    }

    half4 frag(v2f IN) : COLOR
    {
    fixed4 col = IN.color;
    col.a *= tex2D(_MainTex, IN.texcoord).a;
    return col;
    }
        ENDCG
    }
    }
}
