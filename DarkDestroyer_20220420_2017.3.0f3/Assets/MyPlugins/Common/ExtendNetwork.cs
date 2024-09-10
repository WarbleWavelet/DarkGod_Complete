/****************************************************
    文件：ExtendNetwork.cs
	作者：lenovo
    邮箱: 
    日期：2024/7/13 11:29:15
	功能：
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public static partial class ExtendNetwork         //UDPTCP
{


    #region interface
    /// <summary>时序性</summary>
    interface ITemporality { void Temporality(); }
    /// <summary>重传机制</summary>
    interface IRetransmissionMechanism { void RetransmissionMechanism(); }
    /// <summary>应答机制</summary>
    interface IResponseMechanism { void ResponseMechanism(); }
    /// <summary>延时机制</summary>
    interface IDelayMechanism { void DelayMechanism(); }
    /// <summary>数据安全检验机制</summary>
    interface IDataSecurityVerificationMechanism :ITemporality ,IRetransmissionMechanism ,    IResponseMechanism{ }
    /// <summary>通信协议</summary>
    interface ICommunicationProtocol :  IDelayMechanism  , IDataSecurityVerificationMechanism
    { }
    #endregion


    #region TCP

    #endregion

    class TCP : ICommunicationProtocol
    {
        public void DelayMechanism()
        {
            //集装箱概念
            //一般50ms
        }

        public void ResponseMechanism()
        {
            //喊一次没反应,再喊一次
        }

        public void RetransmissionMechanism()
        {
            //同ResponseMechanism()
        }

        public void Temporality()
        {
            //能保证
        }
    }
    class UDP : ICommunicationProtocol
    {
        public void DelayMechanism()
        {
            //像发短信
        }

        public void ResponseMechanism()
        {
           //没有
        }

        public void RetransmissionMechanism()
        {
            //没有
        }

        public void Temporality()
        {
            //没有
        }
    }
    class ReliableUDP : ICommunicationProtocol
    {
        public void DelayMechanism()
        {
            throw new System.NotImplementedException();
        }

        public void ResponseMechanism()
        {
            throw new System.NotImplementedException();
        }

        public void RetransmissionMechanism()
        {
            throw new System.NotImplementedException();
        }

        public void Temporality()
        {
            throw new System.NotImplementedException();
        }
    }
}
public static partial class ExtendNetwork //帧同步
{
    //https://www.bilibili.com/video/BV1TeaveJENy/?spm_id_from=333.1007.tianma.1-3-3.click&vd_source=54db9dcba32c4988ccd3eddc7070f140
    //数据同步:状态同步(发操作,收状态),帧同步(发操作,收操作)
    //高并发:异步并发,分布式,负载均衡
    //通信协议:协议模型,协议定义,协议序列化
    //业务逻辑:业务框架,业务逻辑,消息通讯
    //热更新:数据更新,逻辑更新
    //通信传输:快速可靠,高吞吐量
    //数据库:数据存取,集群
    //


    #region interface
    interface ITechnology { string Technology(); }
    interface ISeverCode { string SeverCode(); }
    interface IClientCode { string ClientCode(); }
    interface IReconnect { string ReConnect(); }
    interface IBandWidth { string BandWidth(); }
    interface IClientExperience { string ClientExperience(); }
    interface IOfflineBattle{ string OfflineBattle(); }
    interface IAntiCheating { string AntiCheating(); }
    interface ISyncTechnology : ITechnology, ISeverCode, IClientCode, IReconnect, IBandWidth, IClientExperience, IOfflineBattle, IAntiCheating { }
    interface IFrameSyncGoal
    {
       //可靠UDP
       //确定性的数学,物理运算库(定点数的碰撞)(取整, 容许小概率, 逻辑表现分离)
       //断线重连
       //比赛回放
       //反作弊
       //避免等待
    }

    #endregion
    class FrameSync : ISyncTechnology, IFrameSyncGoal
    {
        public string AntiCheating()
        {
            //双端协助
            return "";
        }

        public string BandWidth()
        {
            //传递操作,开销小
            return "";
        }

        public string ClientCode()
        {
            //追求数学,物理稳定,定点数学
            //随机数,字典不能用
            return "";
        }

        public string ClientExperience()
        {
            //本地运算,反应灵敏,打击感强
            return "";
        }

        public string OfflineBattle()
        {
            //支持
            return "";
        }

        public string ReConnect()
        {
            //追帧
            return "";
        }

        public string SeverCode()
        {
            //只负责转发
            return "";
        }

        public string Technology()
        {
            throw new System.NotImplementedException();
        }
    }
    class StateSync : ISyncTechnology
    {
        public string AntiCheating()
        {
            //服务器权威
            return "";
        }

        public string BandWidth()
        {
            //手控对象都需要传数据
            return "";
        }

        public string ClientCode()
        {
            //接收使用返回的结果
            return "";
        }

        public string ClientExperience()
        {
            //延迟相对高
            return "";
        }

        public string OfflineBattle()
        {
            //m没说
            return "";
        }

        public string ReConnect()
        {
            //下发状态数据
            return "";
        }

        public string SeverCode()
        {
            //大量逻辑代码
            return "";
        }

        public string Technology()
        {
            throw new System.NotImplementedException();
        }
    }


    abstract  class CtrlMsgBase
    {
        int CurKeyFrameNumber;
        object CtrlValue;
    }
    class UpdateMsg
    {
        int CurKeyFrameNumber;
        int NextKeyFrameNumber;
        CtrlMsgBase[] ctrlMsgBases;
    }

    class KeyFrame
    {
        #region 固定
         int Frames { get{ return 5; } }

		#endregion

        #region 服务器最大Ping预算
        // int Frames { get{ return ServerMaxPing(); } }


        /// <summary>服务器最大Ping预算</summary>
        int ServerMaxPing()
        {
            return 5;
        
        }
        #endregion  


    }

 }



