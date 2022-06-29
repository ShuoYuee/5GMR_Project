namespace SexyBaseball.Server
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using ccU3DEngine;
    using GameLogic;
    using Photon.SocketServer;
 

    /// <summary>
    /// 服务器逻辑处理类，在此进行服务器逻辑处理
    /// </summary>
    public class ServerApp : ApplicationBase
    {
        private MonohUI _MonohUI = new MonohUI();

        SC_Pool _SC_Pool = new SC_Pool();
        SCLoad _SCLoad = new SCLoad();
        GameLogicPool _GameLogicPool = new GameLogicPool();
        GameMainPool _GameMainPool = new GameMainPool();

        ccClientSocketPeer _BaseballPeer = null;
        //当客户端链接过来，这里要创建一个peer,处理连接请求
        protected override PeerBase CreatePeer(InitRequest initRequest)
        {
            _BaseballPeer = new ccClientSocketPeer(initRequest, _GameLogicPool);
            InitMessage();
            return _BaseballPeer;
        }

        //初始化
        protected override void Setup()
        {
            MessageBox.DEBUG("Init...");

            _MonohUI.f_Start();
            MessageBox.f_RegLogCall(_MonohUI.CallBack_Log);

            _SCLoad.f_Start();
            while(!_SCLoad.f_IsComplete())
            {
                System.Threading.Thread.Sleep(100);
            }
            byte[] aFileBuf = File.ReadAllBytes("ccData_W.bytes");
            _SC_Pool.f_LoadSC(aFileBuf);

            _GameLogicPool.f_Init(this.BinaryPath);
            _GameMainPool.f_Init();

            MessageBox.DEBUG("ok");

        }

        //当服务端关闭的时候
        protected override void TearDown()
        {
            _MonohUI.f_Stop();
            _MonohUI.f_Destory();
            _MonohUI = null;
        }


        void InitMessage()
        {
            //登入
            _BaseballPeer.f_AddListener((int)SocketCommand.CS_UserLogin, new CMsg_CTG_AccountEnter(), On_CMsg_CTG_AccountEnter);
            //登出
            _BaseballPeer.f_AddListener((int)SocketCommand.CS_UserLogout, new CMsg_CTG_AccountExit(), On_CMsg_CTG_AccountExit);
            //註冊
            _BaseballPeer.f_AddListener((int)SocketCommand.CS_UserCreate, new CMsg_CTG_AccountCreate(), On_CMsg_CTG_AccountCreate);

            //猜測遊戲狀態
            _BaseballPeer.f_AddListener((int)SocketCommand.ServerCommand, new CMsg_CTG_ServerCommand(), OnCMsg_CTG_CommandCall);
            //猜測請求
            _BaseballPeer.f_AddListener((int)SocketCommand.PlayerGuess, new CMsg_CTG_Guess(), On_CMsg_CTG_Guess);
            //取得分數
            _BaseballPeer.f_AddListener((int)SocketCommand.GameScore, new CMsg_CTG_GetScore(), On_CMsg_CTG_GetScore);
        }



        #region LogIn
        void On_CMsg_CTG_AccountEnter(object Obj)
        {//登入
            if (Obj == null) { return; }
            CMsg_CTG_AccountEnter tCMsg_CTG_AccountEnter = (CMsg_CTG_AccountEnter)Obj;
            //ccClientSocketPeer ccClientSocketPeer = _GameLogicPool.f_Login_CheckPeer(_BaseballPeer, tCMsg_CTG_AccountEnter.m_strAccount);

            CMsg_GTC_LoginRelt tCMsg_GTC_LoginRelt = new CMsg_GTC_LoginRelt();
            tCMsg_GTC_LoginRelt.m_result = 0;
            tCMsg_GTC_LoginRelt = _GameLogicPool.f_UserLogin(_BaseballPeer, tCMsg_CTG_AccountEnter.m_strAccount, tCMsg_CTG_AccountEnter.m_strPassword);
            _BaseballPeer.f_SendBuf((int)SocketCommand.UserLogin_Reps, tCMsg_GTC_LoginRelt);
        }

        private void On_CMsg_CTG_AccountExit(object Obj)
        {//登出
            if (Obj == null) { return; }
            CMsg_CTG_AccountExit tCMsg_CTG_AccountExit = (CMsg_CTG_AccountExit)Obj;
            _GameLogicPool.f_UserLogout(tCMsg_CTG_AccountExit.m_strAccount, tCMsg_CTG_AccountExit.m_iPlayerID);
        }

        private void On_CMsg_CTG_AccountCreate(object Obj)
        {//註冊
            if (Obj == null) { return; }
            CMsg_CTG_AccountCreate tCMsg_CTG_AccountCreate = (CMsg_CTG_AccountCreate)Obj;

            CMsg_CTG_AccountCreateRelt tCMsg_CTG_AccountCreateRelt = new CMsg_CTG_AccountCreateRelt();
            tCMsg_CTG_AccountCreateRelt.m_iResult = _GameLogicPool.f_UserCreate(tCMsg_CTG_AccountCreate);
            tCMsg_CTG_AccountCreateRelt.m_strAccount = tCMsg_CTG_AccountCreate.m_strAccount;

            _BaseballPeer.f_SendBuf((int)SocketCommand.UserCreate_Reps, tCMsg_CTG_AccountCreateRelt);
        }
        #endregion

        private void OnCMsg_CTG_CommandCall(object Obj)
        {//呼叫遊戲狀態
            CMsg_CTG_ServerCommand tCMsg_CTG_ServerCommand = (CMsg_CTG_ServerCommand)Obj;
            if (tCMsg_CTG_ServerCommand.m_iCommand == (int)EM_GameMod.Guess)
            {
                On_CMsg_CTG_GuessState(tCMsg_CTG_ServerCommand);
            }
        }

        private void On_CMsg_CTG_Guess(object Obj)
        {//猜測(抽獎)
            if (Obj == null) { return; }
            CMsg_CTG_Guess tCMsg_CTG_Guess = (CMsg_CTG_Guess)Obj;
            
            int iRelt = _GameMainPool.f_CheckGuessIsWin(tCMsg_CTG_Guess.m_iGuess);
            stClient stClient = _GameLogicPool.f_FindOnlineClient(tCMsg_CTG_Guess.m_szAccount, tCMsg_CTG_Guess.m_lPlayerID);
            if (stClient != null)
            {
                if (iRelt == (int)EM_GuessResult.Win)
                {//猜測成功則增加陣營分數
                    _GameMainPool.f_AddTeamScore(stClient.m_iTeam, 10);
                }
            }

            /*CMsg_CTG_GetScoreResult tCMsg_CTG_GuessResult = new CMsg_CTG_GetScoreResult();
            tCMsg_CTG_GuessResult.m_iResult = iRelt;
            tCMsg_CTG_GuessResult.m_iScoreA = _GameMainPool.f_GetTeamScore((int)EM_TeamID.TeamA);
            tCMsg_CTG_GuessResult.m_iScoreB = _GameMainPool.f_GetTeamScore((int)EM_TeamID.TeamB);
            stClient.m_ccClientSocketPeer.f_SendBuf((int)SocketCommand.PlayerGuessResult, tCMsg_CTG_GuessResult);*/
        }

        private void On_CMsg_CTG_GetScore(object Obj)
        {
            CMsg_CTG_GetScore tCMsg_CTG_GetScore = (CMsg_CTG_GetScore)Obj;
            stClient stClient = _GameLogicPool.f_FindOnlineClient(tCMsg_CTG_GetScore.m_szAccount, tCMsg_CTG_GetScore.m_lPlayerID);

            CMsg_CTG_GetScoreResult tCMsg_CTG_GuessResult = new CMsg_CTG_GetScoreResult();
            tCMsg_CTG_GuessResult.m_iResult = _GameMainPool.f_CheckGuessIsWin(tCMsg_CTG_GetScore.m_iGuess);
            tCMsg_CTG_GuessResult.m_iScoreA = _GameMainPool.f_GetTeamScore((int)EM_TeamID.TeamA);
            tCMsg_CTG_GuessResult.m_iScoreB = _GameMainPool.f_GetTeamScore((int)EM_TeamID.TeamB);
            stClient.m_ccClientSocketPeer.f_SendBuf((int)SocketCommand.PlayerGuessResult, tCMsg_CTG_GuessResult);
        }

        private void On_CMsg_CTG_GuessState(CMsg_CTG_ServerCommand tCMsg_CTG_GuessCommand)
        {//猜測遊戲狀態
            //ClientPeer
            stClient stClient = _GameLogicPool.f_FindOnlineClient(tCMsg_CTG_GuessCommand.m_szAccount, tCMsg_CTG_GuessCommand.m_lPlayerID);

            if (tCMsg_CTG_GuessCommand.m_iCallState == (int)EM_GuessState.CheckState)
            {//確認遊戲
                if (_GameMainPool._RoomMaster == null)
                {
                    _GameMainPool._bGameStart = false;
                }
                else if (!_GameLogicPool.f_CheckIsOnline(_GameMainPool._RoomMaster))
                {
                    _GameMainPool._bGameStart = false;
                }
                int iRelt = _GameMainPool._bGameStart ? (int)EM_GuessState.GameIng : (int)EM_GuessState.NotGameIng;

                CMsg_CTG_CheckGuessRelt tCMsg_CTG_CheckStateRelt = new CMsg_CTG_CheckGuessRelt();
                tCMsg_CTG_CheckStateRelt.m_iResult = iRelt;
                _GameLogicPool.f_BoardCast(0, (int)SocketCommand.GamePlayCheckRelt, tCMsg_CTG_CheckStateRelt);
            }
            else if(tCMsg_CTG_GuessCommand.m_iCallState == (int)EM_GuessState.Start)
            {//開始遊戲
                if (_GameMainPool._bGameStart)
                {//遊戲已由他人開啟
                    CMsg_CTG_CheckGuessRelt tCMsg_CTG_CheckGuessRelt = new CMsg_CTG_CheckGuessRelt();
                    tCMsg_CTG_CheckGuessRelt.m_iResult = (int)EM_GuessState.Error_GameIsStart;
                    stClient.m_ccClientSocketPeer.f_SendBuf((int)SocketCommand.GamePlayCheckRelt, tCMsg_CTG_CheckGuessRelt);
                }
                else
                {//由自己開啟遊戲
                    _GameMainPool.f_Init();
                    _GameMainPool._bGameStart = true;
                    _GameMainPool._RoomMaster = stClient.m_ccClientSocketPeer;
                    
                    //建立房主
                    CMsg_CTG_CheckGuessRelt tCMsg_CTG_CheckGuessRelt = new CMsg_CTG_CheckGuessRelt();
                    tCMsg_CTG_CheckGuessRelt.m_iResult = (int)EM_GuessState.CallRoomMaster;
                    stClient.m_ccClientSocketPeer.f_SendBuf((int)SocketCommand.GamePlayCheckRelt, tCMsg_CTG_CheckGuessRelt);

                    //呼叫在房間內的玩家開啟遊戲
                    CMsg_CTG_CheckGuessRelt aCMsg_CTG_CheckGuessRelt = new CMsg_CTG_CheckGuessRelt();
                    aCMsg_CTG_CheckGuessRelt.m_iResult = (int)EM_GuessState.CallStart;
                    _GameLogicPool.f_BoardCast(0, (int)SocketCommand.GamePlayCheckRelt, aCMsg_CTG_CheckGuessRelt);
                }
            }
            else if (tCMsg_CTG_GuessCommand.m_iCallState == (int)EM_GuessState.Guess)
            {//進行猜測
                _GameMainPool.f_GuessIng();
                CMsg_CTG_ClientCommand tCMsg_CTG_ClientCommand = new CMsg_CTG_ClientCommand();
                tCMsg_CTG_ClientCommand.m_iResult = 0;
                tCMsg_CTG_ClientCommand.m_iCommand = (int)EM_GameMod.Guess;
                tCMsg_CTG_ClientCommand.m_iCallState = (int)EM_GuessState.CallGuess;
                _GameLogicPool.f_BoardCast(0, (int)SocketCommand.ClientCommand, tCMsg_CTG_ClientCommand);
            }
            else if (tCMsg_CTG_GuessCommand.m_iCallState == (int)EM_GuessState.Restart)
            {//遊戲重啟
                CMsg_CTG_ClientCommand tCMsg_CTG_ClientCommand = new CMsg_CTG_ClientCommand();
                tCMsg_CTG_ClientCommand.m_iResult = 0;
                tCMsg_CTG_ClientCommand.m_iCommand = (int)EM_GameMod.Guess;
                tCMsg_CTG_ClientCommand.m_iCallState = (int)EM_GuessState.CallRestart;
                _GameLogicPool.f_BoardCast(0, (int)SocketCommand.ClientCommand, tCMsg_CTG_ClientCommand);
            }
            else if (tCMsg_CTG_GuessCommand.m_iCallState == (int)EM_GuessState.End)
            {//遊戲結束
                _GameMainPool._bGameStart = false;
                _GameMainPool._RoomMaster = null;

                CMsg_CTG_ClientCommand tCMsg_CTG_ClientCommand = new CMsg_CTG_ClientCommand();
                tCMsg_CTG_ClientCommand.m_iResult = 0;
                tCMsg_CTG_ClientCommand.m_iCommand = (int)EM_GameMod.Guess;
                tCMsg_CTG_ClientCommand.m_iCallState = (int)EM_GuessState.CallEnd;
                _GameLogicPool.f_BoardCast(0, (int)SocketCommand.ClientCommand, tCMsg_CTG_ClientCommand);
            }
        }
    }
}
