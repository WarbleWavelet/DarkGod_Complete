/****************************************************
    文件：ExtendObject.GameObject.cs
	作者：lenovo
    邮箱: 
    日期：2023/5/24 17:14:36
	功能：
*****************************************************/

using Common.CharacterRelationship_Sinmple;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using Random = UnityEngine.Random;


public static partial class ExtendGameObject  //GetComponentOrLogError
{
    //public static GameObject GetComponentOrLogError<T>(this GameObject go) where T : Component
    //{ 
    //    T t=go.GetComponent<T>();
    //    if (t.IsNullObject())
    //    {
    //        Debug.LogError("组件为空");
    //    }
    //    return go;
    //}
}
public static partial class ExtendGameObject//Layer  LayerMask
{
    public static void Layer(this GameObject go,ELayer layer)
    {
        go.layer = layer.Enum2Int();
    }

    public static ELayer Layer(this GameObject go)
    {
        return go.layer.Int2Enum<ELayer>();
    }


}
public static partial class ExtendGameObject
{
    /// <summary>改脚本的节点超过一个,就销毁返回true</summary>
    public static bool DestroyIfMoreThanOne<T>(this GameObject go) where T : MonoBehaviour
    {
        if (UnityEngine.Object.FindObjectsOfType<T>().Length > 1)
        {
           GameObject.Destroy(go);
            return true;
        }
        return false;
    }
}
public static partial class ExtendGameObject
{
    public static void Example01(GameObject go)
    {
        go.Hide();
        go.AddComponent<Rigidbody>();
    }
    public static void Example()
    {

        var gameObject = new GameObject();
        var transform = gameObject.transform;
        var selfScript = gameObject.AddComponent<MonoBehaviour>();
        var boxCollider = gameObject.AddComponent<BoxCollider>();

        gameObject.Show(); // gameObject.SetActiveSelf(true)
        selfScript.Show(); // this.gameObject.SetActiveSelf(true)
        boxCollider.Show(); // boxCollider.gameObject.SetActiveSelf(true)
        gameObject.transform.Show(); // transform.gameObject.SetActiveSelf(true)

        gameObject.Hide(); // gameObject.SetActiveSelf(false)
        selfScript.Hide(); // this.gameObject.SetActiveSelf(false)
        boxCollider.Hide(); // boxCollider.gameObject.SetActiveSelf(false)
        transform.Hide(); // transform.gameObject.SetActiveSelf(false)

        selfScript.DestroyGameObj();
        boxCollider.DestroyGameObj();
        transform.DestroyGameObj();

        selfScript.DestroyGameObjGracefully();
        boxCollider.DestroyGameObjGracefully();
        transform.DestroyGameObjGracefully();

        selfScript.DestroyGameObjAfterDelay(1.0f);
        boxCollider.DestroyGameObjAfterDelay(1.0f);
        transform.DestroyGameObjAfterDelay(1.0f);

        selfScript.DestroyGameObjAfterDelayGracefully(1.0f);
        boxCollider.DestroyGameObjAfterDelayGracefully(1.0f);
        transform.DestroyGameObjAfterDelayGracefully(1.0f);

        gameObject.Layer(0);
        selfScript.Layer(0);
        boxCollider.Layer(0);
        transform.Layer(0);

        gameObject.Layer("Default");
        selfScript.Layer("Default");
        boxCollider.Layer("Default");
        transform.Layer("Default");
    }


    /// <summary>
    /// 重名只能用路径区别
    /// 不激活找不到
    /// </summary>	
    public static GameObject FindExtensions(this GameObject gameObject, string tag)
    {
        return GameObject.Find(tag);
    }

    /// <summary>
    /// 不激活找不到
    /// </summary>	
    public static GameObject FindGameObjectWithTagExtensions(this GameObject gameObject, string tag)
    {
        return GameObject.FindGameObjectWithTag(tag);
    }

    #region Show、Hide 注释掉



    //   public static GameObject Show(this GameObject gameObject)
    //{
    //	gameObject.SetActiveSelf(true);
    //	return gameObject;
    //}

    //public static GameObject Hide(this GameObject gameObject)
    //{
    //	gameObject.SetActiveSelf(false);
    //       return gameObject;
    //   }

    //public static Transform Show(this Transform transform)
    //{
    //	transform.gameObject.SetActiveSelf(true);
    //	return transform;
    //}

    //public static Transform Hide(this Transform transform)
    //{
    //	transform.gameObject.SetActiveSelf(false);
    //	return transform;
    //}

    //public static MonoBehaviour Show(this MonoBehaviour mono)
    //{
    //	mono.gameObject.SetActiveSelf(true);
    //	return mono;
    //}

