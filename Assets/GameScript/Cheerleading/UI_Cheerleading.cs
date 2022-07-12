using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ccU3DEngine;
using MR_Edit;
using System;

namespace GameLogic
{
    //新增休息時間
    //新增退出遊戲
    public class UI_Cheerleading : ccUILogicBase
    {
        //按鈕
        ///<summary> 遊戲按鈕介面 </summary>
        private GameObject aBtn;
        private GameObject bBtn;
        private GameObject gameBeginBtn; //開始遊戲按鈕
        private GameObject gameJoinBtn; //加入遊戲按鈕、需等待下回合
        //private GameObject settingBtn;
        private bool isPressBtn; //是否按下選擇
        private bool moraing; //是否正在猜拳
        private bool waitBegin;
        private bool watting; //是否是休息時間
        private bool isPressSelectBtn; //正在猜拳遊戲中
        private bool isARoomMaster; //是否為房主
        private bool waitForGame;
        private EM_TeamID btnString; //選擇了啥選項
        private EM_GuessGameMod guessGameMod;
        private float test;
        private Data_Pool data_Pool;
        //VS標誌
        private GameObject vsIcon;
        //球隊分數、LOGO
        private GameObject logo;
        private Image scoreMaskImage;
        private Text scoreText;
        private Text final;
        //倒數計時
        private Text timer_Seconds;
        private float gameTimer;
        private float gameResetTimer;
        //休息倒數計時
        private float wattingTimer;
        private float wattingResetTimer;

        //遊戲回傳資訊
        public EM_GuessState eM_GuessState;

        private GuessPool guessPool;

        protected override void On_Init()
        {
            MessageBox.DEBUG("啟動 UI_Cheerleading 腳本");
            guessPool = new GuessPool();

            #region 抓取物件
            aBtn = f_GetObject("A_Btn");
            bBtn = f_GetObject("B_Btn");
            gameBeginBtn = f_GetObject("GameBegin");
            gameJoinBtn = f_GetObject("JoinGame");
            //settingBtn = f_GetObject("SettingBtn");
            scoreMaskImage = f_GetObject("ScoreMaskImage").GetComponent<Image>();
            scoreText = f_GetObject("Score").GetComponent<Text>();
            vsIcon = f_GetObject("Vs_Image");
            timer_Seconds = f_GetObject("Second").GetComponent<Text>();
            final = f_GetObject("GetFinal").GetComponentInChildren<Text>();
            #endregion

            #region 按鈕設定
            f_RegClickEvent(f_GetObject("CheckGameState") , f_GameLoginOutCheck);
            //f_RegClickEvent(gameBeginBtn, f_GameStart);
            //f_RegClickEvent(settingBtn, f_EndGame); //測試結束遊戲按鈕
            //f_RegClickEvent(gameJoinBtn, f_JontGameControl);
            #endregion

            #region 設定陣營圖片
            if (StaticValue.m_iTeam.ToString() == "1")
            {
                GameTools.f_SetGameObject(f_GetObject("1_Team_Logo"), true);
                GameTools.f_SetGameObject(f_GetObject("2_Team_Logo"), false);
            }
            else if(StaticValue.m_iTeam.ToString() == "2")
            {
                GameTools.f_SetGameObject(f_GetObject("1_Team_Logo"), false);
                GameTools.f_SetGameObject(f_GetObject("2_Team_Logo"), true);
            }

            #endregion
            
            gameTimer = 10f;

            gameResetTimer = gameTimer;
            wattingTimer = 5f;
            wattingResetTimer = wattingTimer;          
        }

        private void f_Int()
        {
            guessPool.f_InitManager();

            //註冊是否選則啦啦隊
            glo_Main.GetInstance().m_UIMessagePool.f_AddListener(UIMessageDef.UI_SelectionCheerlead, SelectCheerlead);
            glo_Main.GetInstance().m_UIMessagePool.f_AddListener(UIMessageDef.StartGame, f_GameStart);
            glo_Main.GetInstance().m_UIMessagePool.f_AddListener(UIMessageDef.PlayerJionGame, f_JontGameControl);
        }

