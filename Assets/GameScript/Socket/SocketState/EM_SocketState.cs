using UnityEngine;
using System.Collections;

/// <summary>
/// Socket狀態
/// </summary>
public enum EM_Socket
{
    //OffLine,
    Connect = 1,//連接
    Regedit,//註冊
    Login,//登入
    Loop,
    Wait,//等待
    /// <summary>
    /// 選角色
    /// </summary>
    SelCharacter,

    //ChatLogin,//登入
}
