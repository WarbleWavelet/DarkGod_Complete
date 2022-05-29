/****************************************************
    文件：BattleSys.cs
	作者：lenovo
    邮箱: 
    日期：2022/5/29 10:41:4
	功能：战斗系统
*****************************************************/

using UnityEngine;

public class BattleSys : SystemRoot 
{

    #region 单例
    private static BattleSys _instance;      

    public static BattleSys Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new BattleSys();
            }
            return _instance;
        }
    }
    #endregion

 //  [Header("BattleSys")]
  
    public override void InitSys()
    {
        base.InitSys(); 
        _instance= this;
    }

    public void EnterMap(int mapID)
    {
        GameObject go = new GameObject
        { 
            name="BatleRoot",     
        };
        go.transform.SetParent(GameRoot.Instance.transform);
        //
        BattleMgr battleMgr=go.AddComponent<BattleMgr>();
        battleMgr.InitMap(mapID);
        //
    
    }

}