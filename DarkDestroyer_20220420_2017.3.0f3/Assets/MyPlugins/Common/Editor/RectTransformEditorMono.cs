/****************************************************
    文件：RectTransformEditor.cs
	作者：lenovo
    邮箱: 
    日期：2024/8/11 17:53:25
	功能：
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using System;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditorInternal;

//01 UI会插入该组件后面,比如一个Button会生成在TimeStampForTextMono下  
//02 https://blog.csdn.net/UnityCC0526/article/details/135679648
[CustomEditor(typeof(RectTransformEditorMono))]
public class RectTransformEditor : Editor
{

    //重写OnInspectorGUI类(刷新Inspector面板)
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();                                                           

        if (GUILayout.Button("StrechAnchor"))
        {
      

            Debug.Log(ScreenUtil.Size());
            Debug.Log(ScreenUtil.CurResolution());
            RectTransformEditorMono mono = (RectTransformEditorMono)target;
#if NET_4_7_2_OR_NEWER
            mono.transform.Rect().StretchAnchor();
#endif
            EditorUtility.SetDirty(mono); //不加无法保存在Editor的修改结果
        }
    }


}

#endif

public class RectTransformEditorMono : MonoBehaviour
{ 

}