        protected override void On_Open(object e)
        {
            f_Int();
            //f_ResetGame();
            //MessageBox.DEBUG("確認遊戲狀態");
            GameTools.f_SetText(final, "遊戲狀態");
            Check_GameState();

            GameTools.f_SetGameObject(aBtn, false);
            GameTools.f_SetGameObject(bBtn, false);
        }

        private void f_JontGameControl(object obj /*GameObject go, object obj1, object obj2*/)
        {
            gameTimer = 1f;
            guessGameMod = EM_GuessGameMod.Playing;
            waitForGame = true;
            //watting = true;
            isPressBtn = true; //先倒數計時     
            isPressSelectBtn = false;
            GameTools.f_SetText(final, "等待中");
            GameTools.f_SetGameObject(gameJoinBtn, false);
            gameTimer = 100f;
            f_NotSelectCheerleadGameStart();
            #region UI物件開關           
            GameTools.f_SetGameObject(aBtn, false);
            GameTools.f_SetGameObject(bBtn, false);
            GameTools.f_SetGameObject(vsIcon, true);
            GameTools.f_SetGameObject(timer_Seconds.gameObject, true);
            #endregion
        }

        #region 確認遊戲狀態

        //確認遊戲狀態
        private void Check_GameState(/*GameObject go, object obj1, object obj2*/)
        {
            MessageBox.DEBUG("確認遊戲狀態");
            SocketCallbackDT tSocketCallbackDT = new SocketCallbackDT();
            tSocketCallbackDT.m_ccCallbackSuc = CallBack_GetGameSuc;
            tSocketCallbackDT.m_ccCallbackFail = CallBack_GetGameFail;

            guessPool.f_CheLead_CheckState(StaticValue.m_strAccount, StaticValue.m_lPlayerID, tSocketCallbackDT);
        }

        private void CallBack_GetGameSuc(object obj) //其他玩家按下開始遊戲時其他玩家會在觸發一次觸發
        {
            Debug.Log("確認遊戲狀態成功：" + (EM_GuessState)obj);
            eM_GuessState = (EM_GuessState)obj;

            if ((EM_GuessState)obj == EM_GuessState.NotGameIng)
            {
                GameTools.f_SetGameObject(gameBeginBtn, true);
                GameTools.f_SetGameObject(gameJoinBtn, false);
            }
            else if((EM_GuessState)obj == EM_GuessState.GameIng)
            {
                GameTools.f_SetGameObject(gameBeginBtn, false);
                GameTools.f_SetGameObject(gameJoinBtn, true);
            }
        }

        private void CallBack_GetGameFail(object obj)
        {
            eMsgOperateResult tt = (eMsgOperateResult)obj;
            MessageBox.DEBUG("確認遊戲狀態失敗：" + tt.ToString());
        }

        #endregion

        #region 開啟遊戲(按下遊戲者成為房主，並控制遊戲)
        private void f_GameStart(object obj)
        {         
            SocketCallbackDT tSocketCallbackDT = new SocketCallbackDT();
            tSocketCallbackDT.m_ccCallbackSuc = CallBack_GetGameStartSuc;
            tSocketCallbackDT.m_ccCallbackFail = CallBack_GetGameStartFail;

            guessPool.f_CheLead_Start(StaticValue.m_strAccount, StaticValue.m_lPlayerID, tSocketCallbackDT);
        } //按下遊戲並成為房主

        private void CallBack_GetGameStartSuc(object obj)
        {
            MessageBox.DEBUG("遊戲開始 : " + (EM_GuessState)obj);

            eM_GuessState = (EM_GuessState)obj;
            isARoomMaster = true;
            //f_CommandSuc(obj);
            guessGameMod = EM_GuessGameMod.Playing;
        }

        private void CallBack_GetGameStartFail(object obj)
        {
            //MessageBox.DEBUG("遊戲開始失敗" + (EM_GuessState)obj);
            eMsgOperateResult tt = (eMsgOperateResult)obj;
            MessageBox.DEBUG(tt.ToString());
            //f_ResetGame();//初始化遊戲
        }

