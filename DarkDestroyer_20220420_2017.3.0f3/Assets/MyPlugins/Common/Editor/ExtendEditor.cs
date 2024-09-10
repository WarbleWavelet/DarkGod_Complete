/****************************************************
    文件：ExtendEditor.cs
	作者：lenovo
    邮箱: 
    日期：2024/4/7 19:55:13
	功能：
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;


#if UNITY_EDITOR
public static partial class ExtendEditor   //节点的展开收缩
{
    /// <summary>目前只能显示两级</summary>
    public static void Expend(this Transform t, int childDeep=1, bool expand=true)
    {
        if (childDeep == 0)
        { 
             return;
        } 

        ExtendEditor.SetExpanded(t.gameObject, expand);
        for (int i = 0; i < t.childCount; i++)
        {     
            childDeep--;
            ExtendEditor.SetExpanded(t.GetChild(i).gameObject, expand);
           // Expend(t.GetChild(i), childDeep, expand);
          
        }
    }
}


public static partial class ExtendEditor   //组件的展开收缩
{
    //https://blog.csdn.net/qq_25969985/article/details/120088236
    //https://github.com/sandolkakos/unity-utilities

    /// <summary>
    /// Check if the target GameObject is expanded (aka unfolded) in the Hierarchy view.
    /// </summary>
    public static bool IsExpanded(GameObject go)
        {
            return GetExpandedGameObjects().Contains(go);
        }

        /// <summary>
        /// Get l list of all GameObjects which are expanded (aka unfolded) in the Hierarchy view.
        /// </summary>
        public static List<GameObject> GetExpandedGameObjects()
        {
            object sceneHierarchy = GetSceneHierarchy();

            MethodInfo methodInfo = sceneHierarchy
                .GetType()
                .GetMethod("GetExpandedGameObjects");

            object result = methodInfo.Invoke(sceneHierarchy, new object[0]);

            return (List<GameObject>)result;
        }

        /// <summary>
        /// Set the target GameObject as expanded (aka unfolded) in the Hierarchy view.
        /// </summary>
        public static void SetExpanded(GameObject go, bool expand)
        {
            object sceneHierarchy = GetSceneHierarchy();

            MethodInfo methodInfo = sceneHierarchy
                .GetType()
                .GetMethod("ExpandTreeViewItem", BindingFlags.NonPublic | BindingFlags.Instance);

            methodInfo.Invoke(sceneHierarchy, new object[] { go.GetInstanceID(), expand });
        }

        /// <summary>
        /// Set the target GameObject and all children as expanded (aka unfolded) in the Hierarchy view.
        /// </summary>
        public static void SetExpandedRecursive(GameObject go, bool expand)
        {
            object sceneHierarchy = GetSceneHierarchy();

            MethodInfo methodInfo = sceneHierarchy
                .GetType()
                .GetMethod("SetExpandedRecursive", BindingFlags.Public | BindingFlags.Instance);

            methodInfo.Invoke(sceneHierarchy, new object[] { go.GetInstanceID(), expand });
        }


    #region pri


#if UNITY_EDITOR
    private static object GetSceneHierarchy()
    {
        EditorWindow window = GetHierarchyWindow();

        object sceneHierarchy = typeof(EditorWindow).Assembly
            .GetType("UnityEditor.SceneHierarchyWindow")
            .GetProperty("sceneHierarchy")
            .GetValue(window);

        return sceneHierarchy;
    }

    private static EditorWindow GetHierarchyWindow()
    {
        // For it to open, so that it the current focused window.
        EditorApplication.ExecuteMenuItem("Window/General/Hierarchy");
        return EditorWindow.focusedWindow;
    }
#endif



    #endregion


}

public static partial class ExtendEditor   //组件的展开收缩
{

    #region ===Unity事件=== 快捷键： Ctrl + Shift + M /Ctrl + Shift + Q  实现
    // 显示两个可以点击的Button 


   // [MenuItem("GameObject/MyTool/InspectorManager/全部展开组件... %#&m")]
    static void Expansion()
    {

        var type = typeof(EditorWindow).Assembly.GetType(AssemblyType.UnityEditor_InspectorWindow);
        var window = EditorWindow.GetWindow(type);
        FieldInfo info = type.GetField("m_Tracker", BindingFlags.NonPublic | BindingFlags.Instance);
        if (info != null)
        {
            ActiveEditorTracker tracker = info.GetValue(window) as ActiveEditorTracker;

            for (int i = 0; i < tracker.activeEditors.Length; i++)
            {
                // 可以通过名子单独判断组件展开或不展开
                //if (tracker.activeEditors[i].target.GetType().Name != "NewBehaviourScript")
                //{
                //这里1就是展开，0就是合起来
                tracker.SetVisible(i, 1);
                //}
            }
        }
    }

    //[MenuItem("GameObject/MyTool/InspectorManager/全部收起组件... %#&n")]
    static void Shrinkage()
    {
        var type = typeof(EditorWindow).Assembly.GetType(AssemblyType.UnityEditor_InspectorWindow);
        var window = EditorWindow.GetWindow(type);
        FieldInfo info = type.GetField("m_Tracker", BindingFlags.NonPublic | BindingFlags.Instance);
        ActiveEditorTracker tracker = info.GetValue(window) as ActiveEditorTracker;

        if (tracker != null)
            for (int i = 0; i < tracker.activeEditors.Length; i++)
            {
                //这里1就是展开，0就是合起来
                tracker.SetVisible(i, 0);
            }
    }

    #endregion

}

#endif


