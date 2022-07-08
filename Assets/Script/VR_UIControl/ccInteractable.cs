using UnityEngine;
using ccU3DEngine;

namespace ccUI_U3DSpace
{
    public class ccInteractable : MonoBehaviour
    {
        public event ccUIEventListener.VoidDelegateV2 OnHoveredEvent;
        public event ccUIEventListener.VoidDelegateV2 OnClickedEvent;
        public event ccUIEventListener.VoidDelegateV2 UnClickedEvent;
        public event ccUIEventListener.VoidDelegateV2 UnHoveredEvent;
        public string[] _AudioArray = { "", "", "", "" };

        public void OnHovered(object obj1 = null, object obj2 = null)
        {
            if (OnHoveredEvent == null) { return; }
            OnHoveredEvent.Invoke(gameObject, obj1, obj2);
            f_SetAudio(0);
        }

        public void OnClicked(object obj1 = null, object obj2 = null)
        {
            if (OnClickedEvent == null) { return; }
            OnClickedEvent.Invoke(gameObject, obj1, obj2);
            f_SetAudio(1);
        }

        public void UnClicked(object obj1 = null, object obj2 = null)
        {
            if (UnClickedEvent == null) { return; }
            UnClickedEvent.Invoke(gameObject, obj1, obj2);
            f_SetAudio(2);
        }

        public void UnHovered(object obj1 = null, object obj2 = null)
        {
            if (UnHoveredEvent == null) { return; }
            UnHoveredEvent.Invoke(gameObject, obj1, obj2);
            f_SetAudio(3);
        }

        public void f_SetAudio(string strAudio, int iIndex)
        {
            try
            {
                _AudioArray[iIndex] = strAudio;
            }
            catch
            {
                MessageBox.ASSERT("音效設置錯誤！Index超出範圍");
            }
        }

        public void f_SetAudio(int iIndex)
        {
            string strAudio = _AudioArray[iIndex];
            if (strAudio != "")
            {
                glo_Main.GetInstance().m_AudioManager._EffectAudio.clip = glo_Main.GetInstance().m_ResourceManager.f_CreateAudio(_AudioArray[iIndex]);
                glo_Main.GetInstance().m_AudioManager.f_PlayAudioEffect();
            }
        }
    }
}
