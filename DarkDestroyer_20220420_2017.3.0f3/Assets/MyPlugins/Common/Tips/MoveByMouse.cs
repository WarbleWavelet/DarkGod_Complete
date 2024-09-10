/****************************************************
    文件：MoveByMouse.cs
	作者：lenovo
    邮箱: 
    日期：2024/8/11 13:39:19
	功能：
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class MoveByMouse : MonoBehaviour
{
    #region 属性
    //  public Transform MoveTrans;
    public Rigidbody2D staticRgb;
    public Rigidbody2D dynamicRgb;
    bool isDrag;
    #endregion

    #region 生命

    /// <summary>首次载入</summary>
    void Awake()
    {

    }


    /// <summary>Go激活</summary>
    void OnEnable()
    {

    }

    public void Init(Rigidbody2D staticRgb, Rigidbody2D dynamicRgb)
{
        this.staticRgb = staticRgb;
        this.dynamicRgb = dynamicRgb;
        isDrag = false;
}
/// <summary>首次载入且Go激活</summary>
void Start()
        {
            
        }

    /// <summary>固定更新</summary>
    void FixedUpdate()
    {
        if (Input.GetMouseButtonUp(0))
        {
            isDrag = true;
        }
        if (Input.GetMouseButtonDown(0))
        {
            isDrag = false;


        }

        if (isDrag)
        {
                    dynamicRgb.isKinematic = false;
        }
        else
        {               dynamicRgb.isKinematic = true;
            transform.position = transform.position.FollowMouseV();
    
        }
    }

    void Update()
        {
            
        }

         /// <summary>延迟更新。适用于跟随逻辑</summary>
        void LateUpdae()
        {
            
        }

        /// <summary> 组件重设为默认值时（只用于编辑状态）</summary>
        void Reset()
        {
            
        }
      

        /// <summary>当对象设置为不可用时</summary>
        void OnDisable()
        {
            
        }


        /// <summary>组件销毁时调用</summary>
        void OnDestroy()
        {
            
        }
        #endregion 

        #region 系统

        #endregion 

        #region 辅助

        #endregion

    }



