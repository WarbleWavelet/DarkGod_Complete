/****************************************************
    文件：ExtendAssetDatabase.cs
	作者：lenovo
    邮箱: 
    日期：2023/6/24 22:13:11
	功能：
*****************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;
 

public static partial class ExtendAssetDatabase 
{


#if UNITY_EDITOR
    public interface IAssetDatabaseLoader : ILoader
    {
        void LoadAsset<T>(string path, Action<T> cb) where T:UnityEngine.Object;
        T CreateAsset<T>(string path) where T : UnityEngine.ScriptableObject, new();
        void AddObjectToAsset<T>(string path) where T : UnityEngine.Object;
        void CopyAsset(string from, string to);
        void MoveAsset(string from, string to);
        void DeleteAsset(string path);
    }
    public class AssetDatabaseLoader : IAssetDatabaseLoader
    {
        /// <summary>path like "Assets/Map/shooter.asset"</summary>
        public  void LoadAsset<T>(string path,Action<T> cb) where T:UnityEngine.Object
        {
            try
            {
                T t=  AssetDatabase.LoadAssetAtPath<T>(path);
                cb(t);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
                
            }
            catch (System.Exception)
            {


                throw new System.Exception("异常:404");
            }
        }


        public  T CreateAsset<T>(string path ) where T : UnityEngine.ScriptableObject, new()
        {
            T t = new T();
            //写如数据
            //foreach (var item in pathPointList)
            //{
            //    t.pathPointList.Add(item);
            //}
            AssetDatabase.CreateAsset(t, path);
            AssetDatabase.SaveAssets();

            return t;
        }


        /// <summary>like  "Assets/Map/map.asset"</summary>
        public  void AddObjectToAsset<T>(string path) where T:UnityEngine.Object
        {

            LoadAsset<T>(path, asset => 
            {
                AssetDatabase.AddObjectToAsset(asset, path);
                AssetDatabase.Refresh();
            });

        }


        /// <summary>
        ///  "Assets/Asset1.txt"
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public  void CopyAsset(string from, string to)
        {
            try
            {
                //将资源数据库置于大多数 API
                //都暂停导入的状态
                AssetDatabase.StartAssetEditing();
                AssetDatabase.CopyAsset(from,to);

            }
            finally
            {
                //在 "finally" 代码块中添加
                //对 StopAssetEditing 的调用可确保
                //在离开此函数时重置 AssetDatabase 状态
                AssetDatabase.StopAssetEditing();
            }
        }

        public  void MoveAsset(string from,string to)
        {
            try
            {
                //将资源数据库置于大多数 API
                //都暂停导入的状态
                AssetDatabase.StartAssetEditing();
                AssetDatabase.MoveAsset(from,to);

            }
            finally
            {
                //在 "finally" 代码块中添加
                //对 StopAssetEditing 的调用可确保
                //在离开此函数时重置 AssetDatabase 状态
                AssetDatabase.StopAssetEditing();
                AssetDatabase.Refresh();
            }
        }


        /// <summary>like  "Assets/Asset3.txt" </summary>
        public void DeleteAsset( string path)
        {
            try
            {
                //将资源数据库置于大多数 API
                //都暂停导入的状态
                AssetDatabase.StartAssetEditing();
                AssetDatabase.DeleteAsset(path);
            }
            finally
            {
                //在 "finally" 代码块中添加
                //对 StopAssetEditing 的调用可确保
                //在离开此函数时重置 AssetDatabase 状态
                AssetDatabase.StopAssetEditing();
                AssetDatabase.Refresh();
            }
        }

    }
#endif



}


