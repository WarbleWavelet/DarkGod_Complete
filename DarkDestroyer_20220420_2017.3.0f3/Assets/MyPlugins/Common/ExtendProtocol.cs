/****************************************************
    文件：ExtendProtocol.cs
	作者：lenovo
    邮箱: 
    日期：2024/8/20 22:3:17
	功能：协议
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public static partial class ExtendProtocol//Socket
{






    #region SocketPack
    class SocketPack
    {  
        //Client异步
        //Server多路复用
        //
        //机密协议,公钥撕咬
        //心跳包
        //基于protobuf的数据协议
    }
    #endregion


    #region SocketLink

    /// <summary>Socket链接方式</summary>     enum ESocketLint    {
        /// <summary> BlockingIO，非阻塞式。纯非阻塞式，对IO的就绪与否需要在用户空间通过轮询来实现。</summary>  
        BIO,
        /// <summary> NonBlockingIO，阻塞式。纯采用阻塞式，这种方式很少见，基本只会出现在demo中。多个描述符需要用多个进程或者线程来一一对应处理。</summary>  
        NBIO,
        /// <summary>    BlockingMultiplexingIO，IO多路复用+阻塞式。仅使用一个线程就可以实现对多个描述符的状态管理，但由于IO输入输出调用本身是阻塞的，可能出现某个IO输入输出过慢，影响其他描述符的效率，从而体现出整体性能不高。此种方式编程难度比较低。</summary>  
        MBIO,
        /// <summary>    NonBlockingMultiplexingIO，IO多路复用+非阻塞式。在多路复用的基础上，IO采用非阻塞式，可以大大降低单个描述符的IO速度对其他IO的影响，不过此种方式编程难度较高，主要表现在需要考虑一些慢速读写时的边界情况，比如读黏包、写缓冲不够等。</summary>  
        MNBIO,
    }

    interface ISocketLink { }
    interface ISyncSocket:ISocketLink { }
    interface IAsyncSocket:ISocketLink { }
    // IO多路复用模型是建立在内核提供的多路分离函数select基础之上的，\
    // 使用select函数可以避免同步非阻塞IO模型中轮询等待的问题，此外poll、epoll都是这种模型。
    // 在该种模式下，用户首先将需要进行IO操作的socket添加到select中，然后阻塞等待select系统调用返回。
    // 当数据到达时，socket被激活，select函数返回。
    // 用户线程正式发起read请求，读取数据并继续执行。
    // 从流程上来看，使用select函数进行IO请求和同步阻塞模型没有太大的区别，甚至还多了添加监视socket，以及调用select函数的额外操作，效率更差。
    // 但是，使用select以后最大的优势是用户可以在一个线程内同时处理多个socket的IO请求。
    // 用户可以注册多个socket，然后不断地调用select读取被激活的socket，即可达到在同一个线程内同时处理多个IO请求的目的。

    /// <summary>
    /// 多路复用
    /// 同一个线程内同时处理多个IO请求
    /// </summary>
    interface IMultiplexingSocket : ISocketLink { }
    interface IAsyncEventArgsSocket: ISocketLink { }
    #endregion


    #region Socket


    interface ISocket
    {
        /// <summary>建立一个Socket对像
        /// <br/>用指定的端口号和服务器的ip建立一个EndPoint对像</summary>
        void Socket();
        void Send();
        void Receive();
        void Close();
    }
    interface ClientSocket : ISocket
    {
        /// <summary>用socket对像的Connect()方法以上面建立的EndPoint对像做为参数，向服务器发出连接请求</summary>
        void Connect();
    }
    interface ServerSocket : ISocket
    {
        /// <summary>用socket对像的Bind()方法绑定EndPoint</summary>
        void Bind();
        void Listen();
        void Accept();
        void Send(PublicKey publicKey);
    }
    #endregion

    class PublicKey { }
}



