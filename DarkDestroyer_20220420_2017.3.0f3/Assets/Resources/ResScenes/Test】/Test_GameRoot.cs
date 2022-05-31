/****************************************************
    文件：Test_GameRoot.cs
	作者：lenovo
    邮箱: 
    日期：2022/5/31 16:53:54
	功能：
*****************************************************/

using UnityEngine;

public class Test_GameRoot : MonoBehaviour 
{
    void Start()
    {
        BattleSys.Instance.EnterMap(10001);
    }
}