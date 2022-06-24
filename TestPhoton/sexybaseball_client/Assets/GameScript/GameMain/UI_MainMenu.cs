using System.Collections.Generic;
using ccU3DEngine;
using SexyBaseball.Server;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GameLogic
{
    public class UI_MainMenu : ccUILogicBase
    {
        /*private Text _textTitle;
        private Text _textEnergy;
        private Button _btnStoryMode;

        private GameObject _gobjLastSelected;
        //private List<ItemInUse> _lstItems = new List<ItemInUse>();
        private int _iTextTimer;*/
        private ccMachineManager _machineMgr = null;
        private Button _BtnWinA, _BtnWinB, _BtnLogout;
        private GameObject _BtnStart, _Panel2;
        private Text _InforText, _ScoreText;
        public int _iCurSelTeam = 0;

        protected override void On_Init()
        {
            /*_textTitle = f_GetObject("Title").GetComponent<Text>();
            _textEnergy = f_GetObject("Energy").GetComponent<Text>();
            _btnStoryMode = f_GetObject("StoryMode").GetComponent<Button>();

            _textTitle.text = LanguageManager.GetInstance().f_GetText("GmStr_MainMenu_TextTitle");

            f_RegClickEvent("StoryMode", OnClick_StoryMode);
            f_RegClickEvent("ChallengeMode", OnClick_ChallengeMode);
            f_RegClickEvent("Shop", OnClick_Shop);
            f_RegClickEvent("Gallery", OnClick_Gallery);
            f_RegClickEvent("Settings", OnClick_Settings);
            f_RegClickEvent("Logout", OnClick_Logout);
            f_RegClickEvent("Exit", OnClick_Exit);

            _textEnergy.text = "";*/
            _machineMgr = new ccMachineManager(new ccMachineStateBase(-1));
            _machineMgr.f_RegState(new MainState_MenuMain());
            _machineMgr.f_RegState(new MainState_Idle());
            _machineMgr.f_RegState(new MainState_Logout());

            _BtnWinA = f_GetObject("BtnWinA").GetComponent<Button>();
            _BtnWinB = f_GetObject("BtnWinB").GetComponent<Button>();
            _BtnLogout = f_GetObject("Logout").GetComponent<Button>();
            _InforText = f_GetObject("InforText").GetComponent<Text>();
            _ScoreText = f_GetObject("ScoreText").GetComponent<Text>();
            _BtnStart = f_GetObject("StartButton");
            _Panel2 = f_GetObject("Panel2");

            f_RegClickEvent(_BtnWinA.gameObject, OnClickBtn_Win, (int)EM_TeamID.TeamA);
            f_RegClickEvent(_BtnWinB.gameObject, OnClickBtn_Win, (int)EM_TeamID.TeamB);
            f_RegClickEvent(_BtnLogout.gameObject, OnClickBtn_Logout);
            f_RegClickEvent(f_GetObject("BtnStart"), OnClickBtn_Start);
        }

        protected override void On_Open(object e)
        {
            //f_OnShow();
            //_iTextTimer = ccTimeEvent.GetInstance().f_RegEvent(1.0f, true, null, (_) => f_UpdateText());
            f_MainUICtrl(0);
            _machineMgr.f_ChangeState((int)EM_MainState.Idle, this);
        }

        protected override void On_Hold(object e)
        {
            //_gobjLastSelected = EventSystem.current.currentSelectedGameObject;
        }

        protected override void On_UnHold(object e)
        {
            //f_OnShow();
        }

        protected override void On_Close()
        {
            //ccTimeEvent.GetInstance().f_UnRegEvent(_iTextTimer);
        }

#if UNITY_EDITOR
        protected override void On_Update()
        {
            //if (Input.GetKey(KeyCode.K))
            //{
            //    SocketCallbackDT tSocketCallbackDT = new SocketCallbackDT();
            //    tSocketCallbackDT.m_ccCallbackSuc = CallBackPlayGameSuc;
            //    tSocketCallbackDT.m_ccCallbackFail = CallBackPlayGameFail;
            //    Data_Pool.m_PlayerPool.f_PlayerGame((GirlDT)glo_Main.GetInstance().m_SC_Pool.m_GirlSC.f_GetSC(2), tSocketCallbackDT);
            //}
            _machineMgr.f_Update();
        }
#endif

        //void CallBackPlayGameSuc(object Obj)
        //{

        //}

        //void CallBackPlayGameFail(object Obj)
        //{

        //}

        protected override void On_UpdateGUI()
        {
        }

        protected override void On_Destory()
        {
        }

        public void f_MainUICtrl(int iSet)
        {
            if (iSet == 0)
            {//遊戲未開始
                _BtnStart.SetActive(true);
                _Panel2.SetActive(false);
                _InforText.text = "遊戲尚未開始";
                _ScoreText.text = "";
            }
            else if (iSet == 1)
            {//遊戲進行中
                _BtnStart.SetActive(false);
                _Panel2.SetActive(true);
            }
            else if (iSet == 2)
            {//按鈕可選擇
                _BtnWinA.interactable = true;
                _BtnWinB.interactable = true;
            }
            else if (iSet == 3)
            {//按鈕不可選擇
                _BtnWinA.interactable = false;
                _BtnWinB.interactable = false;
            }
        }

        public void f_Start()
        {//遊戲開始
            f_ReGame();
            _machineMgr.f_ChangeState((int)EM_MainState.MenuMain, this);
        }

        public void f_ReGame()
        {//重啟遊戲
            f_MainUICtrl(1);
            f_MainUICtrl(2);
            _iCurSelTeam = 0;
        }

        public void f_GameOver()
        {//遊戲結束
            f_MainUICtrl(0);
            _iCurSelTeam = 0;
            _machineMgr.f_ChangeState((int)EM_MainState.Idle, this);
        }

        public void f_UpdateText(int iSet, string strInfor)
        {//更新資訊文字
            if (iSet == 0)
            {
                _InforText.text = strInfor;
            }
            else if (iSet == 1)
            {
                _ScoreText.text = strInfor;
            }
        }

        private void OnClickBtn_Start(GameObject go, object obj1, object obj2)
        {
            CMsg_CTG_GuessCommand tCMsg_CTG_GuessCommand = new CMsg_CTG_GuessCommand();
            tCMsg_CTG_GuessCommand.m_iCheckState = (int)EM_GuessState.Start;
            glo_Main.GetInstance().m_GameSocket.f_SendBuf((int)SocketCommand.GamePlayCheck, tCMsg_CTG_GuessCommand);

            f_Start();
        }

        private void OnClickBtn_Win(GameObject go, object obj1, object obj2)
        {
            _iCurSelTeam = (int)obj1;
            f_MainUICtrl(3);
        }

        private void OnClickBtn_Logout(GameObject go, object obj1, object obj2)
        {
            _machineMgr.f_ChangeState((int)EM_MainState.Logout);
        }


        /*private void f_OnShow()
        {
            //_lstItems = glo_Main.GetInstance().m_lstItems;

            // XXX: Delay a frame to fix button highlight issue.
            /*ccTimeEvent.GetInstance().f_RegEvent(0.0f, false, null, (_) =>
            {
                // For controllers like keyboard or game pad.
                EventSystem.current.SetSelectedGameObject(_btnStoryMode.gameObject);
            });

            f_UpdateText();*
        }

        private void f_UpdateText()
        {


            //for (int i = 0; i < _lstItems.Count; i++)
            //{
            //    if (_lstItems[i].m_emItem == EM_Item.None)
            //        continue;

            //    displayText += "\n";

            //    if (_lstItems[i].m_bIsOneTimeUse)
            //    {
            //        displayText += $"\n{_lstItems[i].m_emItem}";
            //    }
            //    else
            //    {
            //        System.TimeSpan remain = _lstItems[i].m_dtTimeUpDate - System.DateTime.Now;
            //        if (remain > System.TimeSpan.Zero)
            //        {
            //            displayText += $"\n{_lstItems[i].m_emItem}: {remain.ToString(@"dd\.hh\:mm")}";
            //        }
            //        else
            //        {
            //            _lstItems[i].m_emItem = EM_Item.None;
            //        }
            //    }
            //}

        }

        private void OnClick_StoryMode(GameObject go, object obj1, object obj2)
        {
            ccUIHoldPool.GetInstance().f_Hold(this);
            ccUIManage.GetInstance().f_SendMsg(StrUI.StoryTeam, BaseUIMessageDef.UI_OPEN);
        }

        private void OnClick_ChallengeMode(GameObject go, object obj1, object obj2)
        {
            ccUIHoldPool.GetInstance().f_Hold(this);
            ccUIManage.GetInstance().f_SendMsg(StrUI.ChallengeTeam, BaseUIMessageDef.UI_OPEN);
        }

        private void OnClick_Shop(GameObject go, object obj1, object obj2)
        {
            // TODO
        }

        private void OnClick_Gallery(GameObject go, object obj1, object obj2)
        {
            // TODO
        }

        private void OnClick_Settings(GameObject go, object obj1, object obj2)
        {
            ccUIHoldPool.GetInstance().f_Hold(this);
            ccUIManage.GetInstance().f_SendMsg(StrUI.Settings, BaseUIMessageDef.UI_OPEN);
        }

        private void OnClick_Logout(GameObject go, object obj1, object obj2)
        { 
            GameDataLoad.f_SaveGameSystemMemory();

            var request = new CMsg_LoginUserReq()
            {
                m_szUserName = "",
                m_szUserPwd = ""
            };

            glo_Main.GetInstance().m_GameSocket.f_SendBuf((int)SocketCommand.CS_UserLogin, request);

            f_Close();
            ccUIManage.GetInstance().f_SendMsg(StrUI.Login, BaseUIMessageDef.UI_OPEN);
        }

        private void OnClick_Exit(GameObject go, object obj1, object obj2)
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }*/
    }
}