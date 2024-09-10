/****************************************************
    文件：SceneStart.cs
	作者：lenovo
    邮箱: 
    日期：2024/5/1 22:9:40
	功能： 
            原来想跟 https://blog.csdn.net/qq_49152649/article/details/135119672
            发现用另外一种也能实现
            没有做节点间连线的唯一性,
            没有做连线随着节点的移动而移动
*****************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UIElements;
using Random = UnityEngine.Random;

namespace Common.CharacterRelationship_Sinmple
{

    public class SceneCtrl : MonoBehaviour
    {
        public float w;
        public float h;
        public int fontSize;
        //Assets/MyPlugins/Common/System/DrawLineSystem/Line.prefab
        public GameObject LinePrefab;


        void Awake()
        {
            if (gameObject.DestroyIfMoreThanOne<SceneCtrl>())
            {
                return;
            }        
        }

#if UNITY_EDITOR
        private void OnGUI()
        {
            // Test_OnGUI类似css的位置设置();
            // Test_观察坐标();
            //Test_观察坐标来动态设置Game分辨率();
            // Test_Rect和OnGUI的关系();
             人物关系图();
        }


#endif

        private void 人物关系图()
        {
            Canvas canvas = transform.TopCanvas();
            Vector2Int canvasSize = canvas.CanvasSizeInt();
            int btnW = 100;
            int btnH = 30;
            //
            Vector2Int pos1 = ExtendOnGUI.GetGUIPos(btnW, btnH, 10, EDir.RIGHTBOTTOM, canvasSize);
            Vector2Int pos2 = ExtendOnGUI.GetGUIPos(btnW, btnH, 10 * 2 + btnW, 10, EDir.RIGHTBOTTOM, canvasSize);

            if (GUI.Button(new Rect(pos1.x, pos1.y, btnW, btnH), "Add节点"))
            {
                GameObject prefab = Resources.Load<GameObject>(GameObjectName.Node);
                GameObject go = GameObject.Instantiate(prefab, canvas.transform);

            }
            if (GUI.Button(new Rect(pos2.x, pos2.y, btnW, btnH), "Add连线"))
            {
                GameMgr.Instance.LineDrawing = true; ;
                //
            }
        }

        #region pri

        #endregion
        void Test_OnGUI类似css的位置设置()
        {
            Canvas canvas = transform.TopCanvas();
            Vector2Int canvasSize = canvas.CanvasSizeInt();
            int btnW = 100;
            int btnH = 30;
            //
            Vector2Int pos1 = ExtendOnGUI.GetGUIPos(btnW, btnH, 10, EDir.RIGHTBOTTOM, canvasSize);
            Vector2Int pos2 = ExtendOnGUI.GetGUIPos(btnW, btnH, 10*2+btnW, 10, EDir.RIGHTBOTTOM, canvasSize);
           
            if( GUI.Button(new Rect(pos1.x, pos1.y, btnW, btnH), "Add节点"))
            {
            }
            if (GUI.Button(new Rect(pos2.x, pos2.y, btnW, btnH), "Add连线"))
            {
            }
            
        }
        void Test_观察坐标()
        {
            Canvas canvas = transform.FindTop(GameObjectName.Canvas).GetComponent<Canvas>();
            Vector2Int canvasSize = canvas.CanvasSizeInt();
            int w = (int)Mathf.Ceil(ScalableBufferManager.widthScaleFactor * Screen.currentResolution.width);
            int h = (int)Mathf.Ceil(ScalableBufferManager.heightScaleFactor * Screen.currentResolution.height);
            Debug.Log(canvasSize);
            //
            Debug.Log(Screen.currentResolution); //1920 x 1080 @ 60Hz
            Debug.Log(new Vector2(w,h)); //1920 x 1080 
        }


        /// <summary>某种模式的CanvasScaler才可以</summary>
        void Test_观察坐标来动态设置Game分辨率()
        {
            Camera camera = Camera.main.MainCameraIfNull();
            camera.allowDynamicResolution = true;
            Canvas canvas = transform.FindTop(GameObjectName.Canvas).GetComponent<Canvas>();
            int rezWidth = (int)Mathf.Ceil(ScalableBufferManager.widthScaleFactor * Screen.currentResolution.width);
            int rezHeight = (int)Mathf.Ceil(ScalableBufferManager.heightScaleFactor * Screen.currentResolution.height);

            //Debug.Log(new Vector2Int(rezWidth,rezHeight));
            //Debug.Log(new Vector2(ScalableBufferManager.widthScaleFactor, ScalableBufferManager.widthScaleFactor));
            //Debug.Log(Screen.currentResolution);
        
        }

        void Test_Rect和OnGUI的关系()
        { 
        
        }

    }
}







