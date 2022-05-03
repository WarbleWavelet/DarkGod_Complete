/****************************************************
    文件：ClientSession.cs
	作者：lenovo
    邮箱: 
    日期：2022/5/2 23:0:4
	功能：客户端网络会话
*****************************************************/

using PENet;
using PEProtocol;
using UnityEngine;

public class ClientSession : PESession<GameMsg>
{
    protected override void OnConnected()
    {
        GameRoot.AddTips("已连接服务器");
        PECommon.Log("A Client Connected",LogType.Log);

    }

    protected override void OnReciveMsg(GameMsg msg)
    {
        PECommon.Log("RcvPack CMD：" + ((CMD)msg.cmd).ToString());

    }

    protected override void OnDisConnected()
    {
        GameRoot.AddTips("已断开服务器");
        PECommon.Log("A Client Disconnected", LogType.Log);

    }
}