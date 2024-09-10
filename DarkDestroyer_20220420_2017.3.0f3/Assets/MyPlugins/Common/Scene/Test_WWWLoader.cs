/****************************************************
    文件：Test_WWWLoader.cs
	作者：lenovo
    邮箱: 
    日期：2024/7/21 19:11:0
	功能：
*****************************************************/

using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;



public class Test_WWWLoader : MonoBehaviour
{
    #region 属性
     public static  Dictionary<string, List<ConfigClassBase>> Dic = new Dictionary<string, List<ConfigClassBase>>();

    //key:表名 val 表数据列表

    EBDWWWLoader Loader
    {
        get 
        {
            return new EBDWWWLoader();
        }
    }


    #endregion

    #region 生命


    /// <summary>首次载入且Go激活</summary>
    void Start()
    {
        //
        //Debug.Log(ExcelTool.Excel2Bin(Application.streamingAssetsPath+"/Config/Xls/test.xls"))  ;
        //
        this.StartCoroutine(Loader.Bin2Dic("ExcelATest1.msconfig", LogData,Dic));
        this.StartCoroutine(Loader.Bin2Dic("ExcelATest2.msconfig", LogData,Dic));
       // this.StartCoroutine(Loader.Bin2Dic("ExcelBTest1.msconfig", LogData));

    }

    void LogData()
    {
        Debug.Log(JsonMapper.ToJson(Dic["ExcelATest1.msconfig"][0]));
        Debug.Log(JsonMapper.ToJson(Dic["ExcelATest1.msconfig"][1]));
       // Debug.Log(JsonMapper.ToJson(Dic["ExcelATest1.msconfig"][2]));
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



