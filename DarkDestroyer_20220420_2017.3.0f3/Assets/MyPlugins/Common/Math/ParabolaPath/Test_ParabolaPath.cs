/****************************************************
    文件：Test_ParabolaPath.cs
	作者：lenovo
    邮箱: 
    日期：2023/12/9 18:11:17
	功能：
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;
 

public class Test_ParabolaPath : MonoBehaviour
{
    #region 属性
    public GameObject MissileGo;
    public Transform to;
    //

        #endregion

        #region 生命

        /// <summary>首次载入</summary>
        void Awake()
        {
            
        }
        

        /// <summary>Go激活</summary>
        void OnEnable ()
        {
            
        }

        /// <summary>首次载入且Go激活</summary>
        void Start()
        {
            
        }

         /// <summary>固定更新</summary>
        void FixedUpdate()
        {
            
        }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject go = GameObject.Instantiate(MissileGo);
            go.SetParent(transform);
            go.AddComponent<Missile02>().Init(to);
        }
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