        private void f_NotSelectCheerleadGameStart() //還沒選擇啦啦隊時的開始
        {         
            SocketCallbackDT tSocketCallbackDT = new SocketCallbackDT();
            tSocketCallbackDT.m_ccCallbackSuc = CallBack_GetGameCallGuessSuc;
            tSocketCallbackDT.m_ccCallbackFail = CallBack_GetGameCallGuessFail;

            guessPool.f_CheLead_CallGuess(StaticValue.m_strAccount, StaticValue.m_lPlayerID, tSocketCallbackDT);
        }

        private void CallBack_GetGameCallGuessSuc(object obj)
        {
            //MessageBox.DEBUG("玩家進行猜測 : " + (EM_GuessState)obj);
            eM_GuessState = (EM_GuessState)obj;
            
            
           //f_CommandSuc(obj);
        }

        private void CallBack_GetGameCallGuessFail(object obj)
        {
            MessageBox.DEBUG("玩家無法進行猜測" +(EM_GuessState)obj);

            eMsgOperateResult tt = (eMsgOperateResult)obj;
            //MessageBox.DEBUG(tt.ToString());
        }
        #endregion

        #region 遊戲流程
        //已選擇好選項，等待結果
        private void SelectCheerlead(object obj)
        {
            //Debug.Log("以選擇好選項");
            //Debug.Log((EM_TeamID)obj);
            btnString = (EM_TeamID)obj; //獲得所選擇的啦啦隊
            isPressSelectBtn = true;
            //MessageBox.DEBUG("遊戲狀態 : " + (EM_GuessState)obj);
            #region 物件開關
            GameTools.f_SetGameObject(aBtn, false);
            GameTools.f_SetGameObject(bBtn, false);
            GameTools.f_SetGameObject(vsIcon, false);
            GameTools.f_SetGameObject(timer_Seconds.gameObject, true);
            GameTools.f_SetGameObject(gameBeginBtn, false);
            #endregion

            guessPool.f_CheLead_Guess(StaticValue.m_strAccount, StaticValue.m_lPlayerID, (int)btnString);        
        }

        protected override void On_Update()
        {
            Debug.Log(eM_GuessState);
            f_CommandSuc(eM_GuessState);

            if (gameTimer > 0 && isPressBtn && !watting) //是否按下遊戲開始
                CountdownTimer(); //倒數計時
            else if (gameTimer <= 0 && !moraing)
            {
                moraing = true;
                glo_Main.GetInstance().m_UIMessagePool.f_Broadcast(UIMessageDef.UI_CheerleadMoraGame, CheerleadStateClass.Mora);
                waitBegin = true;
            }
            else if(waitBegin)
            {
                watting = true;
                gameTimer = gameResetTimer;
                f_GetFinal();
            }
            else if (watting)
            {
                CowndownToWaitting();
            }
        }
        protected override void On_UpdateGUI()
        {

        }

