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

    PENet.PESocket<ClientSession, GameMsg> client = null;
    public static NetSvc Instance = null;
    public Queue<GameMsg> msgQue = new Queue<GameMsg>();
    public static readonly string obj = "lock";


    public void InitSvc()
    {
        Instance = this;
        PECommon.Log("Init NetSvc ", LogType.Log);

        client = new PESocket<ClientSession,GameMsg>();
       

        client.SetLog(true, (string msg, int lv) => {
            switch (lv)
            {
                case 0:
                    msg = "Log:" + msg;
                    Debug.Log(msg);
                    break;
                case 1:
                    msg = "Warn:" + msg;
                    Debug.LogWarning(msg);
                    break;
                case 2:
                    msg = "Error:" + msg;
                    Debug.LogError(msg);
                    break;
                case 3:
                    msg = "Info:" + msg;
                    Debug.Log(msg);
                    break;
            }
        });

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
        if (msg.err != (int)ErrorCode.None)
        {
            ProgressError(msg);
            return;
        }
        else
        {
            ProgressCMD(msg);
        }
    }

    private void ProgressCMD(GameMsg msg)
    {
        switch ((CMD)msg.cmd)
        { 
            case CMD.RspLogin:
                {
                    LoginSys.Instance.RspLogin(msg);
                }break;
        
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
            default:
                {

                }
                break;
        }
    }
    #endregion

}