    //public static MonoBehaviour Hide(this MonoBehaviour mono)
    //{
    //	mono.gameObject.SetActiveSelf(false);
    //		return mono;
    //}
    //public static Behaviour Show(this Behaviour behaviour)
    //{
    //	behaviour.gameObject.SetActiveSelf(true);
    //	return behaviour;
    //}

    //public static Behaviour Hide(this Behaviour behaviour)
    //{
    //	behaviour.gameObject.SetActiveSelf(false);
    //	return behaviour;
    //}
    #endregion


    #region SetActive /Active

    public static bool ActiveWork(this GameObject go)
    {
        return go.activeInHierarchy;
    }
    public static bool ActiveSelf(this GameObject go)
    {
        return go.activeSelf;
    }


    public static bool ActiveSelf(this Transform t)
    {
        return t.gameObject.activeSelf;
    }

    public static void SetActiveSelf(this Transform trans, bool state)
    {
        trans.gameObject.SetActive(state);
    }

    public static void SetActiveSelf(this Behaviour behaviour, bool state)
    {
        behaviour.gameObject.SetActive(state);
    }

    public static void SetActiveSelf(this MonoBehaviour monoBehaviour, bool state)
    {
        monoBehaviour.gameObject.SetActive(state);
    }

	public static void SetActiveSelf(this Component mono, bool state)
	{
		mono.gameObject.SetActive(state);
	}
	#endregion

	#region CEGO001 Show

	/// <summary>gameObject.SetActiveSelf(true)</summary>
	public static GameObject Show(this GameObject selfObj)
    {
        selfObj.SetActive(true);
        return selfObj;
    }

    public static T Show<T>(this T selfComponent) where T : Component
    {
        selfComponent.gameObject.Show();
        return selfComponent;
    }

    #endregion

    #region CEGO002 Hide

    /// <summary>gameObject.SetActiveSelf(false)</summary>
    public static GameObject Hide(this GameObject selfObj)
    {
        selfObj.SetActive(false);
        return selfObj;
    }

    public static T Hide<T>(this T selfComponent) where T : Component
    {
        selfComponent.gameObject.Hide();
        return selfComponent;
    }



	#endregion
	#region Disabled
	public static GameObject Disabled(this GameObject selfObj)
	{
		selfObj.Hide();
		return selfObj;
	}

	public static T Disabled<T>(this T selfComponent) where T : Component
	{
		selfComponent.gameObject.Hide();
		return selfComponent;
	}
	#endregion


	#region CEGO003 DestroyGameObj


	/// <summary>Destroy(this.gameObject)</summary>
	public static void DestroyGameObj<T>(this T selfBehaviour) where T : Component
    {
        selfBehaviour.gameObject.DestroySelf();
    }

    #endregion

    #region CEGO004 DestroyGameObjGracefully


    /// <summary>if(this != null && this.gameObject) Destroy(this.gameObject)</summary>
    public static void DestroyGameObjGracefully<T>(this T selfBehaviour) where T : Component
    {
        if (selfBehaviour && selfBehaviour.gameObject)
        {
            selfBehaviour.gameObject.DestroySelfGracefully();
        }
    }

    #endregion

    #region CEGO005 DestroyGameObjGracefully


    /// <summary>延时销毁</summary>
    public static T DestroyGameObjAfterDelay<T>(this T selfBehaviour, float delay) where T : Component
    {
        selfBehaviour.gameObject.DestroySelfAfterDelay(delay);
        return selfBehaviour;
    }

    /// <summary>
    /// 判空延时销毁
    /// <para/>if (this && this.gameObject0 this.gameObject.DestroySelfAfterDelay(1.5f)
    /// </summary>
    public static T DestroyGameObjAfterDelayGracefully<T>(this T selfBehaviour, float delay) where T : Component
    {
        if (selfBehaviour && selfBehaviour.gameObject)
        {
            selfBehaviour.gameObject.DestroySelfAfterDelay(delay);
        }

        return selfBehaviour;
    }

    #endregion

    #region CEGO006 Layer

    /// <summary> gameObject.layer = 10</summary>
    public static GameObject Layer(this GameObject selfObj, int layer)
    {
        selfObj.layer = layer;
        return selfObj;
    }
    /// <summary> gameObject.layer</summary>
    public static T Layer<T>(this T selfComponent, int layer) where T : Component
    {
        selfComponent.gameObject.layer = layer;
        return selfComponent;
    }


    /// <summary> gameObject.layer = LayerMask.NameToLayer("Default);</summary>
    public static GameObject Layer(this GameObject selfObj, string layerName)
    {
        selfObj.layer = LayerMask.NameToLayer(layerName);
        return selfObj;
    }

