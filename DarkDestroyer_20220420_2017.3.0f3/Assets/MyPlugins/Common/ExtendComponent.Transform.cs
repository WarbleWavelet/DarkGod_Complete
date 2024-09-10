/****************************************************
    文件：ExtendComponent.Transform.cs
	作者：lenovo
    邮箱: 
    日期：2023/4/22 15:36:3
	功能：
*****************************************************/


using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Animations;
using Random = UnityEngine.Random;
using Transform = UnityEngine.Transform;



public static partial class ExtendTransform
{
    public static void Example_EulerAngle(this Transform t)
    {
        if (Input.GetKeyDown(KeyCode.W)) t.SetDirByEuler(EDir.UP);
        if (Input.GetKeyDown(KeyCode.S)) t.SetDirByEuler(EDir.DOWN);
        if (Input.GetKeyDown(KeyCode.A)) t.SetDirByEuler(EDir.LEFT);
        if (Input.GetKeyDown(KeyCode.D)) t.SetDirByEuler(EDir.RIGHT);
    }
}
public static partial class ExtendTransform  //Position Local
{
#if NET_4_7_2_Or_NEWER

    #region Reverse  Position Local
    public static Transform ReverseLocalPosX(this Transform t)
    {
        Vector3 v = t.localPosition;
        v.ReverseX();
        t.localPosition = v;
        return t;
    }

    public static Transform ReverseLocalPosY(this Transform t)
    {
        Vector3 v = t.localPosition;
        v.ReverseY();
        t.localPosition = v;
        return t;
    }

    public static Transform ReverseLocalPosZ(this Transform t)
    {
        Vector3 v = t.localPosition;
        v.ReverseZ();
        t.localPosition = v;
        return t;
    }

    public static Transform ReversePosX(this Transform t)
    {
        Vector3 v = t.position;
        v.ReverseX();
        t.position = v;
        return t;
    }

    public static Transform ReversePosY(this Transform t)
    {
        Vector3 v = t.position;
        v.ReverseY();
        t.position = v;
        return t;
    }

    public static Transform ReversePosZ(this Transform t)
    {
        Vector3 v = t.position;
        v.ReverseZ();
        t.position = v;
        return t;
    }
    #endregion
#endif
}

public static partial class ExtendTransform
{

    /// <summary>对象池需要在后面加相关计数
    /// <br/>比如BulletPool_Sum_Active,只有包含BulletPool就是了,后面是自定义的计数</summary>
    public static Transform FindContains<T>(this T parent,string containsName) where T : Transform
    {
        for (int i = 0; i < parent.childCount; i++)
        {
           Transform item =parent.GetChild(i);
            if (item.gameObject.name.Contains(containsName))
            { 
            return item;
            }
        }

        return null;
    }

}


public static partial class ExtendTransform
{ 

}
public static partial class ExtendTransform   //位置
{
    /// <summary>
    /// 主要是Transform,RectTransform
    /// </summary>
    public static Vector3 LocalPos<T>(this T t) where T : Transform
    {
        return t.transform.localPosition;
    }

}
public static partial class ExtendTransform   //找一些常用的节点组件
{
    public static Canvas TopCanvas(this Transform t)
    {
        Canvas canvas = t.FindTop(GameObjectName.Canvas).GetComponent<Canvas>();
        return canvas;

    }
    public static Camera TopMainCamera(this Transform t)
    {
      return Camera.main;
    }
}
public static partial class ExtendTransform //四元数旋转
{

    #region Rotate
    public static Transform LocalRotationDown(this Transform t)
    {
        t.SetLocalRotationZ(180);
        return t;
    }

    public static Transform LocalRotationUp(this Transform t)
    {
        t.SetLocalRotationZ(0);
        return t;
    }

    public static Transform SetLocalRotationX(this Transform t, float x)
    {

        Quaternion quaternion = t.localRotation;
        quaternion.x = x;
        t.localRotation = quaternion;
        return t;
    }

    public static Transform SetLocalRotationY(this Transform t, float y)
    {
        Quaternion quaternion = t.localRotation;
        quaternion.y = y;
        t.localRotation = quaternion;
        return t;
    }
    public static Transform SetLocalRotationZ(this Transform t,float z)
    {
        Quaternion quaternion = t.localRotation;
        quaternion.z = z;
        t.localRotation = quaternion;
        return t;
    }

    #endregion


    #region SetEuler
    public static Transform SetEulerX(this Transform t, float x)
    {

        Vector3 eulerAngles = t.eulerAngles;
        eulerAngles.x = x;
        t.eulerAngles = eulerAngles;
        return t;
    }

    public static Transform SetEulerY(this Transform t, float y)
    {

        Vector3 eulerAngles = t.eulerAngles;
        eulerAngles.y = y;
        t.eulerAngles = eulerAngles;
        return t;
    }

