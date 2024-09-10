/****************************************************
    文件：BuffSystm.cs
	作者：lenovo
    邮箱: 
    日期：2024/4/22 11:49:20
	功能：
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;









#region BuffInfo
#region 操作
/// <summary>所以buff统一上来,在加到身上</summary>
public interface BuffHandler
{       
    void AddBuff();

    void RemoveBuff();
    void SortBuff();
    void BuffTick();

}
public abstract class BaseBuffModule
{
    /// <summary>参数可以加其它信息参数</summary>
    protected abstract void Apply(BuffInfo buffInfo);
}
#endregion


public class BuffInfo
{
    BuffData buffData;
    GameObject buffCreator;
    GameObject buffTarget;
    /// <summary>典韦被动层数</summary>
    int curStack;
    /// <summary>buff持续时间</summary>
    float durationTimer;
    /// <summary>buff当前层数持续时间</summary> 
    float tickTimer;


}
/// <summary>数据结构</summary>
public class BuffData
{
    BuffData_BaseInfo buffData_BaseInfo;
    BuffData_TimeInfo buffData_TimeInfo;
    BuffRemoveTimeUpdateEnum E_BuffRemoveTimeUpdateEnum;
    BuffTimeUpdateEnum E_BuffTimeUpdateEnum;
}
public class BuffData_BaseInfo
{
    int id;
    string buffName;
    string description;
    Sprite icon;
    /// <summary>最大层数</summary>
    int maxStack;
    /// <summary>斩杀与金身</summary>
    int priotity;
    /// <summary>持续伤害也分,灼烧和中毒</summary>
    string[] tags;
}
public class BuffData_TimeInfo
{
    /// <summary>典韦的被动</summary>
    bool isForever;
    float duration;
    /// <summary>每隔时间久回调</summary>
    float tickTime;
}

public interface IBuffData_CallBack
{
    void OnCreate();
    void OnRemove();
    void OnHit();
    void OnBeHurt();
    void OnKill();
    void OnBeKill();
    void OnTick();
    //On其他业务回调点



}


#region Enum
/// <summary>叠层buff减少方式</summary>
public enum BuffRemoveTimeUpdateEnum
{
    CLEAR,
    REDUCE
}
public enum BuffTimeUpdateEnum
{
    /// <summary>吸血百分比</summary>
    ADD,
    /// <summary>红蓝buff就是这</summary>
    REPLACE,
    NONE
}
#endregion
#endregion

public class BuffSystm : MonoBehaviour
    {
        #region 属性

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



