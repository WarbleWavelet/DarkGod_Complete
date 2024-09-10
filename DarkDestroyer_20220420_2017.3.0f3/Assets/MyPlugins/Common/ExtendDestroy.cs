/****************************************************
    文件：ExtendDestroy.cs
	作者：lenovo
    邮箱: 
    日期：2023/9/12 19:42:27
	功能：
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/** 
链接：https://www.jianshu.com/p/131b13ef3792/
GameObject被销毁时当前帧可以继续使用属性（gameObject,parent,transform等等）。
GameObject被销毁的下一帧判定null是相等的但是物体类型还是GameObject。
特别注意GameObject被销毁时当前帧 根据它的Parent获取childCount时是包含销毁的GameObject，所以这里计数不是想象的那样，如果使用childCount则在Destory前将父子关系解除

 */
public static partial class ExtendDestroy 
{
    public static void DestroyGracefully(this GameObject go)
    { 
        go.transform.SetParent(null);//销毁当前帧不会将parent.childCount刷新 ，所以需要手动设置
        GameObject.Destroy(go);
    }

}