    /// <summary>gameObject.layer = LayerMask.NameToLayer("Default);</summary>
    public static T Layer<T>(this T selfComponent, string layerName) where T : Component
    {
        selfComponent.gameObject.layer = LayerMask.NameToLayer(layerName);
        return selfComponent;
    }

    #endregion

    #region CEGO007 Component

    public static T[] AllGetOrAddComponent<T>(this Transform[] ts) where T : Component
    {
        int L = ts.Length;
        T[] cs=new T[L];
        for (int i = 0; i < L; i++)
        {
            cs[i] = ts[i].GetOrAddComponent<T>();
        }

        return cs;
    }

    public static T GetOrAddComponent<T>(this GameObject selfComponent) where T : Component
    {
        var cpt = selfComponent.gameObject.GetComponent<T>();
        return cpt ? cpt : selfComponent.gameObject.AddComponent<T>();
    }

    public static T GetOrAddComponent<T>(this Transform selfComponent) where T : Component
    {
        var cpt = selfComponent.gameObject.GetComponent<T>();
        return cpt ? cpt : selfComponent.gameObject.AddComponent<T>();
    }

    public static Component GetOrAddComponent(this GameObject selfComponent, Type type)
    {
        var cpt = selfComponent.gameObject.GetComponent(type);
        return cpt ? cpt : selfComponent.gameObject.AddComponent(type);
    }



    #endregion



    #region GameObject

    /// <summary>根据名字，查找场景中所有节点，返回唯一值</summary>
    public static GameObject FindAll(this GameObject gameObject, string tarName)
    {
        GameObject[] topGos = UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects();
        foreach (GameObject topGo in topGos)
        {
            if (topGo.name == tarName)
            {
                return topGo;
            }
        }
        foreach (GameObject topGo in topGos)
        {
            Transform t = topGo.transform.FindChildDeep(tarName);
            if (t != null)
            {
                return t.gameObject;
            }
        }

        return null;
    }



    /// <summary>
    /// 场景中根节点
    /// 未激活也可以找到
    /// </summary>
    public static GameObject FindTop(this GameObject curGo, string tarName)
    {
        GameObject[] gos = UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects();
        foreach (GameObject go in gos)
        {
            if (go.name == tarName)
            {
                return go;
            }
        }
        return null;
    }





    public static T GetComponentWithTag<T>(this GameObject go, string tag) where T : Component
    {
        GameObject tarGo = GameObject.FindGameObjectWithTag(tag);
        if (tarGo == null)
        {
            throw new System.Exception("异常");
        }

        T t = tarGo.GetComponent<T>();

        if (t == null)
        {

            throw new System.Exception("异常");
        }

        return t;
    }


    /// <summary>
    /// 全查找<para />
    /// 源码不给写注释，只能自己弄
    /// </summary>
    public static GameObject FindGlobal(this GameObject go, string goName)
    {
        GameObject res = GameObject.Find(goName);
        if (res == null)
        {

            Debug.LogError("异常：public static GameObject Find(string goName)");
        }
        return res;
    }





    /// <summary>如果存在就不要创建（返回时常用）</summary>
    public static void DestoryOnExistOne<T>(this GameObject go) where T : MonoBehaviour
    {
        if (UnityEngine.Object.FindObjectsOfType<T>().Length > 1)
        {
            GameObject.Destroy(go);
            return;
        }
    }

    /// <summary>
    /// bug 回溢栈
    /// 换个写法，少写括号
    /// </summary>
    private static void DontDestroyOnLoad(this GameObject go)
    {
        DontDestroyOnLoad(go);
    }
    #endregion


    #region SetParent
    public static GameObject SetParent(this GameObject go,GameObject parent)
    {
        if (parent.IsNullObject())
        {
            Debug.LogError($"父节点为空，父节点名为：{parent.name}");
        }
        if (go.IsNullObject())
        {
            Debug.LogError($"子节点为空，子节点名为：{go.name}");
        }
        if (parent.IsNotNullObject() && go.IsNotNullObject())
        {
            go.transform.SetParent(parent.transform);
        }
        return go;
    }

    public static void SetParent(this GameObject go, Transform parent)
    {
        if (parent == null)
        {
            Debug.LogError($"父节点为空，父节点名为：{parent.name}");
        }
        if (go == null)
        {
            Debug.LogError($"子节点为空，子节点名为：{go.name}");
        }
        if(parent != null && go!=null)
        {
            go.transform.SetParent(parent);
        }
    }


    public static void SetParent(this Transform t, GameObject parent)
    {
        if (parent == null)
        {
            Debug.LogError($"父节点为空，父节点名为：{parent.name}");
        }
        if (t == null)
        {
            Debug.LogError($"子节点为空，子节点名为：{t.name}");
        }
        if (parent != null && t != null)
        {
            t.SetParent(parent.transform);
        }
    }
    #endregion



}