    public static Transform SetEulerZ(this Transform t, float z)
    {

        Vector3 eulerAngles = t.eulerAngles;
        eulerAngles.z = z;
        t.eulerAngles = eulerAngles;
        return t;
    }
    #endregion


    #region SetLocalEuler
    public static Transform SetLocalEulerX(this Transform t, float x)
    {

        Vector3 eulerAngles = t.eulerAngles;
        eulerAngles.x = x;
        t.localEulerAngles = eulerAngles;
        return t;
    }

    public static Transform SetLocalEulerY(this Transform t, float y)
    {

        Vector3 eulerAngles = t.eulerAngles;
        eulerAngles.y = y;
        t.localEulerAngles = eulerAngles;
        return t;
    }

    public static Transform SetLocalEulerZ(this Transform t, float z)
    {

        Vector3 eulerAngles = t.eulerAngles;
        eulerAngles.z = z;
        t.localEulerAngles = eulerAngles;
        return t;
    }


    #endregion


    #region SetLocalEuler SetEuler Rotate

    public static Transform SetDirByEuler(this Transform t, EDir dir)
    {

        Vector3 eulerAngles = t.eulerAngles;
        eulerAngles.z=GetEulerZByDir(dir);
        t.eulerAngles = eulerAngles;
        return t;
    }
    public static Transform SetLocalDirByEuler(this Transform t, EDir dir)
    {
        Vector3 eulerAngles = t.localEulerAngles;
        eulerAngles.z = GetEulerZByDir(dir);
        t.localEulerAngles = eulerAngles;
        return t;
    }

    static float  GetEulerZByDir(EDir dir)
    {
        switch (dir)
        {
            case EDir.UP:    return 0;    
            case EDir.DOWN:  return 180;  
            case EDir.LEFT:  return 90;   
            case EDir.RIGHT: return -90;  
            default: throw new System.Exception("未定义异常");
        }
    }

    #endregion

}


public static partial class ExtendTransform  //forward right up
{

    /// <summary>X</summary>
    public static Vector3 Right( this Transform t)
    {
        return t.right;
    }


    /// <summary>Y</summary>
    public static Vector3 Up(this Transform t)
    {
        return t.up;
    }


    /// <summary>Z</summary>
    public static Vector3 Forward(this Transform t)
    {
        return t.forward;
    }
}
public static partial class ExtendTransform
{

    public static void DestroyChildren_Common(this Transform trans)
    {
        if (trans.childCount <= 0)
        {
            return;
        }
        
        foreach (Transform item in trans)
        {
            trans.gameObject.name = "djahjs";
           GameObject.Destroy(item.gameObject);
        }
    }


    /// <summary>兄弟节点中排第几（从0开始）</summary>
    public static int SelfIdx(this Transform trans)
    {
        return trans.GetSiblingIndex();
    }





    public static void AddChild(this Transform parentTrans, Transform childTrans)
    {
        childTrans.SetParent(parentTrans);
    }

    #region Transform


    /// <summary>
    /// 不管激不激活都能找到
    /// 多谢这个主要为了注释，防止遗忘
    /// </summary>
    public static Transform FindExtensions(this Transform t, string tarPath)
    {
        return t.Find(tarPath);
    }


    /// <summary>
    /// 场景中根节点
    /// 不激活也可以找到
    /// 缺点是DontDestroyOnLoad找不到</summary>
    public static Transform FindTopInActiveScene(this Transform t, string tarName)
    {
        GameObject[] gos = UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects();
        foreach (GameObject go in gos)
        {
            if (go.name == tarName)
            {
                return go.transform;
            }
        }
        return null;
    }


    /// <summary>
    /// GameObject.FindObjectsOfType
    /// </summary>
    public static Transform FindTop(this Transform t, string tarName)
    {
        GameObject[] gos = GameObject.FindObjectsOfType<GameObject>();
        foreach (GameObject go in gos)
        {
            if (go.name == tarName)
            {
                return go.transform;
            }
        }
        return null;
    }

    /// <summary>相当于GameObject.Find。优势是Hide时</summary>
    public static Transform FindTopOrNew(this Transform t, string tarName)
    {
        Transform tar= t.FindTop(tarName);
        if (tar.IsNull())
        { 
            tar=new GameObject(tarName).transform;
        }  
        return tar;
    }

