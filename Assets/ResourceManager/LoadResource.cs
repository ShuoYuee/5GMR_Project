using UnityEngine;
using System.Collections;
using ccU3DEngine;
using System;
/// <summary>
/// 資源載入
/// </summary>
public class LoadResource
{
    private ccMachineManager _ResManager = null;
    private int _iLoadResourceTime = 0;
    private string _strResourceMd5;
    //public delegate void Callback_LoadHttp(HttpDataDT eHttpDataDT);

    /// <summary>
    /// 資源加載完回調
    /// </summary>
    private ccCallback _hCallBack;
    /// <summary>
    /// 資源開始加載
    /// </summary>
    /// <param name="hCallBack">資源加載完回調</param>
    public void f_StartLoad(ccCallback hCallBack)
    {
        _hCallBack = hCallBack;
        InitResManager();
    }

    private void InitResManager()
    {
        MessageBox.DEBUG("載入資源");

        _ResManager = new ccMachineManager(new ResManagerState_Loop());
        //GameObject tLoginPage = GameObject.Find("LoginPage");

        ccMachineStateBase tFstMachineStateBase = new ResManagerState_Ver();
        _ResManager.f_RegState(tFstMachineStateBase);
        _ResManager.f_RegState(new ResManagerState_LoadSC());
        _ResManager.f_RegState(new ResManagerState_DispSC());
        //_ResManager.f_RegState(new ResManagerState_UpdateRes());
        //_ResManager.f_RegState(new ResManagerState_UpdateResForLocal());
        //_ResManager.f_RegState(new ResManagerState_UpdateResForServer());
        _ResManager.f_RegState(new ResManagerState_Login(LoadResourceSuc));
        _ResManager.f_ChangeState(tFstMachineStateBase);

        _iLoadResourceTime = ccTimeEvent.GetInstance().f_RegEvent(0.1f, true, null, Callback_Update);
    }

    void Callback_Update(object Obj)
    {
        _ResManager.f_Update();
    }

    private void LoadResourceSuc(object Obj)
    {
        ccTimeEvent.GetInstance().f_UnRegEvent(_iLoadResourceTime);
        _hCallBack(eMsgOperateResult.OR_Succeed);
    }






}
