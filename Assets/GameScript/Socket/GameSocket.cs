using UnityEngine;
using System.Collections;
using ccU3DEngine;
using System.Collections.Generic;
using System;


public class GameSocket : BaseSocket
{
    private Socket_Loop _Socket_Loop = null;
    private static GameSocket _Instance = null;
    private System.DateTime m_fLastTime;

    public static GameSocket GetInstance()
    {
        if (null == _Instance)
        {
            _Instance = new GameSocket();
        }

        return _Instance;
    }

    public GameSocket()
    {
        m_strTTTT = "GameSocket";
    }

    protected override void InitMachine()
    {
        base.InitMachine();

        Socket_Wait tSocket_Wait = new Socket_Wait(this);
        _Socket_Loop = new Socket_Loop(this);
        _SocketMachineManger = new ccMachineManager(_Socket_Loop);
        _SocketMachineManger.f_RegState(new Socket_Connect(this));
        _SocketMachineManger.f_RegState(new Socket_Login(this));
        _SocketMachineManger.f_RegState(new Socket_Wait(this));
        _SocketMachineManger.f_ChangeState(tSocket_Wait);

    }

    public override void f_Close()
    {
        base.f_Close();

    }

    #region 登陸相關

    public void f_Login(ccCallback func = null)
    {
        //Socket_Login tSocket_Login = (Socket_Login)_SocketMachineManger.f_GetStaticBase((int)EM_Socket.Login);
        //tSocket_Login.f_Login(func);
        //_SocketMachineManger.f_ChangeState(tSocket_Login, func);
    }

    #endregion

    #region 內部消息處理

    public override void Destroy()
    {
        base.Destroy();
        GameSocket._Instance = null;
    }

    public override void f_Update()
    {
        base.f_Update();

    }

    #endregion

    #region 外部消息處理

    protected override void InitMessage()
    {
        base.InitMessage();

        //m_GMSocketMessagePool.f_AddListener(SocketCommand.GM_SCK_UNBUILD.ToString(), On_GM_SCK_UNBUILD, null);
        //stGameCommandReturn tGameCommandRet = new stGameCommandReturn();
        //f_AddListener((int)SocketCommand.CONTROL_CTG_OperateResult, tGameCommandRet, On_CMsg_GameCommandReturn);

        //////////////////////////////////////////////////////////////////////////
        //DATA
        //stCarStatic tstCarStatic = new stCarStatic();
        //f_AddListener((int)SocketCommand.emResultUpdate, tstCarStatic, OnResultUpdate);

        basicNode1 tPing = new basicNode1();
        f_AddListener((int)SocketCommand.PING, tPing, On_Ping);


        //stLedCommand tstLedCommand = new stLedCommand();
        //f_AddListener((int)SocketCommand.emLedMotor, tstLedCommand, On_stLedCommand);

        //stPlayerFishing tstPlayerFishing = new stPlayerFishing();
        //f_AddListener((int)SocketCommand.emPlayerFishing, tstPlayerFishing, OnPlayerFishing);

        //stGameControll tstGameControll = new stGameControll();
        //f_AddListener((int)SocketCommand.emGameControll, tstGameControll, OnGameControll);


    }

    #endregion

    #region 被踢下線相關

    private void f_QuitGameSureHandle(object value)
    {
        //退出遊戲
        Application.Quit();
    }

    #endregion


    #region 時間

    public override void f_Ping()
    {
        CreateSocketBuf tCreateSocketBuf = new CreateSocketBuf();
        byte[] bBuf = tCreateSocketBuf.f_GetBuf();
        int iNum = f_SendBuf2Force((int)SocketCommand.PING, bBuf);

        MessageBox.DEBUG("Game Ping");
    }


    #endregion


    protected void OnPlayerFishing(object Obj)
    {
        if (Obj == null)
        {
            return;
        }
        stPlayerFishing tstPlayerFishing = (stPlayerFishing)Obj;

        MessageBox.DEBUG("OnPlayerFishing:" + tstPlayerFishing.iFishScId);
        glo_Main.GetInstance().m_GameMessagePool.f_Broadcast(GameMessageDef.PlayerFishing, tstPlayerFishing);

    }

    protected void OnGameControll(object Obj)
    {
        if (Obj == null)
        {
            return;
        }
        stGameControll tstGameControll = (stGameControll)Obj;

        MessageBox.DEBUG("OnGameControll:" + tstGameControll.iGameControllId);
        glo_Main.GetInstance().m_GameMessagePool.f_Broadcast(GameMessageDef.GameControll, tstGameControll);
    }

    public void f_SendPlayerCharge(stPlayerCharge tstPlayerCharge)
    {
        CreateSocketBuf tCreateSocketBuf = new CreateSocketBuf();
        tCreateSocketBuf.f_Add(tstPlayerCharge);
        byte[] bBuf = tCreateSocketBuf.f_GetBuf();
        //int iNum = f_SendBuf2Force((int)SocketCommand.emPlayerCharge, bBuf);
    }


}//END Class

