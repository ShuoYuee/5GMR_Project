using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using ccU3DEngine;

namespace ccUI_U3DSpace
{
    public class MRUI_LogicBase : MonoBehaviour
    {
        [SerializeField]
        [Header("UI對象清單")]
        protected List<GameObject> m_aObjList;
        protected GameObject _Panel;

        private bool _bOpen = false;

        private void Start()
        {
            _Panel = GameTools.f_GetGameObject(transform, "Panel");
            On_Init();
        }

        protected virtual void On_Init()
        {
            ccUImrManager.GetInstance().f_AddBase(this);
            GameTools.f_SetGameObject(_Panel, false);
        }

        protected virtual void On_Open(object e)
        {

        }

        protected virtual void On_Close(object e)
        {

        }

        public void f_Open(object e)
        {
            GameTools.f_SetGameObject(_Panel, true);
            On_Open(e);
        }

        public void f_Close(object e)
        {
            GameTools.f_SetGameObject(_Panel, false);
            On_Close(e);
        }

        protected void f_RegOnClickEvent(GameObject go, ccUIEventListener.VoidDelegateV2 ccCallback, string strAudioName = "")
        {
            ccInteractable _interactable = go.GetComponent<ccInteractable>();
            if (!_interactable) { _interactable = go.AddComponent<ccInteractable>(); }
            _interactable.OnClickedEvent += ccCallback;
            _interactable.f_SetAudio(strAudioName, 1);
        }

        protected void f_RegOnHoverEvent(GameObject go, ccUIEventListener.VoidDelegateV2 ccCallback, string strAudioName = "")
        {
            ccInteractable _interactable = go.GetComponent<ccInteractable>();
            if (!_interactable) { _interactable = go.AddComponent<ccInteractable>(); }
            _interactable.OnHoveredEvent += ccCallback;
            _interactable.f_SetAudio(strAudioName, 0);
        }

        protected void f_RegUnClickEvent(GameObject go, ccUIEventListener.VoidDelegateV2 ccCallback, string strAudioName = "")
        {
            ccInteractable _interactable = go.GetComponent<ccInteractable>();
            if (!_interactable) { _interactable = go.AddComponent<ccInteractable>(); }
            _interactable.UnClickedEvent += ccCallback;
            _interactable.f_SetAudio(strAudioName, 2);
        }

        protected void f_RegUnHoverEvent(GameObject go, ccUIEventListener.VoidDelegateV2 ccCallback, string strAudioName = "")
        {
            ccInteractable _interactable = go.GetComponent<ccInteractable>();
            if (!_interactable) { _interactable = go.AddComponent<ccInteractable>(); }
            _interactable.UnHoveredEvent += ccCallback;
            _interactable.f_SetAudio(strAudioName, 3);
        }

        protected GameObject f_GetObject(string strName)
        {
            return GameTools.f_GetGameObject(m_aObjList, strName);
        }
    }
}