    /// <summary>只会查找当前节点下的子节点路径
    /// <br/>IgnoreLayerCollision/B/C=>C</summary>
    public static Transform FindCurChildPath(this Transform t, string childPath)
    {
        //string childName = childPath.TrimName(TrimNameType.SlashFirst); //IgnoreLayerCollision/B/C  => IgnoreLayerCollision
        //Transform child = parent.Find(childName);


        string[] names = childPath.Split(CharMark.ForwardSlash); //IgnoreLayerCollision/B/C  => IgnoreLayerCollision B C
        Transform[] ts = new Transform[names.Length];
        for (int i = 0; i < names.Length; i++)  ////生成了节点B ，B的父节点是A。生成了节点C ，C的父节点是B
        {
            Transform child = t.Find(names[i]);
            if (child.IsNullObject())
            {
                return null;
            }
            else
            {
                ts[i] = child;
                string curName = childPath.Substring(childPath.LastIndexOf(CharMark.ForwardSlash)+1);//IgnoreLayerCollision/B/C=>C
                if (!childPath.Contains(CharMark.ForwardSlash) && child.name == curName)
                {
                    return child;
                }
                int index = childPath.IndexOf(CharMark.ForwardSlash) + 1;//IgnoreLayerCollision/B/C=>B/C
                string curChildPath = childPath.Substring(index);

                return child.FindCurChildPath(curChildPath);
            }
        }
        return null;
    }

    /// <summary>IgnoreLayerCollision/B/C => IgnoreLayerCollision B(父节点A) C(父节点B) 。返回了C</summary>
    public static Transform[] FindTopOrNewAllPath(this Transform t,string path)
    {
        string topName = path.TrimName(TrimNameType.SlashFirst); //IgnoreLayerCollision/B/C  => IgnoreLayerCollision
                                                                 // Transform top = parent.FindTop(topName);
        Transform top = t.FindTop(topName);
        if (top.IsNull()) //  return (UnityEngine.Object)obj == null;
        {
            top = new GameObject(topName).transform;
        }
        string[] names=path.Split('/'); //IgnoreLayerCollision/B/C  => IgnoreLayerCollision B C
        Transform[] ts = new Transform[names.Length];
        ts[0]=top;
        for (int i = 1; i < names.Length; i++)  ////生成了节点B ，B的父节点是A。生成了节点C ，C的父节点是B
        {
            Transform parent = ts[i-1];
            Transform cur = parent.Find(names[i]);
            if (cur.IsNullObject())
            {
                cur = new GameObject(names[i]).transform;
                cur.SetParent(parent);
            }

            ts[i] = cur;
        }

        return ts;
    }


    /// <summary>IgnoreLayerCollision/B/C => IgnoreLayerCollision B(父节点A) C(父节点B) 。返回了C</summary>
    public static Transform FindTopOrNewPath(this Transform t, string path)
    {
        string[] names = path.Split('/'); //IgnoreLayerCollision/B/C  => IgnoreLayerCollision B C
        Transform[] ts = new Transform[names.Length];
        ts = t.FindTopOrNewAllPath(path);


        return ts[names.Length-1];
    }


    public static void SetActive(this Transform t, bool state)
    {
        if (t.gameObject == null)
        {
            Debug.LogErrorFormat("{0}未找到GameObject组件", t.name);
            return;
        }
        t.gameObject.SetActive(state);
    }

    /// <summary>
    /// 找到 自身 下的 所有子节点  children
    /// </summary>
    /// <param name="parent"></param>
    /// <param name="children"></param>
    /// <returns></returns>
    public static Transform[] GetChildren(this Transform parent, out Transform[] children)
    {

        children = new Transform[parent.childCount];
        for (int i = 0; i < parent.childCount; i++)
        {
            children[i] = parent.GetChild(i);
        }

        return children;
    }


   



    public static List<GameObject> GetChildrenLst(this GameObject go)
    {
        Transform parent = go.transform;
        if (parent.childCount <= 0)
        {
            Debug.LogError("没有子节点");
            return null;
        }

        List<GameObject> lst = new List<GameObject>();
        for (int i = 0; i < parent.childCount; i++)
        {
            lst.Add(parent.GetChild(i).gameObject);
        }

        return lst;
    }

    public static List<Transform> GetChildrenLst(this Transform parent)
    {
        if (parent.childCount <= 0)
        {
            Debug.LogError("没有子节点");
            return null;
        }

        List<Transform> lst = new List<Transform>();
        for (int i = 0; i < parent.childCount; i++)
        {
            lst.Add(parent.GetChild(i));
        }

        return lst;
    }


    /// <summary>
    ///  找到 自身 的 叫 parentName的子节点 下的所有子节点 children 
    /// </summary>
    /// <param name="t"></param>
    /// <param name="parentName"></param>
    /// <param name="children"></param>
    /// <returns></returns>
    public static Transform[] GetChildrenDeep(this Transform t, string parentName, out Transform[] children)
    {
        Transform parent = t.FindChildDeep(parentName);
        children = new Transform[parent.childCount];
        for (int i = 0; i < children.Length; i++)
        {
            children[i] = parent.GetChild(i);
        }

        return children;
    }


    public static T AddComponent<T>(this Transform t, string path) where T : Component
    {
        return t.Find(path).gameObject.AddComponent<T>();
    }

