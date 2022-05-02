using PENet;
using Protocol;
using UnityEngine;

public class ClientSession : PESession<NetMsg> {
    protected override void OnConnected()
    {
        Debug.Log("Connectting to Server");

    }

    protected override void OnReciveMsg(NetMsg msg)
    {
        Debug.Log("Connected To Server：" + msg.text);

    }

    protected override void OnDisConnected()
    {
        Debug.Log("Disconnected To Server");
        

    }
}