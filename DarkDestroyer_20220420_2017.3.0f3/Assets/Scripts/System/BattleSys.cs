/****************************************************
    文件：BattleSys.cs
	作者：lenovo
    邮箱: 
    日期：2022/5/29 10:41:4
	功能：战斗系统(Sys=>各种Mgr)
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

    [Header("BattleSys")]
   public BattleMgr battleMgr;
    public PlayerCtrlWnd playerCtrlWnd;
    public override void InitSys()
    {
        base.InitSys(); 
        _instance= this;
    }

    public void EnterMap(int mapID)
    {

        InstantiateBattleRoot( mapID);
        //
    }
    void InstantiateBattleRoot(int mapID)
    { 
        GameObject go = new GameObject
        { 
            name="BattleRoot",     
           
        };
        go.transform.SetParent(GameRoot.Instance.transform);
        //
        battleMgr=go.AddComponent<BattleMgr>();
        //TODO
        battleMgr.InitMap(mapID);
    }



    #region 控制玩家        
    public void ReqReleaseSkill(int idx)
    {
        battleMgr.ReqReleaseSkill(idx);
    }
    public void SetPlayerMoveDir(Vector2 dir)
    {
        battleMgr.SetPlayerMoveDir(dir);
    }
    #endregion


    public Vector2 GetInputDir()
    {
        return playerCtrlWnd.curDir;
    }
}