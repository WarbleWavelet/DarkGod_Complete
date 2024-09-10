/****************************************************
    文件：Test_LineRenderer.cs
	作者：lenovo
    邮箱: 
    日期：2024/8/10 22:34:39
	功能：
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
 

    public class Test_LineRenderer : MonoBehaviour
    {
        #region 属性
    Transform Player;
 public   LineRenderer Line0;
 public   LineRenderer Line1;
 public   Rigidbody2D staticRgb;
 public Rigidbody2D dynamicRgb;
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
        //Line0.GetComponent<SpringJoint2D>().Init();
        //Line1.GetComponent<SpringJoint2D>().Init();
        // Player.GetComponent<SpringJoint2D>().Init();
        Player = dynamicRgb.transform;
        ExtendSpringJoint2D.Slingshot(staticRgb,dynamicRgb,new LineRenderer[2] { Line0,Line1});
        dynamicRgb.GetComponent<MoveByMouse>().Init(staticRgb, dynamicRgb);
        }

         /// <summary>固定更新</summary>
        void FixedUpdate()
        {
            Line0.Draw(Line0.transform.position, Player.position); 
            Line1.Draw(Line1.transform.position, Player.position); 
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



