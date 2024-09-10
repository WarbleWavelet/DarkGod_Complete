/****************************************************
    文件：PRSInspector.cs
	作者：lenovo
    邮箱: 
    日期：2024/6/19 11:28:7
	功能：  Postion Rotation.eulerAngles Scale And Local 
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


/// <summary>Postion Rotation.eulerAngles Scale And Local </summary>
public class PRSInspector : MonoBehaviour
{

    [SerializeField] Vector3 _position;
    [SerializeField] Vector3 _LocalPosition;
    [SerializeField] Vector3 _rotation;
    [SerializeField] Vector3 _localRotation;
    [SerializeField] Vector3 _scale;
    [SerializeField] Vector3 _localScale;
    float timing = 0f;
    private void Update()
    {
        timing = this.Timer(timing, 1f, () => 
        {
            _position=transform.position;
            _LocalPosition=transform.localPosition;
            _rotation=transform.rotation.eulerAngles;
            _localRotation= transform.localRotation.eulerAngles;
            _scale=transform.lossyScale;
            _localScale=transform.localScale;
        });   
    }
}





