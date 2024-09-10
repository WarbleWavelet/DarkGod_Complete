/****************************************************
    文件：ExtendInspector.cs
	作者：lenovo
    邮箱: 
    日期：2023/9/11 20:36:12
	功能：
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Reflection;


public static partial class ExtendInspector
{
    class XxxMono : MonoBehaviour
    {

        [SerializeField] //私有但显示
        int _i;

        [HideInInspector]//公有但隐藏
        public int J;
    }


     class ButtonMono : MonoBehaviour
    {
        [InspectorButton("Rotate")]//Rotate对应
        public bool rotate;
        void Rotate()
        {
            transform.Rotate(0, 0, 30);
        }
    }


}
public static partial class ExtendInspector
{

}



#region Inspector按钮 https://www.bilibili.com/read/cv24645273/



/// <summary>
/// This attribute can only be applied _to fields because its
/// associated PropertyDrawer only operates on fields (either
/// public or tagged with the [SerializeField] attribute) in
/// the _to MonoBehaviour.
/// 未运行也有作用
/// </summary>
[System.AttributeUsage(System.AttributeTargets.Field)]
public class InspectorButtonAttribute : PropertyAttribute
{
    public static float kDefaultButtonWidth = 80;

    public readonly string MethodName;

    private float _buttonWidth = kDefaultButtonWidth;
    public float ButtonWidth
    {
        get { return _buttonWidth; }
        set { _buttonWidth = value; }
    }

    public InspectorButtonAttribute(string MethodName)
    {
        this.MethodName = MethodName;
    }
}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(InspectorButtonAttribute))]
public class InspectorButtonPropertyDrawer : PropertyDrawer
{
    private MethodInfo _eventMethodInfo = null;

    public override void OnGUI(Rect position, SerializedProperty prop, GUIContent label)
    {
        InspectorButtonAttribute inspectorButtonAttribute = (InspectorButtonAttribute)attribute;
        Rect buttonRect = new Rect(position.x + (position.width - inspectorButtonAttribute.ButtonWidth) * 0.5f, position.y, inspectorButtonAttribute.ButtonWidth, position.height);
        if (GUI.Button(buttonRect, label.text))
        {
            System.Type eventOwnerType = prop.serializedObject.targetObject.GetType();
            string eventName = inspectorButtonAttribute.MethodName;

            if (_eventMethodInfo == null)
                _eventMethodInfo = eventOwnerType.GetMethod(eventName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);

            if (_eventMethodInfo != null)
                _eventMethodInfo.Invoke(prop.serializedObject.targetObject, null);
            else
                Debug.LogWarning(string.Format("InspectorButton: Unable to find method {0} in {1}", eventName, eventOwnerType));
        }
    }
}
#endif 
#endregion




