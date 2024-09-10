/****************************************************
    文件：InstanceSys.cs
	作者：lenovo
    邮箱: 
    日期：2022/5/27 1:25:49
	功能：副本系统
*****************************************************/

using PEProtocol;
using System;
using UnityEngine;

public class InstanceSys : SystemRoot 
{

    #region 单例
    private static InstanceSys _instance;      

    public static InstanceSys Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new InstanceSys();
            }
            return _instance;
        }
    }
    #endregion

    [Header("副本系统")]
    public InstanceWnd instanceWnd;
    public PlayerCtrlWnd playerCtrlWnd;

    public override void InitSys()
    {
        base.InitSys();
        _instance = this;
    }

    internal void RspInstanceFight(GameMsg msg)
    {
        RspInstanceFight data = msg.rspInstanceFight;
        GameRoot.Instance.SetPlayerDataByInstance(data);
        MainCitySys.Instance.maincityWnd.CloseWnd();
        MainCitySys.Instance.instanceWnd.CloseWnd();

        BattleSys.Instance.StartBattle(data.instance);
    }

    public void SetInstanceState(bool state=true)
    {
        instanceWnd.SetWndState(state);
       
    }


}