    public static void ResetPos(this Transform t)
    {
        t.position = Vector3.zero;
    }
    public static void ResetLocal(this Transform t)
    {
        t.localPosition = Vector3.zero;
        t.localRotation = Quaternion.identity;
        t.localScale = Vector3.one;
    }

    public static void Reset(this Transform t)
    {
        t.position = Vector3.zero;
        t.rotation = Quaternion.identity;
       // t.lossyScale;  readOnly
    }

    public static Transform FindOrNew(this Transform t, string childName)
    {
        Transform tar = t.Find(childName);
        if (tar.IsNullObject())
        {
            GameObject go = new GameObject();
            go.name = childName;
            //go.transform.ResetLocal(); //有的父节点发生变化,不需要子节点local变化
            go.SetParent(t);
            go.transform.LocalIdentity();

            return go.transform;
        }

        return tar;
    }


    #region FindChildDeep
    /// <summary>
    /// 深度查找子对象transform引用
    /// </summary>
    /// <param name="root">父对象</param>
    /// <param name="childName">具体查找的子对象名称</param>
    /// <returns></returns>
    public static Transform FindChildDeep(this Transform root, string childName)
    {
        Transform result = null;
        result = root.Find(childName);
        if (!result)
        {
            foreach (Transform item in root)
            {
                result = FindChildDeep(item, childName);
                if (result != null)
                {
                    return result;
                }
            }
        }

        return result;
    }



    public static GameObject FindChildDeep(this GameObject go, string childName)
    {
        Transform result = null;
        Transform root = go.transform;
        result = root.Find(childName);
        if (!result)
        {
            foreach (Transform item in root)
            {
                result = FindChildDeep(item, childName);
                if (result != null)
                {
                    return result.gameObject;
                }
            }
        }
        return result.gameObject;
    }
    #endregion

    #endregion




    #region 偏交互



    /// <summary>平面上物体跟随鼠标的旋转而旋转</summary>

    public static Transform RotateByMouse(this Transform trans)
    {
        Vector3 mousePos =    Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float y = mousePos.y - trans.position.y;
        float x = mousePos.x - trans.position.x;
        //弧度角 弧度转度
        float angle = Mathf.Atan2(y, x) * Mathf.Rad2Deg;
        trans.eulerAngles = new Vector3(0, 0, angle - 90);

        return trans;
    }

    /// <summary>平面上物体跟随鼠标的旋转而旋转</summary>

    public static RectTransform RotateByMouse(this RectTransform trans)
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float y = mousePos.y - trans.localPosition.y;
        float x = mousePos.x - trans.localPosition.x;
        //弧度角 弧度转度
        float angle = Mathf.Atan2(y, x) * Mathf.Rad2Deg;
        trans.localEulerAngles = new Vector3(0, 0, angle - 90);

        return trans;
    }

    #endregion


    /// <summary>Transform实现了子节点的枚举，所以可以这样写Transform item in parent</summary>
    public static void LogChildNames(Transform parent)
    {
        foreach (Transform item in parent)
        {
            Debug.Log(item.name);
        }
    }


    /// <summary>.比/容易敲？</summary>
    public static Transform FindByPath(this Transform selfTrans, string path)
    {
        return selfTrans.Find(path.Replace(".", "/"));
    }


    /// <summary>递归深度找子孙节点</summary>
    public static Transform SeekTrans(this Transform selfTransform, string uniqueName)
    {
        var childTrans = selfTransform.Find(uniqueName);

        if (null != childTrans)
            return childTrans;

        foreach (Transform trans in selfTransform)
        {
            childTrans = trans.SeekTrans(uniqueName);

            if (null != childTrans)
                return childTrans;
        }

        return null;
    }

    public static T ShowChildTransByPath<T>(this T selfComponent, string tranformPath) where T : Component
    {
        selfComponent.transform.Find(tranformPath).gameObject.Show();
        return selfComponent;
    }

    public static T HideChildTransByPath<T>(this T selfComponent, string tranformPath) where T : Component
    {
        selfComponent.transform.Find(tranformPath).Hide();
        return selfComponent;
    }

    public static void CopyDataFromTrans(this Transform selfTrans, Transform fromTrans)
    {
        selfTrans.SetParent(fromTrans.parent);
        selfTrans.localPosition = fromTrans.localPosition;
        selfTrans.localRotation = fromTrans.localRotation;
        selfTrans.localScale = fromTrans.localScale;
    }

    /// <summary>
    /// 递归遍历子物体，并调用函数
    /// </summary>
    /// <param name="tfParent"></param>
    /// <param name="action"></param>
    public static void ActionRecursion(this Transform tfParent, Action<Transform> action)
    {
        action(tfParent);
        foreach (Transform tfChild in tfParent)
        {
            tfChild.ActionRecursion(action);
        }
    }

    /// <summary>
    /// 递归遍历查找指定的名字的子物体
    /// </summary>
    /// <param name="tfParent">当前Transform</param>
    /// <param name="name">目标名</param>
    /// <param name="stringComparison">字符串比较规则</param>
    /// <returns></returns>
    public static Transform FindChildRecursion(this Transform tfParent, string name,
        StringComparison stringComparison = StringComparison.Ordinal)
    {
        if (tfParent.name.Equals(name, stringComparison))
        {
            //Debug.Log("Hit " + tfParent.name);
            return tfParent;
        }

        foreach (Transform tfChild in tfParent)
        {
            Transform tfFinal = null;
            tfFinal = tfChild.FindChildRecursion(name, stringComparison);
            if (tfFinal)
            {
                return tfFinal;
            }
        }

        return null;
    }

    /// <summary>
    /// 递归遍历查找相应条件的子物体
    /// </summary>
    /// <param name="tfParent">当前Transform</param>
    /// <param name="predicate">条件</param>
    /// <returns></returns>
    public static Transform FindChildRecursion(this Transform tfParent, Func<Transform, bool> predicate)
    {
        if (predicate(tfParent))
        {
            Debug.Log("Hit " + tfParent.name);
            return tfParent;
        }

        foreach (Transform tfChild in tfParent)
        {
            Transform tfFinal = null;
            tfFinal = tfChild.FindChildRecursion(predicate);
            if (tfFinal)
            {
                return tfFinal;
            }
        }

        return null;
    }

    public static string GetPath(this Transform transform)
    {
        var sb = new System.Text.StringBuilder();
        var t = transform;
        while (true)
        {
            sb.Insert(0, t.name);
            t = t.parent;
            if (t)
            {
                sb.Insert(0, "/");
            }
            else
            {
                return sb.ToString();
            }
        }
    }
}


