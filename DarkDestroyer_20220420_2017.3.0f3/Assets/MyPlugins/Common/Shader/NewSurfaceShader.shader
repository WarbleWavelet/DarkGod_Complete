 //#pragma vertex vert//�������� ����(�����)//ϵͳ�Դ��Ķ�����ɫ��
Shader "Custom/NewSurfaceShader"
{

    //float4 v:POSITION //��������float4  //��������v  //POSITION�������壬��ֻҪ��POSITION���ܻ�ȡ��ģ�͵Ķ���
    // :POSITION               ��ȡ��ģ�͵Ķ�������
    // :SV_POSITION        �����������ɫ������Ļ����
    // :SV_TARGET           ���ֱֵ��������Ⱦ��
    // SubShader//�ֿ�
    // {    
    //     //��������дˮ����Ⱦ
    //     pass{    }
    //     //��������дʯͷ����Ⱦ
    //     pass{    }
    //     pass
    //     { 
    //         CGPROGRAM//��ʼ CG ������
 
    //         ENDCG// CG ���Խ�����   
    //     }
    // }
    // float3 vert3()//���巽��
    // {
    //     return //һ��float4
    // }
 
    // float4 vert4()//���巽��
    // {
    //     return //һ��float4
    // }
    SubShader//�ֿ�
    {    
        //��������дˮ����Ⱦ
        pass{    }
        //��������дʯͷ����Ⱦ
        pass{    } 
      
        CGPROGRAM//��ʼ CG ������


        #pragma vertex vert
        #pragma fragment surface
        float4 vert(float4 pos:POSITION ):SV_POSITION
        {
            return UnityObjectToClipPos(pos)//ת��unity����Ļλ��
        }

         float4 vert(float4 vext:POSITION ):SV_POSITION
        {
            if(pos.x<=0)
            {
                pos.x=0;    
            }
            if(pos.y<=0)
            {
                pos.y=0;    
            }
            re
            return UnityObjectToClipPos(pos)//ת��unity����Ļλ��
        }

        float4 surface( ):SV_TARGET
        {
            return float4(1,1,1,1)//��ɫ
            return float4(1,0,0,1)//��ɫ
            return float4(0,1,0,1)//��ɫ
            return float4(0,0,1,1)//��ɫ
        }

        struct a2v
        {
            float4  vect:POSITION//����
            float3 normal:NORMAL//����
            float4 coor:TEXT//���� UV��ͼ
        }


        ENDCG// CG ���Խ�����   
    }
    

    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
    }
   

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
