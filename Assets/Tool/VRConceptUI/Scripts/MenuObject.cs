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

        public void f_CreateObj()
        {
            relatedObject = GameMain.GetInstance().f_AddObj((CharacterDT)m_SCData).gameObject;
        }

        public void f_InitMenuObj(NBaseSCDT data)
        {
            m_SCData = data;
        }
    }
}