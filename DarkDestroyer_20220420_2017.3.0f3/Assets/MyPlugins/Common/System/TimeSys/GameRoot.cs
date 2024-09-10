using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



[RequireComponent(typeof(Time8FrameTaskProcrssorMgr))]
public class GameRoot : MonoBehaviour
{
    Time8FrameTaskProcrssorMgr timerSys;
    [Tooltip("Ϊ�˲���ɾ��")] public int id;
    //

    [SerializeField] Button AddTimeBtn;
    [SerializeField] Button DeleteTimeBtn;
    [SerializeField] Button ReplaceTimeBtn;
    [SerializeField] Button AddFrameBtn;
    [SerializeField] Button DeleteFrameBtn;
    [SerializeField] Button ReplaceFrameBtn;
    // Start is called before the first frame update
    void Start()
    {
        timerSys = GetComponent<Time8FrameTaskProcrssorMgr>();
        timerSys.Init();
        //
        AddTimeBtn=transform.GetComponentDeep<Button>("AddTimeBtn");
        DeleteTimeBtn = transform.GetComponentDeep<Button>("DeleteTimeBtn");
        ReplaceTimeBtn = transform.GetComponentDeep<Button>("ReplaceTimeBtn");
        AddFrameBtn = transform.GetComponentDeep<Button>("AddFrameBtn"); ;
        DeleteFrameBtn = transform.GetComponentDeep<Button>("DeleteFrameBtn");
        ReplaceFrameBtn = transform.GetComponentDeep<Button>("ReplaceFrameBtn");
        //
        AddTimeBtn.onClick.AddListener(OnAddTimeTaskClick);
        DeleteTimeBtn.onClick.AddListener(OnDeleteTimeTaskClick); ;
        ReplaceTimeBtn.onClick.AddListener(OnReplaceTimeTaskClick); ;
        AddFrameBtn.onClick.AddListener(OnAddFrameTaskClick); ;
        DeleteFrameBtn.onClick.AddListener(OnDeleteFrameTaskClick);
        ReplaceFrameBtn.onClick.AddListener(OnReplaceFrameTaskClick); ;
    }

    #region pri
    #region ʱ��
    void OnAddTimeTaskClick()
    {
        id = timerSys.AddTimeTask(
            () =>
            {
                print("FuncA,id:" + id+",Time:" + System.DateTime.Now);
            },
            1000f, PETimeUnit.MillSecond, 0);//0��ѭ��
    }
     void OnDeleteTimeTaskClick()
    {
        timerSys.DeleteTimeTask(id);
    }
     void OnReplaceTimeTaskClick()
    {
        bool isReplaced = timerSys.ReplaceTimeTask(id, () =>
        {
            print("FuncB");
        }, 1000f, PETimeUnit.MillSecond, 1);
        if (isReplaced) { print("FuncA replaced by FuncB"); }
    }
    #endregion
    #region ֡
     void OnAddFrameTaskClick()
    {
        id = timerSys.AddFrameTask(() =>
        {
            print("FuncA,id:" + id+",ʱ��:" + System.DateTime.Now);
        },
        60, 0);//0��ѭ��
    }
     void OnDeleteFrameTaskClick()
    {
        timerSys.DeleteFrameTask(id);
    }
     void OnReplaceFrameTaskClick()
    {
        bool isReplaced = timerSys.ReplaceFrameTask(id, () => 
        { 
            print("FuncB"); 
        }, 60,  1);
        if (isReplaced) { print("֡�滻�ɹ���"); }
    }
    #endregion
    #endregion

}


