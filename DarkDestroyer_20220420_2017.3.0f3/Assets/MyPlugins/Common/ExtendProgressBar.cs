/****************************************************
    文件：ExtendProgressBar.cs
	作者：lenovo
    邮箱: 
    日期：2023/8/15 8:14:36
	功能：
*****************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using Transform = UnityEngine.Transform;




public static partial class ExtendProgressBar
{
    public static void Example( MonoBehaviour mono
        , Image image
        , Slider slider=null)
    {
        if (true)
        {
            mono.PlayPrgSmooth(slider
                , 0.2f
                , 0.5f
                , 0.05f
                , true);
        }
        if (false)
        { 
            mono.PlayPrgLoadAndInstantiateGameObject("Prefab/Game/EnemyLife", mono.transform, image);
        }
        if (false)
        {
            mono.PlayPrgNull( image);
        }
        //
        if (false)
        {// 获取斐波那契数列的前 10 个数 
            IEnumerable<int> fibonacciNumbers = GetFibonacciNumbers(10);
            Debug.Log("前10个斐波那契数：");  // 打印斐波那契数列
            foreach (int number in fibonacciNumbers)
            {
                Debug.Log(number);
            }        
        }
                                
    }
}
public static partial class ExtendProgressBar
{
    #region Smooth、Slider
    /// <summary>
    /// 
    /// </summary>
    /// <param name="mono">协程Mono</param>
    /// <param name="slider">滑动条</param>
    /// <param name="step">步进值</param>
    /// <param name="stepTime">步进时间</param>
    /// <param name="stepSmooth">步进平滑值</param>
    /// <exception cref="System.Exception"></exception>
    public static void PlayPrgSmooth(this MonoBehaviour mono
    , Slider slider
    , Text text
    , float step = 0.2f
    , float stepTime = 0.5f
    , float stepSmooth = 0.01f
    , Action onCompleted=null)
    {
        if (step <= stepSmooth)
        {
            throw new System.Exception("Prg step <= smooth异常");
        }
        mono.StartCoroutine(OnValueChangeSmooth(slider,text, step, stepTime, stepSmooth, onCompleted));


    }

    /// <summary>平滑点</summary>
    static IEnumerator OnValueChangeSmooth(Slider slider
       , Text text
       , float step
       , float stepTime
       , float stepSmooth
       , Action onCompleted)
    {
        slider.value = 0f;
        text.SetText(0.00f);
        //
        float process = 0f;
        while (process < 1f)
        {
            process += step;
            //类似帧循环，快点
            yield return new WaitUntil(() =>
            {
                float prg= Mathf.SmoothStep(from: slider.value
                    , to: process
                    , t: stepSmooth);
                text.SetText((prg).ToString("0.00%") ); //感觉要乘以100.0f，但不乘就有想要的效果
                slider.value = prg;
                return process - slider.value < stepSmooth;
            });

            yield return new WaitForSeconds(stepTime);//每0.5s会执行一遍循环体

        }
        slider.value = 1.0f;
        text.SetText("100.00%");
        onCompleted.DoIfNotNull();
    }
    #endregion
}
public static partial class ExtendProgressBar  //斐波那契
{

   
    public static IEnumerable<int> GetFibonacciNumbers(int count)
    {

        int a = 0;
        int b = 1;

        for (int i = 0; i < count; i++)
        {

            // 使用 yield return 返回当前的斐波那契数
            yield return a;

            // 计算下一个斐波那契数
            int temp = a + b;
            a = b;
            b = temp;
        }
    }
}
public static partial class ExtendProgressBar  //实际加载一个GameObject
{
    // Start is called before the first frame update
    public static void PlayPrgLoadAndInstantiateGameObject(this MonoBehaviour mono
        , string resourcesPath
        , Transform parent
        , Image image

        )
    {
       
        mono.StartCoroutine(LoadResourcesAsync(resourcesPath, parent, image));
    }
    static IEnumerator LoadResourcesAsync(string resourcesPath
        , Transform parent
        , Image image)
    {
        ResourceRequest req = Resources.LoadAsync<GameObject>(resourcesPath);//类型，名字
        yield return req; 


        GameObject prefab = (req.asset) as GameObject;
        if (prefab != null)
        {
            GameObject go = GameObject.Instantiate(prefab, parent);
        }
        else
        {
            throw new System.Exception("LoadResourcesAsync异常");
        }
    }
}
public static partial class ExtendProgressBar  //可以选择是否平滑
{
    #region Smooth、Slider
    /// <summary>
    /// 
    /// </summary>
    /// <param name="mono">协程Mono</param>
    /// <param name="slider">滑动条</param>
    /// <param name="step">步进值</param>
    /// <param name="stepTime">步进时间</param>
    /// <param name="stepSmooth">步进平滑值</param>
    /// <param name="isSmooth">是否步进</param>
    /// <exception cref="System.Exception"></exception>
    public static void PlayPrgSmooth(this MonoBehaviour mono
    , Slider slider
    , float step = 0.1f
    , float stepTime = 0.5f     
    , float stepSmooth = 0.01f
    , bool isSmooth = true)
    {
        if (step <= stepSmooth)
        {
            throw new System.Exception("Prg step <= smooth异常");
        }
        switch (isSmooth)
        {
            case false:
                {
                    mono.StartCoroutine(OnValueChange(slider, step,stepTime));//需要Mono等
                }
                break;
            case true:
                {
                    mono.StartCoroutine(OnValueChangeSmooth(slider, step, stepTime, stepSmooth));
                }
                break;
            default: break;
        }

    }


    /// <summary>生硬点</summary> 
    static IEnumerator OnValueChange(Slider slider
        , float step
        , float stepTime)
    {
        slider.value = 0f;
        while (slider.value <= 1f)
        {
            slider.value += step;

            yield return new WaitForSeconds(stepTime);//每0.5s会执行一遍循环体
        }
    }


    /// <summary>平滑点</summary>
    static IEnumerator OnValueChangeSmooth(Slider slider
       , float step
       , float stepTime
       , float stepSmooth)
    {
        slider.value = 0f;
        //
        float process = 0f;
        while (process < 1f)
        {
            process += step;
            //类似帧循环，快点
            yield return new WaitUntil(() =>
            {
                slider.value = Mathf.SmoothStep(from: slider.value
                    , to: process
                    , t: stepSmooth);
                return process - slider.value < stepSmooth;
            });

            yield return new WaitForSeconds(stepTime);//每0.5s会执行一遍循环体
           
        }
        slider.value = 1.0f;
    }
    #endregion
}

public static partial class ExtendProgressBar  //强制设置进度
{
    // Start is called before the first frame update
    public static void PlayPrgNull(this MonoBehaviour mono, Image image)
    {

        mono.StartCoroutine(LoadNull(image));
    }
    static IEnumerator LoadNull(Image image)
    {
        image.fillAmount = 0f;
        yield return null;

        image.fillAmount = 0.1f;
        yield return new WaitForSeconds(1f);


        image.fillAmount = 0.3f;
        yield return new WaitForSeconds(1f);

        image.fillAmount = 0.6f;
        yield return new WaitForSeconds(1f);

        image.fillAmount = 0.9f;
        yield return new WaitForSeconds(1f);

        image.fillAmount = 1.0f;
        yield return new WaitForSeconds(1f);


    }
}





