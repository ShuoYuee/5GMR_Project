using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Epibyte.ConceptVR;

public class GameMainTriggerCtrl : MonoBehaviour
{
    Transform Player;
    //public float _fLookTimeLimt = 2f;

    private EditObjControll _EditObjControll = null;
    private Interactable _Interactable = null;

    private GameObject oCurObj = null;
    private EM_TriggerObj _ObjEm = EM_TriggerObj.None;
    //private float _fLookTime = 0f;
    private bool _bLookTime = false;

    public static ControlState State = ControlState.VR;
    private InputDevice[] _Device = new InputDevice[2];

    public enum EM_TriggerObj
    {
        None = 0,
        EditObj = 1,    //編輯物
        Button = 2,     //按鈕
    }

    // Start is called before the first frame update
    void Start()
    {
        Player = transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        switch (State)
        {
            case ControlState.VR:
                break;

            case ControlState.PC:
                f_MouseMoveInput();
                break;
        }
        
        f_RayTrigger();
        f_InputKey();
        f_EditInput();
    }

    /// <summary>PC移動輸入</summary>
    private void f_MouseMoveInput()
    {
        if (Input.GetKey(KeyCode.W))
        {
            Player.localPosition += transform.forward * 3f * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            Player.localPosition += -transform.forward * 3f * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            Player.localPosition += -transform.right * 3f * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            Player.localPosition += transform.right * 3f * Time.deltaTime;
        }
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

    #region 一般輸入
    /// <summary>一般輸入</summary>
    private void f_InputKey()
    {
        switch (State)
        {
            case ControlState.VR:
                f_VRInputKey();
                break;

            case ControlState.PC:
                f_PCInputKey();
                break;
        }
    }

    /// <summary>VR輸入</summary>
    private void f_VRInputKey()
    {
        if (!_Device[0].isValid || !_Device[1].isValid)
        {
            _Device[0] = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
            _Device[1] = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
        }

        bool triggerBtnAction = false;
        _fBtnTime += Time.deltaTime;//按鈕間隔時間
        if (_fBtnTime < 0.15f) { return; }
        if (_Device[0].TryGetFeatureValue(CommonUsages.triggerButton, out triggerBtnAction) && triggerBtnAction)
        {
            if (oCurObj == null) { return; }
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

        _fBtnTime = 0;
    }

    /// <summary>PC輸入</summary>
    private void f_PCInputKey()
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
    #endregion

    #region 編輯模式輸入
    float _fBtnTime = 0f;
    /// <summary>編輯模式下輸入</summary>
    private void f_EditInput()
    {
        if (!GameMain.GetInstance()._bEdit) { return; }
        if (!GameMain.GetInstance()._bSelectEdit) { return; }
        _EditObjControll = GameMain.GetInstance().f_GetCurEditObj();
        if (_EditObjControll == null) { return; }

        switch (State)
        {
            case ControlState.VR:
                f_VREditInput();
                break;

            case ControlState.PC:
                f_PCEditInput();
                break;
        }
    }

    /// <summary>VR輸入</summary>
    private void f_VREditInput()
    {
        if (!_Device[0].isValid || !_Device[1].isValid)
        {
            _Device[0] = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
            _Device[1] = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
        }
        
        bool triggerBtnAction = false;
        if (_Device[1].TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 vPos))
        {
            if (vPos.x > 0.5f)
            {
                _EditObjControll.f_SetInput(1);
            }
            else if(vPos.x < -0.5f)
            {
                _EditObjControll.f_SetInput(-1);
            }
            else
            {
                _EditObjControll.f_SetInput(0);
            }
        }

        _fBtnTime += Time.deltaTime;//按鈕間隔時間
        if (_fBtnTime < 0.15f) { return; }
        if (_Device[0].TryGetFeatureValue(CommonUsages.triggerButton, out triggerBtnAction) && triggerBtnAction)//移動座標用
        {
            _EditObjControll.OnClicked();
            _fBtnTime = 0;
        }
    }

    /// <summary>PC輸入</summary>
    private void f_PCEditInput()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))//拉前、旋轉、放大用
        {
            _EditObjControll.f_SetInput(1);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))//退後、旋轉、縮小用
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
    #endregion
}
