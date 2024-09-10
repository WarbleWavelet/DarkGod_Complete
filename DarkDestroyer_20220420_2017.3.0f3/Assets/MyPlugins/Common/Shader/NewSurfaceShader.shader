 //#pragma vertex vert//引入内容 起名(随便起)//系统自带的顶点着色器
Shader "Custom/NewSurfaceShader"
{

    //float4 v:POSITION //声明变量float4  //变量名称v  //POSITION就是语义，你只要：POSITION就能获取到模型的顶点
    // :POSITION               获取到模型的顶点坐标
    // :SV_POSITION        输出给像素着色器的屏幕坐标
    // :SV_TARGET           输出值直接用于渲染了
    // SubShader//分块
    // {    
    //     //假如这里写水的渲染
    //     pass{    }
    //     //假如这里写石头的渲染
    //     pass{    }
    //     pass
    //     { 
    //         CGPROGRAM//开始 CG 语言了
 
    //         ENDCG// CG 语言结束了   
    //     }
    // }
    // float3 vert3()//定义方法
    // {
    //     return //一个float4
    // }
 
    // float4 vert4()//定义方法
    // {
    //     return //一个float4
    // }
    SubShader//分块
    {    
        //假如这里写水的渲染
        pass{    }
        //假如这里写石头的渲染
        pass{    } 
      
        CGPROGRAM//开始 CG 语言了


        #pragma vertex vert
        #pragma fragment surface
        float4 vert(float4 pos:POSITION ):SV_POSITION
        {
            return UnityObjectToClipPos(pos)//转成unity的屏幕位置
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
            return UnityObjectToClipPos(pos)//转成unity的屏幕位置
        }

        float4 surface( ):SV_TARGET
        {
            return float4(1,1,1,1)//白色
            return float4(1,0,0,1)//红色
            return float4(0,1,0,1)//绿色
            return float4(0,0,1,1)//蓝色
        }

        struct a2v
        {
            float4  vect:POSITION//顶点
            float3 normal:NORMAL//法线
            float4 coor:TEXT//纹理 UV贴图
        }


        ENDCG// CG 语言结束了   
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
