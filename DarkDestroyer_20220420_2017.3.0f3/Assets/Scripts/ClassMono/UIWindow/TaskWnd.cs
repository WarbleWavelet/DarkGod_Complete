/****************************************************
    文件：TaskWnd.cs
	作者：lenovo
    邮箱: 
    日期：2022/5/23 18:21:43
	功能：任务UI
*****************************************************/

using PEProtocol;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskWnd : WindowRoot 
{
    #region 单例
    private static TaskWnd _instance;      

    public static TaskWnd Instance

    {
        get
        {
            if (_instance == null)
            {
                _instance = new TaskWnd();
            }
        return _instance;
        }
    }
    #endregion
    [Header("TaskWnd")]

    public Button btnClose;
   // public Button btnReward;
    bool isFist = true;
    public Transform content;
    PlayerData pd = null;
    List<TaskRewardData> taskDataLst = new List<TaskRewardData>();

    protected override void InitWnd()
    {
        base.InitWnd();
        pd = GameRoot.Instance.PlayerData;
        if (isFist)
        {
            InitWndOnce();
            isFist = false;
        }
    }
    public void RefreshUI()
    {
        
        UpdateTaskDataList();
        InitTaskItemGoArr();
        //
        for (int i = 0; i < taskDataLst.Count; i++)
        {
            Transform t = content.GetChild(i);
            GameObject go = t.gameObject;
            TaskRewardData data = taskDataLst[i];
            TaskRewardCfg cfg = resSvc.GetTaskRewardCfg(data.ID);
            SetItemUI(t,cfg,data);
        }
    }

    void InitTaskItemGoArr()
    {


        int offset =content.childCount - taskDataLst.Count; 
        int offset_abs = Mathf.Abs(offset);
        if (offset_abs==0)
        {
            return;
        }
       else if (content.childCount > taskDataLst.Count)
        {
            for (int i = 0; i < offset_abs; i++)
            {
                Transform t = content.GetChild(i);
                Destroy(t.gameObject);
            }
        }
        else if (content.childCount < taskDataLst.Count)
        {
            if (resSvc == null) resSvc = ResSvc.Instance;
            for (int i = 0; i < offset_abs; i++)
            {
                GameObject go = resSvc.LoadPrefab(PathDefine.TaskItemPrefab);
                go.transform.parent = content;
            }
        }
    }




        private void SetItemUI(Transform t, TaskRewardCfg cfg, TaskRewardData data)
    {
        SetSprite( GetTrans( t,"imgTask"),PathDefine.TaskDailySprite);
        SetText( GetTrans(t,"txtName"),cfg.taskName);
        SetText( GetTrans(t,"txtPrg"),data.prgs+"/"+cfg.count);
        GetTrans( t,"prgbar/imgpre").GetComponent<Image>().fillAmount = (1.0f * data.prgs) / cfg.count;
        SetText( GetTrans(t,"prizeRoot/exp/txt"), cfg.exp);
        SetText( GetTrans(t,"prizeRoot/coin/txt"), cfg.coin);
        //
        SetBtnPrizeUI(t, cfg, data);

    }




    void SetBtnPrizeUI(Transform t, TaskRewardCfg cfg, TaskRewardData data)
    {
        Transform btnPrizeTrans = GetTrans(t, "btnPrize");
        Button btnPrize = btnPrizeTrans.GetComponent<Button>();


        switch (data.state)
        {
            case TaskState.Accept:
                {
                    SetSprite(btnPrizeTrans, PathDefine.TaskPrizeGrayImg);
                    SetActive(GetTrans(btnPrizeTrans, "imgDone"), false);
                    btnPrize.onClick.AddListener(() => {
                        GameRoot.AddTips("任务未完成");
                    });
                }
                break;
            case TaskState.Done:
                {
                    SetSprite(btnPrizeTrans, PathDefine.TaskPrizeImg);
                    SetActive(GetTrans(btnPrizeTrans, "imgDone"), false);
                    btnPrize.onClick.AddListener(()=>ClickBtnPrize(cfg,data));
                }
                break;
            case TaskState.Got:
                {
                    SetSprite(btnPrizeTrans, PathDefine.TaskPrizeGrayImg);
                    SetActive(GetTrans(btnPrizeTrans, "imgDone"), true);
                    t.SetAsLastSibling();
                }
                break;
            default: break;
        }

    }
    void InitWndOnce()
    {
        btnClose.onClick.AddListener(ClickBtnClose);
    }



    void ClickBtnPrize(TaskRewardCfg cfg, TaskRewardData data)
    {
        GameMsg msg = new GameMsg
        {
            cmd = (int)CMD.ReqTakeTaskReward,
            reqTakeTaskReward = new ReqTakeTaskReward
            {
                id = data.ID,
            }
        };

        netSvc.SendMsg(msg);
        GameRoot.AddTips(Constants.Color("获得经验" + cfg.exp, TxtColor.Yellow));
        GameRoot.AddTips(Constants.Color("获得金币" + cfg.coin, TxtColor.Blue));
    }
    private void ClickBtnClose()
    {
        audioSvc.PlayUIAudio(Constants.UIClickBtn);
        InitTaskItemGoArr();
        SetWndState(false);
    }

    void UpdateTaskDataList()
    {
        taskDataLst.Clear();
        taskDataLst = new List<TaskRewardData>();
        List<TaskRewardData> lst = new List<TaskRewardData>();
        List<TaskRewardData> doneLst = new List<TaskRewardData>();
        List<TaskRewardData> gotLst = new List<TaskRewardData>();
        PlayerData pd = GameRoot.Instance.PlayerData;
        for (int i = 0; i < pd.taskRewardArr.Length; i++)
        {

            string[] dataArr = pd.taskRewardArr[i].Split('|');
            // ID | 已经完成次数 | 是否已经领取奖励
            int ID = int.Parse(dataArr[0]);
            int count = int.Parse(dataArr[1]);
            TaskState state = (TaskState)int.Parse(dataArr[2]);

            TaskRewardData data = new TaskRewardData
            {
                ID = ID,
                prgs = count,
                state = state,
            };
            //
            if (data.state == TaskState.Accept)
            {
                lst.Add(data);
            }
            else if (data.state == TaskState.Done)
            {
                doneLst.Add(data);
            }
            else if (data.state == TaskState.Got)
            {
                gotLst.Add(data);
            }
        }
        taskDataLst.AddRange(doneLst);
        taskDataLst.AddRange(lst);
        taskDataLst.AddRange(gotLst);

    }



}