using UnityEngine;

namespace Epibyte.ConceptVR
{
    [RequireComponent(typeof(Interactable))]
    public class MenuObject : MonoBehaviour
    {
        public GameObject relatedObject;

        /// <summary>物件對應的角色資料</summary>
        public NBaseSCDT m_SCData;

        void Start()
        {
            if (null != GetComponent<Interactable>())
            {
                f_CreateEvent(GetComponent<Interactable>());
                GetComponent<Interactable>().onClickedEvent.AddListener(f_CreateObj);
            }
        }

        void OnDestroy()
        {
            if (null != GetComponent<Interactable>())
            {
                GetComponent<Interactable>().onClickedEvent.RemoveListener(f_CreateObj);
            }
        }

        public void GenerateGO()
        {
            GameObject go = Instantiate(relatedObject, LaserPointer.instance.pointer.position, Quaternion.identity);
            if (null != go.GetComponent<IInteractable>())
            {
                LaserPointer.instance.Target = go.GetComponent<IInteractable>();
                go.GetComponent<IInteractable>().OnClicked();
            }
        }

        /// <summary>創建物件</summary>
        public void f_CreateObj()
        {
            //relatedObject = GameMain.GetInstance().f_AddObj((CharacterDT)m_SCData).gameObject;
            GameMain.GetInstance().f_AddObj((CharacterDT)m_SCData);
        }

        /// <summary>初始化物件資料</summary>
        public void f_InitMenuObj(NBaseSCDT data)
        {
            m_SCData = data;
            
            CharacterDT tData = (CharacterDT)m_SCData;
            transform.localPosition = Vector3.zero;
            transform.localScale = new Vector3(tData.fDisplayScale, tData.fDisplayScale, tData.fDisplayScale);
        }

        /// <summary>初始化互動物件</summary>
        public void f_CreateEvent(Interactable Obj)
        {
            if (Obj.onClickedEvent == null) { Obj.onClickedEvent = new UnityEngine.Events.UnityEvent(); }
            if (Obj.onHoveredEvent == null) { Obj.onHoveredEvent = new UnityEngine.Events.UnityEvent(); }
            if (Obj.onReleasedEvent == null) { Obj.onReleasedEvent = new UnityEngine.Events.UnityEvent(); }
            if (Obj.onLeaveEvent == null) { Obj.onLeaveEvent = new UnityEngine.Events.UnityEvent(); }
        }
    }
}