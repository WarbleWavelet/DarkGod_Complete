/****************************************************
    文件：NetSvc.cs
	作者：lenovo
    邮箱: 
    日期：2022/5/2 22:47:4
	功能：网络服务
*****************************************************/

using PEProtocol;
using UnityEngine;
using PENet;
using System.Collections.Generic;
using System;

public class NetSvc : MonoBehaviour
{

  public  PESocket<ClientSession, GameMsg> client = null;
    public static NetSvc Instance = null;
    public Queue<GameMsg> msgQue = new Queue<GameMsg>();
    public static readonly string obj = "lock";


    public void InitSvc()
    {
        Instance = this;
        PECommon.Log("Init NetSvc ", LogType.Log);

        client = new PESocket<ClientSession,GameMsg>();


        #region SetLog


        client.SetLog(true, (string msg, int lv) => 
        {
            msg += "\n";
            switch (lv)
            {
                case 0:
                    msg += "Log:" + msg;
                    Debug.Log(msg);
                    break;
                case 1:
                    msg += "Warn:" + msg;
                    Debug.LogWarning(msg);
                    break;
                case 2:  //强制类型转换Error过 ,这种情况一般是启动错服务器了.一个是简单例子(代码少),一个是对应的
                    msg += "Error:" + msg;
                    Debug.LogError(msg);
                    break;
                case 3:
                    msg += "Info:" + msg;
                    Debug.Log(msg);
                    break;
            }
        });
        #endregion
   

        client.StartAsClient(SrvCfg.srvIp, SrvCfg.srvPort);
    }

    private void Update()
    {
        if (msgQue.Count > 0)
        {
            GameMsg msg = msgQue.Dequeue();
            ProgressMsg(msg);
        }
    }

    private void ProgressMsg(GameMsg msg)
    {
        if ( msg.err != (int)ErrorCode.None)
        {
            ProgressError(msg);
            return;
        }
        else
        {
            ProgressCMD(msg);
        }
    }



    public void SendMsg(GameMsg msg)
    {
        if (client.session != null)
        {
            client.session.SendMsg(msg);
        }
        else
        {
            GameRoot.AddTips("ReConnecting");
            InitSvc();
        }
    }

    /// <summary>
    /// 服务器返回的消息推入Queue
    /// </summary>
    /// <param name="msg"></param>
    public void AddNetPkg(GameMsg msg)
    {
        lock (obj)
        {
            msgQue.Enqueue(msg);
        
        }
    }


    #region 辅助
    void ProgressError(GameMsg msg)
    {
        switch ((ErrorCode)msg.err)
        {
            case ErrorCode.AcctIsOnLine:
                {
                    GameRoot.AddTips("账号已被登录中");
                }
                break;
            case ErrorCode.WrongPass:
                {
                    GameRoot.AddTips("密码错误");
                }
                break;
            case ErrorCode.UpdateDBError:
                {
                    //一个非技术性的问题
                    //GameRoot.AddTips("数据库更新异常");
                    PECommon.Log("数据库更新异常",LogType.Error);
                    GameRoot.AddTips("网络不稳定");
                }
                break;
            case ErrorCode.ServerDataError:
                {
                    PECommon.Log("服务器数据异常", LogType.Error);
                    GameRoot.AddTips("客户端数据异常");
                }
                break;
            case ErrorCode.LackCoin:
              
                {  PECommon.Log(ErrorCode.LackCoin.ToDes(), LogType.Error);
                }
                break;
            case ErrorCode.LackDiamond:

                {
                    PECommon.Log(ErrorCode.LackDiamond.ToDes(), LogType.Error);
                }
                break;
            case ErrorCode.LackCrystal:

                {
                    PECommon.Log(ErrorCode.LackCrystal.ToDes(), LogType.Error);
                }
                break;
            case ErrorCode.LackPower:

                {
                    PECommon.Log(ErrorCode.LackPower.ToDes(), LogType.Error);
                }
                break;
            case ErrorCode.LackLv:

                {
                    PECommon.Log(ErrorCode.LackLv.ToDes(), LogType.Error);
                }
                break;
            case ErrorCode.ClientDataError:

                {
                    PECommon.Log("客户端数据异常", LogType.Error);
                }
                break;
            default:
                {
                    GameRoot.AddTips("未知错误");
                }
                break;
        }
    }

    private void ProgressCMD(GameMsg msg)
    {
        switch ((CMD)msg.cmd)
        {
            case CMD.RspLogin:
                {
                    LoginSys.Instance.RspLogin(msg);
                }
                break;
            case CMD.RspRename:
                {
                    LoginSys.Instance.RspRename(msg);
                }
                break;
            case CMD.RspGuide:
                {
                    MainCitySys.Instance.RspGuide(msg);
                }
                break;
            case CMD.RspStrong:
                {
                    MainCitySys.Instance.RspStrong(msg);
                }
                break;
            case CMD.PshChat:
                {
                    MainCitySys.Instance.PshChat(msg);
                }
                break;
            case CMD.RspBuy:
                {
                    MainCitySys.Instance.RspBuy(msg);
                }
                break;
            case CMD.PshPower:
                {
                    MainCitySys.Instance.RspPower(msg);
                }
                break;
            case CMD.RspTakeTaskReward:
                {
                    MainCitySys.Instance.RspTakeTaskReward(msg);
                }
                break;
            case CMD.PshTaskPrgs://包合并，节省流量
                {
                   MainCitySys.Instance.PshTaskPrgs(msg);
                }
                break;
            case CMD.RspInstanceFight:
                {
                    InstanceSys.Instance.RspInstanceFight(msg);
                }
                break;
            case CMD.RspInstanceFightEnd:
                {
                    BattleSys.Instance.RspInstanceFightEnd(msg);
                }
                break;
        }
    }
    #endregion

}