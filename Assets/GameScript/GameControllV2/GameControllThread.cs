using ccU3DEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllThread
{
    private int _iActionId = 0;
    private int _iConditionId = 0;
    private GameControll _GameControll = null;
    private bool _bIsComplete = false;
    public GameControllThread(int iConditionId, int iActionId)
    {
        _iActionId = iActionId;
        _iConditionId = iConditionId;
        if (_iActionId == 0 || _iConditionId == 0)
        {
            MessageBox.ASSERT("条件任务对应的执行任务为错误. iConditionId:" + _iConditionId + " iRunAction:" + iActionId);
        }
    }

    public void f_Start()
    {
        _GameControll = new GameControll(_iConditionId);
        _GameControll.f_Start(_iActionId);
        _bIsComplete = false;
    }
    
    public void f_Update()
    {
        if (_bIsComplete)
        {
            return;
        }
        _GameControll.f_Update();
        if (_GameControll.f_IsEnd())
        {
            _bIsComplete = true;
        }
    }

    public bool f_IsComplete()
    {
        return _bIsComplete;
    }

        //#region 通讯接口
        //public void f_RunServerActionState(int iId)
        //{
        //    if (_GameControll == null)
        //    {
        //        _GameControll = new GameControll(_iConditionId);
        //    }
        //    //_GameControll.f_RunServerActionState(iId);
        //}
        //#endregion

    }