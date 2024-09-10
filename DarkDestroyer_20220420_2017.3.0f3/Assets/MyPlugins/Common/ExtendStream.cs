/****************************************************
    文件：ExtendStream.cs
	作者：lenovo
    邮箱: 
    日期：2023/9/8 21:31:10
	功能：
*****************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Random = UnityEngine.Random;



/// <summary>字节序列</summary>
public static partial class ExtendStream 
{
     class MyStreamExample : Stream
    {
        /// <summary>只读属性，判断该流是否能够读取</summary>
        public override bool CanRead
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>只读属性，判断该流是否支持跟踪查找</summary>
        public override bool CanSeek
        {
            get { throw new NotImplementedException(); }
        }


        /// <summary>只读属性，判断当前流是否可写</summary>
        public override bool CanWrite
        {
            get { throw new NotImplementedException(); }
        }


        /// <summary>内存=>硬存（写的过程，）</summary>
        public override void Flush()
        {
            throw new NotImplementedException();
        }


        /// <summary>流的长度</summary>
        public override long Length
        {
            get { throw new NotImplementedException(); }
        }

        public override long Position
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotImplementedException();
        }

        public override void SetLength(long value)
        {
            throw new NotImplementedException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException();
        }
    }

}




