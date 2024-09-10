/****************************************************
    文件：AiMonster.cs
	作者：lenovo
    邮箱: 
    日期：2022/6/12 13:19:38
	功能：MonsterAI
*****************************************************/

using System;
using UnityEngine;

public class AIMonster :MonoBehaviour, IAILogic
{
    [Header("AiMonster")]
    public float findTimer = 0f;
    /// <summary>检测玩家位置的间隔</summary>
    public float findTime = 2f;
    public float atkTimer = 0f;
    public float atkTime = 1f;
    public EntityMonster from;
    public EntityPlayer to;
    public bool runAI = false;
    bool isInit = false;

    public void Init(EntityMonster from, EntityPlayer to)
    {
        runAI = true;
        findTimer = 0f;
        findTime = 3f;//Animator Born的时长是2.13
        atkTimer = 0f;
        atkTime = 3f;
        //
        this.from = from;
        this.to = to;
        isInit = true;
    }


    /// <summary>
    /// 放在Update的AI逻辑
    /// </summary>
    public  void TickAILogic()
    {      
        if (isInit == false) return;
        if (!runAI)  return;


        if (from.CurState == EAniState.Idle || from.CurState == EAniState.Move)
        {
            if (from.battleMgr.isPauseGame)//暂停游戏
            {
                from.StateIdle();
                return;
            }


            findTimer += Time.deltaTime;
            if (findTimer < findTime)
            {
                return;
            }
            else
            {
                Vector2 toDir = CalcTargetDir();
                if (toDir == Vector2.zero)
                {
                    runAI = false;
                }
                //
                to = from.battleMgr.playerEntity;
                if (InAtkRange(from, to, from.monsterData.mCfg.atkDis)== false )
                {
                    from.SetDir(toDir);
                    from.StateMove();
                }
                else
                {
                    from.SetDir(Vector2.zero);
                    atkTimer += findTime;//跑归来的时间算进攻速里面
                    if (atkTimer > atkTime)
                    {

                        from.SetAtkDir(toDir);
                        int skillID=from.monsterData.mCfg.skillID;
                       
                        from.SkillAttack(skillID);
                        //
                        atkTimer = 0f;
                    }
                    else
                    {
                        from.SetAniAction(Constants.ActionDefault);
                        from.StateIdle();
                    }
                }
                findTimer = 0f;
                findTime = (PETools.RDInt(1, 5) * 1.0f) / 10;   
            }
        }
    }

    
    #region pri

    Vector2 CalcTargetDir()
    {
       return from.CalcTargetDir();
    }


    /// <summary>
    /// 打得着
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    bool InAtkRange(EntityMonster from, EntityPlayer to, float atkDis)
    {
        if (to == null || to.CurState == EAniState.Die )
        {
            runAI = false;//试了不要提出去
            return false;
        }
        else
        {
            Vector3 fromPos = from.GetPos();
            Vector3 toPos = to.GetPos();
            fromPos.y = 0;
            toPos.y = 0;
            float dis = Vector3.Distance(fromPos, toPos);
            bool inRange = dis <=atkDis;

            return inRange;
        }
    }
    #endregion


}
