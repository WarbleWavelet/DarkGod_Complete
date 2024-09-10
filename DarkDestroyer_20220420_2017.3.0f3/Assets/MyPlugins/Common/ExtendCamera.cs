/****************************************************
    文件：ExtendCamera.cs
	作者：lenovo
    邮箱: 
    日期：2023/7/9 19:31:26
	功能：
*****************************************************/
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public static partial class ExtendCamera
{      
    public static void Example(Camera camera)
    {
        camera.orthographic = true;
        camera.orthographicSize = 3f;//长方体面的缩放
        camera.aspect = 1;//W/H
    }

}

public static partial class ExtendCamera//深度模式下的宽和高
{   
    
    /// <summary>宽/高</summary>
    public static float Aspect(this Camera camera)
    {
        return camera.aspect;
    }


    #region Size Height Width


    public static float OrthographicHeight(this Camera camera)
    {
        return   camera.orthographicSize * 2.0f;
        //return (camera.OrthographicMaxPoint() - camera.OrthographicMinPoint()).y;
    }


    public static float OrthographicWidth(this Camera camera)
    {
        return camera.Aspect() * camera.OrthographicHeight();
        //return (camera.OrthographicMaxPoint() - camera.OrthographicMinPoint()).x;
    }


    public static Vector2 OrthographicSize(this Camera camera)
    {
        return new Vector2(camera.OrthographicWidth(),camera.OrthographicHeight());
    }
    #endregion


    #region min max Point


    public static Vector2 OrthographicMaxPoint(this Camera camera)
    {
        var pos = camera.transform.position;
        var size = camera.OrthographicSize();
        var maxPoint = ExtendVector.Add(pos, size * 0.5f);
        return maxPoint;
    }

    public static Vector2 OrthographicMinPoint(this Camera camera)
    {
        var pos = camera.transform.position;
        var size = camera.OrthographicSize();
        var minPoint = ExtendVector.Sub(pos, size * 0.5f);
        return minPoint;
    }
    #endregion
}
public static partial class ExtendCamera
{

    #region Camera
    public static Camera UICamera(this Transform t)
    {
        return t.gameObject.FindComponentWithTag<Camera>(Tags.UICAMERA);
    }
    public static Camera MainCamera(this Transform t)
    {
        return GameObject
            .FindGameObjectWithTag(Tags.MAINCAMERA)
            .GetComponent<Camera>();
    }


    public static Camera AnyOneCamera(this Transform t)
    {
        Camera _camera = Object.FindObjectOfType<Camera>();
        if (_camera == null)
        {
            if (_camera == null)
                Debug.LogError("当前场景中没有相机");

            return _camera;
        }
        else
        {
            return _camera;
        }
    }

    public static Camera MainOrOtherCamera(this Transform t)
    {
        Camera camera = t.MainCamera();
        if (camera == null)
        {
            return t.AnyOneCamera();
        }
        return camera;

    }
    #endregion
}

public static partial class ExtendCamera//查找
{
    public static Camera MainCameraIfNull(this Camera camera)
    {
        if (camera.IsNull())
        { 
        camera = Camera.main;
        }

        return camera;
                }
}
public static partial class ExtendCamera//CameraSize
{
    #region CameraSize
    /// <summary>这个是高和宽的Vector2，跟pos无关</summary>
    public static Vector2 CameraSize(this Camera camera)
    {
        Vector2 size = Vector2.zero;
        if (size == Vector2.zero)
        {
            var heigth = camera.orthographicSize * 2; //unity中测试了总是2倍关系
            var width = heigth * camera.aspect;//W/H
            size = new Vector2(width, heigth);
        }
        return size;
    }


    /// <summary>这个是高和宽的Vector2，跟pos无关</summary>
    public static Vector2 CameraSize(this Transform t)
    {
        Camera camera = t.MainOrOtherCamera();
        Vector2 size = Vector2.zero;
        if (camera != null)
        {
            if (size == Vector2.zero)
            {
                var heigth = camera.orthographicSize * 2;
                var width = heigth * camera.aspect;
                size = new Vector2(width, heigth);
            }
        }

        return size;
    }

    public static Vector2 CameraSizeMin(this Transform t)
    {
        Camera camera = t.MainOrOtherCamera();

        return camera.CameraSizeMin();
    }

    public static Vector2 CameraSizeMin(this Camera camera)
    {
        if (camera != null)
        {
            var pos = camera.transform.position;
            var size = camera.CameraSize();
            return new Vector3(
                  pos.x - size.x * 0.5f,
                  pos.y - size.y * 0.5f,
                  pos.z);
        }

        return Vector2.zero;
    }

    public static Vector2 CameraSizeMax(this Transform t)
    {
        Camera camera = t.MainOrOtherCamera();
        return camera.CameraSizeMax();
    }

    public static Vector2 CameraSizeMax(this Camera camera)
    {
        if (camera != null)
        {
            var pos = camera.transform.position;
            var size = camera.CameraSize();
            return new Vector3(
                  pos.x + size.x * 0.5f,
                  pos.y + size.y * 0.5f,
                  pos.z);
        }

        throw new System.Exception("异常!未找到Camera");
    }


    //public static Vector2 CameraSizeMax(this Camera camera)
    //{
    //    if (camera != null)
    //    {
    //        var pos = camera.transform.position;
    //        var size = camera.CameraSize();
    //        return new Vector3(
    //              pos.x + size.x * 0.5f,
    //              pos.y + size.y * 0.5f,
    //              pos.z);
    //    }

    //    throw new System.Exception("异常!未找到Camera");
    //}

    #endregion  


}





