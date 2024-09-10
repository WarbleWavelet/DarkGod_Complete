/****************************************************
    文件：ExtendComponent.Joint.cs
	作者：lenovo
    邮箱: 
    日期：2024/8/10 22:11:26
	功能：
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using Random = UnityEngine.Random;

public static partial class ExtendSpringJoint2D
{
    public static SpringJoint2D Init(this SpringJoint2D sj)
    {
        sj.enableCollision = false;
        sj.autoConfigureDistance = false;
        sj.distance = 1f; //绳长
        sj.dampingRatio = 0f; //重力
        sj.frequency = 10f; //

        return sj; 
    }
}
public static partial class ExtendSpringJoint2D
{

    /// <summary>
    /// 弹弓
    ///<url=https://blog.csdn.net/weixin_39538253/article/details/117381349?ops_request_misc=%257B%2522request%255Fid%2522%253A%2522172329929616800211576255%2522%252C%2522scm%2522%253A%252220140713.130102334.pc%255Fblog.%2522%257D&request_id=172329929616800211576255&biz_id=0&utm_medium=distribute.pc_search_result.none-task-blog-2~blog~first_rank_ecpm_v1~rank_v31_ecpm-1-117381349-null-null.nonecase&utm_term=%E5%B0%8F%E9%B8%9F&spm=1018.2226.3001.4450/> 
    /// </summary>
    public static void Slingshot(Rigidbody2D staticRgb, Rigidbody2D dynamicRgb, LineRenderer[] lrs)
    {
        staticRgb.simulated = true;
        staticRgb.bodyType = RigidbodyType2D.Static;
        //
        dynamicRgb.bodyType = RigidbodyType2D.Dynamic;
        dynamicRgb.simulated = true;
        dynamicRgb.mass = 1;
        dynamicRgb.angularDrag = 2;
        dynamicRgb.gravityScale = 0.5f;
        dynamicRgb.collisionDetectionMode = CollisionDetectionMode2D.Discrete;
        //
        SpringJoint2D sj = dynamicRgb.GetComponent<SpringJoint2D>();    
        sj.enableCollision = true;
        sj.connectedBody = staticRgb;
        sj.autoConfigureConnectedAnchor = false;
        sj.autoConfigureDistance = false;
        sj.distance = 0.36f;
        sj.dampingRatio = 0f;
        sj.frequency = 2;
        //
        for (int i = 0; i <lrs.Length ; i++)
        {
            LineRenderer lr = lrs[i];
            lr.shadowCastingMode = ShadowCastingMode.On;
            lr.receiveShadows = true;
            lr.allowOcclusionWhenDynamic = true;
            lr.motionVectorGenerationMode = MotionVectorGenerationMode.Camera;
            lr.useWorldSpace =true;
            lr.alignment =LineAlignment.View;
            lr.textureMode = LineTextureMode.Stretch;
            //lr.shadowBias = 0.5f;
            lr.generateLightingData = false;            
        }


        //

    }

}

public static partial class ExtendSpringJoint
{ 

}



