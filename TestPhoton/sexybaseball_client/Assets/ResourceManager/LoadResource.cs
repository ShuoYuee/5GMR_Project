using System;
using System.Collections;
using ccU3DEngine;
using UnityEngine;
/// <summary>
/// 资源加载
/// </summary>
public class LoadResource
{
    private ccMachineManager _ResManager = null;
    private int _iLoadResourceTime = 0;
    private string _strResourceMd5;

    /// <summary>
    /// 资源加载完回调
    /// </summary>
    private ccCallback _hCallBack;

    /// <summary>
    /// 资源开始加载
    /// </summary>
    /// <param name="hCallBack">资源加载完函数回调</param>
    public void f_StartLoad(ccCallback hCallBack)
    {
        _hCallBack = hCallBack;
        InitResManager();
    }

    private void InitResManager()
    {
        MessageBox.DEBUG("加载资源");

        _ResManager = new ccMachineManager(new ResManagerState_Loop());

        ccMachineStateBase tFstMachineStateBase = new ResManagerState_Ver();
        _ResManager.f_RegState(tFstMachineStateBase);
        _ResManager.f_RegState(new ResManagerState_LoadSC());
        _ResManager.f_RegState(new ResManagerState_DispSC());
        _ResManager.f_RegState(new ResManagerState_Login(LoadResourceSuc));
        _ResManager.f_ChangeState(tFstMachineStateBase);

        _iLoadResourceTime = ccTimeEvent.GetInstance().f_RegEvent(0.1f, true, null, Callback_Update);
    }

    private void Callback_Update(object Obj)
    {
        _ResManager.f_Update();
    }

    private void LoadResourceSuc(object Obj)
    {
        ccTimeEvent.GetInstance().f_UnRegEvent(_iLoadResourceTime);
        _hCallBack(eMsgOperateResult.OR_Succeed);
    }
}