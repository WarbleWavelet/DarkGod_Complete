/****************************************************
    文件：Test_DrawMap.cs
	作者：lenovo
    邮箱: 
    日期：2024/8/17 23:36:33
	功能：
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;   using static ExtendGeometry;
 
namespace Common.Test
{
    public class Test_DrawMap : MonoBehaviour
    {
        #region 属性
        public GameObject prefab;
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
            NodeProxy.Init(prefab);
           LineProxy.Init(prefab);
           Node node0= NodeProxy.AddNode();
           Node node1= NodeProxy.AddNode();
           Node node2= NodeProxy.AddNode();
           Node node3= NodeProxy.AddNode();
           Node node4= NodeProxy.AddNode();
#if NET_4_7_OR_NEWER
            node0.SetPos(new Vector3((0,100).RR(), (0, 100).RR(),0)); 
            node1.SetPos(new Vector3((0,100).RR(), (0, 100).RR(),0)); 
            node2.SetPos(new Vector3((0,100).RR(), (0, 100).RR(),0)); 
            node3.SetPos(new Vector3((0,100).RR(), (0, 100).RR(),0)); 
            node4.SetPos(new Vector3((0,100).RR(), (0, 100).RR(),0)); 
#endif
            LineProxy.DrawLine(node0, node1);
            LineProxy.DrawLine(node1, node2);
            LineProxy.DrawLine(node2, node3);
            LineProxy.DrawLine(node3, node4);
            LineProxy.DrawLine(node0, node4);
        }

         /// <summary>固定更新</summary>
        void FixedUpdate()
        {
            
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
}



