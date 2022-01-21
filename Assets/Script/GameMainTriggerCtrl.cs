using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Epibyte.ConceptVR;

public class GameMainTriggerCtrl : MonoBehaviour
{
    //public float _fLookTimeLimt = 2f;
    private EditObjControll _EditObjControll = null;
    private Interactable _Interactable = null;
    private InputField _InputField = null;
    private Button _Button = null;

    private GameObject oCurObj = null;
    private EM_TriggerObj _ObjEm = EM_TriggerObj.None;
    //private float _fLookTime = 0f;
    private bool _bLookTime = false;

    public enum EM_TriggerObj
    {
        None = 0,
        EditObj = 1,    //編輯物
        Button = 2,     //按鈕
        InpuUI = 3,     //輸入文字UI
        ButtonUI = 4,   //按鈕UI
    }

    private void Start()
    {
        //暫預定四個輸入事件
        GameInputCtrl.OnClickCtrlEvent += f_OnClick;
        GameInputCtrl.OnClickBtnOne += f_EditCtrl;
        GameInputCtrl.OnClickBtnTwo += f_EditCtrl;
        GameInputCtrl.OnClickBtnThree += f_EditCtrl;
    }

    // Update is called once per frame
    void Update()
    {
        f_RayTrigger();
    }

    /// <summary>射線碰撞偵測</summary>
    private void f_RayTrigger()
    {
        Ray tRay = new Ray(transform.position, transform.forward * 20);
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.forward * 20, Color.yellow);
        if(Physics.Raycast(tRay, out hit, Mathf.Infinity))
        {
            Debug.DrawLine(tRay.origin, hit.point, Color.red);

            if (hit.collider.gameObject != oCurObj && oCurObj != null)
            {
                if (_Interactable != null)
                {
                    _Interactable.OnLeave();
                    _Interactable = null;
                }
            }

            //將要偵測的元件一一填入
            if (GameMain.GetInstance().m_EditManager._bEdit && hit.collider.GetComponent<EditObjControll>() != null)//編輯物件
            {
                _ObjEm = EM_TriggerObj.EditObj;
                oCurObj = hit.collider.gameObject;
                _EditObjControll = oCurObj.GetComponent<EditObjControll>();
            }

            else if (!_bLookTime && hit.collider.GetComponent<Interactable>() != null)//按鈕物件
            {
                _ObjEm = EM_TriggerObj.Button;
                oCurObj = hit.collider.gameObject;
                _Interactable = oCurObj.GetComponent<Interactable>();
                _Interactable.OnHovered();
            }
            else if(hit.collider.GetComponent<InputField>() != null)//輸入文字框物件
            {
                _ObjEm = EM_TriggerObj.InpuUI;
                oCurObj = hit.collider.gameObject;
                _InputField = hit.collider.GetComponent<InputField>();//按鈕UI物件
            }
            else if(hit.collider.GetComponent<Button>() != null)
            {
                _ObjEm = EM_TriggerObj.ButtonUI;
                oCurObj = hit.collider.gameObject;
                _Button = hit.collider.GetComponent<Button>();
            }

        }
        #region
        /*else
        {
            _ObjEm = EM_TriggerObj.None;
            oCurObj = null;

            if (_Interactable != null)
            {
                _Interactable.OnLeave();
                _Interactable = null;
            }

            _fLookTime = 0;
            _bLookTime = false;
            glo_Main.GetInstance().m_UIMessagePool.f_Broadcast(MessageDef.UI_GameAnchorEnd);
            return;
        }

        if (_bLookTime)
        {
                _fLookTime += Time.deltaTime;
            if (_fLookTime >= _fLookTimeLimt)
            {
                _fLookTime = 0;
                _bLookTime = false;

                switch (_ObjEm)
                {
                    case EM_TriggerObj.EditObj:

                        break;
                    case EM_TriggerObj.Button:
                        _Interactable.OnClicked();
                        break;
                }
                glo_Main.GetInstance().m_UIMessagePool.f_Broadcast(MessageDef.UI_GameAnchorEnd);
            }
        }*/
        #endregion
    }

    /// <summary>一般輸入事件</summary>
    private void f_OnClick(int e)
    {
        if (oCurObj == null) { return; }

        //將各對象的執行動作一一填入
        switch (_ObjEm)
        {
            case EM_TriggerObj.Button:
                if (_Interactable == null)
                {
                    _ObjEm = EM_TriggerObj.None;
                    return;
                }
                _Interactable.OnClicked();
                break;

            case EM_TriggerObj.EditObj:
                if (!GameMain.GetInstance().m_EditManager._bEdit) { return; }
                if (_EditObjControll == null)
                {
                    _ObjEm = EM_TriggerObj.None;
                    return;
                }

                GameMain.GetInstance().m_EditManager._bSelectEdit = true;
                _EditObjControll.f_SetEditState(true);
                _EditObjControll.OnClicked();
                break;

            case EM_TriggerObj.InpuUI:
                if (_InputField == null)
                {
                    _ObjEm = EM_TriggerObj.None;
                    return;
                }
                _InputField.Select();
                break;

            case EM_TriggerObj.ButtonUI:
                if (_Button == null)
                {
                    _ObjEm = EM_TriggerObj.None;
                    return;
                }
                _Button.onClick.Invoke();
                break;
        }
    }

    /// <summary>
    /// 編輯模式輸入事件
    /// </summary>
    /// <param name="iInput">判別值(左為-1  右為1)</param>
    private void f_EditCtrl(int iInput)
    {
        if (!GameMain.GetInstance().m_EditManager._bEdit) { return; }
        if (!GameMain.GetInstance().m_EditManager._bSelectEdit) { return; }
        _EditObjControll = GameMain.GetInstance().f_GetCurEditObj();
        if (_EditObjControll == null) { return; }

        _EditObjControll.f_SetInput(iInput);
    }
}
