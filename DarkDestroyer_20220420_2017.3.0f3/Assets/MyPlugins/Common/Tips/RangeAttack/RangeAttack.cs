/****************************************************
    文件：RangeAttack.cs
	作者：lenovo
    邮箱: 
    日期：2023/7/27 23:17:36
	功能：
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;



/// <summary>扇型攻击范围</summary> 
public class RangeAttack : MonoBehaviour
{
    #region 属性


    //扇形角度

    [SerializeField] private float _angle = 80f;
    //扇形半径
    [SerializeField] private float _radius = 3.5f;
    //物体B
    [SerializeField] private Transform _target;
    private bool _flag;
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
        _flag = IsInRange(_angle, _radius, transform, _target);
        if (_flag)
        {

            Debug.Log("InRange");
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

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Handles.color = _flag ? Color.cyan : Color.red;

        float x = _radius * Mathf.Sin(_angle / 2f * Mathf.Deg2Rad);
        float y = Mathf.Sqrt(Mathf.Pow(_radius, 2f) - Mathf.Pow(x, 2f));
        Vector3 a = new Vector3(transform.position.x - x, 0f, transform.position.z + y);
        Vector3 b = new Vector3(transform.position.x + x, 0f, transform.position.z + y);

        Handles.DrawLine(transform.position, a);
        Handles.DrawLine(transform.position, b);

        float half = _angle / 2;
        for (int i = 0; i < half; i++)
        {
            x = _radius * Mathf.Sin((half - i) * Mathf.Deg2Rad);
            y = Mathf.Sqrt(Mathf.Pow(_radius, 2f) - Mathf.Pow(x, 2f));
            a = new Vector3(transform.position.x - x, 0f, transform.position.z + y);
            x = _radius * Mathf.Sin((half - i - 1) * Mathf.Deg2Rad);
            y = Mathf.Sqrt(Mathf.Pow(_radius, 2f) - Mathf.Pow(x, 2f));
            b = new Vector3(transform.position.x - x, 0f, transform.position.z + y);

            Handles.DrawLine(a, b);
        }
        for (int i = 0; i < half; i++)
        {
            x = _radius * Mathf.Sin((half - i) * Mathf.Deg2Rad);
            y = Mathf.Sqrt(Mathf.Pow(_radius, 2f) - Mathf.Pow(x, 2f));
            a = new Vector3(transform.position.x + x, 0f, transform.position.z + y);
            x = _radius * Mathf.Sin((half - i - 1) * Mathf.Deg2Rad);
            y = Mathf.Sqrt(Mathf.Pow(_radius, 2f) - Mathf.Pow(x, 2f));
            b = new Vector3(transform.position.x + x, 0f, transform.position.z + y);

            Handles.DrawLine(a, b);
        }
    }
#endif


    #endregion

    #region 辅助


    /// <summary>
    /// 判断target是否在扇形区域内
    /// </summary>
    /// <param name="sectorAngle">扇形角度</param>
    /// <param name="sectorRadius">扇形半径</param>
    /// <param name="attacker">攻击者的transform信息</param>
    /// <param name="target">目标</param>
    /// <returns>目标target在扇形区域内返回true 否则返回false</returns>
    public bool IsInRange(float sectorAngle, float sectorRadius, Transform attacker, Transform target)
    {
        //攻击者位置指向目标位置的向量
        Vector3 direction = target.position - attacker.position;
        //点乘积结果
        float dot = Vector3.Dot(direction.normalized, transform.forward);
        //反余弦计算角度
        float offsetAngle = Mathf.Acos(dot) * Mathf.Rad2Deg; //弧度转度
        return offsetAngle < sectorAngle * .5f && direction.magnitude < sectorRadius;
    }
    #endregion

}




