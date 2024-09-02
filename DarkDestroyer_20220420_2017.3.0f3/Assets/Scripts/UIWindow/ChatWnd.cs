/****************************************************
    文件：ChatWnd.cs
	作者：lenovo
    邮箱: 
    日期：2022/5/18 13:49:20
	功能：聊天系统
*****************************************************/

using PEProtocol;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatWnd : WindowRoot
{

    #region 单例
    private static ChatWnd _instance;

    public static ChatWnd Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new ChatWnd();
            }
            return _instance;
        }

    }
    #endregion

    [Header("ChatWnd")]
    bool isOnce = false;
    public Image imgWorld;
    public Image imgGuild;
    public Image imgFriend;
    public ChatType chatType = 0;
    public Button btnClose;
    public InputField iptChat;
    public Button btnSend;

    public Dictionary<ChatType, List<String>> chatDic = new Dictionary<ChatType, List<string>>();
    public Text txtChat;
    public bool canSend = true;

    protected override void InitWnd()
    {
        base.InitWnd();

        if (isOnce == false)
        {
            btnClose.onClick.AddListener(ClickBtnClose);
            btnSend.onClick.AddListener(ClickBtnSend);
            imgWorld.GetComponent<Button>().onClick.AddListener(ClickBtnWorld);
            imgGuild.GetComponent<Button>().onClick.AddListener(ClickBtnGuild);
            imgFriend.GetComponent<Button>().onClick.AddListener(ClickBtnFriend);
            imgFriend.GetComponent<Button>().onClick.AddListener(ClickBtnFriend);
            //
            chatDic.Add(ChatType.WORLD, new List<string> { "特农业人", "一句话发" });
            chatDic.Add(ChatType.GUILD, new List<string> { "阿斯顿发顺丰", "阿斯顿发烧的" });
            chatDic.Add(ChatType.FRIEND, new List<string> { "大萨达是", "阿斯达谁的" });
            //
            isOnce = true;
        }



    }



    #region Click
  void ClickBtnClose()
    {
        audioSvc.PlayUIAudio(Constants.UIClickBtn);
        chatType=ChatType.WORLD;
        SetWndState(false);

    }

    void ClickBtnSend()
    {

        if (!canSend)
        {
            GameRoot.AddTips("还需要5s才能再次发言");
            return;
        }
      
        //
        if (iptChat.text != null && iptChat.text != "" && iptChat.text != " ")
        {
            string str = iptChat.text;
            if (iptChat.text.Length > Constants.MaxChatLenth)
            {
                GameRoot.AddTips("聊天内容超出最大长度");
                str = iptChat.text.Substring(0, Constants.MaxChatLenth);
            }
            GameMsg msg = new GameMsg
            {
                cmd = (int)CMD.SndChat,
                sndChat = new SndChat
                {
                    chat = str,
                }
            };
            canSend = false;
            iptChat.text = "";
            netSvc.SendMsg(msg);
            //
            timerSvc.AddTimerTask((int tid) => {
                canSend = true;
            }, 5,PETimeUnit.Second);
        }
        else
        {
            GameRoot.AddTips("未输入聊天内容");
        }
    }

    internal void OpenChatWnd()
    {
        SetWndState(true);
    }

    public void ClickBtnWorld()
    {
        audioSvc.PlayUIAudio(Constants.UIClickBtn);
        chatType = ChatType.WORLD;
        RefreshUI();
    }
    public void ClickBtnGuild()
    {
        audioSvc.PlayUIAudio(Constants.UIClickBtn);
        chatType = ChatType.GUILD;
        RefreshUI();
    }
    public void ClickBtnFriend()
    {
        audioSvc.PlayUIAudio(Constants.UIClickBtn);
        chatType = ChatType.FRIEND;
        RefreshUI();
    }
    #endregion



 

  
    public void RefreshUI()
    {
        SetSprite(imgWorld, PathDefine.BtnTypeUnselect);
        SetSprite(imgWorld, PathDefine.BtnTypeUnselect);
        SetSprite(imgWorld, PathDefine.BtnTypeUnselect);

        switch ( chatType )
        {
            case ChatType.WORLD :
                {

                    SetSprite(imgWorld, PathDefine.BtnTypeSelect);
                }
                break;
            case ChatType.GUILD:
                {
                    SetSprite(imgWorld, PathDefine.BtnTypeSelect);
                }
                break;
            case ChatType.FRIEND:
                {
                    SetSprite(imgWorld, PathDefine.BtnTypeSelect);
                }
                break;
            default:
                {

                }
                break;
        }
        txtChat.text = "";
        List<string> list = new List<string>();
        list = chatDic[chatType];
        for (int i = 0; i < list.Count; i++)
        {
            txtChat.text +=list[i] + "\n\n";
        }
    }

    public void AddChatMsg(string name, string chat)
    {
        string str1=Constants.Color(name + "："+"\n", TxtColor.Blue);
        string str2= Constants.Color(chat + "：", TxtColor.Yellow);

        chatDic[ChatType.WORLD].Add(str1 + str2);

        while (chatDic[ChatType.WORLD].Count > Constants.MaxChatCount)
        {
            chatDic[ChatType.WORLD].RemoveAt(0);
        }

        if (gameObject.activeSelf)
        { 
            RefreshUI();
        }
       
    }
}

public enum ChatType
{
    WORLD,
    GUILD,
    FRIEND
}