/****************************************************
    文件：ExtendComponent.Behaviour.cs
	作者：lenovo
    邮箱: 
    日期：2023/5/4 15:57:15
	功能：
*****************************************************/

using UnityEngine;

public static partial class ExtendBehaviour //   Enable Unable  Disable
                               {

    public static void Example()
    {
        var gameObject = new GameObject();
        var monoBehaviour = gameObject.GetComponent<MonoBehaviour>();

        monoBehaviour.Enable();  // component.enabled = true
        monoBehaviour.Unable(); // component.enabled = false
    }


    //public static void Enabled(this Behaviour behaviour)
    //{
    //    behaviour.enabled = true;
    //}

    //public static void Disabled(this Behaviour behaviour)
    //{
    //    behaviour.enabled = false;
    //}
    public static T Enable<T>(this T behaviour) where T : Behaviour
    {
        behaviour.enabled = true;
        return behaviour;
    }


    /// <summary>有时一个对象出来的物体挂有两个脚本(只能存在一个,所以unable其中一个是使用)</summary>
    public static T Unable<T>(this T behaviour) where T : Behaviour
    {
        behaviour.enabled = false;
        return behaviour;
    }


}