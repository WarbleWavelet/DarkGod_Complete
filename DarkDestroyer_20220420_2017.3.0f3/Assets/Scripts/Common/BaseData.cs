/****************************************************
    文件：BaseData.cs
	作者：lenovo
    邮箱: 
    日期：2022/5/8 15:59:36
	功能：数据配置类
*****************************************************/

using UnityEngine;

public class BaseData <T>
{
    public int ID;
}

public class MapCfg : BaseData<MapCfg>
{
    public string sceneName;
    public string mapName;
    public Vector3 mainCamPos;
    public Vector3 mainCamRote;
    public Vector3 playerBornPos;
    public Vector3 playerBornRote;
}