public static partial class ExtendTransform //QFramework
{

    /// <summary>
    /// 缓存的一些变量,免得每次声明
    /// </summary>
    private static Vector3 mLocalPos;

    private static Vector3 mScale;
    private static Vector3 mPos;


    public static void Example()
    {
        var selfScript = new GameObject().AddComponent<MonoBehaviour>();
        var transform = selfScript.transform;

        transform
            .Parent(null)
            .LocalIdentity()
            .LocalPositionIdentity()
            .LocalRotationIdentity()
            .LocalScaleIdentity()
            .LocalPosition(Vector3.zero)
            .LocalPosition(0, 0, 0)
            .LocalPosition(0, 0)
            .LocalPositionX(0)
            .LocalPositionY(0)
            .LocalPositionZ(0)
            .LocalRotation(Quaternion.identity)
            .LocalScale(Vector3.one)
            .LocalScaleX(1.0f)
            .LocalScaleY(1.0f)
            .Identity()
            .PositionIdentity()
            .RotationIdentity()
            .Position(Vector3.zero)
            .PositionX(0)
            .PositionY(0)
            .PositionZ(0)
            .Rotation(Quaternion.identity)
            .DestroyAllChild()
            .AsLastSibling()
            .AsFirstSibling()
            .SiblingIndex(0);

        selfScript
            .Parent(null)
            .LocalIdentity()
            .LocalPositionIdentity()
            .LocalRotationIdentity()
            .LocalScaleIdentity()
            .LocalPosition(Vector3.zero)
            .LocalPosition(0, 0, 0)
            .LocalPosition(0, 0)
            .LocalPositionX(0)
            .LocalPositionY(0)
            .LocalPositionZ(0)
            .LocalRotation(Quaternion.identity)
            .LocalScale(Vector3.one)
            .LocalScaleX(1.0f)
            .LocalScaleY(1.0f)
            .Identity()
            .PositionIdentity()
            .RotationIdentity()
            .Position(Vector3.zero)
            .PositionX(0)
            .PositionY(0)
            .PositionZ(0)
            .Rotation(Quaternion.identity)
            .DestroyAllChild()
            .AsLastSibling()
            .AsFirstSibling()
            .SiblingIndex(0);
    }



    #region CETR001 Parent
    /// <summary>transform.SetParent(xxx)</summary>
    public static T Parent<T>(this T selfComponent, Component parentComponent) where T : Component
    {
        selfComponent.transform.SetParent(parentComponent == null ? null : parentComponent.transform);
        return selfComponent;
    }

    /// <summary>
    /// 设置成为顶端 Transform
    /// </summary>
    /// <returns>The root transform.</returns>
    /// <param name="selfComponent">Self component.</param>
    /// <typeparam name="T">The 1st type parameter.</typeparam>
    public static T AsRootTransform<T>(this T selfComponent) where T : Component
    {
        selfComponent.transform.SetParent(null);
        return selfComponent;
    }

    #endregion

    #region CETR002 LocalIdentity

