/****************************************************
    文件：DynamicPolygon.cs
	作者：lenovo
    邮箱: 
    日期：2024/8/8 20:21:25
	功能： https://zhuanlan.zhihu.com/p/244884120
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
 

    public class DynamicPolygon : MonoBehaviour
    {
        #region 属性
        //MeshFilter组件用于获取网格信息,我们生成的网格就需要添加到MeshFilter.MeshRenderer用于渲染网格,在这里使用默认的材质来演示.
        //要生成一个多边形,我们应该要有多边形的顶点数组.现在开始定义我们的变量
    //顶点数组
    public Vector3[] Vertexes;
        //网格过滤器
        private MeshFilter _meshFilter { get { return GetComponent<MeshFilter>(); } }
        //网格
        private Mesh _mesh { get { return _meshFilter.mesh; } }
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
        if (Input.GetKeyDown(KeyCode.Q))
        {
            //虽然只有短短几行代码,**但是值得注意的是我们生成三角形的顶点顺序必须为顺时针方向 * *.所以在传入顶点的时候我们应该按照顺时针方向传入顶点.
    Vertexes = new Vector3[] { Vector3.zero, new Vector3(-1, 1, 0), new Vector3(1, 2, 0), new Vector3(2, -1, 0) };
            Refresh();


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
//    接下来我们根据顶点动态生成一个网格.网格都是由三角形组成的,所以我们应该根据顶点数量求出三角形数量,
//然后需要确定每个三角形的顶点在我们的顶点数组中的索引.下面上代码
public void Refresh()
    {
        //得到三角形的数量
        int trianglesCount = Vertexes.Length - 2;

        //三角形顶点ID数组
        int[] triangles = new int[trianglesCount * 3];

        //绘制三角形
        _mesh.vertices = Vertexes;

        //三角形顶点索引,确保按照顺时针方向设置三角形顶点
        for (int i = 0; i < trianglesCount; i++)
        {
            for (int j = 0; j < 3; ++j)
            {
                triangles[i * 3 + j] = j == 0 ? 0 : i + j;
            }
        }
        _mesh.triangles = triangles;
        _meshFilter.mesh = _mesh;
    }
    #endregion

}



