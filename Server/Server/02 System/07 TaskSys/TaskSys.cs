/****************************************************
	文件：TaskSys.cs
	作者：WWS
	邮箱: 
	日期：2022/05/23 22:43   	
	功能：任务系统
*****************************************************/
using PEProtocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class TaskSys
{
    #region 单例
    private static TaskSys _instance;

    public static TaskSys Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new TaskSys();
            }
            return _instance;
        }
    }
    #endregion

    public CacheSvc cacheSvc;
    private TimerSvc timerSvc;
    CfgSvc cfgSvc;
    NetSvc netSvc;

    public void Init()
    {
        cacheSvc = CacheSvc.Instance;
        timerSvc = TimerSvc.Instance;
        cfgSvc = CfgSvc.Instance;
        netSvc = NetSvc.Instance;
        //
        PECommon.Log("TaskSys Init");
    }


    /// <summary>
    /// 根据任务状态处理
    /// </summary>
    /// <param name="pack"></param>
    internal void ReqTakeTaskReward(MsgPack pack)
    {
        PlayerData pd = cacheSvc.GetPlayerDataBySession(pack.session);
        ReqTakeTaskReward reqData = pack.msg.reqTakeTaskReward;
        TaskRewardCfg cfg=   cfgSvc.GetTaskRewardCfg(reqData.id);
        TaskRewardData pdData = CalcTaskRewardData_ToClass(pd, reqData.id);
        GameMsg msg = new GameMsg
        {
            cmd = (int)CMD.RspTakeTaskReward
        };
        //

       
        switch ( pdData.state )
        {
            case TaskState.Accept:
                {
                    GetTaskPrgs(pd, (TaskID)reqData.id);

                }break;
            case TaskState.Done:
                {
                    pd.coin += cfg.coin;
                    PECommon.CalcExp(pd, cfg.exp);
                    pdData.state = TaskState.Got;
                    //
                    CalcTaskRewardData_ToString(pd, pdData);

                }
                break;
            default: break;
        }
        //
        if (!cacheSvc.UpdatePlayerData(pd.id, pd))
        {
            msg.err = (int)ErrorCode.UpdateDBError;
        }
        else
        {

            msg.rspTakeTaskReward = new RspTakeTaskReward
            {
                coin = pd.coin,
                exp = pd.exp,
                lv = pd.lv,
                taskArr = pd.taskRewardArr
            };


        }
        CalcTaskRewardData_ToString(pd, pdData);

        pack.session.SendMsg(msg);
    }

    /// <summary>
    /// 完成一次任务次数(返回PshTaskPrgs一起加到使用的msg，优化网络)
    /// </summary>
    /// <param name="pd"></param>
    /// <param name="taskid"></param>
    internal void CalcTaskPrgs(PlayerData pd, TaskID taskid)
    {
      TaskRewardCfg cfg=  cfgSvc.GetTaskRewardCfg((int)taskid);
        TaskRewardData data = CalcTaskRewardData_ToClass(pd, (int)taskid);

        if (data.prgs < cfg.count)
        {
            data.state = TaskState.Accept;
            data.prgs++;
            if (data.prgs == cfg.count)
            {
                data.state = TaskState.Done;
            }
            CalcTaskRewardData_ToString(pd, data);

            ServerSession session = cacheSvc.GetOnlineServerSession(pd.id);
            if (session != null)
            {
                GameMsg msg = new GameMsg
                {
                    cmd = (int)CMD.PshTaskPrgs,
                    pshTaskPrgs = new PshTaskPrgs
                    {
                        taskArr = pd.taskRewardArr
                    }
                };
                session.SendMsg(msg);
            }

        }

           
    }
    internal PshTaskPrgs GetTaskPrgs(PlayerData pd, TaskID taskid)
    {
        TaskRewardCfg cfg = cfgSvc.GetTaskRewardCfg((int)taskid);
        TaskRewardData data = CalcTaskRewardData_ToClass(pd, (int)taskid);

        if (data.prgs < cfg.count)
        {
            data.state = TaskState.Accept;
            data.prgs++;
            if (data.prgs == cfg.count)
            {
                data.state = TaskState.Done;
            }
            CalcTaskRewardData_ToString(pd, data);
            return new PshTaskPrgs
            {
                taskArr = pd.taskRewardArr
            };

        }
        else
        {
            return null;
        }

    }

    /// <summary>
    /// 返回TaskRewardData的类形式（根据taskId到pd.taskRewardArr里面找）
    /// </summary>
    /// <param name="pd"></param>
    /// <param name="taskId"></param>
    /// <returns></returns>
    TaskRewardData CalcTaskRewardData_ToClass(PlayerData pd, int taskId)
    {
        TaskRewardData data=null;
        string[] taskArr = pd.taskRewardArr;
        for (  int i=0;i<taskArr.Length;i++ )
        {
            string[] task = taskArr[i].Split('|');
            int ID = int.Parse(task[0]);
            if (ID == taskId)
            {
                data = new TaskRewardData
                {
                    ID = int.Parse(task[0]),
                    prgs = int.Parse(task[1]),
                    state = (TaskState)(int.Parse(task[2]))
                };
                break;
              
            }
        }
        return data;
    }


    /// <summary>
    /// 返回TaskRewardData的字符串形式（根据taskId到pd.taskRewardArr里面找）
    /// </summary>
    /// <param name="pd"></param>
    /// <param name="data"></param>
    void CalcTaskRewardData_ToString(PlayerData pd, TaskRewardData data)
    {

        string str = data.ID + "|" + data.prgs + "|" + (int)data.state;
        int idx = 0;
        for (int i = 0; i < pd.taskRewardArr.Length; i++)
        {
            string[] task = pd.taskRewardArr[i].Split('|');
            int ID = int.Parse(task[0]);
            if (ID == data.ID)
            {
                idx = i;
                break;
            }
        }

        pd.taskRewardArr[idx] = str;
    }
}

