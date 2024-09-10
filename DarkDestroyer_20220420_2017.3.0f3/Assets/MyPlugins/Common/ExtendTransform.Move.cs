/****************************************************
    文件：ExtendMove.cs
	作者：lenovo
    邮箱: 
    日期：2023/8/22 0:9:47
	功能：
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public static partial class ExtendRotate  //贪吃蛇的移动
{
    /// <summary>
    /// 蛇头节点,舌头的下一步位置,身体节点列表(包括蛇头)
    /// </summary>
    public static void BodyMove(this Transform t, Vector3 headPos, List<Transform> bodyList)
    {

        //身体移动
        if (bodyList.Count > 0)
        {
            //方法一
            //Transform lastBody = bodyList[bodyList.Count - 1];
            //lastBody.localPosition = headPos;
            //bodyList.Insert(0, lastBody);
            //bodyList.RemoveAt(bodyList.Count - 1);

            //方法二
            for (int i = bodyList.Count - 1; i > 0; i--)//前一个赋值给后一个
            {
                bodyList[i].localPosition = bodyList[i - 1].localPosition;
            }


            bodyList[0].localPosition = headPos;
        }
    }
}


public static partial class ExtendMove    //Frame
{
    public static void TranslateFixed(this Transform t
    , float speed
    , Vector3 dir
    , Space space = Space.World)
    {
        t.Translate(dir  * speed * Time.fixedDeltaTime, Space.World);
    }
}
public static partial class ExtendMove 
{
    public static void Translate(this Transform t
        , float speed
        , Vector3 dir
        , Space space=Space.World)
    {
        t.Translate(speed * dir.normalized * Time.deltaTime, space);
    }




    #region Move、SimpleMove


    /// <summary>
    /// <para /> 无限制
    /// <para /> 移动时候需要注意乘以时间
    /// <para /> 需要自己做重力
    /// </summary> 
    public static void Move(this CharacterController ctrl
    , float speed
    , Vector3 dir)
    {
        ctrl.Move(speed * dir.normalized * Time.deltaTime);
    }

    /// <summary>
    /// <para />无限制
    /// <para />移动时候需要注意乘以时间
    /// <para />需要自己做重力
    /// </summary> 
    public static void Move(this Transform t
        , float speed
        , Vector3 dir)
    {
        CharacterController ctrl = t.gameObject.GetOrAddComponent<CharacterController>();
        ctrl.Move(speed * dir.normalized * Time.deltaTime);
    }


    /// <summary>
    ///  <para />只能在平面移动，不带Y轴移动
    ///  <para />默认每帧，移动时不需要乘以deltaTime
    ///  <para />重力自动施加。 如果该角色落地，则返回
    /// </summary>
    public static void SimpleMove(this CharacterController ctrl
        , float speed
        , Vector3 dir)
    {
        ctrl.SimpleMove( speed * dir.normalized );
    }

    /// <summary>
    ///  <para />只能在平面移动，不带Y轴移动
    ///  <para />默认每帧，移动时不需要乘以deltaTime
    ///  <para />重力自动施加。 如果该角色落地，则返回
    /// </summary>
    public static void SimpleMove(this Transform t
        , float speed
        , Vector3 dir)
    {
        CharacterController ctrl = t.gameObject.GetOrAddComponent<CharacterController>();
        ctrl.SimpleMove(speed * dir.normalized);

    }
    #endregion

    public static void MovePosition(this Transform t
        , float speed
        , Vector3 dir)
    {
        Rigidbody rbg = t.gameObject.GetOrAddComponent<Rigidbody>();
        rbg.MovePosition(speed * dir.normalized);
    }


    public static void MovePosition(this Rigidbody  rbg
        , float speed
        , Vector3 dir)
    {
        rbg.MovePosition(speed * dir.normalized);
    }

}




