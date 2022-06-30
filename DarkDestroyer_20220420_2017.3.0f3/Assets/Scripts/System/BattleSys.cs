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
    public EndBattleWnd endBattleWnd;
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

    /// <summary>
    /// 敌人打死了；玩家被打死；玩家自行退出战斗；玩家掉线
    /// </summary>
    public void EndBattle(bool isWIn, int hp)
    {
        playerCtrlWnd.SetWndState(false);
        GameRoot.Instance.dynamicWnd.ClearHpItemInfo();

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

    public bool CanRlsSkill()
    {
        if (battleMgr.playerEntity != null)
        { 
        return battleMgr.playerEntity.canRlsSkill;
        }

        return false;
        
    }


    public void SetEndBattleWndState(EndBattleType type, bool state=true)
    { 
        endBattleWnd.SetEndBattleWndState(type, state);
    }
}