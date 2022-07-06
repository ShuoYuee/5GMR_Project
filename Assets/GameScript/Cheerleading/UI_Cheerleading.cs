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
        private GameObject settingBtn;
        private bool isPressBtn; //是否按下選擇
        private bool moraing; //是否正在猜拳
        private bool waitBegin;
        private bool watting; //是否是休息時間
        private bool isGameing; //正在遊戲中
        private EM_TeamID btnString; //選擇了啥選項
        private EM_GuessGameMod guessGameMod;
        private float test;
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

        private Animator GetFinalAnim;
        //private GameObject checkBTN;
        private GuessPool guessPool;

        protected override void On_Init()
        {
            MessageBox.DEBUG("啟動 UI_Cheerleading 腳本");

            //按鈕註冊
            //ccUIEventListener.Get(_BtnMenuExit.gameObject).f_RegClick(CallBack_BtnMenuExitClick, null, null, "");
            //註冊UI_ShopBuyBtn 如果被呼叫就執行 CallBack_OnClick_BtnBuy
            // glo_Main.GetInstance().m_UIMessagePool.f_AddListener(UIMessageDef.UI_ShopBuyBtn, CallBack_OnClick_BtnBuy);

            guessPool = new GuessPool();

            #region 抓取物件
            aBtn = f_GetObject("A_Btn");
            bBtn = f_GetObject("B_Btn");
            gameBeginBtn = f_GetObject("GameBegin");
            gameJoinBtn = f_GetObject("JoinGame");
            settingBtn = f_GetObject("SettingBtn");
            scoreMaskImage = f_GetObject("ScoreMaskImage").GetComponent<Image>();
            scoreText = f_GetObject("Score").GetComponent<Text>();
            vsIcon = f_GetObject("Vs_Image");
            timer_Seconds = f_GetObject("Second").GetComponent<Text>();
            final = f_GetObject("GetFinal").GetComponentInChildren<Text>();
            #endregion

            #region 按鈕設定
            f_RegClickEvent(f_GetObject("CheckGameState") , Check_GameState);
            f_RegClickEvent(gameBeginBtn, f_GameStart);
            f_RegClickEvent(settingBtn, f_EndGame);
            f_RegClickEvent(gameJoinBtn, f_JontGameControl);
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
            f_Int();
        }

        private void f_SettingControl(GameObject go, object obj1, object obj2)
        {
            ccUIManage.GetInstance().f_SendMsgV3("ui_gameset.bundle", "UI_SettingMenu", UIMessageDef.UI_OPEN);
        }   

        private void f_Int()
        {
            guessPool.f_InitManager();

            //註冊是否選則啦啦隊
            glo_Main.GetInstance().m_UIMessagePool.f_AddListener(UIMessageDef.UI_SelectionCheerlead, GameStart);
            
        }

        protected override void On_Open(object e)
        {
            //f_ResetGame();
            MessageBox.DEBUG("確認遊戲狀態");
            //Check_GameState();
        }

        private void f_JontGameControl(GameObject go, object obj1, object obj2)
        {
            guessGameMod = EM_GuessGameMod.Waitting;
           
            GameTools.f_SetText(final, "等待中");

            GameTools.f_SetGameObject(aBtn, false);
            GameTools.f_SetGameObject(bBtn, false);
            GameTools.f_SetGameObject(vsIcon, false);
            GameTools.f_SetGameObject(timer_Seconds.gameObject, false);
            GameTools.f_SetGameObject(gameBeginBtn, false);
            GameTools.f_SetGameObject(gameJoinBtn, false);
        }

        #region 確認遊戲狀態

        //確認遊戲狀態
        private void Check_GameState(GameObject go, object obj1, object obj2)
        {
            MessageBox.DEBUG("確認遊戲狀態");
            SocketCallbackDT tSocketCallbackDT = new SocketCallbackDT();
            tSocketCallbackDT.m_ccCallbackSuc = CallBack_GetGameSuc;
            tSocketCallbackDT.m_ccCallbackFail = CallBack_GetGameFail;

            guessPool.f_CheLead_CheckState(StaticValue.m_strAccount, StaticValue.m_lPlayerID, tSocketCallbackDT);
        }

        private void CallBack_GetGameSuc(object obj)
        {
            Debug.Log((EM_GuessState)obj);      
            
            if((EM_GuessState)obj == EM_GuessState.NotGameIng)
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
            MessageBox.DEBUG(tt.ToString());
        }

        #endregion

        #region 開啟遊戲(按下遊戲者成為房主，並控制遊戲)
        private void f_GameStart(GameObject go, object obj1, object obj2)
        {
            MessageBox.DEBUG("確認遊戲開始");
            SocketCallbackDT tSocketCallbackDT = new SocketCallbackDT();
            tSocketCallbackDT.m_ccCallbackSuc = CallBack_GetGameStartSuc;
            tSocketCallbackDT.m_ccCallbackFail = CallBack_GetGameStartFail;

            guessPool.f_CheLead_Start(StaticValue.m_strAccount, StaticValue.m_lPlayerID, tSocketCallbackDT);
        }

        private void CallBack_GetGameStartSuc(object obj)
        {
            MessageBox.DEBUG("遊戲開始 : " + (EM_GuessState)obj);

            f_NotSelectCheerleadGameStart();
        }

        private void CallBack_GetGameStartFail(object obj)
        {
            MessageBox.DEBUG("遊戲開始失敗");
            eMsgOperateResult tt = (eMsgOperateResult)obj;
            MessageBox.DEBUG(tt.ToString());
            //f_ResetGame();//初始化遊戲
        }

        private void f_NotSelectCheerleadGameStart() //還沒選擇啦啦隊時的開始
        {
            isPressBtn = true; //遊戲開始          
            #region 物件開關
            GameTools.f_SetGameObject(aBtn, true);
            GameTools.f_SetGameObject(bBtn, true);
            GameTools.f_SetGameObject(vsIcon, false);
            GameTools.f_SetGameObject(timer_Seconds.gameObject, true);
            GameTools.f_SetGameObject(gameBeginBtn, false);
            #endregion

            SocketCallbackDT tSocketCallbackDT = new SocketCallbackDT();
            tSocketCallbackDT.m_ccCallbackSuc = CallBack_GetGameCallGuessSuc;
            tSocketCallbackDT.m_ccCallbackFail = CallBack_GetGameCallGuessFail;

            guessPool.f_CheLead_CallGuess(StaticValue.m_strAccount, StaticValue.m_lPlayerID, tSocketCallbackDT);
        }

        private void CallBack_GetGameCallGuessSuc(object obj)
        {
            MessageBox.DEBUG("玩家進行猜測 : " + (EM_GuessState)obj);

            //MessageBox.DEBUG("結果 :  " + guessPool.f_GetWin().ToString());
        }

        private void CallBack_GetGameCallGuessFail(object obj)
        {
            MessageBox.DEBUG("玩家無法進行猜測");

            eMsgOperateResult tt = (eMsgOperateResult)obj;
            MessageBox.DEBUG(tt.ToString());
        }
        #endregion

        #region 遊戲流程
        //已選擇好選項，等待結果
        private void GameStart(object Obj)
        {
            Debug.Log("以選擇好選項");
            Debug.Log((EM_TeamID)Obj);
            btnString = (EM_TeamID)Obj;           

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
            //MessageBox.DEBUG("執行中" + timer + " " + isPressBtn);

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
                f_GetFinal();
            }
            else if (watting)
            {               
                CowndownToWaitting();
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
            MessageBox.DEBUG("分數保存成功 : " + obj.ToString());
            EM_GuessResult tt = (EM_GuessResult)guessPool.f_GetWin();
            if(guessGameMod == EM_GuessGameMod.Playing)
                GameTools.f_SetText(final, tt.ToString());
            //Debug.Log((EM_GuessResult)guessPool.f_GetWin());
            
            if((EM_GuessResult)guessPool.f_GetWin() == EM_GuessResult.Win)
            {
                GameTools.f_SetText(scoreText, guessPool.f_GetScore(StaticValue.m_iTeam).ToString());
                //scoreText.text = guessPool.f_GetScore(StaticValue.m_iTeam).ToString();
                SetScoreMaskImage(guessPool.f_GetScore(StaticValue.m_iTeam));
            }

            waitBegin = false;
            gameTimer = gameResetTimer;
        }

        private void CallBack_GetScoreFail(object obj)
        {
            MessageBox.DEBUG("分數保存失敗");
            eMsgOperateResult tt = (eMsgOperateResult)obj;
            MessageBox.DEBUG(tt.ToString());
            //f_ReturnGame();//初始化遊戲
        }

        #endregion     

        protected override void On_UpdateGUI()
        {

        }       

        #region 遊戲重新一輪
        private void f_ReturnGame()
        {
            MessageBox.DEBUG("重新一局");

            SocketCallbackDT tSocketCallbackDT = new SocketCallbackDT();
            tSocketCallbackDT.m_ccCallbackSuc = f_GetGameRestartSuc;
            tSocketCallbackDT.m_ccCallbackFail = f_GetGameRestartFail;

            guessPool.f_CheLead_Restart(StaticValue.m_strAccount, StaticValue.m_lPlayerID, tSocketCallbackDT);
            
        }

        private void f_GetGameRestartSuc(object obj)
        {
            MessageBox.DEBUG("重新成功");//重猜測開始
            guessGameMod = EM_GuessGameMod.Playing;
            gameTimer = gameResetTimer;
            moraing = false;
            GameTools.f_SetGameObject(aBtn, true);
            GameTools.f_SetGameObject(bBtn, true);
            GameTools.f_SetGameObject(vsIcon, false);
            GameTools.f_SetGameObject(timer_Seconds.gameObject, true);
            GameTools.f_SetGameObject(gameBeginBtn, false);
            isPressBtn = true;
            //f_Close();
        }

        private void f_GetGameRestartFail(object obj)
        {
            MessageBox.DEBUG("重新失敗");
            //f_Close();
        }

        #endregion

        #region 退出遊戲
        private void f_EndGame(GameObject go, object obj1, object obj2) //當房主退掉，回到有開始遊戲的畫面，重新選擇房主
        {
            SocketCallbackDT tSocketCallbackDT = new SocketCallbackDT();
            tSocketCallbackDT.m_ccCallbackSuc = CallBack_EndGameSuc;
            tSocketCallbackDT.m_ccCallbackFail = CallBack_EndGameFail;

            guessPool.f_CheLead_End(StaticValue.m_strAccount, StaticValue.m_lPlayerID, tSocketCallbackDT);
        }

        private void CallBack_EndGameSuc(object obj)
        {
            Debug.Log("關閉遊戲成功");
            f_ResetGameToInt();
        }

        private void CallBack_EndGameFail(object obj)
        {
            Debug.Log("關閉遊戲失敗");

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
        }
        #endregion

       
        private void SetScoreMaskImage(float h) //分數進度條
        {
            Debug.Log(h);

            scoreMaskImage.GetComponent<RectTransform>().sizeDelta = new Vector2(scoreMaskImage.GetComponent<RectTransform>().sizeDelta.x ,
                                                                        scoreMaskImage.GetComponent<RectTransform>().sizeDelta.y + h);
        }

        #endregion

        #region 房主退出後..
        //當房主退出遊戲，遊戲回到最初的樣子，等待有人成為房主
        protected override void On_Close()
        {
            guessPool.f_DisManager();
        }

        protected override void On_Destory() 
        {
            guessPool.f_DisManager();

            //f_EndGame();
        }
        #endregion
    }
}

