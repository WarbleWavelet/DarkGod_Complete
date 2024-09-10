/****************************************************
    文件：SpriteRendererBinder.cs
	作者：lenovo
    邮箱: 
    日期：2024/8/30 20:32:47
	功能：
*****************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
//using static UnityEngine.GraphicsBuffer;
using Random = UnityEngine.Random;
 



#if UNITY_EDITOR
using UnityEditor; 
using UnityEditorInternal;

//01 UI会插入该组件后面,比如一个Button会生成在TimeStampForTextMono下  
//02 https://blog.csdn.net/UnityCC0526/article/details/135679648
[CustomEditor(typeof(SpriteRendererBinder))]
public class SpriteRendererBinderEditor : Editor
{
	static string _tmp;
	//重写OnInspectorGUI类(刷新Inspector面板)
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
		SpriteRendererBinder mono = (SpriteRendererBinder)target;
		if (GUILayout.Button("更新图片路径"))
		{
			DateTime nowDt = DateTime.Now;
			mono.Path=	AssetDatabase.GetAssetPath(Selection.objects[0]);
		}			
		EditorUtility.SetDirty(mono); //不加无法保存在Editor的修改结果

	}


}

#endif


public class SpriteRendererBinder : MonoBehaviour
{
	public SpriteRenderer SR { get { return GetComponent<SpriteRenderer>(); } }
	public string Path;

}



