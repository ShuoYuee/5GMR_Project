using ccU3DEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

namespace GameLogic
{
    public class UI_GameSet : ccUILogicBase
    {

        //PowerIndicator _PowerIndicator;
        protected override void On_Init()
        {
            MessageBox.DEBUG("啟用遊戲包中的UI_GameSet腳本");

            f_RegClickEvent(f_GetObject("BtnExit"), OnClick_BtnExit);
        }

        protected override void On_Open(object e)
        {

        }


        protected override void On_Close()
        {

        }

        protected override void On_Update()
        {
            base.On_Update();

        }

        protected override void On_UpdateGUI()
        {

        }


        void OnClick_BtnExit(GameObject go, object obj1, object obj2)
        {
            ccUIHoldPool.GetInstance().f_UnHold();
            f_Close();
        }

        private void DoExit()
        {

        }

        protected override void On_Destory()
        {

        }



    }
}
