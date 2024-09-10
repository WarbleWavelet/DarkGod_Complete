/****************************************************
    文件：Missile.cs
	作者：lenovo
    邮箱: 
    日期：2023/12/9 17:46:22
	功能：
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
 
/// <summary>
/// 抛物线导弹
/// <para>计算弹道和转向</para>
/// <para>ZhangYu 2019-02-27</para>
/// </summary>
public class Missile02 : MonoBehaviour
{ 

    Transform _to;        // 目标
    float _height = 16f;      // 高度
    float _gravity = -9.8f;   // 重力加速度
    private ParabolaPath _path;      // 抛物线运动轨迹


    /// <summary>这种会更新位置</summary>
    public  void Init(Transform to)
    {
        _to = to;
        _path = new ParabolaPath(transform.position, _to.position, _height, _gravity);
        _path.isClampStartEnd = true;
        transform.LookAt(_path.GetPosition(_path.timer + Time.deltaTime));
    }


    /// <summary>这种定死位置</summary>
    public void Init(Vector3 toPos)
    {
        _path = new ParabolaPath(transform.position, toPos, _height, _gravity);
        _path.isClampStartEnd = true;
        transform.LookAt(_path.GetPosition(_path.timer + Time.deltaTime));
    }

    private void Update()
    {
        // 计算位移
        float t = Time.deltaTime;
        _path.timer += t;
        transform.position = _path.position;

        // 计算转向
        transform.LookAt(_path.GetPosition(_path.timer + t));

        // 简单模拟一下碰撞检测
        if (_path.timer >= _path.time)
        { 
            enabled = false;
            DestroyImmediate(gameObject);
        }
    }

}




