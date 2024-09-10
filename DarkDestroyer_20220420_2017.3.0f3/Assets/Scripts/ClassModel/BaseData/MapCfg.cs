/****************************************************
    文件：MapCfg.cs
	作者：lenovo
    邮箱: 
    日期：2024/9/7 22:25:12
	功能：
*****************************************************/

using System.Collections.Generic;
using UnityEngine;

#region 地图
/**
-<item ID="10001">
    <mapName>荒野遗迹</mapName>
    <sceneName>SceneOrge</sceneName>
    <power>5</power>
    <mainCamPos>-13.19,18.87,14.69</mainCamPos>
    <mainCamRote>45,135,0</mainCamRote>
    <playerBornPos>-10,13.2,11.5</playerBornPos>
    <playerBornRote>0,145,0</playerBornRote>
    <monsterLst>#|1001,-4.39,13.3,3.79,-50,1|1001,-7.55,13.3, 3,0,1#|1001,18.86,13.6,3.7,-107.3,2|1001,14.35,13.35,5.95,-117.4,2|1001,15.11,13.35,1.63,-66.1,2#|1001,18.16,8.8,32,188,3|1001,11.8,8.8,30.8,145.5,3|1001,15.38,8.8,40.7,173.3,3|1001,9,8.9,38.6,145.5,3|2001,11.4,8.85,41,142,1</monsterLst>
    <exp>1250</exp>
    <coin>980</coin>
    <crystal>48</crystal>
</item>
    MapID,nmoster位置
    |一个
    #一波
**/
public class MapCfg : BaseData<MapCfg>
{
    public string sceneName;
    public string mapName;
    public Vector3 mainCamPos;
    public Vector3 mainCamRote;
    public Vector3 playerBornPos;
    public Vector3 playerBornRote;
    /// <summary>体力限制</summary>
    public int power;
    public List<MonsterData> monsterLst;
    //通关奖励
    public int exp;
    public int coin;
    public int crystal;
}
#endregion