    /// <summary>
    ///  transform.localPosition = Vector3.zero;  <para/>
    ///  transform.localRotation = Quaternion.identity;  <para/>
    ///  transform.localScale = Vector3.one;
    /// </summary>
    public static T LocalIdentity<T>(this T selfComponent) where T : Component
    {
        selfComponent.transform.localPosition = Vector3.zero;
        selfComponent.transform.localRotation = Quaternion.identity;
        selfComponent.transform.localScale = Vector3.one;
        return selfComponent;
    }

    #endregion

    #region CETR003 LocalPosition

    public static T LocalPosition<T>(this T selfComponent, Vector3 localPos) where T : Component
    {
        selfComponent.transform.localPosition = localPos;
        return selfComponent;
    }

    public static Vector3 GetLocalPosition<T>(this T selfComponent) where T : Component
    {
        return selfComponent.transform.localPosition;
    }



    public static T LocalPosition<T>(this T selfComponent, float x, float y, float z) where T : Component
    {
        selfComponent.transform.localPosition = new Vector3(x, y, z);
        return selfComponent;
    }

    public static T LocalPosition<T>(this T selfComponent, float x, float y) where T : Component
    {
        mLocalPos = selfComponent.transform.localPosition;
        mLocalPos.x = x;
        mLocalPos.y = y;
        selfComponent.transform.localPosition = mLocalPos;
        return selfComponent;
    }

    public static T LocalPositionX<T>(this T selfComponent, float x) where T : Component
    {
        mLocalPos = selfComponent.transform.localPosition;
        mLocalPos.x = x;
        selfComponent.transform.localPosition = mLocalPos;
        return selfComponent;
    }

    public static T LocalPositionY<T>(this T selfComponent, float y) where T : Component
    {
        mLocalPos = selfComponent.transform.localPosition;
        mLocalPos.y = y;
        selfComponent.transform.localPosition = mLocalPos;
        return selfComponent;
    }

    public static T LocalPositionZ<T>(this T selfComponent, float z) where T : Component
    {
        mLocalPos = selfComponent.transform.localPosition;
        mLocalPos.z = z;
        selfComponent.transform.localPosition = mLocalPos;
        return selfComponent;
    }

    /// <summary>transform.localPosition = Vector3.zero;</summary>
    public static T LocalPositionIdentity<T>(this T selfComponent) where T : Component
    {
        selfComponent.transform.localPosition = Vector3.zero;
        return selfComponent;
    }

    #endregion

    #region CETR004 LocalRotation

    public static Quaternion GetLocalRotation<T>(this T selfComponent) where T : Component
    {
        return selfComponent.transform.localRotation;
    }

    public static T LocalRotation<T>(this T selfComponent, Quaternion localRotation) where T : Component
    {
        selfComponent.transform.localRotation = localRotation;
        return selfComponent;
    }


    /// <summary>transform.localRotation = Quaternion.identity;</summary>
    public static T LocalRotationIdentity<T>(this T selfComponent) where T : Component
    {
        selfComponent.transform.localRotation = Quaternion.identity;
        return selfComponent;
    }

    #endregion

    #region CETR005 LocalScale

    public static T LocalScale<T>(this T selfComponent, Vector3 scale) where T : Component
    {
        selfComponent.transform.localScale = scale;
        return selfComponent;
    }

    public static Vector3 GetLocalScale<T>(this T selfComponent) where T : Component
    {
        return selfComponent.transform.localScale;
    }

    public static T LocalScale<T>(this T selfComponent, float xyz) where T : Component
    {
        selfComponent.transform.localScale = Vector3.one * xyz;
        return selfComponent;
    }

    public static T LocalScale<T>(this T selfComponent, float x, float y, float z) where T : Component
    {
        mScale = selfComponent.transform.localScale;
        mScale.x = x;
        mScale.y = y;
        mScale.z = z;
        selfComponent.transform.localScale = mScale;
        return selfComponent;
    }

    public static T LocalScale<T>(this T selfComponent, float x, float y) where T : Component
    {
        mScale = selfComponent.transform.localScale;
        mScale.x = x;
        mScale.y = y;
        selfComponent.transform.localScale = mScale;
        return selfComponent;
    }

    public static T LocalScaleX<T>(this T selfComponent, float x) where T : Component
    {
        mScale = selfComponent.transform.localScale;
        mScale.x = x;
        selfComponent.transform.localScale = mScale;
        return selfComponent;
    }

    public static T LocalScaleY<T>(this T selfComponent, float y) where T : Component
    {
        mScale = selfComponent.transform.localScale;
        mScale.y = y;
        selfComponent.transform.localScale = mScale;
        return selfComponent;
    }

    public static T LocalScaleZ<T>(this T selfComponent, float z) where T : Component
    {
        mScale = selfComponent.transform.localScale;
        mScale.z = z;
        selfComponent.transform.localScale = mScale;
        return selfComponent;
    }


