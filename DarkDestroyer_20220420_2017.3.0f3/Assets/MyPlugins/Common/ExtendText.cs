/****************************************************
    文件：ExtendText.cs
	作者：lenovo
    邮箱: 
    日期：2023/7/29 4:22:22
	功能：
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
 
public static class ExtendText 
{


    #region SetText(this Text text, object obj)


    /// <summary>节省ToString()</summary>
    public static Text SetText(this Text text, object obj)
    {
        if (text != null)
        { 
        text.text=obj.ToString();
        }

        //
        return text;
    }

    public static Transform SetText(this Transform trans, object obj)
    {
        Text text = trans.GetComponent<Text>();
        if (text == null)
        {
            throw new System.Exception("SetText异常");
        }
        if (text != null)
        {
            ("SetText " + trans.name +" "+obj).LogInfo();
            text.text = obj.ToString();
        }

        //
        return trans;
    }



    #region SetButtonText


    public static Transform SetButtonText(this Button btn, object obj)
    {
        return btn.transform.SetButtonText(obj).transform ;
    }


    /// <summary>SelfOrChild</summary>

    public static Transform SetButtonText(this Transform trans, object obj)
    {
        Text text = trans.GetComponent<Text>();
        if (text == null)
        {
            text = trans.GetComponentInChildren<Text>();
            if (text == null)
            {
                throw new System.Exception("SetText异常");
            }
         }
        if (text != null)
        {
            text.text = obj.ToString();
        }

        //
       
    #endregion
 return trans;
    }

    public static GameObject SetText(this GameObject go, object obj)
    {
        Text text = go.GetComponent<Text>();
        if (text == null)
        {
            throw new System.Exception("SetText异常");
        }
        if (text != null)
        {
            text.text = obj.ToString();
        }

        //
        return go;
    }

    #endregion


    public static Text SetText(this Text text, int content)
    {
        if (text != null)
        { 
               text.text = content.ToString();
        }
        //
       
        return text;
    }

    public static Text SetText(this Text text, float content)
    {
        if (text != null)
        { 
               text.text = content.ToString();
        }
        //
        
        return text;
    }
}




