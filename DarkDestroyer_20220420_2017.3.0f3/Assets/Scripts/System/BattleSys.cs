/****************************************************
    文件：BattleSys.cs
	作者：lenovo
    邮箱: 
    日期：2022/5/29 10:41:4
	功能：战斗系统(Sys=>各种Mgr)
*****************************************************/

using PEProtocol;
using System;
using UnityEngine;

public class BattleSys : SystemRoot 
{

    #region 单例

    [Header("BattleSys")]
    private static BattleSys _instance;
    double startTime;
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
    public MainCitySys mainCitySys;
    public DynamicWnd dynamicWnd;

    public BattleMgr battleMgr;
    public PlayerCtrlWnd playerCtrlWnd;
    public EndBattleWnd endBattleWnd;

   public int instanceID = -1;
    public override void InitSys()
    {
        base.InitSys(); 
        _instance= this;
    }



    #region Start End Battle
 public void StartBattle(int mapID)
    {
        instanceID = mapID;
        //
        GameObject go = new GameObject
        { 
            name="BattleRoot",     
           
        };
        go.transform.SetParent(GameRoot.Instance.transform);
        //
        battleMgr=go.AddComponent<BattleMgr>();
        mainCitySys=GameRoot.Instance.GetComponent<MainCitySys>();
        dynamicWnd = GameRoot.Instance.dynamicWnd;
        //TODO
        battleMgr.InitMap(mapID, () => {
            startTime = TimerSvc.Instance.GetNowTime();
        });
    }
    /// <summary>
    /// 敌人打死了；玩家被打死；玩家自行退出战斗；玩家掉线
    /// </summary>
    public void EndBattle(bool isWin, int hp)
    {
        playerCtrlWnd.SetWndState(false);
        GameRoot.Instance.dynamicWnd.ClearHpItemInfo();

        if (isWin)
        {
            double endTime=TimerSvc.Instance.GetNowTime();
            GameMsg msg = new GameMsg
            {
                cmd = (int)CMD.ReqInstanceFightEnd,
                reqInstanceFightEnd = new ReqInstanceFightEnd
                {
                    instance = instanceID,
                    isWin = isWin,
                    remainHP = hp,
                    costTime = (int)((endTime-startTime)/1000)
                }

            };
            netSvc.SendMsg(msg);
        }

    }


    #endregion



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

    internal void DestroySelf()
    {
        BattleSys.Instance.playerCtrlWnd.SetWndState(false);
        BattleSys.Instance.endBattleWnd.SetWndState(false);

        BattleSys.Instance.dynamicWnd.ClearHpItemInfo();
        Destroy( battleMgr.gameObject );
    }

    public void SetEndBattleWndState(EndBattleType type, bool state=true)
    { 
        endBattleWnd.SetEndBattleWndState(type, state);
    }

    internal void RspInstanceFightEnd(GameMsg msg)
    {
        RspInstanceFightEnd data = msg.rspInstanceFightEnd;
        GameRoot.Instance.SetPlayerDataByInstanceEnd(data);
        //顺序
        endBattleWnd.SetEndBattleData(data.instance, data.costTime, data.remainHP);
        SetEndBattleWndState(EndBattleType.Win);
    }
}