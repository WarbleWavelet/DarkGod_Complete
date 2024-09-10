/****************************************************
    文件：DrawLineSystem.cs
	作者：lenovo
    邮箱: 
    日期：2024/5/4 20:6:50
	功能：
*****************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawLineSystem
{


    #region 字属
    #region 单例
    private static DrawLineSystem _instance;
    public static DrawLineSystem Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new DrawLineSystem();
                canvas = GameObject.FindObjectOfType<Canvas>();
            }
            return _instance;
        }

        set
        {
            _instance = value;
        }
    }
    #endregion


    public static Canvas canvas;
    #endregion



    //通用设置线的，如果只设置两点之间连线，只需要初入对应的ui控件的Position
    //针对手指位置和对应UI控件之间的连线需要转换坐标处理
    public RectTransform SetLine(Vector3 startPos, Vector3 endPos, Transform parent,GameObject lineSource)
    {
        try
        {
            GameObject go = GameObject.Instantiate(lineSource, parent);
            RectTransform line = go.GetComponent<RectTransform>();

            line.pivot = new Vector2(0, 0.5f);
            Vector2 pos = canvas.GetComponent<RectTransform>().sizeDelta;
            line.position = startPos + new Vector3(pos.x / 2f, pos.y / 2f, 0f);
            line.eulerAngles = new Vector3(0, 0, GetAngle(startPos, endPos));
            line.sizeDelta = new Vector2(GetDistance(startPos, endPos), line.sizeDelta.y);
            return line;
        }
        catch (Exception e)
        {


            throw new System.Exception("异常" + e);
        }

    }

    #region pri


    private float GetAngle(Vector3 startPos, Vector3 endPos)
    {
        Vector3 dir = endPos - startPos;
        float angle = Vector3.Angle(Vector3.right, dir);
        Vector3 cross = Vector3.Cross(Vector3.right, dir);
        float dirF = cross.z > 0 ? 1 : -1;
        angle = angle * dirF;
        return angle;
    }


    private float GetDistance(Vector3 startPos, Vector3 endPos)
    {
        float distance = Vector3.Distance(endPos, startPos);
        return distance * 1 / canvas.transform.localScale.x;
    }
    #endregion


    #region Unknow
    //#region 手动连线
    ////测试起点位置
    //public RectTransform start;

    ////手指或鼠标在屏幕上的点击位置
    //private Vector3 touchPos;

    //private bool isPress = false;


    //public OnPress()
    //{ 

    //}


    //List<Transform> lineTmpList = new List<Transform>();
    //private void Update()
    //{
    //    //下面为测试连接鼠标位置

    //    if (Application.isMobilePlatform)
    //    {
    //        for (int i = 0; i < Input.touchCount; ++i)
    //        {
    //            UnityEngine.Touch touch = Input.GetTouch(i);
    //            if (touch.phase == TouchPhase.Began)
    //            {
    //                isPress = true;
    //                touchPos = touch.position;
    //            }
    //            else
    //            {
    //                isPress = false;
    //            }
    //        }
    //    }
    //    else
    //    {
    //        if (Input.GetMouseButton(0))
    //        {
    //            isPress = true;
    //            touchPos = Input.mousePosition;
    //        }
    //        else
    //        {
    //            isPress = false;
    //        }
    //    }

    //    if (isPress && )
    //    {
    //        GameObject[] gos = GameObject.FindGameObjectsWithTag("Tmp");
    //        foreach (var go in gos)
    //        {
    //            Destroy(go);
    //        }
    //        RectTransform t = SetLine(start.position);
    //        lineSource.gameObject.SetActive(true);
    //        UpdateFingerLine(start.position, touchPos, );
    //    }
    //    else
    //    {
    //        GameObject[] gos= GameObject.FindGameObjectsWithTag("Tmp");
    //        foreach (var go in gos)
    //        {
    //            Destroy(go);
    //        }

    //    }

    //}


    ////针对手指位置和对应UI控件之间的连线需要转换坐标处理
    //private void UpdateFingerLine(Vector3 startPos, RectTransform fingerLine)
    //{
    //    Vector3 uiStartPos = Vector3.zero;
    //    Vector3 uitouchPos = Vector3.zero;
    //    if (canvas.renderMode == RenderMode.ScreenSpaceOverlay)
    //    {
    //        uiStartPos = startPos;
    //        uitouchPos = touchPos;
    //    }
    //    else if (canvas.renderMode == RenderMode.ScreenSpaceCamera)
    //    {
    //        Camera camera = canvas.worldCamera;

    //        //UI世界的起点世界坐标转换为UGUI坐标
    //        Vector2 screenStartPos = RectTransformUtility.WorldToScreenPoint(camera, startPos);
    //        RectTransformUtility.ScreenPointToWorldPointInRectangle(canvas.GetComponent<RectTransform>(), screenStartPos,
    //            camera, out uiStartPos);

    //        //鼠标坐标转换为UGUI坐标
    //        RectTransformUtility.ScreenPointToWorldPointInRectangle(canvas.GetComponent<RectTransform>(), touchPos,
    //            camera, out uitouchPos);
    //    }

    //    fingerLine.pivot = new Vector2(0, 0.5f);
    //    fingerLine.position = startPos;
    //    fingerLine.eulerAngles = new Vector3(0, 0, GetAngle(uiStartPos, uitouchPos));
    //    fingerLine.sizeDelta = new Vector2(GetDistance(uiStartPos, uitouchPos), fingerLine.sizeDelta.y);
    //}

    //#endregion
    #endregion

}



