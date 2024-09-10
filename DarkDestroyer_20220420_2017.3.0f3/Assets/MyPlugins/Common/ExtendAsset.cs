/****************************************************
	文件：ExtendAsset.cs
	作者：lenovo
	邮箱: 
	日期：2024/7/15 19:59:3
	功能：
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;
using static ExtendAssetDatabase;
using static ExtendResources;
using static ExtendAssetBundles;




public static partial class ExtendAsset
{
    static void Loader()
    {
        new AssetDatabaseLoader();
        new ResourcesLoader();
        new ABLoader("");
        //new AddressableLoader();//集外
    
    }

}
public static partial class ExtendAsset 
{
    /// <summary>
    /// 目的起源于AssetDataBase.Find时的Filter
    /// 摘录于Addressable Asset system
	/// 这玩意应该和Editor搜索框的Filter有关联
    /// </summary>
    public enum EAssetType
	{
		Prefabs, 
		Textures, 
		Materials,
		AudioClips,
		Animations,
		Meshs,
		Shaders,
	}



    //Local
    //Remote
    //
    //Instance
    //Lots Of
    //
    //


}




/// <summary></summary>
public enum LoadType
{
    /// <summary>直接引用,挂上去</summary>
    DirectReferences,


    /// <summary>
    /// 在任意地方引用资源，打成AssetBundles。
    /// 如果使用了AssetBundles，你还是需要传递资源路径以及在加载的时候将它们根据一些策略组合起来。
    /// 如果你的AssetBundles在远程或者有些依赖项在其它的AssetBundles里面，你还需要自己写代码去管理下载、加载和卸载它们。</summary>
    AB,


    /// <summary>资源规划在Resources</summary>                                                                                                                                                   
    Resources,


    /// <summary></summary>
    QFResLoader,


    /// <summary>
    /// 给Asset绑定一个地址，然后就可以通过这个地址去加载它，不用关注它的位置、目录或者怎么构建这个资源。
    /// </summary>
    Addressable,


}