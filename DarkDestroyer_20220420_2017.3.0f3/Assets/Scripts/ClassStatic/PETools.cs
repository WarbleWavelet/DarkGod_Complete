/****************************************************
    文件：PETools.cs
	作者：lenovo
    邮箱: 
    日期：2022/4/30 14:19:7
	功能：工具类
*****************************************************/

using UnityEngine;

public static class PETools
{
    public static int RDInt(int min, int max,System.Random rd=null)
    {
        if (rd == null)
        { 
          rd=new  System.Random();
        }
        
        int val = rd.Next(min, max + 1);

        return val;
    }
}