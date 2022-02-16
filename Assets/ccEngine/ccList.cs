using ccU3DEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace ccU3DEngine
{

    class ccListIT<T> : IEnumerator
    {
        List<T> _aList = null;
        private int _iIndex;
        private T _CurData = default(T);

        public ccListIT(List<T> aList)
        {
            _aList = aList;
            _iIndex = 0;
        }

        object IEnumerator.Current
        {
            get
            {
                return _CurData;
            }
        }

        public bool MoveNext()
        {
            if (_iIndex < _aList.Count)
            {
                _CurData = _aList[_iIndex];
                _iIndex++;
                return true;
            }
            return false;
        }

        public void Reset()
        {
            _iIndex = 0;
            _CurData = default(T);
        }
    }

    public class ccList<T> : IEnumerable
    {

        List<T> _aList = new List<T>();

        #region List

        /// <summary>
        /// 獲取一個值，該值指示 IList 是否具有固定大小。
        /// </summary>
        public bool IsFixedSize
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// 獲取一個值，該值指示 IList 是否為唯讀。
        /// </summary>
        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public int Count
        {
            get
            {
                return _aList.Count;
            }
        }

        /// <summary>
        /// 獲取一個值，該值指示是否同步對 ICollection 的訪問（執行緒安全）。 （繼承自 ICollection。）
        /// </summary>
        public bool IsSynchronized
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// 獲取可用於同步 ICollection 訪問的對象。 （繼承自 ICollection。）
        /// 可以看到，在不斷的繼承過程中，這些介面不斷地添加自己的東西，越繼承越多，越繼承越多
        /// </summary>
        // public object SyncRoot
        //object ICollection.SyncRoot
        //{
        //    get
        //    {
        //        return this;
        //    }
        //}

        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= Count)
                {
                    throw new Exception("index Error.");
                }
                return _aList[index];
            }

            set
            {
                _aList[index] = value;
            }
        }

        #endregion


        public int f_GetCount()
        {
            return _aList.Count;
        }

        public T f_Get(T tData)
        {
            T tFind = _aList.Find(delegate (T tItem)
            {
                if (tItem.Equals(tData))
                {
                    return true;
                }
                return false;
            });
            return tFind;
        }

        public void f_Add(T tData)
        {
            T tFind = f_Get(tData);
            if (tFind == null || tFind.Equals(default(T)))
            {
                _aList.Add(tData);
            }
        }

        public void f_Remove(T tData)
        {
            _aList.Remove(tData);
        }

        public void f_RemoveAt(int i)
        {
            _aList.RemoveAt(i);
        }

        public IEnumerator GetEnumerator()
        {
            return new ccListIT<T>(_aList);
        }

    }


}
