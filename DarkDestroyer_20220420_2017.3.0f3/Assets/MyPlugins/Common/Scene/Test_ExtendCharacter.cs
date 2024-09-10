/****************************************************
    文件：Test_ExtendCharacter.cs
	作者：lenovo
    邮箱: 
    日期：2024/1/10 17:19:6
	功能：
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
 
namespace Demo00_00
{
    public class Test_ExtendCharacter : MonoBehaviour
    {
        #region 属性

        [SerializeField] float _curLife;
        [SerializeField] float _change;
        [SerializeField] Slider _slider;
        #endregion


        /// <summary>首次载入</summary>
        void Awake()
        {
            _slider=GetComponentInChildren<Slider>();
            _slider.onValueChanged.AddListener(Injure);

        }
        void Injure(float change)
        {
            _change = change;
#if !NET_4_6
            _curLife.Injure(_change, () => Debug.Log("受伤"), () => Debug.Log("死亡"));
#endif
        }

    }
}



