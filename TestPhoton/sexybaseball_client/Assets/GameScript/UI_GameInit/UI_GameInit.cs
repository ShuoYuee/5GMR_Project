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

      
        private void Start()
        {
            MessageBox.DEBUG("启用游戏包中的UI_GameInit脚本");

            m_Progress.value = 0;
            glo_Main.GetInstance().m_UIMessagePool.f_AddListener(MessageDef.UI_UpdateInitProgress, On_UI_UpdateInitProgress);
            glo_Main.GetInstance().m_UIMessagePool.f_AddListener(MessageDef.UI_UpdateInitSuccess, On_UI_UpdateInitSuccess);
        }

        private void On_UI_UpdateInitProgress(object Obj)
        {
            m_Progress.value = (float)Obj;
        }

        private void On_UI_UpdateInitSuccess(object data)
        {
            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            glo_Main.GetInstance().m_UIMessagePool.f_RemoveListener(MessageDef.UI_UpdateInitProgress, On_UI_UpdateInitProgress);
            glo_Main.GetInstance().m_UIMessagePool.f_RemoveListener(MessageDef.UI_UpdateInitSuccess, On_UI_UpdateInitSuccess);
        }
    }

}