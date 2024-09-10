/****************************************************
    文件：missile.cs
	作者：lenovo
    邮箱: 
    日期：2023/10/18 20:59:17
	功能：作者：落日余晖_LRYH https://www.bilibili.com/read/cv26823744/ 

*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class Missile : MonoBehaviour
{
    //最大转弯速度
    private float _maximumRotationSpeed = 120.0f;
    //加速度
    private float _acceleratedVeocity = 12.8f;
    //最高速度
    private float _maxVelocity = 100.0f;
    //生命周期
    private float _maximumLifeTime = 20.0f;
    //上升期时间
    private float _accelerationPeriod = 15.0f;
    //目标
    public Transform Target;
    //当前速度
    private float _curVelocity = 0.1f;
    //生命期
    private float _lifeTime = 0.0f;

    private void Update()
    {
        float deltaTime = Time.deltaTime;
        _lifeTime += deltaTime;

        //如果超出生命周期,则3s后销毁
        if (_lifeTime > _maximumLifeTime)
        {
            Destroy(this.gameObject, 3.0f);
            return;
        }

        //计算朝向目标的方向偏移量,如果处于上升期,则忽略目标
        Vector3 offset = Vector3.zero;
        if ( _lifeTime < _accelerationPeriod && Target != null )
        {
            offset = Vector3.up;
        }
        else
        {
            offset=(Target.position - transform.position).normalized;
        }


        //计算当前方向与目标方向的角度差
        float angle = Vector3.Angle(transform.forward, offset);

        //根据最大旋转速度,计算转向目标共计需要的时间
        float needTime = angle / (_maximumRotationSpeed * (_curVelocity / _maxVelocity));

        //如果角度很小,就直接对准目标
        if (needTime < 0.001f)
        {
            transform.forward = offset;
        }
        else
        {
            //当前帧间隔时间除以需要的时间,获取本次球形插值,并旋转游戏对象方向至面向目标
            transform.forward = Vector3.Slerp(transform.forward, offset, deltaTime / needTime).normalized;
        }

        //如果当前速度小于最高速度,则进行加速
        if (_curVelocity < _maxVelocity)
        { 
           _curVelocity += deltaTime * _acceleratedVeocity;
        } 

        //朝自己的前方位移
        transform.position += transform.forward * _curVelocity * deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        //如果碰撞,则销毁
        Destroy(gameObject);
      //  Destroy(gameObject, 3.0f);
    }
}

