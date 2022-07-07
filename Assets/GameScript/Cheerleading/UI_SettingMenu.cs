using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ccU3DEngine;
using System;

namespace GameLogic
{
    public class UI_SettingMenu : ccUILogicBase
    {
        private GameObject RestartBtn;
        private GameObject QuitBtn;
        private GameObject QuitMenu;
        private GuessPool guessPool;

        protected override void On_Init()
        {
            MessageBox.DEBUG("啟動 UI_SettingMenu 腳本");
            guessPool = new GuessPool();

            //抓取物件
            RestartBtn = f_GetObject("RestartBtn");
            QuitBtn = f_GetObject("QuitBtn");
            QuitMenu = f_GetObject("QuitMenuBtn");

            //註冊事件
            f_RegClickEvent(RestartBtn, f_RestartGame);
            f_RegClickEvent(QuitBtn, f_QuitGame);
            f_RegClickEvent(QuitMenu, f_QuitMenu);
        }

        #region 重新開始遊戲事件
        private void f_RestartGame(GameObject go, object obj1, object obj2)
        {
            SocketCallbackDT tSocketCallbackDT = new SocketCallbackDT();
            tSocketCallbackDT.m_ccCallbackSuc = f_GetGameRestartSuc;
            tSocketCallbackDT.m_ccCallbackFail = f_GetGameRestartFail;

            guessPool.f_CheLead_Restart(StaticValue.m_strAccount, StaticValue.m_lPlayerID, tSocketCallbackDT);
        }

        private void f_GetGameRestartSuc(object obj)
        {
            MessageBox.DEBUG("重新成功");//重猜測開始
            f_Close();
        }

        private void f_GetGameRestartFail(object obj)
        {
            MessageBox.DEBUG("重新失敗");
            f_Close();
        }

        #endregion

        private void f_QuitGame(GameObject go, object obj1, object obj2)
        {

        }

        private void f_QuitMenu(GameObject go, object obj1, object obj2)
        {
            f_Close();
        }

        protected override void On_Open(object e)
        {

        }

        protected override void On_UpdateGUI()
        {

        }

        protected override void On_Close()
        {

        }

        protected override void On_Destory()
        {

        }
    }
}