    /// <summary> transform.localScale = Vector3.one;</summary>
    public static T LocalScaleIdentity<T>(this T selfComponent) where T : Component
    {
        selfComponent.transform.localScale = Vector3.one;
        return selfComponent;
    }

    #endregion

    #region CETR007 Position

    public static T Position<T>(this T selfComponent, Vector3 position) where T : Component
    {
        selfComponent.transform.position = position;
        return selfComponent;
    }

    public static Vector3 GetPosition<T>(this T selfComponent) where T : Component
    {
        return selfComponent.transform.position;
    }

    public static T Position<T>(this T selfComponent, float x, float y, float z) where T : Component
    {
        selfComponent.transform.position = new Vector3(x, y, z);
        return selfComponent;
    }

    public static T Position<T>(this T selfComponent, float x, float y) where T : Component
    {
        mPos = selfComponent.transform.position;
        mPos.x = x;
        mPos.y = y;
        selfComponent.transform.position = mPos;
        return selfComponent;
    }


    /// <summary>transform.position = Vector3.zero</summary>
    public static T PositionIdentity<T>(this T selfComponent) where T : Component
    {
        selfComponent.transform.position = Vector3.zero;
        return selfComponent;
    }

    public static T PositionX<T>(this T selfComponent, float x) where T : Component
    {
        mPos = selfComponent.transform.position;
        mPos.x = x;
        selfComponent.transform.position = mPos;
        return selfComponent;
    }

    public static T PositionX<T>(this T selfComponent, Func<float, float> xSetter) where T : Component
    {
        mPos = selfComponent.transform.position;
        mPos.x = xSetter(mPos.x);
        selfComponent.transform.position = mPos;
        return selfComponent;
    }

    public static T PositionY<T>(this T selfComponent, float y) where T : Component
    {
        mPos = selfComponent.transform.position;
        mPos.y = y;
        selfComponent.transform.position = mPos;
        return selfComponent;
    }

    public static T PositionY<T>(this T selfComponent, Func<float, float> ySetter) where T : Component
    {
        mPos = selfComponent.transform.position;
        mPos.y = ySetter(mPos.y);
        selfComponent.transform.position = mPos;
        return selfComponent;
    }

    public static T PositionZ<T>(this T selfComponent, float z) where T : Component
    {
        mPos = selfComponent.transform.position;
        mPos.z = z;
        selfComponent.transform.position = mPos;
        return selfComponent;
    }

    public static T PositionZ<T>(this T selfComponent, Func<float, float> zSetter) where T : Component
    {
        mPos = selfComponent.transform.position;
        mPos.z = zSetter(mPos.z);
        selfComponent.transform.position = mPos;
        return selfComponent;
    }

    #endregion

    #region CETR008 Rotation


    /// <summary>transform.rotation = Quaternion.identity;</summary>
    public static T RotationIdentity<T>(this T selfComponent) where T : Component
    {
        selfComponent.transform.rotation = Quaternion.identity;
        return selfComponent;
    }

    public static T Rotation<T>(this T selfComponent, Quaternion rotation) where T : Component
    {
        selfComponent.transform.rotation = rotation;
        return selfComponent;
    }

    public static Quaternion GetRotation<T>(this T selfComponent) where T : Component
    {
        return selfComponent.transform.rotation;
    }

    #endregion

    #region CETR009 WorldScale/LossyScale/GlobalScale/Scale

    public static Vector3 GetGlobalScale<T>(this T selfComponent) where T : Component
    {
        return selfComponent.transform.lossyScale;
    }

    public static Vector3 GetScale<T>(this T selfComponent) where T : Component
    {
        return selfComponent.transform.lossyScale;
    }

    public static Vector3 GetWorldScale<T>(this T selfComponent) where T : Component
    {
        return selfComponent.transform.lossyScale;
    }

    public static Vector3 GetLossyScale<T>(this T selfComponent) where T : Component
    {
        return selfComponent.transform.lossyScale;
    }

    #endregion

    #region CETR010 Destroy All Child


    /// <summary> 删除所有子节点</summary>
    public static T DestroyAllChild<T>(this T selfComponent) where T : Component
    {
        var childCount = selfComponent.transform.childCount;

        for (var i = 0; i < childCount; i++)
        {
            selfComponent.transform.GetChild(i).DestroyGameObjGracefully();
        }

        return selfComponent;
    }

    public static GameObject DestroyAllChild(this GameObject selfGameObj)
    {
        var childCount = selfGameObj.transform.childCount;

        for (var i = 0; i < childCount; i++)
        {
            selfGameObj.transform.GetChild(i).DestroyGameObjGracefully();
        }

        return selfGameObj;
    }

    #endregion

    #region CETR011 Sibling Index


    public static T AsLastSibling<T>(this T selfComponent) where T : Component
    {
        selfComponent.transform.SetAsLastSibling();
        return selfComponent;
    }


