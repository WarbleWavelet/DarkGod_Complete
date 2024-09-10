/****************************************************
    文件：ExtendUnityOthers.cs
	作者：lenovo
    邮箱: 
    日期：2023/5/24 17:16:44
	功能：还没处理整合，先扔这里
*****************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using Object = UnityEngine.Object;






public static class CameraExtension
{
    public static void Example()
    {
        var screenshotTexture2D = Camera.main.CaptureCamera(new Rect(0, 0, Screen.width, Screen.height));
        Debug.Log(screenshotTexture2D.width);
    }

    public static Texture2D CaptureCamera(this Camera camera, Rect rect)
    {
        var renderTexture = new RenderTexture(Screen.width, Screen.height, 0);
        camera.targetTexture = renderTexture;
        camera.Render();

        RenderTexture.active = renderTexture;

        var screenShot = new Texture2D((int)rect.width, (int)rect.height, TextureFormat.RGB24, false);
        screenShot.ReadPixels(rect, 0, 0);
        screenShot.Apply();

        camera.targetTexture = null;
        RenderTexture.active = null;
        Object.Destroy(renderTexture);

        return screenShot;
    }
}

public static class ColorExtension
{
    public static void Example()
    {
        var color = "#C5563CFF".HtmlStringToColor();
        Debug.Log(color);
    }

    /// <summary>
    /// #C5563CFF -> 197.0f / 255,86.0f / 255,60.0f / 255
    /// </summary>
    /// <param name="htmlString"></param>
    /// <returns></returns>
    public static Color HtmlStringToColor(this string htmlString)
    {
        Color retColor;
        var parseSucceed = ColorUtility.TryParseHtmlString(htmlString, out retColor);
        return parseSucceed ? retColor : Color.black;
    }

    /// <summary>
    /// unity's color always new l color
    /// </summary>
    public static Color White = Color.white;
}



public static class GraphicExtension
{
    public static void Example()
    {
        var gameObject = new GameObject();
        var image = gameObject.AddComponent<Image>();
        var rawImage = gameObject.AddComponent<RawImage>();

        // image.color = new Color(image.color.r,image.color.g,image.color.m,1.0f);
        image.ColorAlpha(1.0f);
        rawImage.ColorAlpha(1.0f);
    }

    public static T ColorAlpha<T>(this T selfGraphic, float alpha) where T : Graphic
    {
        var color = selfGraphic.color;
        color.a = alpha;
        selfGraphic.color = color;
        return selfGraphic;
    }
}

public static class ImageExtension
{
    public static void Example()
    {
        var gameObject = new GameObject();
        var image1 = gameObject.AddComponent<Image>();

        image1.FillAmount(0.0f); // image1.fillAmount = 0.0f;
    }

    public static Image FillAmount(this Image selfImage, float fillamount)
    {
        selfImage.fillAmount = fillamount;
        return selfImage;
    }
}

public static class LightmapExtension
{
    public static void SetAmbientLightHTMLStringColor(string htmlStringColor)
    {
        RenderSettings.ambientLight = htmlStringColor.HtmlStringToColor();
    }
}

public static class ObjectExtension
{
    public static void Example()
    {
        var gameObject = new GameObject();

        gameObject.Instantiate()
            .Name("ExtensionExample")
            .DestroySelf();

        gameObject.Instantiate()
            .DestroySelfGracefully();

        gameObject.Instantiate()
            .DestroySelfAfterDelay(1.0f);

        gameObject.Instantiate()
            .DestroySelfAfterDelayGracefully(1.0f);

        gameObject
            .ApplySelfTo(selfObj => Debug.Log(selfObj.name))
            .Name("TestObj")
            .ApplySelfTo(selfObj => Debug.Log(selfObj.name))
            .Name("ExtensionExample")
            .DontDestroyOnLoad();
    }


    #region CEUO001 Instantiate

    public static T Instantiate<T>(this T selfObj) where T : Object
    {
        return Object.Instantiate(selfObj);
    }
                               
    #endregion

    #region CEUO002 Instantiate

    /// <summary>gameObject.name = "Yeah" (这是UnityEngine.Object的API)</summary>
    public static T Name<T>(this T selfObj, string name) where T : Object
    {
        selfObj.name = name;
        return selfObj;
    }

    #endregion

    #region CEUO003 Destroy Self

    /// <summary>Destroy(gameObject) (这是UnityEngine.Object的API)</summary> 
    public static void DestroySelf<T>(this T selfObj) where T : Object
    {
        Object.Destroy(selfObj);
    }


    /// <summary>
    /// 加了判空<para />
    /// if (gameObject) Destroy(gameObject)
    /// </summary>
    public static T DestroySelfGracefully<T>(this T selfObj) where T : Object
    {
        if (selfObj)
        {
            Object.Destroy(selfObj);
        }

        return selfObj;
    }

    #endregion

    #region CEUO004 Destroy Self AfterDelay 


    /// <summary>Destroy(gameObject,1.5f)</summary>
    public static T DestroySelfAfterDelay<T>
    (
        this T selfObj
        , float afterDelay
    ) where T : Object
    {
        Object.Destroy(selfObj, afterDelay);
        return selfObj;
    }

    /// <summary>Gracefully优雅的</summary>
    public static T DestroySelfAfterDelayGracefully<T>(this T selfObj, float delay) where T : Object
    {
        if (selfObj)
        {
            Object.Destroy(selfObj, delay);
        }

        return selfObj;
    }

    #endregion

    #region CEUO005 Apply Self To 


    /// <summary>回调（以实例为例）</summary>
    public static T ApplySelfTo<T>(this T selfObj, System.Action<T> toFunction) where T : Object
    {
        toFunction.InvokeGracefully(selfObj);
        return selfObj;
    }

    #endregion

    #region CEUO006 DontDestroyOnLoad

    public static T DontDestroyOnLoad<T>(this T selfObj) where T : Object
    {
        Object.DontDestroyOnLoad(selfObj);
        return selfObj;
    }

    #endregion

    public static T As<T>(this Object selfObj) where T : Object
    {
        return selfObj as T;
    }

    public static T LogInfo<T>(this T selfObj, string msgContent, params object[] args) where T : Object
    {
        Log.I(msgContent, args);
        return selfObj;
    }
}


public static class SelectableExtension
{
    public static T EnableInteract<T>(this T selfSelectable) where T : Selectable
    {
        selfSelectable.interactable = true;
        return selfSelectable;
    }

    public static T DisableInteract<T>(this T selfSelectable) where T : Selectable
    {
        selfSelectable.interactable = false;
        return selfSelectable;
    }

    public static T CancalAllTransitions<T>(this T selfSelectable) where T : Selectable
    {
        selfSelectable.transition = Selectable.Transition.None;
        return selfSelectable;
    }
}

public static class ToggleExtension
{
    public static void RegOnValueChangedEvent(this Toggle selfToggle, UnityAction<bool> onValueChangedEvent)
    {
        selfToggle.onValueChanged.AddListener(onValueChangedEvent);
    }
}

#if SLUA_SUPPORT
    using SLua;
    [CustomLuaClass]
#endif


