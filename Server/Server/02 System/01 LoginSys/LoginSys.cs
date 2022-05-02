using PENet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 登录业务系统
/// </summary>
    class LoginSys
    {
    private static LoginSys _instance;

    public static LoginSys Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new LoginSys();
            }
            return _instance;
        }
    }

    public void Init()
    {
        PECommon.Log("LoginSys Inited");
    }
}

