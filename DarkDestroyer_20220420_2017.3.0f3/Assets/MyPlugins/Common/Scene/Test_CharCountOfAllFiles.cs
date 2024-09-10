/****************************************************
    文件：Test_CharCountOfAllFiles.cs
	作者：lenovo
    邮箱: 
    日期：2024/7/28 18:29:25
	功能：
*****************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;
 

    public class Test_CharCountOfAllFiles : MonoBehaviour
    {
        #region 属性

        #endregion

        #region 生命

        /// <summary>首次载入</summary>
        void Awake()
        {
            
        }


    /// <summary>Go激活</summary>
    void OnEnable()
    {

    }

    /// <summary>首次载入且Go激活</summary>
    async void Start()
    {
        string path = @"D:\Documents\Desktop\新建文件夹 (4)";
        string fileUri = @"D:\Documents\Desktop\AAA.txt";
        string fileName = "AAA.txt";
        //
        ExtendAsync.Example_CharCountOfAllFiles(path);
        //
       // _task<string> task =  ReadAllTextAsync02(fileUri);
      //  Debug.Log(task.Result);
        //
        return;

        string str = File.ReadAllText(fileUri);
        Debug.Log(str.Length);
    }



    #region ReadAllTextAsync
    async Task<string> ReadAllTextAsync( string filePath)
    {
        using (FileStream fs = new FileStream(filePath,
        FileMode.Open, FileAccess.Read, FileShare.Read,
        bufferSize: 4096,
        useAsync: true))
        {
            StringBuilder sb = new StringBuilder();

            byte[] buffer = new byte[0x1000];
            int numRead;
            while ((numRead = await fs.ReadAsync(buffer, 0, buffer.Length)) != 0)//如果读取到的字节数为0，说明已到达文件结尾
            {
                string text = Encoding.UTF8.GetString(buffer, 0, numRead);
                sb.Append(text);
            }

            return sb.ToString();
        }
    }
#if NET_4_8_OR_NEWER
    async Task<string> ReadAllTextAsync02(string path)
    {
        switch (path)
        {
            case "": throw new ArgumentException("Empty path name is not legal.", nameof(path));
            case null: throw new ArgumentNullException(nameof(path));
        }

        using var sourceStream = new FileStream(path, FileMode.Open,FileAccess.Read, FileShare.Read, bufferSize: 4096,useAsync: true);
        using var streamReader = new StreamReader(sourceStream, Encoding.UTF8,detectEncodingFromByteOrderMarks: true);
        return await streamReader.ReadToEndAsync();
    }
#endif
#endregion



    /// <summary>固定更新</summary>
    void FixedUpdate()
        {
            
        }

        void Update()
        {
            
        }

         /// <summary>延迟更新。适用于跟随逻辑</summary>
        void LateUpdae()
        {
            
        }

        /// <summary> 组件重设为默认值时（只用于编辑状态）</summary>
        void Reset()
        {
            
        }
      

        /// <summary>当对象设置为不可用时</summary>
        void OnDisable()
        {
            
        }


        /// <summary>组件销毁时调用</summary>
        void OnDestroy()
        {
            
        }
#endregion

        #region 系统

        #endregion

        #region 辅助

        #endregion

    }




