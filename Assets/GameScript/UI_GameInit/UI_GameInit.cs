using ccU3DEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

namespace GameLogic
{
    public class UI_GameInit : MonoBehaviour
    {
        public Slider m_Progress;
        private float text;

        private void Start()
        {
            MessageBox.DEBUG("啟用遊戲包中的UI_GameInit腳本");

            m_Progress.value = 0;
            glo_Main.GetInstance().m_UIMessagePool.f_AddListener(UIMessageDef.UI_UpdateInitProgress, On_UI_UpdateInitProgress);
        }


        private void On_UI_UpdateInitProgress(object Obj)
        {
            m_Progress.value = (float)Obj;
        }

        private void OnDestroy()
        {
            glo_Main.GetInstance().m_UIMessagePool.f_RemoveListener(UIMessageDef.UI_UpdateInitProgress, On_UI_UpdateInitProgress);
            ccUIManage.GetInstance().f_SendMsgV3("ui_gamemain.bundle", "UI_Cheerleading_MR", UIMessageDef.UI_OPEN);
            //ccUIManage.GetInstance().f_SendMsgV3("ui_gamemain.bundle", "UI_Cheerleading_new", UIMessageDef.UI_OPEN);
            //ccUIManage.GetInstance().f_SendMsgV3("ui_gameset.bundle", "UI_Cheerleading_MR", UIMessageDef.UI_OPEN);
        }
    }

}
