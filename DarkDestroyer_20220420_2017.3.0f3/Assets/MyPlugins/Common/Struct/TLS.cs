/****************************************************
	文件：TLS.cs
	作者：lenovo
	邮箱: 
	日期：2024/6/17 12:5:38
	功能：
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>左上角的</summary>
public enum ETag
{ 
  Untagged=0,
}

/// <summary>
/// 右上角的那个
/// 用于物体的逻辑分层。
/// </summary>
public enum ELayer
{
	Default = 0,
	TransparentFX = 1,
	IgnoreRaycast = 2,
	Water = 4,
	UI = 5

}

/// <summary>
/// ==UnityEngine.SortingLayer
/// <br/>三元素 ID(系统给的数) name value =(key,value)
/// <br/>常见情景在SpriteRenderer
/// <br/>有就设置,没有设置不了
/// </summary>
public enum ESortingLayer
{ 
	Default=0,
}

/// <summary>
/// 常见情景在SpriteRenderer
/// </summary>
//public enum ESortingLayer
//{
//    Default = 0,
//}




/// <summary>Tag Layer SortingLayer OrderInLayer LayerMask</summary>
public class TLSOM 
{
	/// <summary>
	/// 左上角的
	/// </summary>
	public string Tag;
	/// <summary>
	/// 右上角的那个
	/// 用于物体的逻辑分层。
	/// </summary>
	public int  Layer;
	//
	//
	//尝试两者对标,这样SortingLayer就可视化了
	/// <summary>
	/// 越小越先渲染,
	/// 编辑器手动加,不合适代码动态加
	/// </summary>
	public SortingLayer  SortingLayer;
	public ESortingLayer E_SortingLayer;
	//
	/// <summary>两个名字,面板OrderInLayer或者代码sortingOrder
	/// 越小越先渲染</summary>
	public int  OrderInLayer=0;


	/// <summary>举例unity默认</summary>
	public TLSOM(SpriteRenderer sr)
	{
		sr.gameObject.layer=  ELayer.Default.Enum2Int() ;
		//
		E_SortingLayer = SortingLayer.value.Int2Enum<ESortingLayer>();
		E_SortingLayer = SortingLayer.name.String2Enum<ESortingLayer>();
		//
		sr.sortingOrder = 0;
		   
	}
}


/// <summary>渲染顺序</summary>
public static class RendererOrder
{
	public static string Camera = "越靠近相机越小渲染";
	public static string SiblingNode = "索引越小越先渲染,所以越垫底";
	//     
	public static string Layer = "逻辑分层,不影响渲染顺序,一般是default";
	//     
	public static string SortingLayer= "索引越小越先渲染,所以越垫底";
	public static string OrderInLayer = "索引越小越先渲染,所以越垫底";
}



