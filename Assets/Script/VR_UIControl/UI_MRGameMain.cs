using System;
using System.Collections.Generic;
using UnityEngine;
using ccU3DEngine;
using Epibyte.ConceptVR;

namespace ccUI_U3DSpace
{
    public class UI_MRGameMain : MRUI_LogicBase
    {
        private List<GameObject> _PanelList = new List<GameObject>();

        protected override void On_Init()
        {
            base.On_Init();
            _PanelList.Add(f_GetObject("Panel1"));
            _PanelList.Add(f_GetObject("Panel2"));
            _PanelList.Add(f_GetObject("Panel3"));
            _PanelList.Add(f_GetObject("Panel3_1"));
            _PanelList.Add(f_GetObject("Panel3_2"));
            _PanelList.Add(f_GetObject("Panel3_3"));
            _PanelList.Add(f_GetObject("Panel3_4"));
            _PanelList.Add(f_GetObject("Panel4"));

            GameObject oLoadMapBtn = f_GetObject("LoadMapBtn");
            GameObject oSaveMapBtn = f_GetObject("SaveMapBtn");
            GameObject oResetMapBtn = f_GetObject("ResetMapBtn");
            GameObject oExitGameBtn = f_GetObject("BtnExitGame");

            f_RegOnClickEvent(oLoadMapBtn, OnClickBtn_LoadMapBtn);
            f_RegOnClickEvent(oSaveMapBtn, OnClickBtn_SaveBtn);
            f_RegOnClickEvent(oResetMapBtn, OnClickBtn_ResetMapBtn);
            f_RegOnClickEvent(oExitGameBtn, OnClickBtn_ExitGameBtn);
            f_RegOnHoverEvent(oLoadMapBtn, OnClickBtn_LoadMapBtn);
            f_RegOnHoverEvent(oSaveMapBtn, OnClickBtn_SaveBtn);
            f_RegOnHoverEvent(oResetMapBtn, OnClickBtn_ResetMapBtn);
            f_RegOnHoverEvent(oExitGameBtn, OnClickBtn_ExitGameBtn);
            f_RegUnHoverEvent(oLoadMapBtn, OnClickBtn_LoadMapBtn);
            f_RegUnHoverEvent(oSaveMapBtn, OnClickBtn_SaveBtn);
            f_RegUnHoverEvent(oResetMapBtn, OnClickBtn_ResetMapBtn);
            f_RegUnHoverEvent(oExitGameBtn, OnClickBtn_ExitGameBtn);
            f_AddBtnEffect(oLoadMapBtn);
            f_AddBtnEffect(oSaveMapBtn);
            f_AddBtnEffect(oResetMapBtn);
            f_AddBtnEffect(oExitGameBtn);
        }

        private void f_AddBtnEffect(GameObject oBtn)
        {
            TransitionEffect tEffect = oBtn.GetComponent<TransitionEffect>();
            if (tEffect == null)
            {
                tEffect = oBtn.AddComponent<TransitionEffect>();
            }
            tEffect.activatedScaleFactor = 2.5f;
            tEffect.scaleAxis.scaleX = true;
            tEffect.scaleAxis.scaleY = true;
            tEffect.scaleAxis.scaleZ = true;
            tEffect.activatedMaterial = glo_Main.GetInstance().m_ResourceManager.f_LoadMaterial("Blue");
        }

        #region LoadMapBtn
        private void OnClickBtn_LoadMapBtn(GameObject go, object obj1, object obj2)
        {
            
        }

        private void OnHoverBtn_LoadMapBtn(GameObject go, object obj1, object obj2)
        {
            if (go.TryGetComponent(out TransitionEffect tEffect))
            {
                tEffect.ActivateMaterial();
            }
        }

        private void UnClickBtn_LoadMapBtn(GameObject go, object obj1, object obj2)
        {

        }

        private void UnHoverBtn_LoadMapBtn(GameObject go, object obj1, object obj2)
        {
            if (go.TryGetComponent(out TransitionEffect tEffect))
            {
                tEffect.DeactivateAllEffects();
            }
        }
        #endregion

        #region SaveBtn
        private void OnClickBtn_SaveBtn(GameObject go, object obj1, object obj2)
        {

        }

        private void OnHoverBtn_SaveBtn(GameObject go, object obj1, object obj2)
        {
            if (go.TryGetComponent(out TransitionEffect tEffect))
            {
                tEffect.ActivateMaterial();
            }
        }

        private void UnClickBtn_SaveBtn(GameObject go, object obj1, object obj2)
        {

        }

        private void UnHoverBtn_SaveBtn(GameObject go, object obj1, object obj2)
        {
            if (go.TryGetComponent(out TransitionEffect tEffect))
            {
                tEffect.DeactivateAllEffects();
            }
        }
        #endregion

        #region ResetMapBtn
        private void OnClickBtn_ResetMapBtn(GameObject go, object obj1, object obj2)
        {

        }

        private void OnHoverBtn_ResetMapBtn(GameObject go, object obj1, object obj2)
        {
            if (go.TryGetComponent(out TransitionEffect tEffect))
            {
                tEffect.ActivateMaterial();
            }
        }

        private void UnClickBtn_ResetMapBtn(GameObject go, object obj1, object obj2)
        {

        }

        private void UnHoverBtn_ResetMapBtn(GameObject go, object obj1, object obj2)
        {
            if (go.TryGetComponent(out TransitionEffect tEffect))
            {
                tEffect.DeactivateAllEffects();
            }
        }
        #endregion

        #region ExitGameBtn
        private void OnClickBtn_ExitGameBtn(GameObject go, object obj1, object obj2)
        {

        }

        private void OnHoverBtn_ExitGameBtn(GameObject go, object obj1, object obj2)
        {
            if (go.TryGetComponent(out TransitionEffect tEffect))
            {
                tEffect.ActivateMaterial();
            }
        }

        private void UnClickBtn_ExitGameBtn(GameObject go, object obj1, object obj2)
        {

        }

        private void UnHoverBtn_ExitGameBtn(GameObject go, object obj1, object obj2)
        {
            if (go.TryGetComponent(out TransitionEffect tEffect))
            {
                tEffect.DeactivateAllEffects();
            }
        }
        #endregion
    }
}
