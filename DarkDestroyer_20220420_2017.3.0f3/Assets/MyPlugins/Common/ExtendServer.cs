/****************************************************
    文件：ExtendServer.cs
	作者：lenovo
    邮箱: 
    日期：2024/7/7 17:21:18
	功能：
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
 
public static partial class ExtendServer
{
    public enum E请求通信过程
        {
        DNS解析, // 一般不会有什么问题
        使用协议和证书,                                                        
        TLS握手,     //   有可能有证书过期的情况，这个时候页面会弹出不安全的警告   重新配置一下证书
        响应头响应内容   //反映本次请求是否成功

        // 域名对应的服务器IP地址，根据这个地址就可以去对应的服务器查找问题的根源
        //
        // 针对404而言：
        // 第一种情况就是nginx服务器没有配置转发或者转发条件不正确导致找不到页面资源；
        // 第二种情况是页面确实不存在，比如说没有对应的前端路由；所以一般都需要写一个兜底的路由，在nginx上也需要配置一个兜底的资源，这样即使找不到也不会出现404页面影响体验；
        //
        //针对500而言：一般可能是nginx服务挂了，500一般都是服务问题
    }
    public enum E协议
    {
        http,
        ftp,
        dict
            , sftp服务, ipv6地址
    }

    public enum E检测
    {
        referrer检测,
        host检测
    }
    public enum E请求
    {
        PUT,
        GET,
        POST,
        DELETE,
        range请求,
        restful接口请求,

    }
    public enum E功能
    {            
        伪造referrer ,
        展示进度下载速度, 限速下载
    }

    /// <summary></summary>
    public enum E命令行
    {
        npm_run_dev,
        npm_start,
        npm_run_build,
        node_v,
        linux,
        /// <summary>
        ///  01 curl是一个利用URL在命令行工作的文件传输工具
        ///  02 curl可以发送各种请求，支持各种协议，还能进行限速请求
        ///  03 curl可以用来做故障排查以及iconfont自动下载，创建测试数据
        ///  
        /// curl的核心还是在于下载文件
        ///  linux系统一般用yum或者apt-get去下载安装包, 简洁版的Linux系统就可能并不存在，只需通过执行“yum install curl”命令安装即可
        ///  unix系统中自带的就是curl

        

       /// </summary>
        curl
    }
}



