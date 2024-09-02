using PENet;
using Protocol;
using UnityEngine;

public class ClientSession : PESession<NetMsg> {
    protected override void OnConnected()
    {
        Debug.Log("Connect to Server");

    }

    protected override void OnReciveMsg(NetMsg msg)
    {
        Debug.Log("ReciveMsg：" + msg.text);

    }

    protected override void OnDisConnected()
    {
        Debug.Log("Disconnect To Server");
        

    }
}