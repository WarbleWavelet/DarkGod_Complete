/****************************************************
    文件：GuideSys.cs
	作者：lenovo
    邮箱: 
    日期：2022/5/13 17:24:20
	功能：引导系统
*****************************************************/

using PEProtocol;
using System;
using UnityEngine;

public class GuideSys : SystemRoot 
{

    #region 单例
    private static GuideSys _instance;

    public static GuideSys Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GuideSys();
            }
            return _instance;
        }

    }
    #endregion

    public override void InitSys()
    {
        base.InitSys();
        PECommon.Log("GuideSys Init");
    }



}