/****************************************************
    文件：ReadOnlyDrawer.cs
	作者：lenovo
    邮箱: 
    日期：2022/5/22 15:16:9
	功能：只读属性实现（https://blog.csdn.net/cartzhang/article/details/53888588?spm=1001.2014.3001.5501）
*****************************************************/

using UnityEditor;
using UnityEngine;

public class ReadOnlyAttribute : PropertyAttribute
{

}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
public class ReadOnlyDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUI.GetPropertyHeight(property, label, true);
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        GUI.enabled = false;
        EditorGUI.PropertyField(position, property, label, true);
        GUI.enabled = true;
    }
}
#endif