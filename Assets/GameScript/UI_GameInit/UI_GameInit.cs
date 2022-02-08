﻿using ccU3DEngine;
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
            glo_Main.GetInstance().m_UIMessagePool.f_AddListener(UIMessageDef.UI_UpdateInitProgress, On_UI_UpdateInitProgress);
        }


        private void On_UI_UpdateInitProgress(object Obj)
        {
            m_Progress.value = (float)Obj;
        }

        private void OnDestroy()
        {
            glo_Main.GetInstance().m_UIMessagePool.f_RemoveListener(UIMessageDef.UI_UpdateInitProgress, On_UI_UpdateInitProgress);
        }

    }

}