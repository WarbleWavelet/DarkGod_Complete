
/****************************************************
	文件：Chat.cs
	作者：WWS
	邮箱: 
	日期：2022/05/18 20:52   	
	功能：聊天系统
*****************************************************/
using PEProtocol;
using System;
using System.Collections.Generic;

class ChatSys
{
    #region 单例
    private static ChatSys _instance;

    public static ChatSys Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new ChatSys();
            }
            return _instance;
        }

    }
    #endregion

    public CacheSvc cacheSvc;
    public CfgSvc cfgSvc;
    public NetSvc netSvc;

    public void Init()
    {
        cacheSvc = CacheSvc.Instance;
        cfgSvc = CfgSvc.Instance;
        netSvc = NetSvc.Instance;
        PECommon.Log("ChatSys Init");
    }

    public void SndChat(MsgPack pack)
    {
        PlayerData pd = cacheSvc.GetPlayerDataBySession(pack.session);
       

        SndChat data = pack.msg.sndChat;
        GameMsg msg = new GameMsg
        {
            cmd = (int)CMD.PshChat,
            pshChat = new PshChat
            {
                name = pd.name,
                chat = data.chat

            }
           
        };
        msg.pshTaskPrgs = TaskSys.Instance.GetTaskPrgs(pd, TaskID.Speak);
        List<ServerSession> lst = cacheSvc.GetOnlineServerSession();

        byte[] bytes= PENet.PETool.PackNetMsg(msg);//不用多次序列化
        for (int i=0;i<lst.Count;i++)
        {
            lst[i].SendMsg(bytes);
        }
        //

     }
}