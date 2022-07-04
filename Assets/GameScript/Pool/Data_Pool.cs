using UnityEngine;
using System.Collections;



/// <summary>
/// 游戏数据数据结构
/// </summary>
public class Data_Pool
{



    #region POOL
    public GuessPool m_GuessPool;
    #endregion

    private static Data_Pool _Instance = null;
    public static Data_Pool GetInstance()
    {
        if (_Instance == null)
        {
            _Instance = new Data_Pool();
        }

        return _Instance;
    }

    public void f_InitPool()
    {
        m_GuessPool = new GuessPool();
    }

    public void f_LoadPool()
    {

    }

    public void f_Reset()
    {

    }
}