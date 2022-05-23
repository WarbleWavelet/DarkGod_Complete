/************************************************************
    文件：GameStart.cs
	作者：Plane
    QQ ：1785275942
    日期：2018/10/29 5:18
	功能：PESocket客户端使用示例
*************************************************************/

using PENet;
using Protocol;
using UnityEngine;

public class GameStart : MonoBehaviour 
{
    PESocket<ClientSession, NetMsg> socket = null;

    private void Start() {
        socket = new PESocket<ClientSession, NetMsg>();
        socket.StartAsClient(IPCfg.srvIP, IPCfg.srvPort);

        socket.SetLog(true, (string msg, int lv) => {
            switch (lv) {
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
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            socket.session.SendMsg(new NetMsg 
            {
                text = "Hello Unity"
            });
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            socket.ShutDown();
        }
    }
}