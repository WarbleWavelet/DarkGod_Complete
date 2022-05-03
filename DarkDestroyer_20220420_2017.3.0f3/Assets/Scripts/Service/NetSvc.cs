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

public class NetSvc : MonoBehaviour
{

    PENet.PESocket<ClientSession, GameMsg> client = null;
    public static NetSvc Instance = null;


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
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    client.session.SendMsg(new GameMsg
        //    {
        //        text = "Hello Server"
        //    });
        //}
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
}