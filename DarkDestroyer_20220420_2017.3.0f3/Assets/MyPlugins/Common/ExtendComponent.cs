/****************************************************

	文件：
	作者：WWS
	日期：2022/10/31 15:25:09
	功能：追要对Unity的Componetn组件的拓展方法(this大法)


*****************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using GameObject = UnityEngine.GameObject;





public static partial class ExtendComponent
{
    /// <summary>查找最近的父节点及其以上的组件</summary>
    public static T GetComponentInParentRecent<T>(this Transform t) where T : Component
    {
        if (t.parent != null)
        {
            if (t.parent.gameObject.GetComponent<T>() != null)
            {
                return t.parent.gameObject.GetComponent<T>();
            }
            else
            { 
                return t.parent.GetComponentInParentRecent<T>();
            }
        }
        return null;
    }
}
public static partial class ExtendComponent
{





    #region Button
    /// <summary>
    /// 类似于  transform.ButtonAction("Add", AddAction);
    /// <para /> 类似于transform.ButtonAction("OK/Start", () => { this.GetSystem<IUISystem>().Show(Paths.PREFAB_LEVELS_VIEW); });
    /// </summary>
    public static void ButtonAction(this Transform trans
        , string path
        , Action action)
    {
        var tar = trans.Find(path); //GetOut Transform
        if (tar == null)
        {
            Debug.LogError("ButtonAction查找物体为空，路径为：" + path);
        }
        else
        {
            var button = tar.GetComponent<Button>(); //GetOut Button
            if (button == null)
            {
                Debug.LogError("ButtonAction物体上没有Button组件，物体名称：" + tar.name);
            }
            else
            {
                button.onClick.AddListener(() => action());
            }
        }
    }



    public static void Click(this Transform t, Action action)
    {
        t.GetComponent<Button>().onClick.AddListener(() => action());
    }

    public static void Click(this RectTransform t, Action action)
    {
        t.GetComponent<Button>().onClick.AddListener(() => action());
    }

    public static void Click(this GameObject t, Action action)
    {
        t.GetComponent<Button>().onClick.AddListener(() => action());
    }

    /// <summary>
    /// 深度查找子对象transform引用（缺点是该节点隐藏时拿不到身上的组件）
    /// </summary>
    /// <param name="root">父对象</param>
    /// <param name="childName">具体查找的子对象名称</param>
    /// <returns></returns>
    public static Button GetButtonDeep(this Transform root, string childName)
    {
        Transform result = null;
        result = root.Find(childName);
        if (!result)
        {
            foreach (Transform item in root)
            {
                //202305242040，Button在Behaviour与Transform中的改动
                result = item.FindChildDeep(childName);
                if (result != null)
                {
                    return result.GetComponent<Button>();
                }
            }
        }
        return result.GetComponent<Button>();
    }



    public static Button GetButtonDeep
    (
        this Transform root, string childName
        , UnityEngine.Events.UnityAction action
    )
    {
        Button result = root.GetButtonDeep(childName);
        result.onClick.AddListener(action);

        return result;
    }

    public static Button GetButtonDeep
(
    this GameObject root, string childName
    , UnityEngine.Events.UnityAction action
)
    {
        Button result = root.transform.GetButtonDeep(childName);
        result.onClick.AddListener(action);

        return result;
    }

    public static Button GetButtonDeep(
        this Transform root, string childName,
        UnityEngine.Events.UnityAction action1,
        UnityEngine.Events.UnityAction action2
        )
    {
        Button result = root.GetButtonDeep(childName);
        result.onClick.AddListener(action1);
        result.onClick.AddListener(action2);

        return result;
    }
    #endregion




    #region Toggle


    /// <summary>
    /// bug 暂时不会，因为_bool传不出
    /// </summary>
    /// <param name="toggle"></param>
    /// <param name="_bool"></param>
    /// <param name="clickAction"></param>
    private static void AddListener(this Toggle toggle, bool _bool, Action clickAction)
    {
        Text text = toggle.GetComponentInChildren<Text>();  //Start
        text.text = _bool.ToString();
        toggle.isOn = _bool;
        //
        toggle.onValueChanged.AddListener((bool _state) =>   //Update
        {
            _bool = _state;
            text.text = _state.ToString();
            clickAction();
        });

    }
    #endregion


    #region Text

    public static Transform SetText(this Transform t, string str)
    {
        t.GetComponent<Text>().text = str;
        return t;
    }



    #endregion  


    /// <summary>宽高，对齐宽0高1</summary>
    public static CanvasScaler SetResolution(this CanvasScaler canvasScaler
        , float width
        , float height
        , float matchWidthOrHeight)
    {
        canvasScaler.referenceResolution = new Vector2(width, height);
        canvasScaler.matchWidthOrHeight = matchWidthOrHeight;
        return canvasScaler;
    }


	#region Behaviour
	public static Behaviour Enabled(this Behaviour h)
	{
		h.enabled = true;
		return h;
	}

	public static Behaviour Unabled(this Behaviour h)
	{
		h.enabled = false;
		return h;
	}
	#endregion

	#region Component



	public static GameObject RemoveIfExistComponent<T>(this GameObject go) where T : Component
    {
        if (go.GetComponent<T>() != null)
        {
            GameObject.DestroyImmediate(go.GetComponent<T>());
        }
        return go;
    }
    public static GameObject RemoveAllIfExistComponents<T>(this GameObject go) where T : Component
    {
        int l = go.GetComponents<T>().Length;
        for (int i = 0; i < l; i++)
        {
             GameObject.DestroyImmediate(go.GetComponent<T>());
        }

        return go;
    }


    public static T GetComponentWithTag<T>(this Transform trans, string tag) where T : Component
    {
        GameObject go = GameObject.FindGameObjectWithTag(tag);
        if (go == null)
        {
            throw new System.Exception("异常");
        }

        T t = go.GetComponent<T>();

        if (t == null)
        {

            throw new System.Exception("异常");
        }

        return t;
    }

    /// <summary>根据节点上是否有组件来确定</summary>
    public static T AddComponentIfNull<T>(this Component c) where T : Component
    {

        T t = c.GetComponent<T>();
        if (t == null)
        {
            t = c.gameObject.AddComponent<T>();
        }
        return t;
    }


    /// <summary>根据c是否为空来确定</summary>
    public static T AddComponentIfNull<T>(this Transform t,ref T c) where T : Component
    {

        if (c == null)
        {
            c = t.gameObject.AddComponent<T>();
        }
        return c;
    }

    /// <summary>ref存在值类型、结构约束泛型的问题</summary>
    public static GameObject GetComponentIfNotNull<T>( this GameObject go,ref T c) where T:Component
    {
        if (c == null)
        {
            c = go.GetComponent<T>();
        }
        return go;
    }

    /// <summary>可能因为初始化复杂,不进行Add</summary>
    public static T GetComponentOrLogError<T>(this GameObject go) where T : Component
    {
        T c = go.GetComponent<T>();
        if (c == null)
        {
            Debug.LogError("组件取不到");
        }
        return c;
    }

    public static T GetComponentInChildOrLogError<T>(this GameObject go) where T : Component
    {
        T c = go.GetComponentInChildren<T>();
        if (c == null)
        {
            Debug.LogError("组件取不到");
        }
        return c;
    }

    public static T GetComponentInParentOrLogError<T>(this GameObject go) where T : Component
    {
        T c = go.GetComponentInParent<T>();
        if (c == null)
        {
            Debug.LogError("组件取不到");
        }
        return c;
    }
    public static T GetComponent<T>(this Transform trans) where T : Component
    {
        T t = trans.gameObject.GetComponent<T>();
        if (t == null)
        {

            Debug.LogErrorFormat("{0}未找到T组件", trans.name);
        }
        return t;
    }


    public static T GetComponentDeep<T>(this GameObject go, string childName) where T : Component
	{
		Transform result = null;
		Transform root = go.transform;
		result = root.FindChildDeep(childName);
		if (result != null)
		{
			return result.GetComponent<T>();
		}
		else
		{
			Debug.LogErrorFormat("{0}未找到子节点{1}", root.name, childName);
			return null;
		}
	}



	public static T GetComponentDeep<T>(this Transform root, string childName)where T:Component
	{
		Transform result = null;
		result = root.FindChildDeep(childName);
		if (result != null)
		{
			return result.GetComponent<T>();
		}
		else
		{
			Debug.LogErrorFormat("{0}未找到子节点{1}", root.name, childName);
			return null;
		}
	}

    public static T GetOrAddComponentDeep<T>(this Transform root, string childName) where T : Component
    {
        Transform result = null;
        result = root.FindChildDeep(childName);
        if (result != null)
        {
            if (result.GetComponent<T>() != null)
            { 
                 return result.GetComponent<T>();
            }
            return  result.AddComponent<T>();
        }
        else
        {
            Debug.LogErrorFormat("{0}未找到子节点{1}", root.name, childName);
            return null;
        }
    }



    /// <summary>
    /// 获得标签为 tag 的 物体 身上的 T 组件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="go"></param>
    /// <param name="tag"></param>
    /// <returns></returns>
    /// <exception cref="System.Exception"></exception>
    public static T FindComponentWithTag<T>(this GameObject go, string tag) where T : Component
	{
		T res=	GameObject.FindGameObjectWithTag(tag).GetComponent<T>();
		if (res == null)
		{
			throw new System.Exception("异常");
		}

		return res;
	}


	/// <summary>位置。少写个.transform</summary>
	public static Vector3 Position(this Component c) 
    {
        return c.transform.position;
    }


    public static T AddComponent<T>(this Transform t) where T : Component
    {
        return t.gameObject.AddComponent<T>();
    }


    public static T AddComponent<T>(this RectTransform t) where T : Component
    {
        return t.gameObject.AddComponent<T>();
    }

	/** Assets/Plugins/UnityGameFramework/Scripts/Runtime/Utility/UnityExtension.cs有重复的
    public static T GetOrAddComponent<T>(this GameObject go) where T : UnityEngine.Component
    {
        if (go.GetComponent<T>() != null)
        {
            return go.GetComponent<T>();
        }
        else
        {
            return go.AddComponent<T>();
        }
    }

    public static T GetOrAddComponent<T>(this Transform t) where T : UnityEngine.Component
    {
        return GetOrAddComponent<T>(t.gameObject);
    }
    **/
	#endregion








    #region SpriteRender
    public static void SetColor(this SpriteRenderer spriteRenderer, Color color)
    {
        spriteRenderer.material.SetColor("_Color", color);
    }

    #endregion



    #region Image
    public static void SetActive(this Image image, bool state)
    {
        if (image == null)
        {

			Debug.LogErrorFormat("{0}未找到Iamge组件", image.gameObject.name );
        }
        else
        { 
            image.gameObject.SetActive(state);
        }
    }
    #endregion





    /**#region Text Image等Graphic


/// <summary>
/// _image.DOColor报错
/// </summary>
/// <param name="graphic"></param>
/// <param name="endValue"></param>
/// <param name="duration"></param>
public static void DOColor<T>(this T graphic, Color endValue, float duration) where T:Graphic
{
        //Graphic graphic = (Image)image;
        DOTween.To(() => graphic.color,
            newColor => { graphic.color = newColor; },
            endValue,
            duration) ;
}

public static void DOFade<T>(this T graphic, Int32 endAlpha, float duration) where T : Graphic
{
        Color tarColor = graphic.color;
        tarColor.l = endAlpha;
        DOTween.To(() => graphic.color,
            newColor => { graphic.color = newColor; },
            tarColor,
            duration);
}

public static void DOColor<T>(this T graphic, Color endValue, float duration,Action action) where T : Graphic
{
        //Graphic graphic = (Image)image;
        DOTween.To(() => graphic.color,
            newColor => { graphic.color = newColor; },
            endValue,
            duration).OnComplete(() => action()); ;
}

public static void DOFade<T>(this T graphic, Int32 endAlpha, float duration, Action action) where T : Graphic
{
        Color tarColor = graphic.color;
        tarColor.l = endAlpha;
        DOTween.To(() => graphic.color,
            newColor => { graphic.color = newColor; },
            tarColor,
            duration).OnComplete(()=>action());
}

public static void DOFade_Material<T>(this T graphic, Int32 endAlpha, float duration, Action action) where T :  Material
{
        Color tarColor = graphic.color;
        tarColor.l = endAlpha;
        DOTween.To(() => graphic.color,
            newColor => { graphic.color = newColor; },
            tarColor,
            duration).OnComplete(() => action());
}

public static void DOColor<T>(this T graphic, Color endValue, float duration, int loops, LoopType loopType) where T : Graphic
{
        //Graphic graphic = (Image)image;
        DOTween.To(() => graphic.color,
            newColor => { graphic.color = newColor; },
            endValue,
            duration).SetLoops(loops,loopType);
}

#endregion **/


}
