using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ccU3DEngine;
using MR_Edit;
using System;

namespace GameLogic
{
    public class UI_Cheerleading : ccUILogicBase
    {
        //按鈕
        ///<summary> 遊戲按鈕介面 </summary>
        private GameObject aBtn;
        private GameObject bBtn;
        private GameObject gameBeginBtn;
        private GameObject settingBtn;
        private bool isPressBtn; //是否按下選擇
        private bool moraing; //是否正在猜拳
        private EM_TeamID btnString; //選擇了啥選項
        private CheerleadStateClass cheerleadState;
        //VS標誌
        private GameObject vsIcon;
        //球隊分數、LOGO
        private GameObject logo;
        private Image scoreMaskImage;
        private Text scoreText;
        //倒數計時
        private Text timer_Seconds;
        public float timer;
        private float resetTimer;
        private Animator GetFinalAnim;
        //private GameObject checkBTN;
        private GuessPool guessPool;

        protected override void On_Init()
        {
            #region 等有功能再用
            MessageBox.DEBUG("啟動 UI_Cheerleading 腳本");

            //按鈕註冊
            //ccUIEventListener.Get(_BtnMenuExit.gameObject).f_RegClick(CallBack_BtnMenuExitClick, null, null, "");
            //註冊UI_ShopBuyBtn 如果被呼叫就執行 CallBack_OnClick_BtnBuy
            // glo_Main.GetInstance().m_UIMessagePool.f_AddListener(UIMessageDef.UI_ShopBuyBtn, CallBack_OnClick_BtnBuy);

            guessPool = new GuessPool();

            aBtn = f_GetObject("A_Btn");
            bBtn = f_GetObject("B_Btn");
            gameBeginBtn = f_GetObject("GameBegin");
            settingBtn = f_GetObject("SettingBtn");
            scoreMaskImage = f_GetObject("ScoreMaskImage").GetComponent<Image>();
            scoreText = f_GetObject("Score").GetComponent<Text>();
            vsIcon = f_GetObject("Vs_Image");
            timer_Seconds = f_GetObject("Second").GetComponent<Text>();
            f_RegClickEvent(f_GetObject("CheckGameState") , Check_GameState);
            f_RegClickEvent(gameBeginBtn, f_GameStart);

            if(StaticValue.m_iTeam.ToString() == "1")
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

            f_RegClickEvent(settingBtn, f_SettingControl);

            timer = 10f;
            resetTimer = timer;
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
            MessageBox.DEBUG("等待其他玩家進入....");
           
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

            }
        }

        private void CallBack_GetGameFail(object obj)
        {
            MessageBox.DEBUG("F");
            MessageBox.DEBUG(obj.ToString());
        }

        #endregion

        #region 開啟遊戲(房主控制)
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

            //f_ResetGame();//初始化遊戲
        }

        private void f_NotSelectCheerleadGameStart()
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
        } //還沒選擇啦啦隊時的開始
        #endregion

        #region 遊戲開始

        private void CallBack_GetGameCallGuessSuc(object obj)
        {
            MessageBox.DEBUG("玩家進行猜測 : " + (EM_GuessState)obj);

            
            //MessageBox.DEBUG("結果 :  " + guessPool.f_GetWin().ToString());
           
        }

        private void CallBack_GetGameCallGuessFail(object obj)
        {
            MessageBox.DEBUG("玩家無法進行猜測");
        }
        #endregion
   
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

        //確認最終結果
        private void f_GetFinal()
        {
            SocketCallbackDT tSocketCallbackDT = new SocketCallbackDT();
            tSocketCallbackDT.m_ccCallbackSuc = CallBack_GetScoreSuc;
            tSocketCallbackDT.m_ccCallbackFail = CallBack_GetScoreFail;

            guessPool.f_CheLead_GetScore(StaticValue.m_strAccount, StaticValue.m_lPlayerID, (int)btnString, tSocketCallbackDT);
            f_ResetGame();

            GameTools.f_SetGameObject(f_GetObject("1_Team_Logo"), true);
            //GetFinalAnim = f_GetObject("GetFinal").GetComponent<Animator>();
            
            //GameTools.f_PlayAnimator(GetFinalAnim, "FaileIn");
        }

        #region 保存分數

        private void CallBack_GetScoreSuc(object obj)
        {
            MessageBox.DEBUG("分數保存成功 : " + obj.ToString());
            EM_GuessResult tt = (EM_GuessResult)guessPool.f_GetWin();
            f_GetObject("GetFinal").GetComponentInChildren<Text>().text = tt.ToString();
            Debug.Log((EM_GuessResult)guessPool.f_GetWin());
            
            if((EM_GuessResult)guessPool.f_GetWin() == EM_GuessResult.Win)
            {
                scoreText.text = guessPool.f_GetScore(StaticValue.m_iTeam).ToString();
                SetScoreMaskImage(guessPool.f_GetScore(StaticValue.m_iTeam));
            }
        }

        private void CallBack_GetScoreFail(object obj)
        {
            MessageBox.DEBUG("分數保存失敗");

            f_ResetGame();//初始化遊戲
        }

        #endregion


        protected override void On_Update()
        {
            //MessageBox.DEBUG("執行中" + timer + " " + isPressBtn);

            if (timer > 0 && isPressBtn) //是否按下遊戲開始
                CountdownTimer(); //倒數計時
            else if(timer <= 0 && !moraing)
            {
                moraing = true;
                glo_Main.GetInstance().m_UIMessagePool.f_Broadcast(UIMessageDef.UI_CheerleadMoraGame, CheerleadStateClass.Mora);
                f_GetFinal();
            }
        }

        protected override void On_UpdateGUI()
        {

        }

        private void CountdownTimer()
        {
            timer -= Time.deltaTime;

            timer_Seconds.text = Mathf.RoundToInt(timer).ToString();
        }

        private void f_ResetGame()
        {
            MessageBox.DEBUG("重新一局");

            timer = resetTimer;
            isPressBtn = false;
            moraing = false;
            GameTools.f_SetGameObject(aBtn, true);
            GameTools.f_SetGameObject(bBtn, true);
            GameTools.f_SetGameObject(vsIcon, false);
            GameTools.f_SetGameObject(timer_Seconds.gameObject, true);
            GameTools.f_SetGameObject(gameBeginBtn, false);

            SocketCallbackDT tSocketCallbackDT = new SocketCallbackDT();
            tSocketCallbackDT.m_ccCallbackSuc = f_GetGameRestartSuc;
            tSocketCallbackDT.m_ccCallbackFail = f_GetGameRestartFail;

            guessPool.f_CheLead_Restart(StaticValue.m_strAccount, StaticValue.m_lPlayerID, tSocketCallbackDT);
            isPressBtn = true;
        }

        private void f_GetGameRestartSuc(object obj)
        {
            MessageBox.DEBUG("重新成功");//重猜測開始
            //f_Close();
        }

        private void f_GetGameRestartFail(object obj)
        {
            MessageBox.DEBUG("重新失敗");
            //f_Close();
        }

        private void SetScoreMaskImage(float h) //分數進度條
        {
            Debug.Log(h);

            scoreMaskImage.GetComponent<RectTransform>().sizeDelta = new Vector2(scoreMaskImage.GetComponent<RectTransform>().sizeDelta.x ,
                                                                        scoreMaskImage.GetComponent<RectTransform>().sizeDelta.y + h);

        }

       
        protected override void On_Close()
        {
            guessPool.f_DisManager();
        }

        protected override void On_Destory()
        {
            guessPool.f_DisManager();
        }

    }
}

