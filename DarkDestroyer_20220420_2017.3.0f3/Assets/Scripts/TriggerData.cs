/****************************************************
    文件：TriggerData.cs
	作者：lenovo
    邮箱: 
    日期：2022/6/28 11:52:35
	功能：地图触发器
*****************************************************/

using UnityEngine;

public class TriggerData : MonoBehaviour 
{
    
    public MapMgr mapMgr;
    /// <summary>触发怪物的批次</summary>
    public int waveIdx;

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag( Tags.Player))
        {
            if (mapMgr != null)
            {
                mapMgr.TriggerMonsterBorn( this, waveIdx);
            }
        }

    }


}