        private void f_CommandSuc(object obj)
        {
            int iCall = (int)obj;
            if (iCall == (int)EM_GuessState.CallStart)
            {
                if(!waitForGame)
                {
                    guessGameMod = EM_GuessGameMod.Playing;
                    MessageBox.DEBUG("遊戲開始");
                    isPressBtn = true; //遊戲開始 ， 開始計時
                    f_NotSelectCheerleadGameStart(); //進入選擇啦啦隊環節
                }               
            }
            else if (iCall == (int)EM_GuessState.CallRoomMaster)
            {
                if (!waitForGame)
                {
                    isARoomMaster = true;
                }                   
            }
            else if (iCall == (int)EM_GuessState.CallGuess && !isPressSelectBtn)
            {
                if (!waitForGame)
                {
                    #region UI物件開關           
                    GameTools.f_SetGameObject(aBtn, true);
                    GameTools.f_SetGameObject(bBtn, true);
                    GameTools.f_SetGameObject(vsIcon, false);
                    GameTools.f_SetGameObject(timer_Seconds.gameObject, true);
                    GameTools.f_SetGameObject(gameBeginBtn, false);
                    #endregion
                }
            }
            else if (iCall == (int)EM_GuessState.CallRestart)
            {
                //MessageBox.DEBUG(StaticValue.m_strAccount + " 重新啟動");

                if(waitForGame)
                {
                    MessageBox.DEBUG("有玩家正在等待中");
                    wattingTimer = wattingResetTimer;
                    gameTimer = gameResetTimer;
                    //eM_GuessState = EM_GuessState.CallGuess;
                    waitForGame = false;
                }
                else
                {
                    wattingTimer = wattingResetTimer;
                    gameTimer = gameResetTimer;
                    btnString = EM_TeamID.None;
                    isPressSelectBtn = false;
                    f_NotSelectCheerleadGameStart();
                    waitForGame = false;
                }              
            }        
            else if (iCall == (int)EM_GuessState.CallEnd)
            {
                f_ResetGameToInt();
            }
            else if(iCall == (int)EM_GuessState.None)//代表進入休息倒數
            {
                #region UI物件開關 
                GameTools.f_SetGameObject(aBtn, false);
                GameTools.f_SetGameObject(bBtn, false);
                #endregion
            }
        }

        #region 倒數時間
        private void CountdownTimer()
        {
            if(guessGameMod == EM_GuessGameMod.Playing)
                GameTools.f_SetText(final, "進行中..");

            gameTimer -= Time.deltaTime;

            timer_Seconds.text = Mathf.RoundToInt(gameTimer).ToString();
        } //遊戲時間

        private void CowndownToWaitting() //休息時間
        {
            wattingTimer -= Time.deltaTime;

            if (watting)
            {
                //GameTools.f_SetText(final, "休息中..");
                if (wattingTimer <= 0)
                {                   
                    watting = false;
                    moraing = false;
                    //isGuessBegin = false;
                    Debug.Log("休息中");
                    wattingTimer = wattingResetTimer;
                    f_ReturnGame(); //CowndownToWaitting
                }
            }

            timer_Seconds.text = Mathf.RoundToInt(wattingTimer).ToString();
        }

        //確認最終結果

        #endregion

        #region 保存分數
        private void f_GetFinal()
        {
            SocketCallbackDT tSocketCallbackDT = new SocketCallbackDT();
            tSocketCallbackDT.m_ccCallbackSuc = CallBack_GetScoreSuc;
            tSocketCallbackDT.m_ccCallbackFail = CallBack_GetScoreFail;

            guessPool.f_CheLead_GetScore(StaticValue.m_strAccount, StaticValue.m_lPlayerID, (int)btnString, tSocketCallbackDT);

            //GameTools.f_SetGameObject(f_GetObject("1_Team_Logo"), true);
        }

        private void CallBack_GetScoreSuc(object obj)
        {
            //MessageBox.DEBUG("遊戲狀態 : " + (EM_GuessState)obj);
            //MessageBox.DEBUG("分數保存成功 : " + obj.ToString());

            eM_GuessState = (EM_GuessState)obj;
           
            EM_GuessResult tt = (EM_GuessResult)guessPool.f_GetWin();

            if(guessGameMod == EM_GuessGameMod.Playing)
                GameTools.f_SetText(final, tt.ToString());

            //Debug.Log((EM_GuessResult)guessPool.f_GetWin());
            
            if((EM_GuessResult)guessPool.f_GetWin() == EM_GuessResult.Win)
            {
                GameTools.f_SetText(scoreText, guessPool.f_GetScore(StaticValue.m_iTeam).ToString());
                //scoreText.text = guessPool.f_GetScore(StaticValue.m_iTeam).ToString();
                SetScoreMaskImage(guessPool.f_GetScore(StaticValue.m_iTeam) / 10);
            }

            waitBegin = false;
            gameTimer = gameResetTimer;
        }

        private void CallBack_GetScoreFail(object obj)
        {
            MessageBox.DEBUG("分數保存失敗" + (EM_GuessState)obj);
            eMsgOperateResult tt = (eMsgOperateResult)obj;
            MessageBox.DEBUG(tt.ToString());
            //f_ReturnGame();//初始化遊戲
        }

