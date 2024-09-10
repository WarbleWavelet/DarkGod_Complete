
/*=========================================
* Author: Administrator
* DateTime:2017/6/21 14:03:08
* Description:$safeprojectname$
==========================================*/

using System.Collections.Generic;
using UnityEngine;


    public interface IObjectPool<T> where T : MonoBehaviour
    {
        string PoolName { get; }
        IList<T> Pop(int count);
        T Pop();
        void Push(GameObject go);
        void Push(T t);
    }
    public class ObjectPool<T> : IObjectPool<T> where T : MonoBehaviour
    {

        #region FPC


        private GameObject prefab = null;
        private IList<GameObject> pool = null;
        private string poolName = null;
        public string PoolName
        {
            get { return poolName; }
        }
        public ObjectPool(GameObject prefab, string name = "Pool")
        {
            this.prefab = prefab;
            poolName = name;
            pool = new List<GameObject>();
        }
        #endregion  


        public IList<T> Pop(int count)
        {
            IList<T> result = new List<T>();
            for (int i = 0; i < count; i++)
                result.Add(Pop());
            return result;
        }
        public T Pop()
        {
            if (pool.Count > 0)
            {
                var result = pool[0];
                pool.RemoveAt(0);
                return result.GetComponent<T>();
            }
            return Create();
        }
        public void Push(GameObject go)
        {
            go.SetActive(false);
            pool.Add(go);
        }
        public void Push(T t)
        {
            Push(t.gameObject);
        }
        private T Create()
        {
            var obj = UnityEngine.Object.Instantiate(prefab);
            return obj.AddComponent<T>();
        }
    }
