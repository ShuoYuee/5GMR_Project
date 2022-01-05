using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Epibyte.ConceptVR;

public class GameMainTriggerCtrl : MonoBehaviour
{
    public float _fLookTimeLimt = 2f;

    private EditObjControll _EditObjControll = null;
    private Interactable _Interactable = null;

    private GameObject oCurObj = null;
    private EM_TriggerObj _ObjEm = EM_TriggerObj.None;
    private float _fLookTime = 0f;
    private bool _bLookTime = false;

    public enum EM_TriggerObj
    {
        None = 0,
        EditObj = 1,
        Button = 2,
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        f_RayTrigger();
        f_InputKey();
        f_EditInput();
    }

    /// <summary>射線碰撞偵測</summary>
    private void f_RayTrigger()
    {
        Ray tRay = new Ray(transform.position, transform.forward);
        RaycastHit hit;
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

            if (GameMain.GetInstance()._bEdit && hit.collider.GetComponent<EditObjControll>() != null)//編輯物件
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
        }
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
    }

    /// <summary>一般輸入</summary>
    private void f_InputKey()
    {
        if (oCurObj != null && Input.GetKeyUp(KeyCode.Space))//選取物件用
        {
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
                    if (!GameMain.GetInstance()._bEdit) { return; }
                    if (_EditObjControll == null)
                    {
                        _ObjEm = EM_TriggerObj.None;
                        return;
                    }

                    GameMain.GetInstance()._bSelectEdit = true;
                    _EditObjControll.f_SetEditState(true);
                    _EditObjControll.OnClicked();
                    break;
            }
        }
    }

    float _fBtnTime = 0f;
    /// <summary>編輯模式下輸入</summary>
    private void f_EditInput()
    {
        if (!GameMain.GetInstance()._bEdit) { return; }
        if (!GameMain.GetInstance()._bSelectEdit) { return; }
        _EditObjControll = GameMain.GetInstance().f_GetCurEditObj();
        if (_EditObjControll == null) { return; }

        if (Input.GetKeyDown(KeyCode.RightArrow))//旋轉、放大用
        {
            _EditObjControll.f_SetInput(1);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))//旋轉、縮小用
        {
            _EditObjControll.f_SetInput(-1);
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            _EditObjControll.f_SetInput(0);
        }

        _fBtnTime += Time.deltaTime;//按鈕間隔時間
        if (_fBtnTime < 0.1f) { return; }
        if (Input.GetKeyUp(KeyCode.Space))//移動座標用
        {
            _EditObjControll.OnClicked();
        }

        _fBtnTime = 0;
    }
}