        #endregion     
        
        #region 遊戲重新一輪
        private void f_ReturnGame()
        {
            SocketCallbackDT tSocketCallbackDT = new SocketCallbackDT();
            tSocketCallbackDT.m_ccCallbackSuc = f_GetGameRestartSuc;
            tSocketCallbackDT.m_ccCallbackFail = f_GetGameRestartFail;

            guessPool.f_CheLead_Restart(StaticValue.m_strAccount, StaticValue.m_lPlayerID, tSocketCallbackDT);
            
        }

        private void f_GetGameRestartSuc(object obj)
        {
            MessageBox.DEBUG("遊戲重新開始");
            eM_GuessState = (EM_GuessState)obj;
            gameTimer = gameResetTimer;
            moraing = false;
        }

        private void f_GetGameRestartFail(object obj)
        {
            MessageBox.DEBUG("重新失敗" +(EM_GuessState)obj);
            //f_Close();
        }

        #endregion

        private void SetScoreMaskImage(float h) //分數進度條
        {
            Debug.Log(h);

            scoreMaskImage.GetComponent<RectTransform>().sizeDelta = new Vector2(scoreMaskImage.GetComponent<RectTransform>().sizeDelta.x,
                                                                        scoreMaskImage.GetComponent<RectTransform>().sizeDelta.y + h);
        }

        #region 退出遊戲
        private void f_EndGame() //當房主退掉，回到有開始遊戲的畫面，重新選擇房主
        {
            SocketCallbackDT tSocketCallbackDT = new SocketCallbackDT();
            tSocketCallbackDT.m_ccCallbackSuc = CallBack_EndGameSuc;
            tSocketCallbackDT.m_ccCallbackFail = CallBack_EndGameFail;

            guessPool.f_CheLead_End(StaticValue.m_strAccount, StaticValue.m_lPlayerID, tSocketCallbackDT);
        }

        private void CallBack_EndGameSuc(object obj)
        {
            Debug.Log("關閉遊戲成功" +(EM_GuessState)obj);
            eM_GuessState = (EM_GuessState)obj;
        }

        private void CallBack_EndGameFail(object obj)
        {
            Debug.Log("關閉遊戲失敗" + (EM_GuessState)obj);

            eMsgOperateResult tt = (eMsgOperateResult)obj;
            MessageBox.DEBUG(tt.ToString());
        }

        private void f_ResetGameToInt()
        {
            gameTimer = gameResetTimer;
            wattingTimer = wattingResetTimer;
            isPressBtn = false;
            moraing = false;
            GameTools.f_SetGameObject(aBtn, false);
            GameTools.f_SetGameObject(bBtn, false);
            GameTools.f_SetGameObject(vsIcon, true);
            GameTools.f_SetGameObject(timer_Seconds.gameObject, false);
            GameTools.f_SetGameObject(gameBeginBtn, true);

            GameTools.f_SetText(final, "沒有房主");

            if(isARoomMaster)
            {
                guessPool.f_DisManager();
                isARoomMaster = false;
            }
        }
        #endregion
        #endregion

        #region 房主退出後..
        //當房主退出遊戲，遊戲回到最初的樣子，等待有人成為房主

        private void f_GameLoginOutCheck(GameObject go, object obj1, object obj2)
        {
            //ccUIManage.GetInstance().f_SendMsgV3("ui_gamemain.bundle", "UI_Cheerleading_new", UIMessageDef.UI_CLOSE);
            ccUIManage.GetInstance().f_SendMsgV3("ui_gamemain.bundle", "UI_Cheerleading_MR", UIMessageDef.UI_CLOSE);
        }

        protected override void On_Close()
        {
            if(isARoomMaster)
                f_EndGame();
            else
                guessPool.f_DisManager();
        }

        protected override void On_Destory() 
        {
            if (isARoomMaster)
                f_EndGame();
            else
                guessPool.f_DisManager();
        }
        #endregion
    }
}