    /// <summary>
    ///         transform.SetAsFirstSibling(); 
    /// <para/> transform.SetAsLastSibling();  
    /// <para/> transform.SetSiblingIndex(1);
    /// </summary>     
    public static T AsFirstSibling<T>(this T selfComponent) where T : Component
    {
        selfComponent.transform.SetAsFirstSibling();
        return selfComponent;
    }

    public static T SiblingIndex<T>(this T selfComponent, int index) where T : Component
    {
        selfComponent.transform.SetSiblingIndex(index);
        return selfComponent;
    }

    #endregion
}

public static partial class ExtendTransform
{

    public static T SetPos<T>(this T t, Vector3 pos) where T : Component
    {
        t.transform.position = pos;
        return t;
    }

    public static T SetScale<T>(this T t, Vector3 scale) where T : Component
    {
        t.transform.localScale = scale;
        return t;
    }

    #region Identity


    /// <summary>
    ///    transform.position = Vector3.zero; <para/>
    ///    transform.rotation = Quaternion.identity;  <para/>
    ///    transform.localScale = Vector3.one; 
    /// </summary>
    public static T Identity<T>(this T t) where T : Component
    {
        t.transform.localPosition = Vector3.zero;
        t.transform.localScale = Vector3.one;
        t.transform.localRotation = Quaternion.identity;
        return t;
    }
    #endregion

    #region SetPos 
    //不用position，因为是属性，Vector有时值类型（值类型只能用ref out），和属性冲突


    public static T SetPosX<T>(this T t, float x) where T : Component
    {
        var pos = t.transform.position;
        pos.x = x;
        t.transform.position = pos;
        return t;
    }

    public static T SetPosY<T>(this T t, float y) where T : Component
    {
        var pos = t.transform.position;
        pos.y = y;
        t.transform.position = pos;
        return t;
    }

    public static T SetPosZ<T>(this T t, float z) where T : Component
    {
        var pos = t.transform.position;
        pos.z = z;
        t.transform.position = pos;
        return t;
    }

    public static T SetPosXY<T>(this T t, float x, float y) where T : Component
    {
        var pos = t.transform.position;
        pos.x = x;
        pos.y = y;
        t.transform.position = pos;
        return t;
    }

    public static T SetPosXZ<T>(this T t, float x, float z) where T : Component
    {
        var pos = t.transform.position;
        pos.x = x;
        pos.z = z;
        t.transform.position = pos;
        return t;
    }

    public static T SetPosYZ<T>(this T t, float y, float z) where T : Component
    {
        var pos = t.transform.position;
        pos.y = y;
        pos.z = z;
        t.transform.position = pos;
        return t;
    }
    public static T SetPosXYZ<T>(this T t,float x, float y, float z) where T : Component
    {
        var pos = t.transform.position;
        pos.x = x;
        pos.y = y;
        pos.z = z;
        t.transform.position = pos;
        return t;
    }
    #endregion


    #region SetLocalPos
    public static T SetLocalPos<T>(this T t, Vector3 localPos) where T : Component
    {
        t.transform.localPosition = localPos;
        return t;
    }

    public static T SetLocalPosX<T>(this T t, float x) where T : Component
    {
        var localPos = t.transform.localPosition;
        localPos.x = x;
        t.transform.localPosition = localPos;
        return t;
    }

    public static T SetLocalPosY<T>(this T t, float y) where T : Component
    {
        var localPos = t.transform.localPosition;
        localPos.y = y;
        t.transform.localPosition = localPos;
        return t;
    }

    public static T SetLocalPosZ<T>(this T t, float z) where T : Component
    {
        var localPos = t.transform.localPosition;
        localPos.z = z;
        t.transform.localPosition = localPos;
        return t;
    }

    public static T SetLocalPosXY<T>(this T t, float x, float y) where T : Component
    {
        var localPos = t.transform.localPosition;
        localPos.x = x;
        localPos.y = y;
        t.transform.localPosition = localPos;
        return t;
    }

    public static T SetLocalPosXZ<T>(this T t, float x, float z) where T : Component
    {
        var localPos = t.transform.localPosition;
        localPos.x = x;
        localPos.z = z;
        t.transform.localPosition = localPos;
        return t;
    }

    public static T SetLocalPosYZ<T>(this T t, float y, float z) where T : Component
    {
        var localPos = t.transform.localPosition;
        localPos.y = y;
        localPos.z = z;
        t.transform.localPosition = localPos;
        return t;
    }

    public static T SetLocalPosXY<T>(this T t, Vector2 v) where T : Component
    {
        float x = v.x;
        float y = v.y;
        var localPos = t.transform.localPosition;
        localPos.x = x;
        localPos.y = y;
        t.transform.localPosition = localPos;
        return t;
    }
    #endregion


}


