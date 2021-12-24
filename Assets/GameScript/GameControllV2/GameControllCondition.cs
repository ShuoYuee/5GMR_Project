using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllCondition
{
    class GameControllConditionPoolDT
    {
        public GameControllConditionPoolDT(int iId, GameControllPara tGameControllPara, GameControll_ConditionDT tGameControll_ConditionDT)
        {
            _iId = iId;
            _GameControll_ConditionDT = tGameControll_ConditionDT;
            _GameControllPara = tGameControllPara;
        }

        private int _iId;
        private GameControllThread _GameControllThread;
        private GameControll_ConditionDT _GameControll_ConditionDT;
        private GameControllPara _GameControllPara;

        private ConditionState_Base _ConditionState_Base = null;
        private bool _bConditionIsOk = false;

        public void f_Init()
        {
            if (EM_GameControllAction.V3_Init == (EM_GameControllAction)_GameControll_ConditionDT.iConditionId) {
                _ConditionState_Base = new GameControllV3_Init(_iId, _GameControllPara);
            }            
            else if (EM_GameControllAction.Parament == (EM_GameControllAction)_GameControll_ConditionDT.iConditionId)
            {
                _ConditionState_Base = new ConditionState_Parament(_iId, _GameControllPara);
            }         
            else if (EM_GameControllAction.PromptBox == (EM_GameControllAction)_GameControll_ConditionDT.iConditionId)
            {
                //_GameControll_ConditionDT.iLoop = 0;
                _ConditionState_Base = new ConditionState_PromptBoxCheck(_iId, _GameControllPara);
            }
            else {
                MessageBox.ASSERT("无法解析的条件指令请查对 Id:" + _GameControll_ConditionDT.iId + " 条件:" + _GameControll_ConditionDT.iConditionId);
            }
            _ConditionState_Base.f_Init(_GameControll_ConditionDT.szParament, _GameControll_ConditionDT.szParamentData, _GameControll_ConditionDT.szData1, _GameControll_ConditionDT.szData2, _GameControll_ConditionDT.szData3, _GameControll_ConditionDT.szData4);
            _GameControllThread = new GameControllThread(_iId, _GameControll_ConditionDT.iRunAction);
        }

        public void f_Update()
        {
            if (!_bConditionIsOk)
            {
                if (_ConditionState_Base.f_Check())
                {
                    _bConditionIsOk = true;
                    _GameControllThread.f_Start();
                }
            }
            else
            {
                _GameControllThread.f_Update();
                if (_GameControllThread.f_IsComplete() && _GameControll_ConditionDT.iLoop == 1)
                {
                    DoNextCondition();
                }
            }
        }
        private void DoNextCondition()
        {
            _bConditionIsOk = false;
            _ConditionState_Base.f_Reset();
        }

        public int f_GetId()
        {
            return _iId;
        }

        //public void f_RunServerActionState(int iId)
        //{
        //    _GameControllThread.f_RunServerActionState(iId);
        //}
    }

    //End GameControllConditionPoolDT
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////


    private GameControllPara _GameControllPara = null;
    List<GameControllConditionPoolDT> _aData = new List< GameControllConditionPoolDT>();

    public GameControllCondition (GameControllPara tGameControllPara)
    {
        _GameControllPara = tGameControllPara;
    }

    public void f_Init()
    {
        List<NBaseSCDT> aData = glo_Main.GetInstance().m_SC_Pool.m_GameControll_ConditionSC.f_GetAll();
        for (int i = 0; i < aData.Count; i++)
        {
            GameControll_ConditionDT tGameControll_ConditionDT = (GameControll_ConditionDT)aData[i];
            GameControllConditionPoolDT tGameControllConditionPoolDT = new GameControllConditionPoolDT(tGameControll_ConditionDT.iId, _GameControllPara, tGameControll_ConditionDT);
            tGameControllConditionPoolDT.f_Init();
            _aData.Add(tGameControllConditionPoolDT);
        }
    }
    
    public void f_Update()
    {
        for (int i = 0; i < _aData.Count; i++)
        {
            GameControllConditionPoolDT tGameControllConditionPoolDT = _aData[i];
            tGameControllConditionPoolDT.f_Update();
        }

    }


    private GameControllConditionPoolDT GetGameControllConditionPoolDT(int iConditionId)
    {
        GameControllConditionPoolDT tGameControllConditionPoolDT = _aData.Find(delegate (GameControllConditionPoolDT tItem)
                                                                                {
                                                                                    if (tItem.f_GetId() == iConditionId)
                                                                                    {
                                                                                        return true;
                                                                                    }
                                                                                    return false;
                                                                                } );
        return tGameControllConditionPoolDT;
    }

    //public void f_RunServerActionState(int iConditionId, int iId)
    //{
    //    GameControllConditionPoolDT tGameControllConditionPoolDT = GetGameControllConditionPoolDT(iConditionId);        
    //    if (tGameControllConditionPoolDT != null)
    //    {
    //        tGameControllConditionPoolDT.f_RunServerActionState(iId);
    //    }
    //    else
    //    {
    //        MessageBox.ASSERT("未找到对应的事件处理线程，" + iConditionId);
    //    }
    //}



}