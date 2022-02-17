using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Text;
/// <summary>
/// 本地資料保存和獲取工具
/// </summary>
public class LocalDataManager
{
    /// <summary>
    /// 本地資料首碼
    /// </summary>
    private static readonly string localDataStr = "localdata";


    private static string GetKey(string strKey)
    {
        return localDataStr + strKey;
    }

    #region 設置本地數據 設置值
    /// <summary>
    /// 設置本地數據 設置值
    /// </summary>
    /// <typeparam name="T">資料類型（int,float,string,bool等基底資料型別，或由基底資料型別封裝的類）</typeparam>
    /// <param name="dataType">資料名字</param>
    /// <param name="value">數據值</param>
    public static void f_SetLocalData<T>(string strKey, T value)
    {
        Type type = typeof(T);
        string key = GetKey(strKey);
        if (type.Equals(typeof(int)))
        {
            PlayerPrefs.SetInt(key, (int)Convert.ChangeType(value, typeof(int)));
        }
        else if (type.Equals(typeof(float)))
        {
            PlayerPrefs.SetFloat(key, (float)Convert.ChangeType(value, typeof(float)));
        }
        else if (type.Equals(typeof(string)))
        {
            PlayerPrefs.SetString(key, (string)Convert.ChangeType(value, typeof(string)));
        }
        else if (type.Equals(typeof(bool)))
        {
            int data = ((bool)Convert.ChangeType(value, typeof(bool))) ? 1 : 0;
            PlayerPrefs.SetInt(key, (int)Convert.ChangeType(data, typeof(int)));
        }
        else
        {
            MessageBox.ASSERT("不支援的資料類型");
        }
        PlayerPrefs.Save();

        //Debug.Log("Value: " + value + " , Get: " + f_GetLocalData<T>(strKey, value));
    }
    #endregion

    #region 檢查本地資料中是否已經有該資料
    /// <summary>
    /// 檢查本地資料中是否已經有該資料
    /// </summary>
    /// <param name="dataType">資料名字</param>
    /// <returns>是否已經有該資料</returns>
    public static bool f_HasLocalData(string strKey)
    {
        string key = GetKey(strKey);
        return PlayerPrefs.HasKey(key);
    }
    #endregion

    #region 從本地資料中刪除該資料值
    /// <summary>
    /// 從本地資料中刪除該資料值
    /// </summary>
    /// <param name="dataType">資料名字</param>
    public static void f_DeleteLocalData(string strKey)
    {
        string key = GetKey(strKey);
        PlayerPrefs.DeleteKey(key);
    }
    #endregion

    #region 刪除所有本地資料
    /// <summary>
    /// 刪除所有本地資料
    /// </summary>
    public static void f_DeleteAllLocalData()
    {
        PlayerPrefs.DeleteAll();
    }
    #endregion

    #region 獲取本地資料中某個資料值
    /// <summary>
    /// 獲取本地資料中某個資料值
    /// </summary>
    /// <typeparam name="T">資料類型（int,float,string,bool等基底資料型別，或由基底資料型別封裝的類）</typeparam>
    /// <param name="dataType">資料名字</param>
    /// <param name="Defaultalue">如果未發現對應的關健資料則返回此資料</param>
    /// <returns>資料中該資料值</returns>
    public static T f_GetLocalData<T>(string strKey, T Defaultalue = default(T))
    {
        T returnValue = default(T);
        string key = GetKey(strKey);
        Type type = typeof(T);
        if (PlayerPrefs.HasKey(key))
        {
            if (type.Equals(typeof(int)))
            {
                returnValue = (T)Convert.ChangeType(PlayerPrefs.GetInt(key), type);
            }
            else if (type.Equals(typeof(float)))
            {
                returnValue = (T)Convert.ChangeType(PlayerPrefs.GetFloat(key), type);
            }
            else if (type.Equals(typeof(string)))
            {
                returnValue = (T)Convert.ChangeType(PlayerPrefs.GetString(key), type);
            }
            else if (type.Equals(typeof(bool)))
            {
                //bool value = PlayerPrefs.GetInt(key) == 1 ? true : false;
                //returnValue = (T)Convert.ChangeType(value, type);
                returnValue = (T)Convert.ChangeType(PlayerPrefs.GetInt(key), type);
            }
            else
            {
                MessageBox.ASSERT("不支援的資料類型");
            }
        }
        else
        {
            return Defaultalue;
        }
        return returnValue;
    }
    #endregion



}
